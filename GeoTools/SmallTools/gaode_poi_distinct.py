# -*- coding::utf8 -*-

import os
import pymysql
import math
from tqdm import tqdm



def get_distinct_name_location_type():
    select_sql = "SELECT name,location,type,aid FROM china.suzhou_pois_gaode ORDER BY aid;"
    db_conn = pymysql.connect(host='222.29.117.240', port=6667, user='geosoft', passwd='3702', charset='utf8')
    db_cursor = db_conn.cursor(cursor=pymysql.cursors.DictCursor)
    update_conn = pymysql.connect(host='222.29.117.240', port=6667, user='geosoft', passwd='3702', charset='utf8')
    update_cursor = update_conn.cursor(cursor=pymysql.cursors.DictCursor)

    hashmap = {}
    db_cursor.execute(select_sql)
    for record in tqdm(db_cursor.fetchall()):
        hashmap[(record['name'], record['location'], record['type'])] = str(record['aid'])

    print(len(hashmap))
    update_cursor.execute(
        "INSERT INTO china.suzhou_pois_gaode_distinct SELECT * FROM china.suzhou_pois_gaode WHERE aid in (%s);" %
        ','.join(hashmap.values()))


if __name__ == '__main__':
    get_distinct_name_location_type()

