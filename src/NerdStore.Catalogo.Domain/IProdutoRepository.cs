using NerdStore.Core.Data;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> ObterProdutoPorId(Guid id);
        Task<IEnumerable<Produto>> ObterTodos();
        Task<IEnumerable<Produto>> ObterProdutosPorCategoria(int codigo);
        Task<IEnumerable<Categoria>> ObterCategorias();

        void Adicionar(Produto produto);
        void Atualizar(Produto produto);

        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
    }
}
