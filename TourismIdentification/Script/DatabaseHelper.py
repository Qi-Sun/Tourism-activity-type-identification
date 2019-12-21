# -*- coding: utf8 -*-
# __author__ == 'sunqi'

import pymysql


def get_db_cursor():
    db_conn = pymysql.connect(host="222.29.117.240", port=6667, user='geosoft', password='3702', database='suzhou',
                              charset='utf8')
    return db_conn.cursor(pymysql.cursors.DictCursor)

def get_one_weibo():
    select_sql = "SELECT * FROM suzhou_sq.suzhou_weibos_only_sure_1218 LIMIT 1;"
    db_cursor = get_db_cursor()
    db_cursor.execute(select_sql)
    return db_cursor.fetchall()[0]

if __name__ == '__main__':
    record = get_one_weibo()
    print(record)

