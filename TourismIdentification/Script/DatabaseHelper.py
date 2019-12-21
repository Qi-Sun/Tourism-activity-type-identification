# -*- coding: utf8 -*-
# __author__ == 'sunqi'

import pymysql


def get_db_cursor():
    db_conn = pymysql.connect(host="222.29.117.240", port=6667, user='geosoft', password='3702', database='suzhou',
                              charset='utf8')
    return db_conn.cursor(pymysql.cursors.DictCursor)

