from confluent_kafka import Consumer
import json
import GeoClasification
import RabbitPublish

conf = {'bootstrap.servers': 'localhost',
        'group.id': 'bbnnnnn',
        'auto.offset.reset': 'earliest'}
consumer = Consumer(conf)
consumer.subscribe(["b"])

consume_list = []
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

RabbitPublish.publish_by_region(consume_list)