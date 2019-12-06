# -*- coding: utf-8 -*-
from tqdm import tqdm
import random
import math
from typing import List
import pickle
from sklearn.cluster import DBSCAN, KMeans
import numpy as np


class Point(object):
    def __init__(self, id, lat, lng, num):
        self.id = int(id)
        self.lat = float(lat)
        self.lng = float(lng)
        self.num = int(num)
        self.cluster = None

    def get_distance(self, other_point) -> float:
        """
        获取与另一数据点的球面距离
        :param other_point: 另一点
        :return: 距离（米）
        """
        radlat1 = math.radians(self.lat)
        radlat2 = math.radians(other_point.lat)
        a = radlat1 - radlat2
        b = math.radians(self.lng) - math.radians(other_point.lng)
        s = 2 * math.asin(
            math.sqrt(pow(math.sin(a / 2), 2) + math.cos(radlat1) * math.cos(radlat2) * pow(math.sin(b / 2), 2)))
        earth_radius = 6378137
        s = s * earth_radius
        return s

    def get_distance_2(self, other_point) -> float:
        """
        获取与另一数据点的球面距离，优化
        :param other_point: 另一点
        :return: 距离（米）
        """
        dlat = math.radians(self.lat - other_point.lat)
        dlng = math.radians(self.lng - other_point.lng)
        # alat = math.radians((self.lat + other_point.lat) / 2)
        earth_radius = 6378137
        lx = dlng * earth_radius * 0.8542774316992952
        ly = earth_radius * dlat
        return pow(lx * lx + ly * ly, 0.5)


def read_csv(filename) -> List:
    """
    读取csv，生成点集
    :param filename: 文件名称
    :return: 点集（List）
    """
    PointSet = []
    with open(file=filename, mode='r') as rf:
        for line in rf.readlines():
            lat, lng, num = line.strip('\r\n').split(',')
            new_point = Point(len(PointSet), lat, lng, num)
            PointSet.append(new_point)
    return PointSet


def get_distance_hashmap(point_set):
    """
    获取点两两间的距离
    :param point_set:
    :return:
    """
    distance_hashmap = {}
    for i in tqdm(range(len(point_set))):
        for j in range(i + 1, len(point_set)):
            cur_distance = point_set[i].get_distance_2(point_set[j])
            distance_hashmap[(i, j)] = cur_distance
    return distance_hashmap


def get_distance(p1, p2):
    dlat = float(math.radians(p1[0] - p2[0]))
    dlng = float(math.radians(p1[1] - p2[1]))
    earth_radius = 6378137
    lx = dlng * earth_radius * 0.8542774316992952
    ly = earth_radius * dlat
    return pow(lx * lx + ly * ly, 0.5)


def read_csv_matrix(filename):
    pointSet = []
    weights = []
    with open(file=filename, mode='r') as rf:
        for line in rf.readlines():
            lat, lng, num = line.strip('\r\n').split(',')
            pointSet.append(np.array([float(lat), float(lng)]))
            weights.append(int(num))
    return np.array(pointSet), np.array(weights)


if __name__ == '__main__':
    # pointSet = read_csv('./mainpointwithnum.csv')
    # pickle.dump(pointSet, open('./main_point_set.pkl', 'wb'), 2)
    # distance_hashmap = get_distance_hashmap(pointSet)
    # pickle.dump(distance_hashmap, open('./main_point_distance.pkl', 'wb'), 2)

    pointSet, weights = read_csv_matrix('./mainpointwithnum.csv')
    dbscan = DBSCAN(eps=600, min_samples=4, n_jobs=4, algorithm='ball_tree', metric=get_distance)
    y_pred = dbscan.fit_predict(X=pointSet, sample_weight=weights)
    with open('./main_point_res.txt','w') as wf:
        for i in range():
            pass
    y_pred.savetxt('./main_point_pred_all.txt')
    # pickle.dump(y_pred, open('./main_point_pred_all.pkl', 'wb'), 2)
