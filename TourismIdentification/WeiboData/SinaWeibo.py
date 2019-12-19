# -*- coding: utf8 -*-
# __author__ == 'sunqi'

import datetime
import math
import numpy as np

class SinaWeibo(object):

    def __init__(self, id, text=None, source=None, user_source=None, time=None, latitude=None, longitude=None,
                 checkin_poiid=None, checkin_title=None, tourism_by_label=None, userid=None,
                 tourism_by_poi=None, tourism_by_origin=None, tourism_by_border=None, tourism_by_action=None):
        # field
        self.field_id = id
        self.field_text = text
        self.field_source = source
        self.field_user_source = user_source
        self.field_userid = userid
        self.field_time = time
        self.field_lat = latitude
        self.field_lng = longitude
        self.field_checkin_poiid = checkin_poiid
        self.field_checkin_title = checkin_title

        # label
        self.tourism_by_label = tourism_by_label
        self.tourism_by_poi = tourism_by_poi
        self.tourism_by_origin = tourism_by_origin
        self.tourism_by_border = tourism_by_border
        self.tourism_by_action = tourism_by_action

    def __str__(self):
        return str.format("id:{id},text:{text},time:{time},user_source:{user_source},checkin_title:{checkin_title}",
                          id=self.field_id, text=self.field_text, time=self.field_time,
                          user_source=self.field_user_source, checkin_title=self.field_checkin_title)


    def get_time_feature(self):
        WholeDaySenconds = 24 * 60 * 60
        PI = math.pi

        def _get_weekday_feature(ond_date: datetime):
            """
            获取周特征（一周中的某一天）
            :param ond_date:
            :return:
            """
            curWeekDay = ond_date.weekday()
            alpha = curWeekDay / 7 * 2 * PI
            return np.array([math.cos(alpha), math.sin(alpha)])

        def _get_date_feature(one_date: datetime):
            """
            获取日期特征（一年中的某一天）
            :param one_date:
            :return:
            """

            def __isLeapyear(year):
                if year % 400 == 0 or (year % 100 != 0 and year % 4 == 0):
                    return True
                else:
                    return False

            beginningOfYear = datetime.datetime(year=one_date.year, month=1, day=1)
            daynum2beginning = (one_date - beginningOfYear).days
            daynumWholeyear = 366 if __isLeapyear(one_date.year) else 365
            alpha = daynum2beginning / daynumWholeyear * 2 * PI
            return np.array([math.cos(alpha), math.sin(alpha)])

        def _get_daytime_feature(one_day_time: datetime):
            """
            获取时间特征（一天中的时刻，即24小时）
            :param one_day_time:
            :return:
            """
            seconds2zero = one_day_time.hour * 60 * 60 + one_day_time.minute * 60 + one_day_time.second
            alpha = seconds2zero / WholeDaySenconds * 2 * PI
            return np.array([math.cos(alpha), math.sin(alpha)])

        def _get_weekday_feature_one_hot(one_date: datetime):
            one_hot_vector = np.zeros((1,7))
            one_hot_vector[0][one_date.weekday()] = 1
            return one_hot_vector

        wide_vector = None
        deep_vector = None
        deep_vector = np.concatenate((_get_weekday_feature(self.field_time),_get_daytime_feature(self.field_time),
                                      _get_date_feature(self.field_time)),axis=0).reshape(-1)
        wide_vector = np.array(_get_weekday_feature_one_hot(self.field_time)).reshape(-1)
        return wide_vector,deep_vector

    def get_spatial_feature(self):
        pass

if __name__ == "__main__":
    weibo = SinaWeibo(0,time=datetime.datetime.now())
    print(weibo.get_time_feature())