using MySql.Data.MySqlClient;

namespace SeuProjeto
{
    public class Conexao
    {
        private static string servidor = "localhost";
        private static string bancoDados = "tws_editora";
        private static string usuario = "root";
        private static string senha = "";

        private static string stringConexao =
            "SERVER=" + servidor +
            ";DATABASE=" + bancoDados +
            ";UID=" + usuario +
            ";PWD=" + senha +
            ";SslMode=Preferred;";

        public static MySqlConnection Conectar()
        {
            return new MySqlConnection(stringConexao);
        }
    }
}
