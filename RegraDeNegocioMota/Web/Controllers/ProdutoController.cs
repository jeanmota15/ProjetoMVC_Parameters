using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Aplicacao;

namespace Web.Controllers
{
    public class ProdutoController : Controller
    {

        private ProdutoAplicacao aplicacao;
        public ClienteAplicacao appCliente;

        public ProdutoController()
        {
            aplicacao = new ProdutoAplicacao();
            appCliente = new ClienteAplicacao();
        }

        public ActionResult Index(string nome, string ordem, string pesquisa)
        {
            var lista = aplicacao.PesquisarPorNome(nome);
            ViewBag.Pesquisa = string.IsNullOrEmpty(ordem) ? "Nome_Desc" : "Nome";
            ViewBag.Data = ordem == "Date" ? "Data_Desc" : "Date";

            if (!string.IsNullOrEmpty(pesquisa))
            {
                lista = lista.Where(x => x.Cliente.Nome.ToUpper().Contains(pesquisa.ToUpper()) ||
                    x.Cliente.Sobrenome.ToUpper().Contains(pesquisa.ToUpper())).ToList();
            }

            switch (ordem)
            {
                case "Nome_Desc":
                    lista = lista.OrderByDescending(x => x.Cliente.Nome).ToList();
                    break;
                case "Nome":
                    lista = lista.OrderBy(x => x.Cliente.Nome).ToList();
                    break;
                case "Data_Desc":
                    lista = lista.OrderByDescending(x => x.Cliente.DataCadastro).ToList();
                    break;
                case "Date":
                    lista = lista.OrderBy(x => x.Cliente.DataCadastro).ToList();
                    break;
                default:
                    lista = lista.OrderBy(x => x.Cliente.Nome).ToList();
                    break;
            }


            return View(lista);
        }

        public ActionResult Index2(string nome)
        {
            var lista = aplicacao.ListarTodos();

            ViewBag.Nome = (from p in lista
                            select p.Cliente.Nome).Distinct();

            var model = from p in lista
                        orderby p.Cliente.Nome
                        where p.Cliente.Nome == nome
                        select p;
            return View(model);
        }

        // GET: Produto/Details/5
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

        // GET: Produto/Create
        public ActionResult Cadastrar()
        {
            var lista = aplicacao.ListarTodos();
            ViewBag.Cliente = new SelectList(appCliente.ListarTodos(), "IdCliente", "Nome");
            //ViewBag.Cliente2 = (from p in lista
            //                    select p.Cliente.Nome).Distinct();
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Produto produto)
        {
            var lista = aplicacao.ListarTodos();
            if (!ModelState.IsValid)
            {
                ViewBag.Cliente = new SelectList(appCliente.ListarTodos(), "IdCliente", "Nome");
                //ViewBag.Cliente2 = (from p in lista
                //                    select p.Cliente.Nome).Distinct();
                aplicacao.Inserir(produto);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Cliente = new SelectList(appCliente.ListarTodos(), "IdCliente", "Nome");
                //ViewBag.Cliente2 = (from p in lista
                //                    select p.Cliente.Nome).Distinct();
                return View(produto);
            }
        }

        // GET: Produto/Edit/5
        public ActionResult Editar(int id)
        {
            var lista2 = aplicacao.ListarPorId(id);
            var cliente = appCliente.ListarPorId(id);
            var lista = aplicacao.ListarTodos();

            if (lista == null)
            {
                return HttpNotFound();
            }
            else
            {
                //Session["Cliente"] = id;
                ViewBag.Cliente = new SelectList(appCliente.ListarTodos(), "IdCliente", "Nome");
                //ViewBag.Cliente2 = (from p in lista
                //                    select p.Cliente.Nome).Distinct();
                return View(lista2);
            }
        }

        // POST: Produto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                aplicacao.Alterar(produto);
                return RedirectToAction("Index");
            }
            else
            {
                //ViewBag.Cliente = new SelectList(appCliente.ListarTodos(), "IdCliente", "Nome");
                return View(produto);
            }
        }

        // GET: Produto/Delete/5
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

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Excluir")]
        public ActionResult ExcluirComando(int id)
        {
            aplicacao.Excluir(id);
            return RedirectToAction("Index");
          
        }
    }
}
