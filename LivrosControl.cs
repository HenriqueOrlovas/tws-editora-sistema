using MySql.Data.MySqlClient;
using SeuProjeto;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sistema_tws
{
    public partial class LivrosControl : UserControl
    {
        public LivrosControl()
        {
            InitializeComponent();

            btnLivroAdd.Click += btnLivroAdd_Click;
            btnLivroEdit.Click += btnLivroEdit_Click;
            btnLivroDelete.Click += btnLivroDelete_Click;
            btnLivroRefresh.Click += btnLivroRefresh_Click;
            dgvLivros.SelectionChanged += dgvLivros_SelectionChanged;

            CarregarLivros();
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

        private void CarregarLivros()
        {
            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                string sql =
                    "SELECT l.IdLivros, l.Titulo_Liv, l.Preco_Liv, l.Categoria_Liv, l.Formato, l.Estoque_Liv, " +
                    "(SELECT GROUP_CONCAT(a.Nome_Aut SEPARATOR ', ') FROM Autores_Has_Livros ah " +
                    "JOIN Autores a ON ah.IdAutores=a.IdAutores WHERE ah.IdLivros=l.IdLivros) AS Autores " +
                    "FROM Livros l ORDER BY l.Titulo_Liv";

                MySqlDataAdapter da = new MySqlDataAdapter(sql, conexao);

                DataTable tabela = new DataTable();
                da.Fill(tabela);

                dgvLivros.DataSource = tabela;

                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar livros: " + erro.Message);
            }
        }

        private void CarregarAutores()
        {
            try
            {
                cmbLivroAutor.Items.Clear();

                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT IdAutores, Nome_Aut FROM Autores ORDER BY Nome_Aut",
                    conexao
                );

                MySqlDataReader leitor = cmd.ExecuteReader();

                while (leitor.Read())
                {
                    int id = leitor.GetInt32("IdAutores");
                    string nome = leitor.GetString("Nome_Aut");

                    cmbLivroAutor.Items.Add(new ComboItem(id, nome));
                }

                leitor.Close();
                conexao.Close();
            }
            catch
            {
            }
        }

        private void btnLivroAdd_Click(object sender, EventArgs e)
        {
            string titulo = LerTexto(txtLivroTitulo);
            string categoria = LerTexto(txtLivroCategoria);
            string precoTexto = LerTexto(txtLivroPreco);

            string formato = "fisico";
            if (cmbLivroFormato.SelectedItem != null)
            {
                formato = cmbLivroFormato.SelectedItem.ToString();
            }

            int estoque = (int)nudLivroEstoque.Value;

            if (titulo == "")
            {
                MessageBox.Show("Informe o título.");
                return;
            }

            decimal preco;
            decimal.TryParse(precoTexto.Replace(',', '.'), out preco);

            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO Livros (Titulo_Liv, Preco_Liv, Categoria_Liv, Formato, Estoque_Liv) " +
                    "VALUES (@t,@p,@c,@f,@e)",
                    conexao
                );

                cmd.Parameters.AddWithValue("@t", titulo);
                cmd.Parameters.AddWithValue("@p", preco);
                cmd.Parameters.AddWithValue("@c", categoria);
                cmd.Parameters.AddWithValue("@f", formato);
                cmd.Parameters.AddWithValue("@e", estoque);

                cmd.ExecuteNonQuery();

                conexao.Close();

                MessageBox.Show("Livro adicionado.");
                CarregarLivros();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao adicionar livro: " + erro.Message);
            }
        }

        private void btnLivroEdit_Click(object sender, EventArgs e)
        {
            if (dgvLivros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um livro.");
                return;
            }

            int id = int.Parse(dgvLivros.SelectedRows[0].Cells["IdLivros"].Value.ToString());

            string titulo = LerTexto(txtLivroTitulo);
            string categoria = LerTexto(txtLivroCategoria);
            string precoTexto = LerTexto(txtLivroPreco);

            string formato = "fisico";
            if (cmbLivroFormato.SelectedItem != null)
            {
                formato = cmbLivroFormato.SelectedItem.ToString();
            }

            int estoque = (int)nudLivroEstoque.Value;

            decimal preco;
            decimal.TryParse(precoTexto.Replace(',', '.'), out preco);

            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "UPDATE Livros SET Titulo_Liv=@t, Preco_Liv=@p, Categoria_Liv=@c, Formato=@f, Estoque_Liv=@e WHERE IdLivros=@id",
                    conexao
                );

                cmd.Parameters.AddWithValue("@t", titulo);
                cmd.Parameters.AddWithValue("@p", preco);
                cmd.Parameters.AddWithValue("@c", categoria);
                cmd.Parameters.AddWithValue("@f", formato);
                cmd.Parameters.AddWithValue("@e", estoque);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                conexao.Close();

                MessageBox.Show("Livro atualizado.");
                CarregarLivros();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao editar livro: " + erro.Message);
            }
        }

        private void btnLivroDelete_Click(object sender, EventArgs e)
        {
            if (dgvLivros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um livro.");
                return;
            }

            int id = int.Parse(dgvLivros.SelectedRows[0].Cells["IdLivros"].Value.ToString());

            DialogResult resp = MessageBox.Show(
                "Confirma exclusão?",
                "Excluir",
                MessageBoxButtons.YesNo
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
                    "DELETE FROM Autores_Has_Livros WHERE IdLivros=@id",
                    conexao
                );
                cmd1.Parameters.AddWithValue("@id", id);
                cmd1.ExecuteNonQuery();

                MySqlCommand cmd2 = new MySqlCommand(
                    "DELETE FROM Livros WHERE IdLivros=@id",
                    conexao
                );
                cmd2.Parameters.AddWithValue("@id", id);
                cmd2.ExecuteNonQuery();

                conexao.Close();

                MessageBox.Show("Livro excluído.");
                CarregarLivros();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao excluir livro: " + erro.Message);
            }
        }

        private void btnLivroRefresh_Click(object sender, EventArgs e)
        {
            CarregarLivros();
        }

        private void dgvLivros_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLivros.SelectedRows.Count == 0)
            {
                return;
            }

            DataGridViewRow linha = dgvLivros.SelectedRows[0];

            txtLivroTitulo.Text = linha.Cells["Titulo_Liv"].Value.ToString();
            txtLivroTitulo.ForeColor = System.Drawing.Color.Black;

            txtLivroPreco.Text = linha.Cells["Preco_Liv"].Value.ToString();
            txtLivroPreco.ForeColor = System.Drawing.Color.Black;

            txtLivroCategoria.Text = linha.Cells["Categoria_Liv"].Value.ToString();
            txtLivroCategoria.ForeColor = System.Drawing.Color.Black;

            int est;
            if (int.TryParse(linha.Cells["Estoque_Liv"].Value.ToString(), out est))
            {
                nudLivroEstoque.Value = est;
            }
        }

        private class ComboItem
        {
            public int Id;
            public string Texto;

            public ComboItem(int i, string t)
            {
                Id = i;
                Texto = t;
            }

            public override string ToString()
            {
                return Texto;
            }
        }
    }
}
