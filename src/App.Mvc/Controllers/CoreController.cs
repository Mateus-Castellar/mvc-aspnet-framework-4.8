using App.Business.Core.Notificacoes;
using System.Web.Mvc;

namespace App.Mvc.Controllers
{
    public class CoreController : Controller
    {
        private readonly INotificador _notificador;

        public CoreController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            if (_notificador.TemNotificacao() is false) return true;

            var notificacoes = _notificador.ObterNotificacoes();

            notificacoes.ForEach(lbda => ViewData.ModelState
                .AddModelError(string.Empty, lbda.Mensagem));

            return false;
        }
    }
}