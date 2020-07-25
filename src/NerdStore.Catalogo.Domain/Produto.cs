using NerdStore.Core.DomainObjects;

using System;
using System.Drawing;

namespace NerdStore.Catalogo.Domain
{
    public class Produto : Entity, IAggregateRoot
    {
        public Produto(
            string nome, 
            string descricao, 
            bool ativo, 
            decimal valor, 
            Guid categoriaId,
            DateTime dataCadastro,
            string imagem)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            Ativo = ativo;
            CategoriaId = categoriaId;
            DataCadastro = dataCadastro;
            Imagem = imagem;
        }   

        public Guid CategoriaId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Imagem { get; private set; }
        public int QuantidadeEstoque { get; private set; }
        public int QuantidadeMinimaEstoque { get; private set; }
        public Categoria Categoria { get; private set; }


        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;
        public void AlterarCategoria(Categoria categoria)
        {
            Categoria = categoria;
            CategoriaId = categoria.Id;
        }

        public void AlterarDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public void DebitarEstoque(int quantidade)
        {
            if (quantidade < 0) quantidade *= -1;
            QuantidadeEstoque -= quantidade;
        }
        
        public void ReporEstoque(int quantidade)
        {
            QuantidadeEstoque += quantidade;
        }

        public bool PossuiEstoque(int quantidade)
        {
            return QuantidadeEstoque >= quantidade;
        }

        public bool QuantidadeMinimaAtingida(int quantidade)
        {
            return QuantidadeMinimaEstoque > quantidade;
        }

        public void Validar()
        {

        }
    }     
    
    public class Categoria : Entity
    {
        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;
        }
        public string Nome { get; private set; }
        public int Codigo { get; private set; }

        public override string ToString()
        {
            return $"{Nome} - {Codigo}"; 
        }
    }
}
