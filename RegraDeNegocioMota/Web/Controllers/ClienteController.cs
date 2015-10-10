using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Aplicacao;

namespace Web.Controllers
{
    public class ClienteController : Controller
    {
        private ClienteAplicacao aplicacao;

        public ClienteController()
        {
            aplicacao = new ClienteAplicacao();
        }

        public ActionResult Index(string nome)
        {
            var lista = aplicacao.BuscaNome(nome);
            //ViewBag.Nome = new SelectList(aplicacao.ListarTodos(), "IdCliente", "Nome");

            return View(lista);
        }

        //public ActionResult Index2()
        //{
        //    var lista = aplicacao.Contador();
        //    return View(lista);
        //}

        // GET: Cliente/Details/5
        public ActionResult Detalhes(int id)
        {
            var lista = aplicacao.ListarPorId(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(lista);
            }
        }

        // GET: Cliente/Create
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                aplicacao.Inserir(cliente);
                return RedirectToAction("Index");
            }
            else
            {
                return View(cliente);
            }
        }

        // GET: Cliente/Edit/5
        public ActionResult Editar(int id)
        {
            var lista = aplicacao.ListarPorId(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(lista);
            }     
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                aplicacao.Alterar(cliente);
                return RedirectToAction("Index");
            }
            else
            {
                return View(cliente);
            }
        }

       
        public ActionResult Excluir(int id)
        {
            var lista = aplicacao.ListarPorId(id);

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(lista);
            }
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Excluir")]
        public ActionResult ExcluirComando(int id)
        {
            aplicacao.Excluir(id);
            return RedirectToAction("Index");
        
        }
    }
}
