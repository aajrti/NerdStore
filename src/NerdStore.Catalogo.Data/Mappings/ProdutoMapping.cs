using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NerdStore.Catalogo.Domain;

namespace NerdStore.Catalogo.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(keyExpression: c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(600)");

            builder.Property(c => c.Imagem)
                .IsRequired()
                .HasColumnType("varchar(250)");

            //  ownsOne => tranforma o objeto de valor em colunas na tabela de produto.
            builder.OwnsOne(navigationExpression: c => c.Dimensoes, buildAction: cm =>
              {
                  cm.Property(c => c.Altura)
                      .HasColumnName("Altura")
                      .HasColumnType("int");

                  cm.Property(c => c.Largura)
                      .HasColumnName("Largura")
                      .HasColumnType("int");

                  cm.Property(c => c.Profundidade)
                      .HasColumnName("Profundidade")
                      .HasColumnType("int");
              });

            builder.ToTable("Produtos");
        }
    }
}
