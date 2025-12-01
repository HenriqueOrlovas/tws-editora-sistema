// LivrosControl.cs
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
            Wire();
            LoadData();
            LoadAutoresCombo();
        }

        private void Wire()
        {
            btnLivroAdd.Click += (s, e) => AddLivro();
            btnLivroEdit.Click += (s, e) => EditLivro();
            btnLivroDelete.Click += (s, e) => DeleteLivro();
            btnLivroRefresh.Click += (s, e) => LoadData();
            dgvLivros.SelectionChanged += (s, e) => FillSelectedToInputs();
        }

        private void LoadData()
        {
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = @"
                        SELECT l.IdLivros, l.Titulo_Liv, l.Preco_Liv, l.Categoria_Liv, l.Formato, l.Estoque_Liv,
                        (SELECT GROUP_CONCAT(a.Nome_Aut SEPARATOR ', ') FROM Autores_Has_Livros ah JOIN Autores a ON ah.IdAutores=a.IdAutores WHERE ah.IdLivros=l.IdLivros) AS Autores
                        FROM Livros l ORDER BY l.Titulo_Liv";
                    using (var da = new MySqlDataAdapter(sql, conn))
                    {
                        var dt = new DataTable(); da.Fill(dt); dgvLivros.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao carregar livros: " + ex.Message); }
        }

        private void LoadAutoresCombo()
        {
            try
            {
                cmbLivroAutor.Items.Clear();
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SELECT IdAutores, Nome_Aut FROM Autores ORDER BY Nome_Aut", conn))
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read()) cmbLivroAutor.Items.Add(new ComboItem(r.GetInt32("IdAutores"), r.GetString("Nome_Aut")));
                    }
                }
            }
            catch { }
        }

        private string GetText(TextBox tb) => tb == null ? "" : (tb.ForeColor == System.Drawing.Color.Gray ? "" : tb.Text.Trim());

        private void AddLivro()
        {
            string titulo = GetText(txtLivroTitulo);
            string categoria = GetText(txtLivroCategoria);
            string precoText = GetText(txtLivroPreco);
            string formato = cmbLivroFormato?.SelectedItem?.ToString() ?? "fisico";
            int estoque = (int)nudLivroEstoque.Value;
            if (string.IsNullOrWhiteSpace(titulo)) { MessageBox.Show("Informe o título."); return; }
            decimal preco = decimal.TryParse(precoText.Replace(',', '.'), out var p) ? p : 0;
            try
            {
                using (var conn = Conexao.Conectar()) { conn.Open(); using (var cmd = new MySqlCommand("INSERT INTO Livros (Titulo_Liv, Preco_Liv, Categoria_Liv, Formato, Estoque_Liv) VALUES (@t,@p,@c,@f,@e)", conn)) { cmd.Parameters.AddWithValue("@t", titulo); cmd.Parameters.AddWithValue("@p", preco); cmd.Parameters.AddWithValue("@c", categoria); cmd.Parameters.AddWithValue("@f", formato); cmd.Parameters.AddWithValue("@e", estoque); cmd.ExecuteNonQuery(); } }
                MessageBox.Show("Livro adicionado.");
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao adicionar livro: " + ex.Message); }
        }

        private void EditLivro()
        {
            if (dgvLivros.SelectedRows.Count == 0) { MessageBox.Show("Selecione um livro."); return; }
            int id = Convert.ToInt32(dgvLivros.SelectedRows[0].Cells["IdLivros"].Value);
            string titulo = GetText(txtLivroTitulo);
            string categoria = GetText(txtLivroCategoria);
            string precoText = GetText(txtLivroPreco);
            string formato = cmbLivroFormato?.SelectedItem?.ToString() ?? "fisico";
            int estoque = (int)nudLivroEstoque.Value;
            decimal preco = decimal.TryParse(precoText.Replace(',', '.'), out var p) ? p : 0;
            try
            {
                using (var conn = Conexao.Conectar()) { conn.Open(); using (var cmd = new MySqlCommand("UPDATE Livros SET Titulo_Liv=@t,Preco_Liv=@p,Categoria_Liv=@c,Formato=@f,Estoque_Liv=@e WHERE IdLivros=@id", conn)) { cmd.Parameters.AddWithValue("@t", titulo); cmd.Parameters.AddWithValue("@p", preco); cmd.Parameters.AddWithValue("@c", categoria); cmd.Parameters.AddWithValue("@f", formato); cmd.Parameters.AddWithValue("@e", estoque); cmd.Parameters.AddWithValue("@id", id); cmd.ExecuteNonQuery(); } }
                MessageBox.Show("Livro atualizado.");
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao editar livro: " + ex.Message); }
        }

        private void DeleteLivro()
        {
            if (dgvLivros.SelectedRows.Count == 0) { MessageBox.Show("Selecione um livro."); return; }
            int id = Convert.ToInt32(dgvLivros.SelectedRows[0].Cells["IdLivros"].Value);
            if (MessageBox.Show("Confirma exclusão?", "Excluir", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                using (var conn = Conexao.Conectar()) { conn.Open(); using (var cmd = new MySqlCommand("DELETE FROM Autores_Has_Livros WHERE IdLivros=@id", conn)) { cmd.Parameters.AddWithValue("@id", id); cmd.ExecuteNonQuery(); } using (var cmd2 = new MySqlCommand("DELETE FROM Livros WHERE IdLivros=@id", conn)) { cmd2.Parameters.AddWithValue("@id", id); cmd2.ExecuteNonQuery(); } }
                MessageBox.Show("Livro excluído.");
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao excluir livro: " + ex.Message); }
        }

        private void FillSelectedToInputs()
        {
            if (dgvLivros.SelectedRows.Count == 0) return;
            var r = dgvLivros.SelectedRows[0];
            txtLivroTitulo.Text = r.Cells["Titulo_Liv"].Value?.ToString() ?? "";
            txtLivroPreco.Text = r.Cells["Preco_Liv"].Value?.ToString() ?? "";
            txtLivroCategoria.Text = r.Cells["Categoria_Liv"].Value?.ToString() ?? "";
            if (int.TryParse(r.Cells["Estoque_Liv"].Value?.ToString(), out var est)) nudLivroEstoque.Value = est;
        }

        private class ComboItem { public int Id; public string Text; public ComboItem(int id, string t) { Id = id; Text = t; } public override string ToString() => Text; }
    }
}
