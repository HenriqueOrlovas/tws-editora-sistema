using MySql.Data.MySqlClient;
using SeuProjeto;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sistema_tws
{
    public partial class AutoresControl : UserControl
    {
        public AutoresControl()
        {
            InitializeComponent();

            btnAutorAdd.Click += btnAutorAdd_Click;
            btnAutorEdit.Click += btnAutorEdit_Click;
            btnAutorDelete.Click += btnAutorDelete_Click;
            btnAutorRefresh.Click += btnAutorRefresh_Click;
            dgvAutores.SelectionChanged += dgvAutores_SelectionChanged;

            CarregarAutores();
        }

        private string LerTexto(TextBox caixa)
        {
            if (caixa.ForeColor == System.Drawing.Color.Gray)
            {
                return "";
            }
            return caixa.Text.Trim();
        }

        private void CarregarAutores()
        {
            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                MySqlDataAdapter adaptador = new MySqlDataAdapter(
                    "SELECT IdAutores, Nome_Aut, Nacionalidade_Aut, Email_Aut FROM Autores ORDER BY Nome_Aut",
                    conexao
                );

                DataTable tabela = new DataTable();
                adaptador.Fill(tabela);

                dgvAutores.DataSource = tabela;

                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar autores: " + erro.Message);
            }
        }

        private void btnAutorAdd_Click(object sender, EventArgs e)
        {
            string nome = LerTexto(txtAutorNome);
            string nacionalidade = LerTexto(txtAutorNacionalidade);
            string email = LerTexto(txtAutorEmail);

            if (nome == "")
            {
                MessageBox.Show("Informe o nome.");
                return;
            }

            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO Autores (Nome_Aut, Nacionalidade_Aut, Email_Aut) VALUES (@n,@na,@e)",
                    conexao
                );

                cmd.Parameters.AddWithValue("@n", nome);
                cmd.Parameters.AddWithValue("@na", nacionalidade);
                cmd.Parameters.AddWithValue("@e", email);

                cmd.ExecuteNonQuery();

                conexao.Close();

                MessageBox.Show("Autor adicionado.");
                CarregarAutores();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro.Message);
            }
        }

        private void btnAutorEdit_Click(object sender, EventArgs e)
        {
            if (dgvAutores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um autor.");
                return;
            }

            int id = int.Parse(dgvAutores.SelectedRows[0].Cells["IdAutores"].Value.ToString());

            string nome = LerTexto(txtAutorNome);
            string nacionalidade = LerTexto(txtAutorNacionalidade);
            string email = LerTexto(txtAutorEmail);

            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "UPDATE Autores SET Nome_Aut=@n, Nacionalidade_Aut=@na, Email_Aut=@e WHERE IdAutores=@id",
                    conexao
                );

                cmd.Parameters.AddWithValue("@n", nome);
                cmd.Parameters.AddWithValue("@na", nacionalidade);
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                conexao.Close();

                MessageBox.Show("Atualizado.");
                CarregarAutores();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro.Message);
            }
        }

        private void btnAutorDelete_Click(object sender, EventArgs e)
        {
            if (dgvAutores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um autor.");
                return;
            }

            int id = int.Parse(dgvAutores.SelectedRows[0].Cells["IdAutores"].Value.ToString());

            DialogResult resp = MessageBox.Show(
                "Confirma exclusão?", "Excluir", MessageBoxButtons.YesNo
            );

            if (resp != DialogResult.Yes)
            {
                return;
            }

            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                MySqlCommand cmd1 = new MySqlCommand(
                    "DELETE FROM Autores_Has_Livros WHERE IdAutores=@id",
                    conexao
                );
                cmd1.Parameters.AddWithValue("@id", id);
                cmd1.ExecuteNonQuery();

                MySqlCommand cmd2 = new MySqlCommand(
                    "DELETE FROM Autores WHERE IdAutores=@id",
                    conexao
                );
                cmd2.Parameters.AddWithValue("@id", id);
                cmd2.ExecuteNonQuery();

                conexao.Close();

                MessageBox.Show("Excluído.");
                CarregarAutores();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro.Message);
            }
        }

        private void btnAutorRefresh_Click(object sender, EventArgs e)
        {
            CarregarAutores();
        }

        private void dgvAutores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAutores.SelectedRows.Count == 0)
            {
                return;
            }

            DataGridViewRow linha = dgvAutores.SelectedRows[0];

            txtAutorNome.Text = linha.Cells["Nome_Aut"].Value.ToString();
            txtAutorNome.ForeColor = System.Drawing.Color.Black;

            txtAutorNacionalidade.Text = linha.Cells["Nacionalidade_Aut"].Value.ToString();
            txtAutorNacionalidade.ForeColor = System.Drawing.Color.Black;

            txtAutorEmail.Text = linha.Cells["Email_Aut"].Value.ToString();
            txtAutorEmail.ForeColor = System.Drawing.Color.Black;
        }
    }
}
