# -*- coding:utf8 -*-
# python3.*

import pymysql
import codecs
from urllib import request
from typing import List, Dict
import json
from tqdm import tqdm


def get_url_response_geocode(city_name: str) -> Dict:
    pattern_url = "http:////restapi.amap.com/v3/geocode/geo?key=fab711f9cdbe5ee716ac10ecd241e80a" + \
                  "&address=市人民政府&city={city_name}"
    request_url = pattern_url.format(city_name=city_name)
    response = request.urlopen(request_url)
    return json.loads(response.read().decode('utf-8'))


def get_city_location() -> Dict:
    db_conn = pymysql.connect(host='222.29.117.240', port=2048, user='root', passwd='19950310', charset='utf8')
    db_cursor = db_conn.cursor(cursor=pymysql.cursors.DictCursor)
    db_cursor.execute("SELECT NAME, ST_X(the_Centroid) as lng, ST_Y(the_Centroid) as lat FROM suzhou.cityshp;")
    city_location = {}
    for record in db_cursor.fetchall():
        city_name = record['NAME']
        response = get_url_response(city_name)
        if "info" in response and response["info"] == "OK" and len(response["geocodes"]):
            lat, lng = response["geocodes"][0]["location"].split(',')
            city_location[city_name] = [float(lat), float(lng)]
    db_cursor.close()
    return city_location
