def alert_validation(consume_list):
    # consume_list2 = consume_list
    for message in consume_list:
        try:
            if (isinstance(message["lon"], int | float) == False) or (isinstance(message["lon"], int | float) == False):
                consume_list.remove(message)
                continue
            if (message["lon"] < -180 or message["lon"] > 180) or (message["lat"] < -90 or message["lat"] > 90):
                consume_list.remove(message)
                continue
            if str(message["priority"]).upper() not in ['CRITICAL', 'HIGH', 'MEDIUM', 'LOW']:
                consume_list.remove(message)
                continue
            if str(message["classification"]).upper() not in ['UNCLASSIFIED', 'RESTRICTED', 'SECRET', 'TOP_SECRET']:
                consume_list.remove(message)
                continue
            if str(message["status"]).upper() != 'WAITING':
                consume_list.remove(message)
            message["alert_id"]
            message["source"]
            message["title"]
            message["content"]
            message["timestamp"]
        except:
            consume_list.remove(message)
    return consume_list