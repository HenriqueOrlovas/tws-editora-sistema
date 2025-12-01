// AutoresControl.cs
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
            Wire();
            LoadData();
        }

        private void Wire()
        {
            btnAutorAdd.Click += (s, e) => AddAutor();
            btnAutorEdit.Click += (s, e) => EditAutor();
            btnAutorDelete.Click += (s, e) => DeleteAutor();
            btnAutorRefresh.Click += (s, e) => LoadData();
            dgvAutores.SelectionChanged += (s, e) => FillSelected();
        }

        private void LoadData()
        {
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    using (var da = new MySqlDataAdapter("SELECT IdAutores, Nome_Aut, Nacionalidade_Aut, Email_Aut FROM Autores ORDER BY Nome_Aut", conn))
                    {
                        var dt = new DataTable(); da.Fill(dt); dgvAutores.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao carregar autores: " + ex.Message); }
        }

        private string GetText(TextBox tb) => tb == null ? "" : (tb.ForeColor == System.Drawing.Color.Gray ? "" : tb.Text.Trim());

        private void AddAutor()
        {
            string nome = GetText(txtAutorNome);
            string nacional = GetText(txtAutorNacionalidade);
            string email = GetText(txtAutorEmail);
            if (string.IsNullOrWhiteSpace(nome)) { MessageBox.Show("Informe o nome."); return; }
            try { using (var conn = Conexao.Conectar()) { conn.Open(); using (var cmd = new MySqlCommand("INSERT INTO Autores (Nome_Aut, Nacionalidade_Aut, Email_Aut) VALUES (@n,@na,@e)", conn)) { cmd.Parameters.AddWithValue("@n", nome); cmd.Parameters.AddWithValue("@na", nacional); cmd.Parameters.AddWithValue("@e", email); cmd.ExecuteNonQuery(); } } MessageBox.Show("Autor adicionado."); LoadData(); }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void EditAutor()
        {
            if (dgvAutores.SelectedRows.Count == 0) { MessageBox.Show("Selecione um autor."); return; }
            int id = Convert.ToInt32(dgvAutores.SelectedRows[0].Cells["IdAutores"].Value);
            string nome = GetText(txtAutorNome);
            string nacional = GetText(txtAutorNacionalidade);
            string email = GetText(txtAutorEmail);
            try { using (var conn = Conexao.Conectar()) { conn.Open(); using (var cmd = new MySqlCommand("UPDATE Autores SET Nome_Aut=@n,Nacionalidade_Aut=@na,Email_Aut=@e WHERE IdAutores=@id", conn)) { cmd.Parameters.AddWithValue("@n", nome); cmd.Parameters.AddWithValue("@na", nacional); cmd.Parameters.AddWithValue("@e", email); cmd.Parameters.AddWithValue("@id", id); cmd.ExecuteNonQuery(); } } MessageBox.Show("Atualizado."); LoadData(); }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void DeleteAutor()
        {
            if (dgvAutores.SelectedRows.Count == 0) { MessageBox.Show("Selecione um autor."); return; }
            int id = Convert.ToInt32(dgvAutores.SelectedRows[0].Cells["IdAutores"].Value);
            if (MessageBox.Show("Confirma exclusão?", "Excluir", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try { using (var conn = Conexao.Conectar()) { conn.Open(); using (var cmd = new MySqlCommand("DELETE FROM Autores_Has_Livros WHERE IdAutores=@id", conn)) { cmd.Parameters.AddWithValue("@id", id); cmd.ExecuteNonQuery(); } using (var cmd2 = new MySqlCommand("DELETE FROM Autores WHERE IdAutores=@id", conn)) { cmd2.Parameters.AddWithValue("@id", id); cmd2.ExecuteNonQuery(); } } MessageBox.Show("Excluído."); LoadData(); }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void FillSelected()
        {
            if (dgvAutores.SelectedRows.Count == 0) return;
            var r = dgvAutores.SelectedRows[0];
            txtAutorNome.Text = r.Cells["Nome_Aut"].Value?.ToString() ?? "";
            txtAutorNacionalidade.Text = r.Cells["Nacionalidade_Aut"].Value?.ToString() ?? "";
            txtAutorEmail.Text = r.Cells["Email_Aut"].Value?.ToString() ?? "";
        }
    }
}
