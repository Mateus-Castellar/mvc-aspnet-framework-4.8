using App.Business.Core.Notificacoes;
using App.Business.Models.Fornecedores;
using App.Business.Models.Fornecedores.Services;
using App.Business.Models.Produtos;
using App.Business.Models.Produtos.Services;
using App.Infra.Data;
using App.Infra.Data.Repository;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;

namespace App.Mvc.App_Start
{
    public class DependencyInjectionConfig
    {
        public static void RegisterDIContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        public static void InitializeContainer(Container container)
        {
            //lifestyle.Singleton => uma unica instancia por aplicacao
            //lifestyle.Transient => uma nova instancia para cada injecao
            //lifestyle.Scoped => uma unica instancia por request (funciona apenas para app web)

            container.Register<MvcContext>(Lifestyle.Scoped);
            container.Register<IProdutoRepository, ProdutoRepository>(Lifestyle.Scoped);
            container.Register<IProdutoService, ProdutoService>(Lifestyle.Scoped);
            container.Register<IFornecedorRepository, FornecedorRepository>(Lifestyle.Scoped);
            container.Register<IEnderecoRepository, EnderecoRepository>(Lifestyle.Scoped);
            container.Register<IFornecedorService, FornecedorService>(Lifestyle.Scoped);
            container.Register<INotificador, Notificador>(Lifestyle.Scoped);

            container.RegisterSingleton(() => AutomapperConfig.GetMapperConfiguration().CreateMapper(container.GetInstance));
        }
    }
}