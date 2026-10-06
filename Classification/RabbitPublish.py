import pika, os
import GeoClasification
import json

def publish_by_region(alerts_list, q_name):
    print(q_name)
    params = pika.URLParameters("amqp://localhost")
    connection = pika.BlockingConnection(params)
    # connection_n = pika.BlockingConnection(params)
    # connection_s = pika.BlockingConnection(params)
    # connection_o = pika.BlockingConnection(params)
    
    channel = connection.channel() # start a channel
    # channel_n = connection_n.channel() # start a channel
    # channel_s = connection_s.channel() # start a channel
    # channel_o = connection_o.channel() # start a channel


    channel.exchange_declare(exchange='logs', exchange_type='fanout')
    # channel_n.exchange_declare(exchange='logs', exchange_type='fanout')
    # channel_s.exchange_declare(exchange='logs', exchange_type='fanout')
    # channel_o.exchange_declare(exchange='logs', exchange_type='fanout')

    channel.queue_declare(queue=q_name) # Declare a queue
    # channel_n.queue_declare(queue='NORTH2') # Declare a queue
    # channel_s.queue_declare(queue='SOUTH2') # Declare a queue
    # channel_o.queue_declare(queue='OVERSEAS2') # Declare a queue

    # center = 0
    # north = 0
    # s = 0
    # o = 0

    for alert in alerts_list:
        channel.basic_publish(exchange='logs',
                routing_key='',
                body=json.dumps(alert))
        # try:
        #     name = GeoClasification.get_region_with_geopandas("regions.geojson", alert["lon"], alert["lat"])
        #     if (name == 'CENTER'):
        #         center += 1
        #     if (name == 'NORTH'):
        #         north += 1
        #     if (name == 'SOUTH'):
        #         s += 1
        #     if (name == 'OVERSEAS'):
        #         o += 1

        #     if name == 'CENTER':
        #         channel_c.basic_publish(exchange='logs',
        #                                 routing_key='',
        #                                 body=json.dumps(alert))
        #     if name == 'NORTH':
        #         channel_n.basic_publish(exchange='logs',
        #                                 routing_key='',
        #                                 body=json.dumps(alert))
        #     if name == 'SOUTH':
        #         channel_s.basic_publish(exchange='logs',
        #                                 routing_key='',
        #                                 body=json.dumps(alert))            
        #     if name == 'OVERSEAS':
        #         channel_n.basic_publish(exchange='logs',
        #                                 routing_key='',
        #                                 body=json.dumps(alert))
        # except:
        #     continue

    # print(center)
    # print(north)
    # print(s)
    # print(o)
    # connection_c.close()    
    # connection_s.close()   
    # connection_n.close()
    # connection_o.close() 
    connection.close()             
                    