using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Repositorio
{
    public class Contexto
    {
        //private SqlConnection minhaConexao;

        public static SqlConnection Conexao()
        {
            string minhaConexao = ConfigurationManager.ConnectionStrings["RegraDeNegocio"].ConnectionString;
            SqlConnection con = new SqlConnection(minhaConexao);
            con.Open();
            return con;
        }

        public static void ExecutaComando(SqlCommand cmd)
        {
            SqlConnection con = Conexao();
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static SqlDataReader ExecutaComandoComRetorno(SqlCommand cmd)
        {
            SqlConnection con = Conexao();
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }
    }
}
