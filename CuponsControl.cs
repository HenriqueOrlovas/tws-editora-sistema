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

            btnCupomAdd.Click += btnCupomAdd_Click;
            btnCupomDelete.Click += btnCupomDelete_Click;

            CarregarCupons();
        }

        private string LerTexto(TextBox caixa)
        {
            if (caixa == null)
            {
                return "";
            }

            if (caixa.ForeColor == System.Drawing.Color.Gray)
            {
                return "";
            }

            return caixa.Text.Trim();
        }

        private void btnCupomAdd_Click(object sender, EventArgs e)
        {
            string codigo = LerTexto(txtCupomCodigo);
            decimal desconto = nudCupomDesconto.Value;
            DateTime validade = dtpCupomValidade.Value.Date;
            int usoMax = (int)nudCupomUsoMax.Value;

            if (codigo == "")
            {
                MessageBox.Show("Informe o código.");
                return;
            }

            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                string sql =
                    "INSERT INTO Cupons (Codigo, DescontoPercentual, DataValidade, UsoMaximo) " +
                    "VALUES (@c, @d, @v, @u)";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@c", codigo);
                cmd.Parameters.AddWithValue("@d", desconto);
                cmd.Parameters.AddWithValue("@v", validade.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@u", usoMax);

                cmd.ExecuteNonQuery();

                conexao.Close();

                MessageBox.Show("Cupom adicionado.");
                CarregarCupons();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao adicionar cupom: " + erro.Message);
            }
        }

        private void btnCupomDelete_Click(object sender, EventArgs e)
        {
            if (dgvCupons.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um cupom.");
                return;
            }

            string textoId = dgvCupons.SelectedRows[0].Cells["IdCupons"].Value.ToString();
            int id = int.Parse(textoId);

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

                MySqlCommand cmd = new MySqlCommand(
                    "DELETE FROM Cupons WHERE IdCupons=@id",
                    conexao
                );

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                conexao.Close();

                MessageBox.Show("Cupom excluído.");
                CarregarCupons();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao excluir cupom: " + erro.Message);
            }
        }

        private void CarregarCupons()
        {
            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                string sql =
                    "SELECT IdCupons, Codigo, DescontoPercentual, DataValidade, UsoMaximo " +
                    "FROM Cupons ORDER BY IdCupons DESC";

                MySqlDataAdapter adaptador = new MySqlDataAdapter(sql, conexao);
                DataTable tabela = new DataTable();
                adaptador.Fill(tabela);

                dgvCupons.DataSource = tabela;

                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar cupons: " + erro.Message);
            }
        }
    }
}
