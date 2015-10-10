using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Dominio.ViewModel;
using Aplicacao;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private ClienteAplicacao aplicacao;

        public HomeController()
        {
            aplicacao = new ClienteAplicacao();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            var lista = aplicacao.ListarTodos();
            var cliente = from p in lista
                          group p by p.DataCadastro into grupo
                          select new ClienteEstatistica()
                          {
                              Data = grupo.Key,
                              Contador = grupo.Count()
                          };
            return View(cliente);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}