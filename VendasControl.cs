using MySql.Data.MySqlClient;
using SeuProjeto;
using System;
using System.Data;
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
            vendaCarrinho.Columns.Add("Titulo", typeof(string));
            vendaCarrinho.Columns.Add("Quantidade", typeof(int));
            vendaCarrinho.Columns.Add("PrecoUnit", typeof(decimal));
            vendaCarrinho.Columns.Add("Subtotal", typeof(decimal));

            dgvVendaCarrinho.DataSource = vendaCarrinho;
        }

        private void Wire()
        {
            btnVendaAdicionarItem.Click += btnVendaAdicionarItem_Click;
            btnVendaConcluir.Click += btnVendaConcluir_Click;
        }

        private void btnVendaAdicionarItem_Click(object sender, EventArgs e)
        {
            AddItemToCart();
        }

        private void btnVendaConcluir_Click(object sender, EventArgs e)
        {
            ConcluirVenda();
        }

        private void LoadLivros()
        {
            try
            {
                cmbVendaLivro.Items.Clear();

                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT IdLivros, Titulo_Liv, Preco_Liv, Estoque_Liv FROM Livros ORDER BY Titulo_Liv",
                    conexao
                );

                MySqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    int id = r.GetInt32("IdLivros");
                    string texto =
                        id +
                        " - " +
                        r.GetString("Titulo_Liv") +
                        " | R$ " +
                        r.GetDecimal("Preco_Liv") +
                        " (Est:" +
                        r.GetInt32("Estoque_Liv") +
                        ")";

                    cmbVendaLivro.Items.Add(new ComboItem(id, texto));
                }

                r.Close();
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar livros: " + erro.Message);
            }
        }

        private void AddItemToCart()
        {
            if (cmbVendaLivro.SelectedItem == null)
            {
                MessageBox.Show("Selecione um livro.");
                return;
            }

            ComboItem item = (ComboItem)cmbVendaLivro.SelectedItem;
            int idLivro = item.Id;
            int qtd = Convert.ToInt32(nudVendaQtd.Value);

            if (qtd <= 0)
            {
                MessageBox.Show("Quantidade inválida.");
                return;
            }

            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT Titulo_Liv, Preco_Liv, Estoque_Liv FROM Livros WHERE IdLivros=@id",
                    conexao
                );

                cmd.Parameters.AddWithValue("@id", idLivro);

                MySqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    string titulo = r.GetString("Titulo_Liv");
                    decimal preco = r.GetDecimal("Preco_Liv");
                    int estoque = r.GetInt32("Estoque_Liv");

                    if (estoque < qtd)
                    {
                        MessageBox.Show("Estoque insuficiente.");
                        r.Close();
                        conexao.Close();
                        return;
                    }

                    decimal subtotal = preco * qtd;

                    vendaCarrinho.Rows.Add(idLivro, titulo, qtd, preco, subtotal);
                }

                r.Close();
                conexao.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao adicionar item: " + erro.Message);
            }
        }

        private void ConcluirVenda()
        {
            if (vendaCarrinho.Rows.Count == 0)
            {
                MessageBox.Show("Carrinho vazio.");
                return;
            }

            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                // Somar quantidade total
                int totalQtd = 0;
                int i;

                for (i = 0; i < vendaCarrinho.Rows.Count; i++)
                {
                    totalQtd += Convert.ToInt32(vendaCarrinho.Rows[i]["Quantidade"]);
                }

                // Somar valor total
                decimal valorTotal = 0;

                for (i = 0; i < vendaCarrinho.Rows.Count; i++)
                {
                    valorTotal += Convert.ToDecimal(vendaCarrinho.Rows[i]["Subtotal"]);
                }

                // Registrar venda
                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO Vendas (IdCliente,IdFuncionarios,IdFretes,Data_Ven,Quantidade_Ven,Valor_Total_Ven,Status_Ven) VALUES (NULL,NULL,NULL,@d,@q,@v,'Pago')",
                    conexao
                );

                cmd.Parameters.AddWithValue("@d", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@q", totalQtd);
                cmd.Parameters.AddWithValue("@v", valorTotal);

                cmd.ExecuteNonQuery();

                // Obter ID da venda
                MySqlCommand cmd2 = new MySqlCommand("SELECT LAST_INSERT_ID()", conexao);
                long idVenda = Convert.ToInt64(cmd2.ExecuteScalar());

                // Inserir itens e baixar estoque
                for (i = 0; i < vendaCarrinho.Rows.Count; i++)
                {
                    int idLivro = Convert.ToInt32(vendaCarrinho.Rows[i]["IdLivro"]);
                    int qtd = Convert.ToInt32(vendaCarrinho.Rows[i]["Quantidade"]);
                    decimal sub = Convert.ToDecimal(vendaCarrinho.Rows[i]["Subtotal"]);

                    MySqlCommand cmd3 = new MySqlCommand(
                        "INSERT INTO Livros_Has_Vendas (IdLivros,IdVendas,Quantidade,Subtotal) VALUES (@l,@v,@q,@s)",
                        conexao
                    );

                    cmd3.Parameters.AddWithValue("@l", idLivro);
                    cmd3.Parameters.AddWithValue("@v", idVenda);
                    cmd3.Parameters.AddWithValue("@q", qtd);
                    cmd3.Parameters.AddWithValue("@s", sub);
                    cmd3.ExecuteNonQuery();

                    MySqlCommand cmd4 = new MySqlCommand(
                        "UPDATE Livros SET Estoque_Liv = Estoque_Liv - @q WHERE IdLivros=@l",
                        conexao
                    );

                    cmd4.Parameters.AddWithValue("@q", qtd);
                    cmd4.Parameters.AddWithValue("@l", idLivro);
                    cmd4.ExecuteNonQuery();
                }

                conexao.Close();

                MessageBox.Show("Venda concluída!");

                vendaCarrinho.Clear();
                LoadLivros();
                RefreshVendasHistorico();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao concluir venda: " + erro.Message);
            }
        }

        private void RefreshVendasHistorico()
        {
            try
            {
                MySqlConnection conexao = Conexao.Conectar();
                conexao.Open();

                MySqlDataAdapter da = new MySqlDataAdapter(
                    "SELECT IdVendas, Data_Ven, Quantidade_Ven, Valor_Total_Ven, Status_Ven FROM Vendas ORDER BY Data_Ven DESC LIMIT 200",
                    conexao
                );

                DataTable tabela = new DataTable();
                da.Fill(tabela);

                dgvVendasHistorico.DataSource = tabela;

                conexao.Close();
            }
            catch
            {
            }
        }

        private class ComboItem
        {
            public int Id;
            public string Text;

            public ComboItem(int id, string t)
            {
                Id = id;
                Text = t;
            }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
