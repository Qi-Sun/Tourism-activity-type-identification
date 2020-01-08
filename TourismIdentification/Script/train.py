# -*- coding: utf8 -*-

import pickle
from Script.SinaWeibo import SinaWeibo
from Script.WideDeepModel import WideModel, DeepDenseModel, DeepTextModel, WideDeepModel, WideDeepModel_batch, \
    DeepTextModel_batch, DeepModel_batch
import torch
import numpy as np
from tqdm import tqdm
import os
from torch.nn.utils.rnn import pack_padded_sequence, pad_packed_sequence, pad_sequence


def train_main():
    # train and test data
    train_x, train_y = pickle.load(open('../WeiboData/train.plk', 'rb'))
    test_x, test_y = pickle.load(open('../WeiboData/test.plk', 'rb'))
    word_embedding_matrix = np.load('../Data/word_embedding_matrix.npy')

    dev_x, dev_y = train_x[-200:], train_y[-200:]
    train_x, train_y = train_x[:-200], train_y[:-200]

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
    optimizer = torch.optim.Adam(wide_deep_model.parameters(), weight_decay=0.01)

    # dev stage
    min_loss_dev = []
    model_hashmap = {}

    # Training loop
    for epoch in range(1):
        for i in tqdm(range(len(train_x))):
            y_pred = wide_deep_model(train_x[i])

            # Compute and print loss
            loss = criterion(y_pred, train_y[i:i + 1])

            # test step
            if i % 50 == 0:
                print('%s %s %s \n' % (epoch, i, loss.item()))
                total_loss = 0
                with torch.no_grad():
                    for j in range(len(dev_x)):
                        y_dev_pred = wide_deep_model(dev_x[j])
                        dev_loss = criterion(y_dev_pred, dev_y[j:j + 1])
                        total_loss += dev_loss.item()
                    total_loss /= len(dev_x)

                    if not min_loss_dev or total_loss < min_loss_dev[0]:
                        min_loss_dev.append(total_loss)
                        model_hashmap[total_loss] = '../Model/wide_deep_text/epoch_%s_step_%s.pth' % (epoch, i / 50)
                        torch.save(wide_deep_model.state_dict(), model_hashmap[total_loss])
                        min_loss_dev.sort()
                        if len(min_loss_dev) > 5:
                            os.remove(model_hashmap[min_loss_dev[-1]])
                            min_loss_dev.pop(-1)
                    print('loss: %s on dev epoch %s step %d\n' % (total_loss, epoch, i // 50))

            # Zero gradients,perform a backward pass,and update the weights
            optimizer.zero_grad()
            loss.backward()
            optimizer.step()


def train_main_no_text():
    # train and test data
    train_x, train_y = pickle.load(open('../WeiboData/train.plk', 'rb'))
    test_x, test_y = pickle.load(open('../WeiboData/test.plk', 'rb'))
    word_embedding_matrix = np.load('../Data/word_embedding_matrix.npy')

    dev_x, dev_y = train_x[-200:], train_y[-200:]
    train_x, train_y = train_x[:-200], train_y[:-200]

    # Model
    wide_model = WideModel(wide_dim=19, output_dim=2)
    deep_dense_model = DeepDenseModel(input_dim=24, hidden_layers=[16, 8, 2], batchNorm=False, dropout=[0.5, 0.5, 0])
    wide_deep_model = WideDeepModel(wide_model=wide_model, deep_dense_model=deep_dense_model, output_dim=2,
                                    deep_text_model=None, deep_head_layers=None, head_layers_dims=None,
                                    head_layers_dropout=None, head_layers_batchnorm=False)

    criterion = torch.nn.CrossEntropyLoss()
    optimizer = torch.optim.Adam(wide_deep_model.parameters(), weight_decay=0.01)

    # dev stage
    min_loss_dev = []
    model_hashmap = {}

    # Training loop
    for epoch in range(100):
        for i in tqdm(range(len(train_x))):
            y_pred = wide_deep_model(train_x[i])

            # Compute and print loss
            loss = criterion(y_pred, train_y[i:i + 1])

            # test step
            if i % 50 == 0:
                print('%s %s %s \n' % (epoch, i, loss.item()))
                total_loss = 0
                with torch.no_grad():
                    for j in range(len(dev_x)):
                        y_dev_pred = wide_deep_model(dev_x[j])
                        dev_loss = criterion(y_dev_pred, dev_y[j:j + 1])
                        total_loss += dev_loss.item()
                    total_loss /= len(dev_x)

                    if not min_loss_dev or total_loss < min_loss_dev[0]:
                        min_loss_dev.append(total_loss)
                        model_hashmap[total_loss] = '../Model/wide_deep/epoch_%s_step_%s.pth' % (epoch, i / 50)
                        torch.save(wide_deep_model.state_dict(), model_hashmap[total_loss])
                        min_loss_dev.sort()
                        if len(min_loss_dev) > 5:
                            os.remove(model_hashmap[min_loss_dev[-1]])
                            min_loss_dev.pop(-1)
                    print('loss: %s on dev epoch %s step %d\n' % (total_loss, epoch, i // 50))

            # Zero gradients,perform a backward pass,and update the weights
            optimizer.zero_grad()
            loss.backward()
            optimizer.step()


def train_main_batch():
    # train and test data
    train_x, train_y = pickle.load(open('../WeiboData/train_1227.plk', 'rb'))
    test_x, test_y = pickle.load(open('../WeiboData/test_1227.plk', 'rb'))
    word_embedding_matrix = np.load('../Data/word_embedding_matrix.npy')

    train_x_wide = torch.cat([x['wide'] for x in train_x], 0)
    train_x_deep_dense = torch.cat([x['deep_dense'] for x in train_x], 0)
    train_x_deep_text = [x['deep_text'][0] for x in train_x]

    test_x_wide = torch.cat([x['wide'] for x in test_x])
    test_x_deep_dense = torch.cat([x['deep_dense'] for x in test_x])
    test_x_deep_text = [x['deep_text'][0] for x in test_x]

    dev_sample_cnt = 200
    train_sample_cnt = len(train_x) - dev_sample_cnt

    dev_x_wide, dev_x_deep_dense, dev_x_deep_text, dev_y = train_x_wide[-dev_sample_cnt:], \
                                                           train_x_deep_dense[-dev_sample_cnt:], \
                                                           train_x_deep_text[-dev_sample_cnt:], \
                                                           train_y[-dev_sample_cnt:]

    train_x_wide, train_x_deep_dense, train_x_deep_text, train_y = train_x_wide[:-dev_sample_cnt], \
                                                                   train_x_deep_dense[:-dev_sample_cnt], \
                                                                   train_x_deep_text[:-dev_sample_cnt], \
                                                                   train_y[:-dev_sample_cnt]

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
    optimizer = torch.optim.Adam(wide_deep_model.parameters(), weight_decay=0.025,lr=0.005)

    # batch size
    batch_size = 64

    # dev stage
    min_loss_dev = []
    model_hashmap = {}

    # Training loop
    for epoch in range(50):
        for i in range(0, train_sample_cnt, batch_size):
            end_index = min(i + batch_size, train_sample_cnt)
            cur_batch_deep_text = train_x_deep_text[i:end_index]
            cur_batch_lengths = [x.size()[0] for x in cur_batch_deep_text]
            cur_batch_deep_text_pad_sequence = torch.nn.utils.rnn.pad_sequence(cur_batch_deep_text, batch_first=True,
                                                                               padding_value=0)
            cur_batch_deep_text_packed_seq = pack_padded_sequence(cur_batch_deep_text_pad_sequence, cur_batch_lengths,
                                                                  batch_first=True, enforce_sorted=False)

            y_pred = wide_deep_model(train_x_wide[i:end_index], train_x_deep_dense[i:end_index],
                                     cur_batch_deep_text_packed_seq)

            # Compute and print loss
            loss = criterion(y_pred, train_y[i:end_index])
            print('epoch:%s batch:%s loss:%s' % (epoch, i // batch_size, loss.item()))

            # Zero gradients,perform a backward pass,and update the weights
            optimizer.zero_grad()
            loss.backward()
            optimizer.step()

        # dev step
        total_loss = 0
        with torch.no_grad():
            for j in range(0, dev_sample_cnt, batch_size):
                end_index = min(j + batch_size, dev_sample_cnt)
                # text
                dev_batch_deep_text = dev_x_deep_text[j:end_index]
                dev_batch_lengths = [x.size()[0] for x in dev_batch_deep_text]
                dev_batch_deep_text_pad_sequence = torch.nn.utils.rnn.pad_sequence(dev_batch_deep_text,
                                                                                   batch_first=True,
                                                                                   padding_value=0)
                dev_batch_deep_text_packed_seq = pack_padded_sequence(dev_batch_deep_text_pad_sequence,
                                                                      dev_batch_lengths,
                                                                      batch_first=True, enforce_sorted=False)

                # calculate
                y_dev_pred = wide_deep_model(dev_x_wide[j:end_index], dev_x_deep_dense[j:end_index],
                                             dev_batch_deep_text_packed_seq)
                dev_loss = criterion(y_dev_pred, dev_y[j:end_index])
                total_loss += dev_loss.item() * (end_index - j)
            total_loss /= (dev_sample_cnt * 1.0)

            if not min_loss_dev or total_loss < min_loss_dev[0]:
                min_loss_dev.append(total_loss)
                model_hashmap[total_loss] = '../Model/1229_wide_deep_text/epoch_%s.pth' % (epoch)
                torch.save(wide_deep_model.state_dict(), model_hashmap[total_loss])
                min_loss_dev.sort()
                if len(min_loss_dev) > 5:
                    os.remove(model_hashmap[min_loss_dev[-1]])
                    min_loss_dev.pop(-1)
            print('loss: %s on dev epoch %s\n' % (total_loss, epoch))


def train_main_no_wide_batch():
    # train and test data
    train_x, train_y = pickle.load(open('../WeiboData/train.plk', 'rb'))
    test_x, test_y = pickle.load(open('../WeiboData/test.plk', 'rb'))
    word_embedding_matrix = np.load('../Data/word_embedding_matrix.npy')

    train_x_wide = torch.cat([x['wide'] for x in train_x], 0)
    train_x_deep_dense = torch.cat([x['deep_dense'] for x in train_x], 0)
    train_x_deep_text = [x['deep_text'][0] for x in train_x]

    test_x_wide = torch.cat([x['wide'] for x in test_x])
    test_x_deep_dense = torch.cat([x['deep_dense'] for x in test_x])
    test_x_deep_text = [x['deep_text'][0] for x in test_x]

    dev_sample_cnt = 200
    train_sample_cnt = len(train_x) - dev_sample_cnt

    dev_x_wide, dev_x_deep_dense, dev_x_deep_text, dev_y = train_x_wide[-dev_sample_cnt:], \
                                                           train_x_deep_dense[-dev_sample_cnt:], \
                                                           train_x_deep_text[-dev_sample_cnt:], \
                                                           train_y[-dev_sample_cnt:]

    train_x_wide, train_x_deep_dense, train_x_deep_text, train_y = train_x_wide[:-dev_sample_cnt], \
                                                                   train_x_deep_dense[:-dev_sample_cnt], \
                                                                   train_x_deep_text[:-dev_sample_cnt], \
                                                                   train_y[:-dev_sample_cnt]

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
    optimizer = torch.optim.Adam(wide_deep_model.parameters(), weight_decay=0.025)

    # batch size
    batch_size = 64

    # dev stage
    min_loss_dev = []
    model_hashmap = {}

    # Training loop
    for epoch in range(50):
        for i in range(0, train_sample_cnt, batch_size):
            end_index = min(i + batch_size, train_sample_cnt)
            cur_batch_deep_text = train_x_deep_text[i:end_index]
            cur_batch_lengths = [x.size()[0] for x in cur_batch_deep_text]
            cur_batch_deep_text_pad_sequence = torch.nn.utils.rnn.pad_sequence(cur_batch_deep_text, batch_first=True,
                                                                               padding_value=0)
            cur_batch_deep_text_packed_seq = pack_padded_sequence(cur_batch_deep_text_pad_sequence, cur_batch_lengths,
                                                                  batch_first=True, enforce_sorted=False)

            y_pred = wide_deep_model(train_x_wide[i:end_index], train_x_deep_dense[i:end_index],
                                     cur_batch_deep_text_packed_seq)

            # Compute and print loss
            loss = criterion(y_pred, train_y[i:end_index])
            print('epoch:%s batch:%s loss:%s' % (epoch, i // batch_size, loss.item()))

            # Zero gradients,perform a backward pass,and update the weights
            optimizer.zero_grad()
            loss.backward()
            optimizer.step()

        # dev step
        total_loss = 0
        with torch.no_grad():
            for j in range(0, dev_sample_cnt, batch_size):
                end_index = min(j + batch_size, dev_sample_cnt)
                # text
                dev_batch_deep_text = dev_x_deep_text[j:end_index]
                dev_batch_lengths = [x.size()[0] for x in dev_batch_deep_text]
                dev_batch_deep_text_pad_sequence = torch.nn.utils.rnn.pad_sequence(dev_batch_deep_text,
                                                                                   batch_first=True,
                                                                                   padding_value=0)
                dev_batch_deep_text_packed_seq = pack_padded_sequence(dev_batch_deep_text_pad_sequence,
                                                                      dev_batch_lengths,
                                                                      batch_first=True, enforce_sorted=False)

                # calculate
                y_dev_pred = wide_deep_model(dev_x_wide[j:end_index], dev_x_deep_dense[j:end_index],
                                             dev_batch_deep_text_packed_seq)
                dev_loss = criterion(y_dev_pred, dev_y[j:end_index])
                total_loss += dev_loss.item() * (end_index - j)
            total_loss /= (dev_sample_cnt * 1.0)

            if not min_loss_dev or total_loss < min_loss_dev[0]:
                min_loss_dev.append(total_loss)
                model_hashmap[total_loss] = '../Model/deep_text_batch/epoch_%s.pth' % (epoch)
                torch.save(wide_deep_model.state_dict(), model_hashmap[total_loss])
                min_loss_dev.sort()
                if len(min_loss_dev) > 5:
                    os.remove(model_hashmap[min_loss_dev[-1]])
                    min_loss_dev.pop(-1)
            print('loss: %s on dev epoch %s\n' % (total_loss, epoch))


def train_main_batch_0108():
    # train and test data
    train_x, train_y = pickle.load(open('../WeiboData/train_0108.plk', 'rb'))
    test_x, test_y = pickle.load(open('../WeiboData/test_0108.plk', 'rb'))
    word_embedding_matrix = np.load('../Data/word_embedding_matrix.npy')

    train_x_wide = torch.cat([x['wide'] for x in train_x], 0)
    train_x_deep_dense = torch.cat([x['deep_dense'] for x in train_x], 0)
    train_x_deep_text = [x['deep_text'][0] for x in train_x]

    test_x_wide = torch.cat([x['wide'] for x in test_x])
    test_x_deep_dense = torch.cat([x['deep_dense'] for x in test_x])
    test_x_deep_text = [x['deep_text'][0] for x in test_x]

    dev_sample_cnt = 1280
    train_sample_cnt = len(train_x) - dev_sample_cnt

    dev_x_wide, dev_x_deep_dense, dev_x_deep_text, dev_y = train_x_wide[-dev_sample_cnt:], \
                                                           train_x_deep_dense[-dev_sample_cnt:], \
                                                           train_x_deep_text[-dev_sample_cnt:], \
                                                           train_y[-dev_sample_cnt:]

    train_x_wide, train_x_deep_dense, train_x_deep_text, train_y = train_x_wide[:-dev_sample_cnt], \
                                                                   train_x_deep_dense[:-dev_sample_cnt], \
                                                                   train_x_deep_text[:-dev_sample_cnt], \
                                                                   train_y[:-dev_sample_cnt]

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
    optimizer = torch.optim.Adam(wide_deep_model.parameters(), weight_decay=0.025,lr=0.005)

    # batch size
    batch_size = 128

    # dev stage
    min_loss_dev = []
    model_hashmap = {}

    # Training loop
    for epoch in range(15):
        batch_cnt = train_sample_cnt // batch_size
        for i in range(0, train_sample_cnt, batch_size):
            end_index = min(i + batch_size, train_sample_cnt)
            cur_batch_deep_text = train_x_deep_text[i:end_index]
            cur_batch_lengths = [x.size()[0] for x in cur_batch_deep_text]
            cur_batch_deep_text_pad_sequence = torch.nn.utils.rnn.pad_sequence(cur_batch_deep_text, batch_first=True,
                                                                               padding_value=0)
            cur_batch_deep_text_packed_seq = pack_padded_sequence(cur_batch_deep_text_pad_sequence, cur_batch_lengths,
                                                                  batch_first=True, enforce_sorted=False)

            y_pred = wide_deep_model(train_x_wide[i:end_index], train_x_deep_dense[i:end_index],
                                     cur_batch_deep_text_packed_seq)

            # Compute and print loss
            loss = criterion(y_pred, train_y[i:end_index])
            print('epoch:%s batch:%s loss:%s' % (epoch, i // batch_size, loss.item()))

            # Zero gradients,perform a backward pass,and update the weights
            optimizer.zero_grad()
            loss.backward()
            optimizer.step()

        # dev step
        total_loss = 0
        with torch.no_grad():
            for j in range(0, dev_sample_cnt, batch_size):
                end_index = min(j + batch_size, dev_sample_cnt)
                # text
                dev_batch_deep_text = dev_x_deep_text[j:end_index]
                dev_batch_lengths = [x.size()[0] for x in dev_batch_deep_text]
                dev_batch_deep_text_pad_sequence = torch.nn.utils.rnn.pad_sequence(dev_batch_deep_text,
                                                                                   batch_first=True,
                                                                                   padding_value=0)
                dev_batch_deep_text_packed_seq = pack_padded_sequence(dev_batch_deep_text_pad_sequence,
                                                                      dev_batch_lengths,
                                                                      batch_first=True, enforce_sorted=False)

                # calculate
                y_dev_pred = wide_deep_model(dev_x_wide[j:end_index], dev_x_deep_dense[j:end_index],
                                             dev_batch_deep_text_packed_seq)
                dev_loss = criterion(y_dev_pred, dev_y[j:end_index])
                total_loss += dev_loss.item() * (end_index - j)
            total_loss /= (dev_sample_cnt * 1.0)

            if not min_loss_dev or total_loss < min_loss_dev[0]:
                min_loss_dev.append(total_loss)
                model_hashmap[total_loss] = '../Model/0108_wide_deep_text/epoch_%s.pth' % (epoch)
                torch.save(wide_deep_model.state_dict(), model_hashmap[total_loss])
                min_loss_dev.sort()
                if len(min_loss_dev) > 3:
                    os.remove(model_hashmap[min_loss_dev[-1]])
                    min_loss_dev.pop(-1)
            print('loss: %s on dev epoch %s\n' % (total_loss, epoch))


if __name__ == '__main__':
    train_main_batch_0108()
