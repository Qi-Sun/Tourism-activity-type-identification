# -*- coding: utf8 -*-


from Script.SinaWeibo import SinaWeibo
from Script import DatabaseHelper
from Script.TxtHelper import read_points_txt, read_city_distance_csv, read_embed_txt_3
import pickle
import random
from tqdm import tqdm
import numpy as np
import torch


def read_from_db_write_to_file(train_table='suzhou_weibos_only_sure_train_1224',
                               test_table='suzhou_weibos_only_sure_test_1224'):
    test_weibos = DatabaseHelper.get_weibos(test_table)
    train_weibos = DatabaseHelper.get_weibos(train_table)
    city_distance = read_city_distance_csv('../Data/city_distance.csv')
    pois_attraction = read_points_txt('../Data/points_attraction.txt')
    pois_eat = read_points_txt('../Data/points_eat.txt')
    pois_accommodation = read_points_txt('../Data/points_accommodation.txt')
    pois_transportation = read_points_txt('../Data/points_transportation.txt')
    pois_buy = read_points_txt('../Data/points_buy.txt')
    pois_entertainment = read_points_txt('../Data/points_entertainment.txt')

    print('Pre-train Data Done')

    train_weibos = [SinaWeibo.construct_from_record(x) for x in train_weibos]
    test_weibos = [SinaWeibo.construct_from_record(x) for x in test_weibos]

    random.shuffle(train_weibos)
    random.shuffle(test_weibos)

    word2index, index2word = pickle.load(open('../Data/Tencent_ChineseEmbedding_Dict.plk', 'rb'))
    word_embedding_matrix = np.load('../Data/word_embedding_matrix.npy')

    train_weibos_vector = [
        w.get_all_feature(city_distance, pois_attraction, pois_eat, pois_accommodation, pois_transportation, pois_buy,
                          pois_entertainment, word2index, word_embedding_matrix) for w in tqdm(train_weibos)]
    test_weibos_vector = [
        w.get_all_feature(city_distance, pois_attraction, pois_eat, pois_accommodation, pois_transportation, pois_buy,
                          pois_entertainment, word2index, word_embedding_matrix) for w in tqdm(test_weibos)]

    train_label = torch.Tensor([1 if w.tourism_by_label == 'true' else 0 for w in train_weibos]).long()
    test_label = torch.Tensor([1 if w.tourism_by_label == 'true' else 0 for w in test_weibos]).long()
    return train_weibos_vector, train_label, test_weibos_vector, test_label


if __name__ == '__main__':
    # word2index, index2word = pickle.load(open('../Data/Tencent_ChineseEmbedding_Dict.plk', 'rb'))
    # print('Done')

    # 2019年末的实验
    # train_x, train_y, test_x, test_y = read_from_db_write_to_file()
    # pickle.dump((train_x, train_y), open('../WeiboData/train_1227.plk', 'wb'), 2)
    # pickle.dump((test_x, test_y), open('../WeiboData/test_1227.plk', 'wb'), 2)

    # 1.8的实验
    train_x, train_y, test_x, test_y = read_from_db_write_to_file(train_table='suzhou_weibos_checkin_train_sample_0108',
                                                                  test_table='suzhou_weibos_only_sure_test_1224')
    pickle.dump((train_x, train_y), open('../WeiboData/train_0108.plk', 'wb'), 2)
    pickle.dump((test_x, test_y), open('../WeiboData/test_0108.plk', 'wb'), 2)
