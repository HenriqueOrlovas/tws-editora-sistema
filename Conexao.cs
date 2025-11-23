using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace SeuProjeto
{
    public class Conexao
    {
        private static string servidor = "localhost";
        private static string bancoDados = "tws_editora";
        private static string usuario = "root";
        private static string senha = "";

        private static string stringConexao =
            $"SERVER={servidor};DATABASE={bancoDados};UID={usuario};PWD={senha};SslMode=none;";

        public static MySqlConnection Conectar()
        {
            return new MySqlConnection(stringConexao);
        }
    }
}
