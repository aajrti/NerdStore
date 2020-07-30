﻿using AutoMapper;

using NerdStore.Catalogo.Application.ViewModels;
using NerdStore.Catalogo.Domain;
using NerdStore.Core.DomainObjects;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;
        private readonly IMapper _mapper;


        public ProdutoAppService(IProdutoRepository produtoRepository, 
                                 IMapper mapper, 
                                 IEstoqueService estoqueService)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _estoqueService = estoqueService;
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterProdutosPorCategoria(int codigo)
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosPorCategoria(codigo);
        }

        public async Task<ProdutoViewModel> ObterProdutoPorId(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoPorId(id));
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos());
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterCategorias()
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterCategorias());
        }
                                                             
        public async Task AdicionarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);
            _produtoRepository.Adicionar(produto);
            await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task AtualizarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);
            _produtoRepository.Atualizar(produto);
            await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<ProdutoViewModel> DebitarEstoque(Guid id, int quantidade)
        {
            if (!_estoqueService.DebitarEstoque(id,quantidade).Result)
            {
                throw new DomainException(message: "Falha ao debitar estoque");
            }

            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoPorId(id));
        }
                 
        public async Task<ProdutoViewModel> ReporEstoque(Guid id, int quantidade)
        {
            if (!_estoqueService.ReporEstoque(id, quantidade).Result)
            {
                throw new DomainException(message: "Falha ao repor estoque");
            }

            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoPorId(id));
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
            _estoqueService?.Dispose();
        }
    }
}