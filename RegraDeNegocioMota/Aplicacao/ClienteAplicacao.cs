using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Repositorio;
using System.Data;
using System.Data.SqlClient;

namespace Aplicacao
{
    public class ClienteAplicacao
    {
      
        public void Inserir(Cliente cliente)
        {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " INSERT INTO CLIENTE(Nome, Sobrenome, Email, DataCadastro, Ativo) ";
                cmd.CommandText += " VALUES(@Nome, @Sobrenome, @Email, @DataCadastro, @Ativo) ";
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", cliente.Sobrenome);
                cmd.Parameters.AddWithValue("@DataCadastro", cliente.DataCadastro);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@Ativo", cliente.Ativo);

                Contexto.ExecutaComando(cmd);
        }

        public void Alterar(Cliente cliente)
        {
          
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " UPDATE CLIENTE SET Nome=@Nome, Sobrenome=@Sobrenome, Email=@Email, ";
                cmd.CommandText += " DataCadastro=@DataCadastro, Ativo=@Ativo WHERE IdCliente=@IdCliente ";
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", cliente.Sobrenome);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@DataCadastro", cliente.DataCadastro);
                cmd.Parameters.AddWithValue("@Ativo", cliente.Ativo);
                cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);

                Contexto.ExecutaComando(cmd);
        }

        public void Excluir(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " DELETE FROM CLIENTE WHERE IdCliente = @IdCliente ";
            cmd.Parameters.AddWithValue("@IdCliente", id);

            Contexto.ExecutaComando(cmd);
        }

        public Cliente ListarPorId(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " SELECT * FROM CLIENTE WHERE IdCliente = @IdCliente ";
            cmd.Parameters.AddWithValue("@IdCliente", id);

            SqlDataReader reader = Contexto.ExecutaComandoComRetorno(cmd);

            Cliente cliente = new Cliente();
            if (reader.HasRows)
            {
                reader.Read();

                cliente.IdCliente = int.Parse(reader["IdCliente"].ToString());
                cliente.Nome = reader["Nome"].ToString();
                cliente.Sobrenome = reader["Sobrenome"].ToString();
                cliente.Email = reader["Email"].ToString();
                cliente.DataCadastro = DateTime.Parse(reader["DataCadastro"].ToString());
                cliente.Ativo = Boolean.Parse(reader["Ativo"].ToString());
            }
            else
            {
                cliente = null;
            }
            reader.Close();
            return cliente;
        }

        public List<Cliente> ListarTodos()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " SELECT * FROM CLIENTE ";
           
            SqlDataReader reader = Contexto.ExecutaComandoComRetorno(cmd);
            reader.Read();
            var clientes = new List<Cliente>();
          
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = int.Parse(reader["IdCliente"].ToString());
                    cliente.Nome = reader["Nome"].ToString();
                    cliente.Sobrenome = reader["Sobrenome"].ToString();
                    cliente.Email = reader["Email"].ToString();
                    cliente.DataCadastro = DateTime.Parse(reader["DataCadastro"].ToString());
                    cliente.Ativo = Boolean.Parse(reader["Ativo"].ToString());
                    clientes.Add(cliente);
                }  
            }
            else
            {
                clientes = null;
            }
            reader.Close();
            return clientes;
        }

        public List<Cliente> BuscaNome(string nome)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " SELECT * FROM CLIENTE WHERE Nome like @Nome ";
            cmd.Parameters.AddWithValue("@Nome", "%" + nome + "%");

            SqlDataReader reader = Contexto.ExecutaComandoComRetorno(cmd);
            var clientes = new List<Cliente>();
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Cliente cliente = new Cliente();//só add qd é uma lista
                    cliente.IdCliente = int.Parse(reader["IdCliente"].ToString());
                    cliente.Nome = reader["Nome"].ToString();
                    cliente.Sobrenome = reader["Sobrenome"].ToString();
                    cliente.Email = reader["Email"].ToString();
                    cliente.DataCadastro = DateTime.Parse(reader["DataCadastro"].ToString());
                    cliente.Ativo = Boolean.Parse(reader["Ativo"].ToString());

                    clientes.Add(cliente);
                }   
            }
            else
            {
                clientes = null;
            }
            reader.Close();
            return clientes;
        }

        public List<Cliente> Contador()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " SELECT DataCadastro, COUNT(*) as Total FROM CLIENTE GROUP BY DataCadastro ";
            // SELECT CONVERT(varchar(10),DataCadastro,103), COUNT(*) as Total FROM CLIENTE GROUP BY DataCadastro

            SqlDataReader reader = Contexto.ExecutaComandoComRetorno(cmd);
            var clientes = new List<Cliente>();

            if (reader.HasRows)//tem q passar os itens deles, só ñ tem parâmetro
            {
                Cliente cliente = new Cliente();
                while (reader.Read() && cliente.IdCliente > 0)
                {
                   
                    cliente.IdCliente = int.Parse(reader["IdCliente"].ToString());
                    cliente.Nome = reader["Nome"].ToString();
                    cliente.Sobrenome = reader["Sobrenome"].ToString();
                    cliente.Email = reader["Email"].ToString();
                    cliente.DataCadastro = DateTime.Parse(reader["DataCadastro"].ToString());
                    cliente.Ativo = Boolean.Parse(reader["Ativo"].ToString());
                }   
            }
            else
            {
                reader.Read();
                Cliente cliente = new Cliente();
                cliente.IdCliente = int.Parse(reader["IdCliente"].ToString());
                cliente.Nome = reader["Nome"].ToString();
                cliente.Sobrenome = reader["Sobrenome"].ToString();
                cliente.Email = reader["Email"].ToString();
                cliente.DataCadastro = DateTime.Parse(reader["DataCadastro"].ToString());
                cliente.Ativo = Boolean.Parse(reader["Ativo"].ToString());
            }
            //reader.Close();
            return clientes;
        }
    }
}
