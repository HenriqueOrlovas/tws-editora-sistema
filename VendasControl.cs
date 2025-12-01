// VendasControl.cs
using MySql.Data.MySqlClient;
using SeuProjeto;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Sistema_tws
{
    public partial class VendasControl : UserControl
    {
        private DataTable vendaCarrinho = new DataTable();

        public VendasControl()
        {
            InitializeComponent();
            InitCart();
            Wire();
            LoadLivros();
            RefreshVendasHistorico();
        }

        private void InitCart()
        {
            vendaCarrinho.Columns.Add("IdLivro", typeof(int));
            vendaCarrinho.Columns.Add("Título", typeof(string));
            vendaCarrinho.Columns.Add("Quantidade", typeof(int));
            vendaCarrinho.Columns.Add("PreçoUnit", typeof(decimal));
            vendaCarrinho.Columns.Add("Subtotal", typeof(decimal));
            dgvVendaCarrinho.DataSource = vendaCarrinho;
        }

        private void Wire()
        {
            btnVendaAdicionarItem.Click += (s, e) => AddItemToCart();
            btnVendaConcluir.Click += (s, e) => ConcluirVenda();
        }

        private void LoadLivros()
        {
            try
            {
                cmbVendaLivro.Items.Clear();
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SELECT IdLivros, Titulo_Liv, Preco_Liv, Estoque_Liv FROM Livros ORDER BY Titulo_Liv", conn))
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read()) cmbVendaLivro.Items.Add(new ComboItem(r.GetInt32("IdLivros"), $"{r.GetInt32("IdLivros")} - {r.GetString("Titulo_Liv")} | R$ {r.GetDecimal("Preco_Liv")} (Est:{r.GetInt32("Estoque_Liv")})"));
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao carregar livros: " + ex.Message); }
        }

        private void AddItemToCart()
        {
            if (cmbVendaLivro.SelectedItem == null) { MessageBox.Show("Selecione um livro."); return; }
            int idLivro = ((ComboItem)cmbVendaLivro.SelectedItem).Id;
            int qtd = (int)nudVendaQtd.Value;
            if (qtd <= 0) { MessageBox.Show("Quantidade inválida."); return; }
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SELECT Titulo_Liv, Preco_Liv, Estoque_Liv FROM Livros WHERE IdLivros=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idLivro);
                        using (var r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                string titulo = r.GetString("Titulo_Liv");
                                decimal preco = r.GetDecimal("Preco_Liv");
                                int estoque = r.GetInt32("Estoque_Liv");
                                if (estoque < qtd) { MessageBox.Show("Estoque insuficiente."); return; }
                                decimal subtotal = preco * qtd;
                                vendaCarrinho.Rows.Add(idLivro, titulo, qtd, preco, subtotal);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao adicionar item: " + ex.Message); }
        }

        private void ConcluirVenda()
        {
            if (vendaCarrinho.Rows.Count == 0) { MessageBox.Show("Carrinho vazio."); return; }
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    int totalQtd = vendaCarrinho.AsEnumerable().Sum(r => r.Field<int>("Quantidade"));
                    decimal total = vendaCarrinho.AsEnumerable().Sum(r => r.Field<decimal>("Subtotal"));

                    using (var cmd = new MySqlCommand("INSERT INTO Vendas (IdCliente,IdFuncionarios,IdFretes,Data_Ven,Quantidade_Ven,Valor_Total_Ven,Status_Ven) VALUES (NULL,NULL,NULL,@d,@q,@v,'Pago')", conn))
                    {
                        cmd.Parameters.AddWithValue("@d", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@q", totalQtd);
                        cmd.Parameters.AddWithValue("@v", total);
                        cmd.ExecuteNonQuery();
                    }
                    long idVenda;
                    using (var cmd2 = new MySqlCommand("SELECT LAST_INSERT_ID()", conn)) { idVenda = Convert.ToInt64(cmd2.ExecuteScalar()); }

                    foreach (DataRow row in vendaCarrinho.Rows)
                    {
                        int idLivro = Convert.ToInt32(row["IdLivro"]);
                        int qtd = Convert.ToInt32(row["Quantidade"]);
                        decimal subtotal = Convert.ToDecimal(row["Subtotal"]);
                        using (var cmd3 = new MySqlCommand("INSERT INTO Livros_Has_Vendas (IdLivros,IdVendas,Quantidade,Subtotal) VALUES (@l,@v,@q,@s)", conn))
                        { cmd3.Parameters.AddWithValue("@l", idLivro); cmd3.Parameters.AddWithValue("@v", idVenda); cmd3.Parameters.AddWithValue("@q", qtd); cmd3.Parameters.AddWithValue("@s", subtotal); cmd3.ExecuteNonQuery(); }
                        using (var cmd4 = new MySqlCommand("UPDATE Livros SET Estoque_Liv = Estoque_Liv - @q WHERE IdLivros=@l", conn))
                        { cmd4.Parameters.AddWithValue("@q", qtd); cmd4.Parameters.AddWithValue("@l", idLivro); cmd4.ExecuteNonQuery(); }
                    }
                }
                MessageBox.Show("Venda concluída!");
                vendaCarrinho.Clear();
                LoadLivros(); RefreshVendasHistorico();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao concluir venda: " + ex.Message); }
        }

        private void RefreshVendasHistorico()
        {
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    using (var da = new MySqlDataAdapter("SELECT IdVendas, Data_Ven, Quantidade_Ven, Valor_Total_Ven, Status_Ven FROM Vendas ORDER BY Data_Ven DESC LIMIT 200", conn))
                    {
                        var dt = new DataTable(); da.Fill(dt); dgvVendasHistorico.DataSource = dt;
                    }
                }
            }
            catch { }
        }

        private class ComboItem { public int Id; public string Text; public ComboItem(int id, string t) { Id = id; Text = t; } public override string ToString() => Text; }
    }
}
