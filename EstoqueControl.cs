// EstoqueControl.cs
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
            Wire();
            LoadLivros();
        }

        private void Wire()
        {
            btnEstoqueEntrada.Click += (s, e) => AjustarEstoque(true);
            btnEstoqueSaida.Click += (s, e) => AjustarEstoque(false);
            btnEstoqueRefresh.Click += (s, e) => LoadLivros();
        }

        private void LoadLivros()
        {
            try
            {
                cmbEstoqueLivro.Items.Clear();
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SELECT IdLivros, Titulo_Liv, Estoque_Liv FROM Livros ORDER BY Titulo_Liv", conn))
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read()) cmbEstoqueLivro.Items.Add(new ComboItem(r.GetInt32("IdLivros"), $"{r.GetInt32("IdLivros")} - {r.GetString("Titulo_Liv")} (Est:{r.GetInt32("Estoque_Liv")})"));
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao carregar livros: " + ex.Message); }
        }

        private void AjustarEstoque(bool entrada)
        {
            if (cmbEstoqueLivro.SelectedItem == null) { MessageBox.Show("Selecione um livro."); return; }
            int id = ((ComboItem)cmbEstoqueLivro.SelectedItem).Id;
            int qtd = (int)nudEstoqueQtd.Value;
            if (qtd <= 0) { MessageBox.Show("Quantidade inválida."); return; }
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string op = entrada ? "+" : "-";
                    using (var cmd = new MySqlCommand($"UPDATE Livros SET Estoque_Liv = Estoque_Liv {op} @q WHERE IdLivros=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@q", qtd);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Estoque ajustado.");
                LoadLivros();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao ajustar estoque: " + ex.Message); }
        }

        private class ComboItem { public int Id; public string Text; public ComboItem(int id, string t) { Id = id; Text = t; } public override string ToString() => Text; }
    }
}
