# -*- coding::utf8 -*-

import os
import pymysql
import math
from tqdm import tqdm


def gaode2wgs84(lng, lat):
    x_pi = 3.14159265358979324 * 3000.0 / 180.0
    pi = 3.1415926535897932384626  # π
    a = 6378245.0  # 长半轴
    ee = 0.00669342162296594323  # 扁率

    def transform_lat(_lng, _lat):
        ret = -100.0 + 2.0 * _lng + 3.0 * _lat + 0.2 * _lat * _lat + 0.1 * _lng * _lat + 0.2 * math.sqrt(
            math.fabs(_lng))
        ret += (20.0 * math.sin(6.0 * _lng * pi) + 20.0 * math.sin(2.0 * _lng * pi)) * 2.0 / 3.0
        ret += (20.0 * math.sin(_lat * pi) + 40.0 * math.sin(_lat / 3.0 * pi)) * 2.0 / 3.0
        ret += (160.0 * math.sin(_lat / 12.0 * pi) + 320 * math.sin(_lat * pi / 30.0)) * 2.0 / 3.0
        return ret

    def transform_lng(_lng, _lat):
        ret = 300.0 + _lng + 2.0 * _lat + 0.1 * _lng * _lng + 0.1 * _lng * _lat + 0.1 * math.sqrt(math.fabs(_lng))
        ret += (20.0 * math.sin(6.0 * _lng * pi) + 20.0 * math.sin(2.0 * _lng * pi)) * 2.0 / 3.0
        ret += (20.0 * math.sin(_lng * pi) + 40.0 * math.sin(_lng / 3.0 * pi)) * 2.0 / 3.0
        ret += (150.0 * math.sin(_lng / 12.0 * pi) + 300.0 * math.sin(_lng / 30.0 * pi)) * 2.0 / 3.0
        return ret

    dlat = transform_lat(lng - 105.0, lat - 35.0)
    dlng = transform_lng(lng - 105.0, lat - 35.0)
    radlat = lat / 180.0 * pi
    magic = math.sin(radlat)
    magic = 1 - ee * magic * magic
    sqrtmagic = math.sqrt(magic)
    dlat = (dlat * 180.0) / ((a * (1 - ee)) / (magic * sqrtmagic) * pi)
    dlng = (dlng * 180.0) / (a / sqrtmagic * math.cos(radlat) * pi)
    mglat = lat + dlat
    mglng = lng + dlng
    return lng * 2 - mglng, lat * 2 - mglat


def update_database(tablename, field_primeKey, field_lng, field_lat, field_gps_lng, field_gps_lat):
    select_sql = "SELECT %s,%s,%s FROM %s;" % (field_primeKey, field_lng, field_lat, tablename)
    update_sql = "UPDATE %s SET %s = %f, %s = %f WHERE %s = %d;"
    db_conn = pymysql.connect(host='222.29.117.240', port=6667, user='geosoft', passwd='3702', charset='utf8')
    db_cursor = db_conn.cursor(cursor=pymysql.cursors.DictCursor)
    update_conn = pymysql.connect(host='222.29.117.240', port=6667, user='geosoft', passwd='3702', charset='utf8')
    update_cursor = update_conn.cursor(cursor=pymysql.cursors.DictCursor)
    db_cursor.execute(select_sql)
    for record in tqdm(db_cursor.fetchall()):
        pkey = record[field_primeKey]
        gps_lng, gps_lat = gaode2wgs84(float(record[field_lng]), float(record[field_lat]))
        update_cursor.execute(
            update_sql % (tablename, field_gps_lng, gps_lng, field_gps_lat, gps_lat, field_primeKey, pkey))
    return


if __name__ == '__main__':
    update_database("china.beijing_poi", 'aid', 'lng', 'lat', 'gps_lng', 'gps_lat')
