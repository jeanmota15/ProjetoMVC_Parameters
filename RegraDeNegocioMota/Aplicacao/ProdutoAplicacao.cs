using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dominio;
using Repositorio;

namespace Aplicacao
{
    public class ProdutoAplicacao
    {
        public void Inserir(Produto produto)
        {
            
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " INSERT INTO PRODUTO(NomeProduto, Valor, Disponivel, IdCliente) ";
                cmd.CommandText += " VALUES(@NomeProduto, @Valor, @Disponivel, @IdCliente) ";
                cmd.Parameters.AddWithValue("@NomeProduto", produto.NomeProduto);
                cmd.Parameters.AddWithValue("@Valor", produto.Valor);
                cmd.Parameters.AddWithValue("@Disponivel", produto.Disponivel);
                cmd.Parameters.AddWithValue("@IdCliente", produto.IdCliente);

                Contexto.ExecutaComando(cmd);       
        }

        public void Alterar(Produto produto)
        {
          
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " UPDATE PRODUTO SET NomeProduto = @NomeProduto, Valor = @Valor, ";
                cmd.CommandText += " Disponivel = @Disponivel, IdCliente = @IdCliente WHERE IdProduto = @IdProduto ";
                cmd.Parameters.AddWithValue("@NomeProduto", produto.NomeProduto);
                cmd.Parameters.AddWithValue("@Valor", produto.Valor);
                cmd.Parameters.AddWithValue("@Disponivel", produto.Disponivel);
                cmd.Parameters.AddWithValue("@IdCliente", produto.IdCliente);
                cmd.Parameters.AddWithValue("@IdProduto", produto.IdProduto);

                Contexto.ExecutaComando(cmd);
        }

        public void Excluir(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " DELETE FROM PRODUTO WHERE IdProduto = @IdProduto ";
            cmd.Parameters.AddWithValue("@IdProduto", id);

            Contexto.ExecutaComando(cmd);
        }

        public Produto ListarPorId(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " SELECT * FROM PRODUTO AS P ";
            cmd.CommandText += " LEFT JOIN CLIENTE AS C ON(P.IdCliente = C.IdCliente) WHERE IdProduto = @IdProduto ";
            cmd.Parameters.AddWithValue("@IdProduto", id);

            SqlDataReader reader = Contexto.ExecutaComandoComRetorno(cmd);

            Produto produto = new Produto();
            //Cliente cliente = new Cliente();
            ClienteAplicacao aplicacao = new ClienteAplicacao();

            if (reader.HasRows)
            {
                reader.Read();
                produto.IdProduto = int.Parse(reader["IdProduto"].ToString());
                produto.NomeProduto = reader["NomeProduto"].ToString();
                produto.Valor = Decimal.Parse(reader["Valor"].ToString());
                produto.Disponivel = bool.Parse(reader["Disponivel"].ToString());
                produto.IdCliente = int.Parse(reader["IdCliente"].ToString());
                //produto.Cliente = aplicacao.ListarPorId(id);
                //produto.Cliente = aplicacao.ListarPorId((int)reader["IdCliente"]);

                produto.Cliente = new Cliente();
                //produto.Cliente.IdCliente = int.Parse(reader["IdCliente"].ToString());
                produto.Cliente.Nome = reader["Nome"].ToString();
                produto.Cliente.Sobrenome = reader["Sobrenome"].ToString();
                produto.Cliente.Email = reader["Email"].ToString();
                produto.Cliente.DataCadastro = DateTime.Parse(reader["DataCadastro"].ToString());
                produto.Cliente.Ativo = bool.Parse(reader["Ativo"].ToString());
            }
            else
            {
                produto = null;
            }
            reader.Close();
            return produto;
        }

        public List<Produto> ListarTodos()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " SELECT * FROM PRODUTO AS P ";
            cmd.CommandText += " LEFT JOIN CLIENTE AS C ON(P.IdCliente = C.IdCliente) ";

            SqlDataReader reader = Contexto.ExecutaComandoComRetorno(cmd);
            var produtos = new List<Produto>();
            ClienteAplicacao aplicacao = new ClienteAplicacao();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Produto produto = new Produto();
                    produto.IdProduto = int.Parse(reader["IdProduto"].ToString());
                    produto.NomeProduto = reader["NomeProduto"].ToString();
                    produto.Valor = Decimal.Parse(reader["Valor"].ToString());
                    produto.Disponivel = bool.Parse(reader["Disponivel"].ToString());
                    produto.IdCliente = Int32.Parse(reader["IdCliente"].ToString());
                    //produto.Cliente = aplicacao.ListarTodos();
                    //produto.Cliente = aplicacao.ListarPorId((int)reader["IdCliente"]);

                    produto.Cliente = new Cliente();
                    //produto.Cliente.IdCliente = int.Parse(reader["IdCliente"].ToString());
                    produto.Cliente.Nome = reader["Nome"].ToString();
                    produto.Cliente.Sobrenome = reader["Sobrenome"].ToString();
                    produto.Cliente.Email = reader["Email"].ToString();
                    produto.Cliente.DataCadastro = DateTime.Parse(reader["DataCadastro"].ToString());
                    produto.Cliente.Ativo = bool.Parse(reader["Ativo"].ToString());

                    produtos.Add(produto);
                }
            }
            else
            {
                produtos = null;
            }
             reader.Close();
            return produtos;
        }

        public List<Produto> PesquisarPorNome(string nome)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " SELECT * FROM PRODUTO AS P ";
            cmd.CommandText += " LEFT JOIN CLIENTE AS C ON(P.IdCliente = C.IdCliente) WHERE C.Nome LIKE @NOME ";

            cmd.Parameters.AddWithValue("@Nome", "%" + nome + "%");

            SqlDataReader reader = Contexto.ExecutaComandoComRetorno(cmd);
            var produtos = new List<Produto>();
            ClienteAplicacao aplicacao = new ClienteAplicacao();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Produto produto = new Produto();
                    produto.IdProduto = int.Parse(reader["IdProduto"].ToString());
                    produto.NomeProduto = reader["NomeProduto"].ToString();
                    produto.Valor = Decimal.Parse(reader["Valor"].ToString());
                    produto.Disponivel = bool.Parse(reader["Disponivel"].ToString());
                    produto.IdCliente = Int32.Parse(reader["IdCliente"].ToString());
                    //produto.Cliente = aplicacao.ListarTodos();
                    //produto.Cliente = aplicacao.ListarPorId((int)reader["IdCliente"]);

                    produto.Cliente = new Cliente();
                    //produto.Cliente.IdCliente = int.Parse(reader["IdCliente"].ToString());
                    produto.Cliente.Nome = reader["Nome"].ToString();
                    produto.Cliente.Sobrenome = reader["Sobrenome"].ToString();
                    produto.Cliente.Email = reader["Email"].ToString();
                    produto.Cliente.DataCadastro = DateTime.Parse(reader["DataCadastro"].ToString());
                    produto.Cliente.Ativo = bool.Parse(reader["Ativo"].ToString());

                    produtos.Add(produto);
                }
            }
            else
            {
                produtos = null;
            }
            //reader.Close();
            return produtos;
        }
    }
}
