using App.Business.Core.Models;
using App.Business.Models.Produtos;
using System.Collections.Generic;

namespace App.Business.Models.Fornecedores
{
    public class Fornecedor : Entity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoFornecedor TipoFornecedor { get; set; }
        public Endereco Endereco { get; set; }
        public bool Ativo { get; set; }

        //EfCore Relacionamento
        public ICollection<Produto> Produtos { get; set; }
    }
}
