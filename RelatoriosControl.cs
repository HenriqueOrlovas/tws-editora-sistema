// RelatoriosControl.cs
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
            Wire();
        }

        private void Wire()
        {
            btnRelTopVendidos.Click += (s, e) => RelTopVendidos();
            btnRelEstoqueBaixo.Click += (s, e) => RelEstoqueBaixo();
        }

        private void RelTopVendidos()
        {
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = @"
                        SELECT l.Titulo_Liv AS Livro, SUM(lh.Quantidade) AS QtdVendida, SUM(lh.Subtotal) AS Total
                        FROM Livros_Has_Vendas lh
                        JOIN Livros l ON lh.IdLivros = l.IdLivros
                        GROUP BY lh.IdLivros
                        ORDER BY QtdVendida DESC
                        LIMIT 50";
                    using (var da = new MySqlDataAdapter(sql, conn))
                    {
                        var dt = new DataTable(); da.Fill(dt); dgvRelatorios.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void RelEstoqueBaixo()
        {
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    using (var da = new MySqlDataAdapter("SELECT IdLivros, Titulo_Liv, Estoque_Liv FROM Livros WHERE Estoque_Liv <= 5 ORDER BY Estoque_Liv ASC", conn))
                    {
                        var dt = new DataTable(); da.Fill(dt); dgvRelatorios.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }
    }
}
