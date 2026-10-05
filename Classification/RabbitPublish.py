import pika, os
import GeoClasification
import json

def publish_by_region(alerts_list):
    params = pika.URLParameters("amqp://localhost")
    connection = pika.BlockingConnection(params)
    channel = connection.channel() # start a channel
    channel.exchange_declare(exchange='logs', exchange_type='fanout')
    channel.queue_declare(queue='alerts2') # Declare a queue

    for alert in alerts_list:
        try:
            name = GeoClasification.get_region_with_geopandas("regions.geojson", alert["lon"], alert["lat"])
            channel.basic_publish(exchange='logs',
                                        routing_key=name,
                                        body=json.dumps(alert)) 
        except:
            print("------------")
            continue

    connection.close()                    
                    
        # try:
        #     routing_k = GeoClasification.get_region_with_geopandas("regions.geojson", alert["lon"], alert["lat"])
             
        #     channel.basic_publish(exchange='',
        #                         routing_key=routing_k,
        #                         body=json.dumps(alert))
        # except:
        #     print("------------")
        #     continue