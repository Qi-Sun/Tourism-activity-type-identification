# -*- coding: utf8 -*-

import pickle
from Script.SinaWeibo import SinaWeibo
from Script.WideDeepModel import WideModel, DeepDenseModel, DeepTextModel, WideDeepModel, DeepTextModel_batch, \
    WideDeepModel_batch,DeepModel_batch
import torch
import numpy as np
from tqdm import tqdm
import os
from collections import Counter
from torch.nn.utils.rnn import pack_padded_sequence, pad_packed_sequence, pad_sequence


def test_main():
    # train and test data
    train_x, train_y = pickle.load(open('../WeiboData/train.plk', 'rb'))
    test_x, test_y = pickle.load(open('../WeiboData/test.plk', 'rb'))
    word_embedding_matrix = np.load('../Data/word_embedding_matrix.npy')

    # Model
    wide_model = WideModel(wide_dim=19, output_dim=2)
    deep_dense_model = DeepDenseModel(input_dim=24, hidden_layers=[16, 8, 2], batchNorm=False, dropout=[0.5, 0.5, 0])
    deep_text_model = DeepTextModel(n_layers=2, hidden_dim=128, embed_dim=200, vocab_size=8824330, rnn_dropout=0,
                                    padding_idx=0, bidirectional=False, embedding_matrix=word_embedding_matrix,
                                    head_layers=[128, 32, 2], head_dropout=[0.5, 0.5, 0], head_batchnorm=False)
    wide_deep_model = WideDeepModel(wide_model=wide_model, deep_dense_model=deep_dense_model, output_dim=2,
                                    deep_text_model=deep_text_model, deep_head_layers=None, head_layers_dims=None,
                                    head_layers_dropout=None, head_layers_batchnorm=False)

    criterion = torch.nn.CrossEntropyLoss()
    optimizer = torch.optim.Adam(wide_deep_model.parameters())

    wide_deep_model.load_state_dict(torch.load('../Model/epoch_0_step_5.0.pth'))
    with torch.no_grad():
        # wf = open('../Result/test_1225.txt','w')
        y_pred_list = []
        for i in tqdm(range(len(test_x))):
            y_pred = wide_deep_model(test_x[i])
            if y_pred[0][0] > 0.5:
                y_pred_list.append(0)
            else:
                y_pred_list.append(1)

    pred_and_label = zip(y_pred_list, [x.item() for x in test_y])
    pred_and_label_paire = Counter(pred_and_label)
    print(pred_and_label_paire)


def test_main_no_text():
    # train and test data
    train_x, train_y = pickle.load(open('../WeiboData/train.plk', 'rb'))
    test_x, test_y = pickle.load(open('../WeiboData/test.plk', 'rb'))
    word_embedding_matrix = np.load('../Data/word_embedding_matrix.npy')

    # Model
    wide_model = WideModel(wide_dim=19, output_dim=2)
    deep_dense_model = DeepDenseModel(input_dim=24, hidden_layers=[16, 8, 2], batchNorm=False, dropout=[0.5, 0.5, 0])
    deep_text_model = DeepTextModel(n_layers=2, hidden_dim=128, embed_dim=200, vocab_size=8824330, rnn_dropout=0,
                                    padding_idx=0, bidirectional=False, embedding_matrix=word_embedding_matrix,
                                    head_layers=[128, 32, 2], head_dropout=[0.5, 0.5, 0], head_batchnorm=False)
    wide_deep_model = WideDeepModel(wide_model=wide_model, deep_dense_model=deep_dense_model, output_dim=2,
                                    deep_text_model=None, deep_head_layers=None, head_layers_dims=None,
                                    head_layers_dropout=None, head_layers_batchnorm=False)

    criterion = torch.nn.CrossEntropyLoss()
    optimizer = torch.optim.Adam(wide_deep_model.parameters())

    wide_deep_model.load_state_dict(torch.load('../Model/wide_deep/epoch_27_step_0.0.pth'))
    with torch.no_grad():
        # wf = open('../Result/test_1225.txt','w')
        y_pred_list = []
        for i in tqdm(range(len(test_x))):
            y_pred = wide_deep_model(test_x[i])
            if y_pred[0][0] > 0.5:
                y_pred_list.append(0)
            else:
                y_pred_list.append(1)

    pred_and_label = zip([x.item() for x in test_y], y_pred_list)
    pred_and_label_paire = Counter(pred_and_label)
    print(pred_and_label_paire)


def test_main_batch():
    # train and test data
    train_x, train_y = pickle.load(open('../WeiboData/train_1227.plk', 'rb'))
    test_x, test_y = pickle.load(open('../WeiboData/test_1227.plk', 'rb'))
    word_embedding_matrix = np.load('../Data/word_embedding_matrix.npy')

    # test data
    test_x_wide = torch.cat([x['wide'] for x in test_x])
    test_x_deep_dense = torch.cat([x['deep_dense'] for x in test_x])
    test_x_deep_text = [x['deep_text'][0] for x in test_x]

    # Model
    # Model
    wide_model = WideModel(wide_dim=27, output_dim=2)
    deep_dense_model = DeepDenseModel(input_dim=25, hidden_layers=[16, 8, 4], batchNorm=True, dropout=[0, 0, 0])

    deep_text_model = DeepTextModel_batch(n_layers=2, hidden_dim=128, embed_dim=200, vocab_size=8824330, rnn_dropout=0,
                                          padding_idx=0, bidirectional=False, embedding_matrix=word_embedding_matrix,
                                          head_layers=[128, 32, 16], head_dropout=[0, 0, 0], head_batchnorm=True)

    wide_deep_model = WideDeepModel_batch(wide_model=wide_model, deep_dense_model=deep_dense_model, output_dim=2,
                                          deep_text_model=deep_text_model, deep_head_layers=None,
                                          head_layers_dims=[24, 8, 2],
                                          head_layers_dropout=[0, 0, 0], head_layers_batchnorm=False)

    criterion = torch.nn.CrossEntropyLoss()
    optimizer = torch.optim.Adam(wide_deep_model.parameters())

    # batch size
    batch_size = 64
    test_sample_cnt = len(test_x)

    wide_deep_model.load_state_dict(torch.load('../Model/1229_wide_deep_text/epoch_49.pth'))
    with torch.no_grad():
        # wf = open('../Result/test_1225.txt','w')
        y_pred_list = []
        total_loss = 0.0
        for j in range(0, test_sample_cnt, batch_size):
            end_index = min(j + batch_size, test_sample_cnt)
            # text
            test_batch_deep_text = test_x_deep_text[j:end_index]
            test_batch_lengths = [x.size()[0] for x in test_batch_deep_text]
            test_batch_deep_text_pad_sequence = torch.nn.utils.rnn.pad_sequence(test_batch_deep_text,
                                                                                batch_first=True,
                                                                                padding_value=0)
            test_batch_deep_text_packed_seq = pack_padded_sequence(test_batch_deep_text_pad_sequence,
                                                                   test_batch_lengths,
                                                                   batch_first=True, enforce_sorted=False)

            # calculate
            y_test_pred = wide_deep_model(test_x_wide[j:end_index], test_x_deep_dense[j:end_index],
                                          test_batch_deep_text_packed_seq)
            test_loss = criterion(y_test_pred, test_y[j:end_index])
            total_loss += test_loss.item() * (end_index - j)
            for sample in y_test_pred:
                if sample[0] > 0.5:
                    y_pred_list.append(0)
                else:
                    y_pred_list.append(1)

        total_loss /= (test_sample_cnt * 1.0)
        print("loss:%s" % total_loss)

    pred_and_label = zip([x.item() for x in test_y], y_pred_list)
    pred_and_label_paire = Counter(pred_and_label)
    print(pred_and_label_paire)

def test_main_batch_no_wide():
    # train and test data
    train_x, train_y = pickle.load(open('../WeiboData/train.plk', 'rb'))
    test_x, test_y = pickle.load(open('../WeiboData/test.plk', 'rb'))
    word_embedding_matrix = np.load('../Data/word_embedding_matrix.npy')

    # test data
    test_x_wide = torch.cat([x['wide'] for x in test_x])
    test_x_deep_dense = torch.cat([x['deep_dense'] for x in test_x])
    test_x_deep_text = [x['deep_text'][0] for x in test_x]

    # Model
    # Model
    wide_model = WideModel(wide_dim=19, output_dim=2)
    deep_dense_model = DeepDenseModel(input_dim=24, hidden_layers=[16, 8, 4], batchNorm=False, dropout=[0, 0, 0])

    deep_text_model = DeepTextModel_batch(n_layers=2, hidden_dim=128, embed_dim=200, vocab_size=8824330, rnn_dropout=0,
                                          padding_idx=0, bidirectional=False, embedding_matrix=word_embedding_matrix,
                                          head_layers=[128, 32, 16], head_dropout=[0, 0, 0], head_batchnorm=False)

    wide_deep_model = DeepModel_batch(wide_model=wide_model, deep_dense_model=deep_dense_model, output_dim=2,
                                          deep_text_model=deep_text_model, deep_head_layers=None,
                                          head_layers_dims=[20, 8, 2],
                                          head_layers_dropout=[0, 0, 0], head_layers_batchnorm=False)

    criterion = torch.nn.CrossEntropyLoss()
    optimizer = torch.optim.Adam(wide_deep_model.parameters())

    # batch size
    batch_size = 64
    test_sample_cnt = len(test_x)

    wide_deep_model.load_state_dict(torch.load('../Model/deep_text_batch/epoch_45.pth'))
    with torch.no_grad():
        # wf = open('../Result/test_1225.txt','w')
        y_pred_list = []
        total_loss = 0.0
        for j in range(0, test_sample_cnt, batch_size):
            end_index = min(j + batch_size, test_sample_cnt)
            # text
            test_batch_deep_text = test_x_deep_text[j:end_index]
            test_batch_lengths = [x.size()[0] for x in test_batch_deep_text]
            test_batch_deep_text_pad_sequence = torch.nn.utils.rnn.pad_sequence(test_batch_deep_text,
                                                                                batch_first=True,
                                                                                padding_value=0)
            test_batch_deep_text_packed_seq = pack_padded_sequence(test_batch_deep_text_pad_sequence,
                                                                   test_batch_lengths,
                                                                   batch_first=True, enforce_sorted=False)

            # calculate
            y_test_pred = wide_deep_model(test_x_wide[j:end_index], test_x_deep_dense[j:end_index],
                                          test_batch_deep_text_packed_seq)
            test_loss = criterion(y_test_pred, test_y[j:end_index])
            total_loss += test_loss.item() * (end_index - j)
            for sample in y_test_pred:
                if sample[0] > 0.5:
                    y_pred_list.append(0)
                else:
                    y_pred_list.append(1)

        total_loss /= (test_sample_cnt * 1.0)
        print("loss:%s" % total_loss)

    pred_and_label = zip([x.item() for x in test_y], y_pred_list)
    pred_and_label_paire = Counter(pred_and_label)
    print(pred_and_label_paire)



if __name__ == '__main__':
    test_main_batch()
