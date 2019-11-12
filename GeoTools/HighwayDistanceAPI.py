# -*- coding:utf8 -*-
# python3.*

import pymysql
import codecs
from urllib import request
from typing import List, Dict
import json
from tqdm import tqdm


def get_url_response(origin: List[float], destination: List[float], strategy: int = 0) -> Dict:
    pattern_url = "http://restapi.amap.com/v3/direction/driving?key=fab711f9cdbe5ee716ac10ecd241e80a" + \
                  "&origin={origin_lng},{origin_lat}&destination={des_lng},{des_lat}&extensions=all&strategy={strategy}"
    request_url = pattern_url.format(origin_lng=origin[0], origin_lat=origin[1], des_lng=destination[0],
                                     des_lat=destination[1], strategy=strategy)
    response = request.urlopen(request_url)
    return json.loads(response.read().decode('utf-8'))


def get_city_location() -> Dict:
    db_conn = pymysql.connect(host='222.29.117.240', port=2048, user='root', passwd='19950310', charset='utf8')
    db_cursor = db_conn.cursor(cursor=pymysql.cursors.DictCursor)
    db_cursor.execute("SELECT NAME, ST_X(the_Centroid) as lng, ST_Y(the_Centroid) as lat FROM suzhou.cityshp;")
    city_location = {}
    for record in db_cursor.fetchall():
        city_location[record['NAME']] = [record['lng'], record['lat']]
    db_cursor.close()
    return city_location


def get_highway_distance_duration(city1_location: List[float], city2_location: List[float], strategy: int = 0) -> Dict:
    response = get_url_response(city1_location, city2_location, strategy)
    # print(response)
    res = {'distance': None, 'duration': None}
    if 'info' not in response or response['info'] != 'OK':
        return res
    try:
        paths = response['route']['paths']
        path = paths[0]
        res['distance'] = path['distance']
        res['duration'] = path['duration']
        res['strategy'] = strategy
    finally:
        return res


def update_database_data(city1_name: str, city2_name: str, data: Dict):
    db_conn = pymysql.connect(host='222.29.117.240', port=2048, user='root', passwd='19950310', charset='utf8')
    db_cursor = db_conn.cursor(cursor=pymysql.cursors.DictCursor)
    db_cursor.execute(
        "UPDATE suzhou.citynet SET driving_distance_%d = %s , driving_duration_%d = %s WHERE cityA = '%s' and cityB = '%s';" %
        (data['strategy'], data['distance'], data['strategy'], data['duration'], city1_name, city2_name))
    db_cursor.close()


if __name__ == '__main__':
    city_locations = get_city_location()
    target_city = '苏州市'
    other_cities = set(city_locations.keys()) - set([target_city])
    res_info = []
    for other_city in tqdm(other_cities):
        cur_city_info = []
        cur_city_info.append(other_city)
        for strategy in [0, 2]:
            distance_duration = get_highway_distance_duration(city_locations[target_city], city_locations[other_city],
                                                              strategy)
            cur_city_info.append(distance_duration['distance'])
            cur_city_info.append(distance_duration['duration'])
            # update_database_data(target_city, other_city, distance_duration)
        res_info.append(cur_city_info)
    with codecs.open('./amap_driving_distance_duration.csv', 'w', encoding='utf8') as wf:
        wf.write('cityname,distance_0,duration_0,distance_2,duration_2\n')
        for line in res_info:
            wf.write(','.join(line)+'\n')
    print('Done')
