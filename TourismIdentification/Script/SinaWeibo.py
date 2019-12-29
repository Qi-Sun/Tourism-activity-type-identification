# -*- coding: utf8 -*-
# __author__ == 'sunqi'

import datetime
import math
import numpy as np
import jieba
from typing import List, Dict
import torch


class SinaWeibo(object):

    def __init__(self, id, text=None, source=None, user_source=None, time=None, latitude=None, longitude=None,
                 checkin_poiid=None, checkin_title=None, checkin_type=None, tourism_by_label=None, userid=None,
                 tourism_by_poi=None, tourism_by_origin=None, tourism_by_border=None, tourism_by_action=None):
        # field
        self.field_id = id
        self.field_text = text
        self.field_source = source
        self.field_user_source = user_source if user_source is not None else '不是苏州市'
        self.field_userid = userid
        self.field_time = time
        self.field_lat = latitude
        self.field_lng = longitude
        self.field_checkin_poiid = checkin_poiid
        self.field_checkin_title = checkin_title
        self.field_checkin_type = checkin_type

        # label
        self.tourism_by_label = tourism_by_label
        self.tourism_by_poi = tourism_by_poi
        self.tourism_by_origin = tourism_by_origin
        self.tourism_by_border = tourism_by_border
        self.tourism_by_action = tourism_by_action

    @classmethod
    def construct_from_record(cls, record):
        return cls(id=record['id'], source=record['source'], user_source=record['user_source'],
                   time=record['time'], latitude=record['latitude'], text=record['text'],
                   longitude=record['longitude'], checkin_poiid=record['checkin_poiid'],
                   checkin_title=record['checkin_title'], tourism_by_label=record['tourism_by_label'],
                   userid=record['userid'], tourism_by_poi=record['tourism_by_poi'],
                   tourism_by_origin=record['tourism_by_origin'], tourism_by_border=record['tourism_by_border'],
                   tourism_by_action=record['tourism_by_action'])

    def __str__(self):
        return str.format("id:{id},text:{text},time:{time},user_source:{user_source},checkin_title:{checkin_title}",
                          id=self.field_id, text=self.field_text, time=self.field_time,
                          user_source=self.field_user_source, checkin_title=self.field_checkin_title)

    def get_time_feature(self):
        wide_vector = None
        deep_vector = None
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
            one_hot_vector = np.zeros(7)
            one_hot_vector[one_date.weekday()] = 1
            return one_hot_vector

        def _get_hour_feature(one_day_time: datetime):
            one_hot_vector = np.zeros(8)
            index = ((one_day_time.hour + 24 - 7) % 24) // 3
            one_hot_vector[index] = 1
            return one_hot_vector

        def _get_holiday(one_day_time: datetime):
            holidays = [(datetime.datetime(2013, 1, 1), datetime.datetime(2013, 1, 3)),
                        (datetime.datetime(2013, 2, 9), datetime.datetime(2013, 2, 15)),
                        (datetime.datetime(2013, 4, 4), datetime.datetime(2013, 4, 6)),
                        (datetime.datetime(2013, 4, 29), datetime.datetime(2013, 5, 1)),
                        (datetime.datetime(2013, 6, 10), datetime.datetime(2013, 6, 12)),
                        (datetime.datetime(2013, 9, 19), datetime.datetime(2013, 9, 21)),
                        (datetime.datetime(2013, 10, 1), datetime.datetime(2013, 10, 7)),
                        (datetime.datetime(2012, 1, 1), datetime.datetime(2012, 1, 3)),
                        (datetime.datetime(2012, 1, 22), datetime.datetime(2012, 1, 28)),
                        (datetime.datetime(2012, 4, 2), datetime.datetime(2012, 4, 4)),
                        (datetime.datetime(2012, 4, 29), datetime.datetime(2012, 5, 1)),
                        (datetime.datetime(2012, 6, 22), datetime.datetime(2012, 6, 24)),
                        (datetime.datetime(2012, 9, 30), datetime.datetime(2012, 10, 7)),
                        (datetime.datetime(2011, 1, 1), datetime.datetime(2011, 1, 3)),
                        (datetime.datetime(2011, 2, 2), datetime.datetime(2011, 2, 8)),
                        (datetime.datetime(2011, 4, 3), datetime.datetime(2011, 4, 5)),
                        (datetime.datetime(2011, 4, 30), datetime.datetime(2011, 5, 2)),
                        (datetime.datetime(2011, 6, 4), datetime.datetime(2011, 6, 6)),
                        (datetime.datetime(2011, 9, 10), datetime.datetime(2011, 9, 12)),
                        (datetime.datetime(2011, 10, 1), datetime.datetime(2011, 10, 7))
                        ]

            def get_day_to_holiday(one_day: datetime, start_time: datetime, end_time: datetime):
                delta1 = (one_day - start_time).days
                delta2 = (one_day - end_time).days
                if delta1 * delta2 <= 0:
                    return 0
                else:
                    return min(abs(delta1), abs(delta2))

            days_to_holiday = [get_day_to_holiday(one_day_time, start, end) for start, end in holidays]
            return np.array([min(days_to_holiday)])

        deep_vector = np.concatenate((_get_weekday_feature(self.field_time), _get_daytime_feature(self.field_time),
                                      _get_date_feature(self.field_time), _get_holiday(self.field_time)),
                                     axis=0).reshape(-1)
        wide_vector = np.concatenate(
            (_get_weekday_feature_one_hot(self.field_time), _get_hour_feature(self.field_time)), axis=0).reshape(-1)
        return wide_vector, deep_vector

    def get_spatial_feature(self, city_distance_dict: dict, pois_attraction: List, pois_eat: List,
                            pois_accommodation: List, pois_transportation: List, pois_buy: List,
                            pois_entertainment: List):
        wide_vector = None
        deep_vector = None

        def _get_distance_by_latlng(lat1, lng1, lat2, lng2):
            rad_lat1 = math.radians(lat1)
            rad_lat2 = math.radians(lat2)
            delta_lat = rad_lat1 - rad_lat2
            delta_lng = math.radians(lng1) - math.radians(lng2)
            theta = 2 * math.asin(
                math.sqrt(pow(math.sin(delta_lat / 2), 2) + math.cos(rad_lat1) * math.cos(rad_lat2) * pow(
                    math.sin(delta_lng / 2), 2)))
            earth_radius = 6378.137
            return theta * earth_radius

        def _get_user_source_feature_one_hot():
            one_hot_vector = np.zeros(3)
            if self.field_user_source == '苏州市':
                one_hot_vector[0] = 1
            elif self.field_user_source in ['上海市', '无锡市', '嘉兴市', '湖州市', '常州市']:
                one_hot_vector[1] = 1
            else:
                one_hot_vector[2] = 1
            return one_hot_vector

        def _get_checkin_poi_one_hot():
            if self.field_checkin_poiid is not None:
                return np.array([1])
            else:
                return np.array([0])

        def _get_checkin_type_one_hot():
            hashmap_dict = {'游玩': 0, '餐饮': 1, '住宿': 2, '出行': 3, '购物': 4, '娱乐': 5, '其他': 6}
            one_hot_vector = np.zeros(8)
            one_hot_index = 7 if self.field_checkin_type not in hashmap_dict else hashmap_dict[self.field_checkin_type]
            one_hot_vector[one_hot_index] = 1
            return one_hot_vector

        def _get_user_source_feature():
            distance = city_distance_dict[self.field_user_source]
            distance_pow2 = pow(distance, 2)
            distance_pow3 = pow(distance, 3)
            return np.array(
                [distance, distance_pow2, distance_pow3, 1.0 / distance, 1.0 / distance_pow2, 1.0 / distance_pow3])

        def _get_distance_nearest_poi():
            distances = []
            for pois in [pois_attraction, pois_eat, pois_accommodation, pois_transportation, pois_buy,
                         pois_entertainment]:
                if pois is None:
                    continue
                nearest_distance = float('inf')
                nearest_distance = min(
                    [_get_distance_by_latlng(self.field_lat, self.field_lng, x[0], x[1]) for x in pois])
                nearest_distance += 0.01
                distances.append(1.0 / nearest_distance)
                distances.append(1.0 / pow(nearest_distance, 2))
            return np.array(distances)

        wide_vector = np.concatenate(
            (_get_user_source_feature_one_hot(), _get_checkin_poi_one_hot(), _get_checkin_type_one_hot()),
            axis=0).reshape(-1)
        deep_vector = np.concatenate((_get_user_source_feature(), _get_distance_nearest_poi()), axis=0).reshape(-1)
        return wide_vector, deep_vector

    def get_text_feature(self, word2index, embed_matrix):
        deep_vector = None

        def _clean_text_AT(text):
            stack = []
            length = len(text)
            index = 0
            while index < length:
                if text[index] != '@':
                    stack.append(text[index])
                    index += 1
                else:
                    index += 1
                    while index < length and text[index] not in ['@', ' ']:
                        index += 1
            return ''.join(stack)

        def _clean_text_emotion(text):
            stack = []
            length = len(text)
            index = 0
            while index < length:
                if text[index] != '[':
                    stack.append(text[index])
                    index += 1
                else:
                    while index < length and text[index] != ']':
                        index += 1
            return ''.join(stack)

        def _clean_text_url(text):
            return text.split('http')[0]

        def _get_words(text):
            cut_text = [x for x in jieba.cut(text)]
            return cut_text

        text = self.field_text
        text = _clean_text_AT(text)
        text = _clean_text_emotion(text)
        text = _clean_text_url(text)
        words = _get_words(text)
        deep_vector = np.array([word2index[x] if x in word2index else 0 for x in words] + [0])
        deep_matrix = [embed_matrix[index] for index in deep_vector]
        deep_matrix = np.array(deep_matrix)
        return deep_vector, deep_matrix

    def get_all_feature(self, city_distance_dict: dict, pois_attraction: List, pois_eat: List,
                        pois_accommodation: List, pois_transportation: List, pois_buy: List,
                        pois_entertainment: List, word2index: Dict, embed_matrix: List):
        spatial_wide_vector, spatial_deep_vector = self.get_spatial_feature(city_distance_dict, pois_attraction,
                                                                            pois_eat,
                                                                            pois_accommodation, pois_transportation,
                                                                            pois_buy, pois_entertainment)
        time_wide_vector, time_deep_vector = self.get_time_feature()
        text_vector, text_matrix = self.get_text_feature(word2index, embed_matrix)
        return {'wide': torch.Tensor([np.concatenate((spatial_wide_vector, time_wide_vector), axis=0).reshape(-1)]),
                'deep_dense': torch.Tensor(
                    [np.concatenate((spatial_deep_vector, time_deep_vector), axis=0).reshape(-1)]),
                'deep_text': torch.Tensor([text_vector]),
                'deep_text_matrix': torch.Tensor(text_matrix)}


if __name__ == "__main__":
    weibo = SinaWeibo(0, time=datetime.datetime.now())
    print(weibo.get_time_feature())
