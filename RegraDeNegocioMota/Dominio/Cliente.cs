using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        public List<Produto> Produtos { get; set; }

        public Cliente()
        {
            Produtos = new List<Produto>();
            //this.IdCliente = IdCliente;
        }
    }
}
