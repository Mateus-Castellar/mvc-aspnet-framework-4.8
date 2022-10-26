using App.Business.Models.Fornecedores;
using System.Data.Entity.ModelConfiguration;

namespace App.Infra.Data.Mappings
{
    public class EnderecoMapping : EntityTypeConfiguration<Endereco>
    {
        public EnderecoMapping()
        {
            HasKey(lbda => lbda.Id);

            Property(lbda => lbda.Logradouro)
                //.HasColumnName("rua")
                .IsRequired()
                .HasMaxLength(200);

            Property(lbda => lbda.Numero)
                .IsRequired()
                .HasMaxLength(50);

            Property(lbda => lbda.Cep)
                .IsRequired()
                .HasMaxLength(8)
                .IsFixedLength();

            Property(lbda => lbda.Complemento)
                .HasMaxLength(250);

            Property(lbda => lbda.Cidade)
                .IsRequired()
                .HasMaxLength(10);

            Property(lbda => lbda.Estado)
                .IsRequired()
                .HasMaxLength(100);

            ToTable("Enderecos");

        }
    }
}