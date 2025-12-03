using MySql.Data.MySqlClient;
using SeuProjeto;
using System;
using System.Windows.Forms;

namespace Sistema_tws
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            btnLogin.Click += btnLogin_Click;
            btnSair.Click += btnSair_Click;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string senha = txtSenha.Text.Trim();

            if (usuario == "" || senha == "")
            {
                MessageBox.Show("Preencha usuário e senha!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                string query = "SELECT Tipo_Usu FROM Usuarios WHERE Nome_Usu=@usuario AND Senha_Usu=@senha LIMIT 1";

                MySqlCommand cmd = new MySqlCommand(query, conexao);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@senha", senha);

                object resultado = cmd.ExecuteScalar();

                if (resultado != null && usuario == "admin" && senha == "adminPIDS")
                {
                    MessageBox.Show("Login realizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    FormMenu menu = new FormMenu();
                    menu.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Acesso negado. Apenas o admin pode entrar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
