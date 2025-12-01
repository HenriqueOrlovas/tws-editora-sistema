// CuponsControl.cs
using MySql.Data.MySqlClient;
using SeuProjeto;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sistema_tws
{
    public partial class CuponsControl : UserControl
    {
        public CuponsControl()
        {
            InitializeComponent();
            Wire();
            RefreshCupons();
        }

        private void Wire()
        {
            btnCupomAdd.Click += (s, e) => AddCupom();
            btnCupomDelete.Click += (s, e) => DeleteCupom();
        }

        private string GetText(TextBox tb) => tb == null ? "" : (tb.ForeColor == System.Drawing.Color.Gray ? "" : tb.Text.Trim());

        private void AddCupom()
        {
            string codigo = GetText(txtCupomCodigo);
            decimal desconto = nudCupomDesconto.Value;
            DateTime validade = dtpCupomValidade.Value.Date;
            int usoMax = (int)nudCupomUsoMax.Value;
            if (string.IsNullOrWhiteSpace(codigo)) { MessageBox.Show("Informe o código."); return; }
            try { using (var conn = Conexao.Conectar()) { conn.Open(); using (var cmd = new MySqlCommand("INSERT INTO Cupons (Codigo, DescontoPercentual, DataValidade, UsoMaximo) VALUES (@c,@d,@v,@u)", conn)) { cmd.Parameters.AddWithValue("@c", codigo); cmd.Parameters.AddWithValue("@d", desconto); cmd.Parameters.AddWithValue("@v", validade.ToString("yyyy-MM-dd")); cmd.Parameters.AddWithValue("@u", usoMax); cmd.ExecuteNonQuery(); } } MessageBox.Show("Cupom adicionado."); RefreshCupons(); }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void DeleteCupom()
        {
            if (dgvCupons.SelectedRows.Count == 0) { MessageBox.Show("Selecione um cupom."); return; }
            int id = Convert.ToInt32(dgvCupons.SelectedRows[0].Cells["IdCupons"].Value);
            if (MessageBox.Show("Confirma exclusão?", "Excluir", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try { using (var conn = Conexao.Conectar()) { conn.Open(); using (var cmd = new MySqlCommand("DELETE FROM Cupons WHERE IdCupons=@id", conn)) { cmd.Parameters.AddWithValue("@id", id); cmd.ExecuteNonQuery(); } } MessageBox.Show("Cupom excluído."); RefreshCupons(); }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void RefreshCupons()
        {
            try { using (var conn = Conexao.Conectar()) { conn.Open(); using (var da = new MySqlDataAdapter("SELECT IdCupons, Codigo, DescontoPercentual, DataValidade, UsoMaximo FROM Cupons ORDER BY IdCupons DESC", conn)) { var dt = new DataTable(); da.Fill(dt); dgvCupons.DataSource = dt; } } }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }
    }
}
