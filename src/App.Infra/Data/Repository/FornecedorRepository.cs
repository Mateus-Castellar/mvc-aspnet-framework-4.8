using App.Business.Models.Fornecedores;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace App.Infra.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(fornecedor => fornecedor.Endereco)
                .FirstOrDefaultAsync(fornecedor => fornecedor.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
               .Include(fornecedor => fornecedor.Endereco)
               .Include(fornecedor => fornecedor.Produtos)
               .FirstOrDefaultAsync(fornecedor => fornecedor.Id == id);
        }

        public override async Task Remover(Guid id)
        {
            //pode ter uma flag "excluido" em nossa tabela e setar como true/false!

            var fornecedor = await ObterPorId(id);
            fornecedor.Ativo = false;

            await Atualizar(fornecedor);
        }
    }
}