from confluent_kafka import Consumer
import json
import GeoClasification
import RabbitPublish
from Validation import alert_validation

def my_consume():
    conf = {'bootstrap.servers': 'localhost',
            'group.id': 'vAx,sscsaa',
            'auto.offset.reset': 'earliest'}
    consumer = Consumer(conf)
    consumer.subscribe(["ALERTS"])

    consume_list = []

    center_list = []
    south_list = []
    north_list = []
    overseas_list = []

    running = True

    while running:
        msg = consumer.poll(timeout=10)
        if msg == None:
            break
        # msg = json.dumps(msg.value().decode("utf-8"))
        try:
            msg = json.loads(msg.value())
        except:
            continue
        # print(msg)
        consume_list.append(msg)
    print(len(consume_list))
    # print(type(consume_list[5]["lon"]))

    consume_list = alert_validation(consume_list)
    print(len(consume_list))

    for alert in consume_list:
        name = GeoClasification.get_region_with_geopandas("regions.geojson", alert["lon"], alert["lat"])
        if name == 'CENTER':
            center_list.append(alert)
        if name == 'NORTH':
                north_list.append(alert)
        if name == 'SOUTH':
                south_list.append(alert)
        if name == 'OVERSEAS':
                overseas_list.append(alert)
                        

    RabbitPublish.publish_by_region(center_list, 'CENTER2')
    RabbitPublish.publish_by_region(north_list, 'NORTH2')
    RabbitPublish.publish_by_region(south_list, 'SOUTH2')
    RabbitPublish.publish_by_region(overseas_list, 'OVERSEAS2')