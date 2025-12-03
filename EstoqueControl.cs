using MySql.Data.MySqlClient;
using SeuProjeto;
using System;
using System.Windows.Forms;

namespace Sistema_tws
{
    public partial class EstoqueControl : UserControl
    {
        public EstoqueControl()
        {
            InitializeComponent();

            btnEstoqueEntrada.Click += btnEstoqueEntrada_Click;
            btnEstoqueSaida.Click += btnEstoqueSaida_Click;
            btnEstoqueRefresh.Click += btnEstoqueRefresh_Click;

            CarregarLivros();
        }

        private void CarregarLivros()
        {
            try
            {
                cmbEstoqueLivro.Items.Clear();

                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                string sql =
                    "SELECT IdLivros, Titulo_Liv, Estoque_Liv " +
                    "FROM Livros ORDER BY Titulo_Liv";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);
                MySqlDataReader leitor = cmd.ExecuteReader();

                while (leitor.Read())
                {
                    int idLivro = leitor.GetInt32("IdLivros");
                    string titulo = leitor.GetString("Titulo_Liv");
                    int estoque = leitor.GetInt32("Estoque_Liv");

                    string texto =
                        idLivro.ToString() +
                        " - " +
                        titulo +
                        " (Est: " +
                        estoque.ToString() +
                        ")";

                    cmbEstoqueLivro.Items.Add(new ComboItem(idLivro, texto));
                }

                leitor.Close();
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar livros: " + erro.Message);
            }
        }

        private void AjustarEstoque(int idLivro, int quantidade, bool entrada)
        {
            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                string sql;

                if (entrada)
                {
                    sql = "UPDATE Livros SET Estoque_Liv = Estoque_Liv + @q WHERE IdLivros=@id";
                }
                else
                {
                    sql = "UPDATE Livros SET Estoque_Liv = Estoque_Liv - @q WHERE IdLivros=@id";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@q", quantidade);
                cmd.Parameters.AddWithValue("@id", idLivro);
                cmd.ExecuteNonQuery();

                conexao.Close();

                MessageBox.Show("Estoque ajustado.");
                CarregarLivros();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao ajustar estoque: " + erro.Message);
            }
        }

        private void btnEstoqueEntrada_Click(object sender, EventArgs e)
        {
            if (cmbEstoqueLivro.SelectedItem == null)
            {
                MessageBox.Show("Selecione um livro.");
                return;
            }

            ComboItem item = (ComboItem)cmbEstoqueLivro.SelectedItem;
            int quantidade = (int)nudEstoqueQtd.Value;

            if (quantidade <= 0)
            {
                MessageBox.Show("Quantidade inválida.");
                return;
            }

            AjustarEstoque(item.Id, quantidade, true);
        }

        private void btnEstoqueSaida_Click(object sender, EventArgs e)
        {
            if (cmbEstoqueLivro.SelectedItem == null)
            {
                MessageBox.Show("Selecione um livro.");
                return;
            }

            ComboItem item = (ComboItem)cmbEstoqueLivro.SelectedItem;
            int quantidade = (int)nudEstoqueQtd.Value;

            if (quantidade <= 0)
            {
                MessageBox.Show("Quantidade inválida.");
                return;
            }

            AjustarEstoque(item.Id, quantidade, false);
        }

        private void btnEstoqueRefresh_Click(object sender, EventArgs e)
        {
            CarregarLivros();
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
