# -*- coding: utf8 -*-
# __author__ == 'sunqi'

import torch
from typing import List, Dict
import numpy as np
from torch import Tensor
from torch.optim.optimizer import Optimizer
from typing import List, Any, Union, Dict, Callable, Optional, Tuple, Generator, Collection, Iterable
from torch.optim.lr_scheduler import _LRScheduler
import torch.nn.functional as F


class WideModel(torch.nn.Module):
    def __init__(self, wide_dim: int, output_dim: int = 1):
        super(WideModel, self).__init__()
        self.wide_linear = torch.nn.Linear(wide_dim, output_dim)

    def forward(self, X):
        out = self.wide_linear(X.float())
        return out


class DeepDenseModel(torch.nn.Module):
    def __init__(self, input_dim: int, hidden_layers: List[int], batchNorm=False, dropout: List[float] = None):
        super(DeepDenseModel, self).__init__()

        # Continuous

        # Add Layer
        def dense_layer(inp: int, out: int, p: float = 0., bn=False):
            layers = [torch.nn.Linear(inp, out), torch.nn.LeakyReLU(inplace=True)]
            if bn:
                layers.append(torch.nn.BatchNorm1d(out))
            layers.append(torch.nn.Dropout(p))
            return torch.nn.Sequential(*layers)

        # Dense Layer 0316
        self.deep_dense = torch.nn.Sequential()
        hidden_layers = [input_dim] + hidden_layers
        if not dropout:
            dropout = [0.] * len(hidden_layers)
        for i in range(1, len(hidden_layers)):
            self.deep_dense.add_module('dense_layer_{}'.format(i - 1),
                                       dense_layer(hidden_layers[i - 1], hidden_layers[i], dropout[i - 1], batchNorm))

        # the output_dim attribute will be used as input_dim when "merging" the models
        self.output_dim = hidden_layers[-1]

    def forward(self, X):
        return self.deep_dense(X)


class DeepTextModel(torch.nn.Module):
    def __init__(self, n_layers: int, hidden_dim: int, embed_dim: int, vocab_size: int, rnn_dropout: float = 0.,
                 padding_idx: int = 1, bidirectional=False, embedding_matrix: np.ndarray = None,
                 head_layers: [List[int]] = None,
                 head_dropout: [List[float]] = None,
                 head_batchnorm: [bool] = False):
        super(DeepTextModel, self).__init__()

        self.bidirectional = bidirectional
        self.embed_dim = embed_dim
        self.head_layers = head_layers

        # Add Layer
        def dense_layer(inp: int, out: int, p: float = 0., bn=False):
            layers = [torch.nn.Linear(inp, out), torch.nn.LeakyReLU(inplace=True)]
            if bn:
                layers.append(torch.nn.BatchNorm1d(out))
            layers.append(torch.nn.Dropout(p))
            return torch.nn.Sequential(*layers)

        # Pre-trained Embeddings
        if isinstance(embedding_matrix, np.ndarray):
            self.word_embed = torch.nn.Embedding(vocab_size, embedding_matrix.shape[1], padding_idx=padding_idx)
            self.word_embed.weight = torch.nn.Parameter(torch.Tensor(embedding_matrix))
            self.embed_dim = embedding_matrix.shape[1]
        # RNN
        self.rnn = torch.nn.LSTM(embed_dim,
                                 hidden_dim,
                                 num_layers=n_layers,
                                 bidirectional=bidirectional,
                                 dropout=rnn_dropout,
                                 batch_first=True)

        # the output_dim attribute will be used as input_dim when "merging" the models
        self.output_dim = hidden_dim * 2 if bidirectional else hidden_dim

        # FC Layer
        if self.head_layers is not None:
            assert self.head_layers[0] == self.output_dim, (
                "The hidden dimension from the stack or RNNs ({}) is not consistent with "
                "the expected input dimension ({}) of the fc-head".format(
                    self.output_dim, self.head_layers[0]))
            if not head_dropout: head_dropout = [0.] * len(head_layers)
            self.text_head_fc_layers = torch.nn.Sequential()
            for i in range(1, len(head_layers)):
                self.text_head_fc_layers.add_module('dense_layer_{}'.format(i - 1),
                                                    dense_layer(head_layers[i - 1], head_layers[i], head_dropout[i - 1],
                                                                head_batchnorm)
                                                    )
            self.output_dim = head_layers[-1]

    def forward(self, X):

        embed = self.word_embed(X.long())
        o, (h, c) = self.rnn(embed)
        if self.bidirectional:
            last_h = torch.cat((h[-2], h[-1]), dim=1)
        else:
            last_h = h[-1]
        if self.head_layers is not None:
            out = self.text_head_fc_layers(last_h)
            return out
        else:
            return last_h


class WideDeepModel(torch.nn.Module):

    def __init__(self, wide_model, deep_dense_model, output_dim: int = 2, deep_text_model=None, deep_head_layers=None,
                 head_layers_dims: List[int] = None,
                 head_layers_dropout: List[float] = None,
                 head_layers_batchnorm: bool = False
                 ):
        super(WideDeepModel, self).__init__()

        # Add Layer
        def dense_layer(inp: int, out: int, p: float = 0., bn=False):
            layers = [torch.nn.Linear(inp, out), torch.nn.LeakyReLU(inplace=True)]
            if bn:
                layers.append(torch.nn.BatchNorm1d(out))
            layers.append(torch.nn.Dropout(p))
            return torch.nn.Sequential(*layers)

        # Sigmoid
        self.sigmoid = torch.nn.functional.sigmoid

        # The main 5 components of the wide and deep assemble
        self.wide = wide_model
        self.deep_dense = deep_dense_model
        self.deep_text = deep_text_model
        self.deep_head = deep_head_layers

        if self.deep_head is None:
            if head_layers_dims is not None:
                input_dim = self.deep_dense.output_dim + self.deep_text.output_dim if self.deep_text else 0
                head_layers_dims = [input_dim] + head_layers_dims
                if not head_layers_dropout:
                    head_layers_dropout = [0.] * (len(head_layers_dims) - 1)
                self.deep_head = torch.nn.Sequential()
                for i in range(1, len(head_layers_dims)):
                    self.deep_head.add_module('head_layer_{}'.format(i - 1),
                                              dense_layer(head_layers_dims[i - 1], head_layers_dims[i],
                                                          head_layers_dropout[i - 1], head_layers_batchnorm))
                self.deep_head.add_module('head_out', torch.nn.Linear(head_layers_dims[-1], output_dim))
            else:
                self.deep_dense = torch.nn.Sequential(self.deep_dense,
                                                      torch.nn.Linear(self.deep_dense.output_dim, output_dim))
                if self.deep_text is not None:
                    self.deep_text = torch.nn.Sequential(self.deep_text,
                                                         torch.nn.Linear(self.deep_text.output_dim, output_dim))

    def forward(self, X: [Dict[str, Tensor]]) -> Tensor:

        # Wide output: direct connection to the output neuron(s)
        out = self.wide(X['wide'])

        # Deep output: either connected directly to the output neuron(s) or
        # passed through a head first
        if self.deep_head:
            deep_side = self.deepdense(X['deep_dense'])
            if self.deep_text is not None:
                deep_side = torch.cat([deep_side, self.deep_text(X['deep_text'])], axis=1)
            deep_side_out = self.deep_head(deep_side)
            return F.softmax(out.add_(deep_side_out), dim=1)
        else:
            out.add_(self.deep_dense(X['deep_dense']))
            if self.deep_text is not None:
                out.add_(self.deep_text(X['deep_text'])[0])
            return F.softmax(out, dim=1)


class DeepTextModel_batch(torch.nn.Module):
    def __init__(self, n_layers: int, hidden_dim: int, embed_dim: int, vocab_size: int, rnn_dropout: float = 0.,
                 padding_idx: int = 1, bidirectional=False, embedding_matrix: np.ndarray = None,
                 head_layers: [List[int]] = None,
                 head_dropout: [List[float]] = None,
                 head_batchnorm: [bool] = False):
        super(DeepTextModel_batch, self).__init__()

        self.bidirectional = bidirectional
        self.embed_dim = embed_dim
        self.head_layers = head_layers

        # Add Layer
        def dense_layer(inp: int, out: int, p: float = 0., bn=False):
            layers = [torch.nn.Linear(inp, out), torch.nn.LeakyReLU(inplace=True)]
            if bn:
                layers.append(torch.nn.BatchNorm1d(out))
            layers.append(torch.nn.Dropout(p))
            return torch.nn.Sequential(*layers)

        # Pre-trained Embeddings
        if isinstance(embedding_matrix, np.ndarray):
            self.word_embed = torch.nn.Embedding(vocab_size, embedding_matrix.shape[1], padding_idx=padding_idx)
            self.word_embed.weight = torch.nn.Parameter(torch.Tensor(embedding_matrix))
            self.embed_dim = embedding_matrix.shape[1]
        # RNN
        self.rnn = torch.nn.LSTM(embed_dim,
                                 hidden_dim,
                                 num_layers=n_layers,
                                 bidirectional=bidirectional,
                                 dropout=rnn_dropout,
                                 batch_first=True)

        # the output_dim attribute will be used as input_dim when "merging" the models
        self.output_dim = hidden_dim * 2 if bidirectional else hidden_dim

        # FC Layer
        if self.head_layers is not None:
            assert self.head_layers[0] == self.output_dim, (
                "The hidden dimension from the stack or RNNs ({}) is not consistent with "
                "the expected input dimension ({}) of the fc-head".format(
                    self.output_dim, self.head_layers[0]))
            if not head_dropout: head_dropout = [0.] * len(head_layers)
            self.text_head_fc_layers = torch.nn.Sequential()
            for i in range(1, len(head_layers)):
                self.text_head_fc_layers.add_module('dense_layer_{}'.format(i - 1),
                                                    dense_layer(head_layers[i - 1], head_layers[i], head_dropout[i - 1],
                                                                head_batchnorm)
                                                    )
            self.output_dim = head_layers[-1]

    def forward(self, X):

        # point wise
        def elementwise_apply(fn, *args):
            return torch.nn.utils.rnn.PackedSequence(
                fn(*[(arg.data if type(arg) == torch.nn.utils.rnn.PackedSequence else arg) for arg in args]),
                args[0].batch_sizes)

        embed = elementwise_apply(self.word_embed, X.long())

        o, (h, c) = self.rnn(embed)
        if self.bidirectional:
            last_h = torch.cat((h[-2], h[-1]), dim=1)
        else:
            last_h = h[-1]
        if self.head_layers is not None:
            out = self.text_head_fc_layers(last_h)
            return out
        else:
            return last_h


class WideDeepModel_batch(torch.nn.Module):

    def __init__(self, wide_model, deep_dense_model, output_dim: int = 2, deep_text_model=None, deep_head_layers=None,
                 head_layers_dims: List[int] = None,
                 head_layers_dropout: List[float] = None,
                 head_layers_batchnorm: bool = False
                 ):
        super(WideDeepModel_batch, self).__init__()

        # Add Layer
        def dense_layer(inp: int, out: int, p: float = 0., bn=False):
            layers = [torch.nn.Linear(inp, out), torch.nn.LeakyReLU(inplace=True)]
            if bn:
                layers.append(torch.nn.BatchNorm1d(out))
            layers.append(torch.nn.Dropout(p))
            return torch.nn.Sequential(*layers)

        # Sigmoid
        self.sigmoid = torch.nn.functional.sigmoid

        # The main 5 components of the wide and deep assemble
        self.wide = wide_model
        self.deep_dense = deep_dense_model
        self.deep_text = deep_text_model
        self.deep_head = deep_head_layers

        if self.deep_head is None:
            if head_layers_dims is not None:
                input_dim = self.deep_dense.output_dim + self.deep_text.output_dim if self.deep_text else 0
                head_layers_dims = [input_dim] + head_layers_dims
                if not head_layers_dropout:
                    head_layers_dropout = [0.] * (len(head_layers_dims) - 1)
                self.deep_head = torch.nn.Sequential()
                for i in range(1, len(head_layers_dims)):
                    self.deep_head.add_module('head_layer_{}'.format(i - 1),
                                              dense_layer(head_layers_dims[i - 1], head_layers_dims[i],
                                                          head_layers_dropout[i - 1], head_layers_batchnorm))
                self.deep_head.add_module('head_out', torch.nn.Linear(head_layers_dims[-1], output_dim))
            else:
                self.deep_dense = torch.nn.Sequential(self.deep_dense,
                                                      torch.nn.Linear(self.deep_dense.output_dim, output_dim))
                if self.deep_text is not None:
                    self.deep_text = torch.nn.Sequential(self.deep_text,
                                                         torch.nn.Linear(self.deep_text.output_dim, output_dim))

    def forward(self, X_wide: Tensor, X_deep_dense: Tensor, X_deep_text) -> Tensor:

        # Wide output: direct connection to the output neuron(s)
        out = self.wide(X_wide)

        # Deep output: either connected directly to the output neuron(s) or
        # passed through a head first
        if self.deep_head:
            deep_side = self.deep_dense(X_deep_dense)
            if self.deep_text is not None:
                deep_side = torch.cat([deep_side, self.deep_text(X_deep_text)], axis=1)
            deep_side_out = self.deep_head(deep_side)
            return F.softmax(out.add_(deep_side_out), dim=1)
        else:
            out.add_(self.deep_dense(X_deep_dense))
            if self.deep_text is not None:
                out.add_(self.deep_text(X_deep_text)[0])
            return F.softmax(out, dim=1)


class DeepModel_batch(torch.nn.Module):

    def __init__(self, wide_model, deep_dense_model, output_dim: int = 2, deep_text_model=None, deep_head_layers=None,
                 head_layers_dims: List[int] = None,
                 head_layers_dropout: List[float] = None,
                 head_layers_batchnorm: bool = False
                 ):
        super(DeepModel_batch, self).__init__()

        # Add Layer
        def dense_layer(inp: int, out: int, p: float = 0., bn=False):
            layers = [torch.nn.Linear(inp, out), torch.nn.LeakyReLU(inplace=True)]
            if bn:
                layers.append(torch.nn.BatchNorm1d(out))
            layers.append(torch.nn.Dropout(p))
            return torch.nn.Sequential(*layers)

        # Sigmoid
        self.sigmoid = torch.nn.functional.sigmoid

        # The main 5 components of the wide and deep assemble
        self.wide = wide_model
        self.deep_dense = deep_dense_model
        self.deep_text = deep_text_model
        self.deep_head = deep_head_layers

        if self.deep_head is None:
            if head_layers_dims is not None:
                input_dim = self.deep_dense.output_dim + self.deep_text.output_dim if self.deep_text else 0
                head_layers_dims = [input_dim] + head_layers_dims
                if not head_layers_dropout:
                    head_layers_dropout = [0.] * (len(head_layers_dims) - 1)
                self.deep_head = torch.nn.Sequential()
                for i in range(1, len(head_layers_dims)):
                    self.deep_head.add_module('head_layer_{}'.format(i - 1),
                                              dense_layer(head_layers_dims[i - 1], head_layers_dims[i],
                                                          head_layers_dropout[i - 1], head_layers_batchnorm))
                self.deep_head.add_module('head_out', torch.nn.Linear(head_layers_dims[-1], output_dim))
            else:
                self.deep_dense = torch.nn.Sequential(self.deep_dense,
                                                      torch.nn.Linear(self.deep_dense.output_dim, output_dim))
                if self.deep_text is not None:
                    self.deep_text = torch.nn.Sequential(self.deep_text,
                                                         torch.nn.Linear(self.deep_text.output_dim, output_dim))

    def forward(self, X_wide: Tensor, X_deep_dense: Tensor, X_deep_text) -> Tensor:

        # Wide output: direct connection to the output neuron(s)
        # out = self.wide(X_wide)

        # Deep output: either connected directly to the output neuron(s) or
        # passed through a head first
        if self.deep_head:
            deep_side = self.deep_dense(X_deep_dense)
            if self.deep_text is not None:
                deep_side = torch.cat([deep_side, self.deep_text(X_deep_text)], axis=1)
            deep_side_out = self.deep_head(deep_side)
            return F.softmax(deep_side_out, dim=1)
        else:
            out = self.deep_dense(X_deep_dense)
            if self.deep_text is not None:
                out.add_(self.deep_text(X_deep_text)[0])
            return F.softmax(out, dim=1)
