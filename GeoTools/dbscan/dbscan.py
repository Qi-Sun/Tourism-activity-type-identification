# -*- coding: utf-8 -*-
"""
Created on Wed Dec  4 12:58:10 2019

@author: 13197
"""

from math import *
import csv
from tqdm import tqdm
import threading
import random


coreObjs = {}
C = {}


def get_distance(array_1, array_2):
    lon_a = array_1[1]
    lat_a = array_1[0]
    lon_b = array_2[1]
    lat_b = array_2[0]
    radlat1 = radians(lat_a)
    radlat2 = radians(lat_b)
    a = radlat1 - radlat2
    b = radians(lon_a) - radians(lon_b)
    s = 2 * asin(sqrt(pow(sin(a / 2), 2) + cos(radlat1) * cos(radlat2) * pow(sin(b / 2), 2)))
    earth_radius = 6378137
    s = s * earth_radius
    return s


def getNeibor(data, dataSet, e):
    res = []
    for i in range(len(dataSet)):
        if get_distance(data, dataSet[i]) < e:
            res.append(i)
    return res


def DBSCAN(dataSet, e, minPts):
    n = len(dataSet)

    for i in tqdm(range(n)):
        neibor = getNeibor(dataSet[i], dataSet, e)
        if len(neibor) + dataSet[i][2] >= minPts:
            coreObjs[i] = neibor

    '''
    threadList = []
    length = int(n/8)
    for j in tqdm(range(8)):
        threadList.append(myThread(length*j,length*(j+1),dataSet,e,minPts))
    for k in range(8):
        threadList[k].start()
    '''

    oldCoreObjs = coreObjs.copy()
    k = 0  # 初始化聚类簇数
    notAccess = list(range(n))  # 初始化未访问样本集合（索引）
    while len(coreObjs) > 0:
        OldNotAccess = []
        OldNotAccess.extend(notAccess)
        cores = list(coreObjs.keys())
        print(cores)
        # 随机选取一个核心对象
        randNum = random.randint(0, len(cores) - 1)
        print(str(len(cores)) + "  " + str(randNum))
        core = cores[randNum]
        queue = []
        queue.append(core)
        notAccess.remove(core)
        while len(queue) > 0:
            q = queue[0]
            del queue[0]
            if q in oldCoreObjs.keys():
                delte = [val for val in oldCoreObjs[q] if val in notAccess]  # Δ = N(q)∩Γ
                queue.extend(delte)  # 将Δ中的样本加入队列Q
                notAccess = [val for val in notAccess if val not in delte]  # Γ = Γ\Δ
        k += 1
        C[k] = [val for val in OldNotAccess if val not in notAccess]
        for x in C[k]:
            if x in coreObjs.keys():
                del coreObjs[x]
    return C


csvFile = open("D:\suzhou_dbscan\mainpointwithnum.csv", 'r')
reader = csv.reader(csvFile)

line = []
for item in reader:
    line.append([float(item[0]), float(item[1]), int(item[2])])
p = DBSCAN(line, 500, 50)

with open("D:\suzhou_dbscan\mycode_cluster.csv", 'w', newline='') as f:
    k = csv.writer(f)
    for i in range(1, len(p) + 1):
        for j in range(len(p[i])):
            t = []
            t = line[p[i][j]]
            t.append(i)
            k.writerow(t)
