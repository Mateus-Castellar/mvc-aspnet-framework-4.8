using App.Business.Models.Fornecedores;
using System;
using System.Threading.Tasks;

namespace App.Infra.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {

        public EnderecoRepository(MvcContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            //return await Db.Enderecos.AsNoTracking()
            //    .FirstOrDefaultAsync(endereco => endereco.Fornecedor.Id == fornecedorId);

            //PK e FK são a mesma coluna
            //return await Db.Enderecos.AsNoTracking()
            //    .FirstOrDefaultAsync(endereco => endereco.Id == fornecedorId);

            return await ObterPorId(fornecedorId);
        }
    }
}
