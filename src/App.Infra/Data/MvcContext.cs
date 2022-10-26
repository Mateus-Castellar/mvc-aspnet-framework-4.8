using App.Business.Models.Fornecedores;
using App.Business.Models.Produtos;
using App.Infra.Data.Mappings;
using System.Data.Entity;

namespace App.Infra.Data
{
    public class MvcContext : DbContext
    {
        public MvcContext() : base("DefaultConnection")
        { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FornecedorMapping());
            modelBuilder.Configurations.Add(new EnderecoMapping());
            modelBuilder.Configurations.Add(new ProdutoMapping());
        }
    }
}
