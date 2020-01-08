# -*- coding: utf8 -*-
# __author__ == 'sunqi'

import os
import numpy as np
from tqdm import tqdm
import pickle


def read_points_txt(filename):
    points = []
    with open(filename, 'r', encoding='utf8') as rf:
        for line in rf.readlines():
            lat, lng = line.strip('\r\n').split('\t')
            points.append([float(lat), float(lng)])
    return points


def read_city_distance_csv(filename):
    distance_hashmap = {}
    with open(filename, 'r', encoding='utf8') as rf:
        for line in rf.readlines():
            city, distance = line.strip('\r\n').split(',')
            distance_hashmap[city] = float(distance)
    return distance_hashmap


def read_embed_txt(filename):
    word2index = {}
    index2word = {}
    word_embedding_matrix = []
    with open(filename, 'r', encoding='utf8') as rf:
        line = rf.readline()
        while line:
            line = line.split(' ')
            if len(line) > 10:
                vector = np.array([float(x) for x in line[1:]])
                word2index[line[0]] = len(word_embedding_matrix)
                index2word[len(word_embedding_matrix)] = line[0]
                word_embedding_matrix.append(vector)
            line = rf.readline()
    word_embedding_matrix = np.array(word_embedding_matrix)
    return word2index, index2word, word_embedding_matrix


def read_embed_txt_2(filename):
    word2index = {}
    index2word = {}
    word_embedding_matrix = []
    with open(filename, 'r', encoding='utf8') as rf:
        lines = rf.readlines()
        for i in tqdm(range(1, len(lines))):
            digits = lines[i].split(' ')
            # vector = [float(x) for x in digits[1:]]
            word2index[digits[0]] = i - 1
            index2word[i - 1] = digits[0]
            # word_embedding_matrix.append(vector)
    # word_embedding_matrix = np.array(word_embedding_matrix)
    return word2index, index2word


def read_embed_txt_3(filename):
    word_embedding_matrix = []
    with open(filename, 'r', encoding='utf8') as rf:
        lines = rf.readlines()
        word_embedding_matrix = [x for x in lines]
        rf.close()
    return word_embedding_matrix


def read_embed_txt_4(filename):
    word_embedding_matrix = []
    with open(filename, 'r', encoding='utf8') as rf:
        lines = rf.readlines()
        for i in tqdm(range(1, len(lines))):
            digits = lines[i].split(' ')
            vector = [float(x) for x in digits[1:]]
            word_embedding_matrix.append(vector)
    word_embedding_matrix = np.array(word_embedding_matrix)
    return word_embedding_matrix


if __name__ == '__main__':
    word2index, index2word = read_embed_txt_2('/home/sunqi/Weibo2Vec/word2vec/Tencent_AILab_ChineseEmbedding.txt')
    pickle.dump((word2index, index2word), open('../Data/Tencent_ChineseEmbedding_Dict.plk', 'wb'), 2)
    # word_embedding_matrix = read_embed_txt_4('/home/sunqi/Weibo2Vec/word2vec/Tencent_AILab_ChineseEmbedding.txt')
    # word_embedding_matrix.dump('../Data/word_embedding_matrix.mat')
    # np.save('../Data/word_embedding_matrix.npy', word_embedding_matrix)
