using App.Business.Core.Notificacoes;
using App.Business.Core.Services;
using App.Business.Models.Fornecedores.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Business.Models.Fornecedores.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository,
            IEnderecoRepository enderecoRepository, INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Fornecedor fornecedor)
        {
            //ajuste para tratar limitações do ef6
            fornecedor.Endereco.Id = fornecedor.Id;
            fornecedor.Endereco.Fornecedor = fornecedor;

            if (ExecutarValidacao(new FornecedorValidation(), fornecedor) is false
                || ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco) is false) return;

            if (await FornecedorExistente(fornecedor)) return;

            await _fornecedorRepository.Adicionar(fornecedor);
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (ExecutarValidacao(new FornecedorValidation(), fornecedor) is false) return;

            if (await FornecedorExistente(fornecedor)) return;

            await _fornecedorRepository.Atualizar(fornecedor);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (ExecutarValidacao(new EnderecoValidation(), endereco) is false) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterFornecedorProdutosEndereco(id);

            if (fornecedor.Produtos.Any())
            {
                Notificar("O fornecedor possui produtos cadastrados");
                return;
            }

            if (fornecedor.Endereco != null)
                await _enderecoRepository.Remover(fornecedor.Endereco.Id);

            await _fornecedorRepository.Remover(id);
        }

        private async Task<bool> FornecedorExistente(Fornecedor fornecedor)
        {
            var fornecedorAtual = await _fornecedorRepository
                .Buscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id);

            if (fornecedorAtual.Any() is false) return false;

            Notificar("Já existe um fornecedor com este documento informado");
            return true;
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}