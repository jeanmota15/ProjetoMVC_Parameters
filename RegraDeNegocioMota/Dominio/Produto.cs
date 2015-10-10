using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Produto
    {
        public int IdProduto { get; set; }

        public string NomeProduto { get; set; }

        public decimal Valor { get; set; }

        public bool Disponivel { get; set; }

        public int IdCliente { get; set; }

        public Cliente Cliente { get; set; }

        //public Cliente Nome { get; set; }

        public Produto()
        {
            Cliente = new Cliente();
            //this.IdCliente = IdCliente;
        }
    }
}
