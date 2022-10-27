using App.Business.Models.Fornecedores;
using App.Business.Models.Produtos;
using App.Mvc.ViewModels;
using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace App.Mvc.App_Start
{
    public class AutomapperConfig
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            //executa no momento que a aplicação sobe (reflection)
            var profiles = Assembly.GetExecutingAssembly().GetTypes()
                .Where(lbda => typeof(Profile).IsAssignableFrom(lbda));

            return new MapperConfiguration(config =>
            {
                foreach (var profile in profiles)
                    config.AddProfile(Activator.CreateInstance(profile) as Profile);
            });
        }
    }

    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}