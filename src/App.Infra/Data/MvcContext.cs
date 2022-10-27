using App.Business.Models.Fornecedores;
using App.Business.Models.Produtos;
using App.Infra.Data.Mappings;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;

namespace App.Infra.Data
{
    public class MvcContext : DbContext
    {
        public MvcContext() : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p
                    .HasColumnType("varchar")
                    .HasMaxLength(100));

            modelBuilder.Configurations.Add(new FornecedorMapping());
            modelBuilder.Configurations.Add(new EnderecoMapping());
            modelBuilder.Configurations.Add(new ProdutoMapping());

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry =>
                entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;

                //não altera a data de cadastro
                if (entry.State == EntityState.Modified)
                    entry.Property("DataCadastro").IsModified = false;
            }

            return base.SaveChangesAsync();
        }
    }
}
