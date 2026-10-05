from confluent_kafka import Consumer
from confluent_kafka import KafkaException
import json

conf = {'bootstrap.servers': 'localhost',
        'group.id': 'v',
        'auto.offset.reset': 'earliest'}

consumer = Consumer(conf)
consumer.subscribe(["b"])

consume_list = []
running = True

while running:
    msg = consumer.poll(timeout=10)
    if msg == None:
        break

    consume_list.append(msg)
print(len(consume_list))
# print(json.dumps(msg.value()))

# running = True
# consume_list = []

# def basic_consume_loop(consumer, topics):
#     try:
#         consumer.subscribe("b")

#         while running:
#             msg = consumer.poll(timeout=10)
#             print(msg)
#             if msg is None: continue

#             if msg.error():
#                 raise KafkaException(msg.error())
#             else:
#                 msg_process(msg)
#     finally:
#         # Close down consumer to commit final offsets.
#         consumer.close()

# def shutdown():
#     global running
#     running = False

# def msg_process(msg):
#     consume_list.append(msg)
#     # print(msg)
# print(len(consume_list))