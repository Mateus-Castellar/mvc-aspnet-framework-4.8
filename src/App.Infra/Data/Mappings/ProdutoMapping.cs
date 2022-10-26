using App.Business.Models.Produtos;
using System.Data.Entity.ModelConfiguration;

namespace App.Infra.Data.Mappings
{
    public class ProdutoMapping : EntityTypeConfiguration<Produto>
    {
        public ProdutoMapping()
        {
            HasKey(lbda => lbda.Id);

            Property(lbda => lbda.Nome)
                .IsRequired()
                .HasMaxLength(200);

            Property(lbda => lbda.Descricao)
                .IsRequired()
                .HasMaxLength(1000);

            Property(lbda => lbda.Imagem)
                .IsRequired()
                .HasMaxLength(100);

            HasRequired(produto => produto.Fornecedor)
                .WithMany(fornecedor => fornecedor.Produtos)
                .HasForeignKey(Produto => Produto.FornecedorId);

            ToTable("Produtos");
        }
    }
}