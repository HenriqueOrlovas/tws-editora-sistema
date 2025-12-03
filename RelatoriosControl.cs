using MySql.Data.MySqlClient;
using SeuProjeto;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sistema_tws
{
    public partial class RelatoriosControl : UserControl
    {
        public RelatoriosControl()
        {
            InitializeComponent();

            btnRelTopVendidos.Click += btnRelTopVendidos_Click;
            btnRelEstoqueBaixo.Click += btnRelEstoqueBaixo_Click;
        }

        private void btnRelTopVendidos_Click(object sender, EventArgs e)
        {
            RelTopVendidos();
        }

        private void btnRelEstoqueBaixo_Click(object sender, EventArgs e)
        {
            RelEstoqueBaixo();
        }

        private void RelTopVendidos()
        {
            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                string sql =
                    "SELECT l.Titulo_Liv AS Livro, " +
                    "SUM(lh.Quantidade) AS QtdVendida, " +
                    "SUM(lh.Subtotal) AS Total " +
                    "FROM Livros_Has_Vendas lh " +
                    "JOIN Livros l ON lh.IdLivros = l.IdLivros " +
                    "GROUP BY lh.IdLivros " +
                    "ORDER BY QtdVendida DESC " +
                    "LIMIT 50";

                MySqlDataAdapter adaptador = new MySqlDataAdapter(sql, conexao);
                DataTable tabela = new DataTable();
                adaptador.Fill(tabela);

                dgvRelatorios.DataSource = tabela;

                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar relatório: " + erro.Message);
            }
        }

        private void RelEstoqueBaixo()
        {
            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                string sql =
                    "SELECT IdLivros, Titulo_Liv, Estoque_Liv " +
                    "FROM Livros " +
                    "WHERE Estoque_Liv <= 5 " +
                    "ORDER BY Estoque_Liv ASC";

                MySqlDataAdapter adaptador = new MySqlDataAdapter(sql, conexao);
                DataTable tabela = new DataTable();
                adaptador.Fill(tabela);

                dgvRelatorios.DataSource = tabela;

                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar relatório: " + erro.Message);
            }
        }
    }
}
