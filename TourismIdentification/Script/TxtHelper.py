# -*- coding: utf8 -*-
# __author__ == 'sunqi'

import os

def read_points_txt(filename):
    points = []
    with open(filename,'r',encoding='utf8') as rf:
        for line in rf.readlines():
            lat,lng = line.strip('\r\n').split('\t')
            points.append([float(lat),float(lng)])
    return points

def read_city_distance_csv(filename):
    distance_hashmap = {}
    with open(filename,'r',encoding='utf8') as rf:
        for line in rf.readlines():
            city,distance = line.strip('\r\n').split(',')
            distance_hashmap[city] = float(distance)
    return distance_hashmap




