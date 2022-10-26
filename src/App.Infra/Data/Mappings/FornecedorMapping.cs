using App.Business.Models.Fornecedores;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace App.Infra.Data.Mappings
{
    public class FornecedorMapping : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorMapping()
        {
            HasKey(lbda => lbda.Id);

            Property(lbda => lbda.Nome)
                .IsRequired()
                .HasMaxLength(200);

            Property(lbda => lbda.Documento)
              .IsRequired()
              .HasMaxLength(14)
              .HasColumnAnnotation("IX_Documento",
              new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            HasRequired(lbda => lbda.Endereco)
                .WithRequiredPrincipal(endereco => endereco.Fornecedor);

            ToTable("Fornecedores");
        }
    }
}