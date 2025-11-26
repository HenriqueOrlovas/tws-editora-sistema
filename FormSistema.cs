// FormSistema.cs
using MySql.Data.MySqlClient;
using SeuProjeto;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Sistema_tws
{
    public partial class FormSistema : Form
    {
        private DataTable vendaCarrinho = new DataTable();

        public FormSistema()
        {
            InitializeComponent();
            WireRuntimeEvents();
            InitHelpers();
        }

        private void InitHelpers()
        {
            vendaCarrinho.Columns.Add("IdLivro", typeof(int));
            vendaCarrinho.Columns.Add("Título", typeof(string));
            vendaCarrinho.Columns.Add("Quantidade", typeof(int));
            vendaCarrinho.Columns.Add("PreçoUnit", typeof(decimal));
            vendaCarrinho.Columns.Add("Subtotal", typeof(decimal));

            // safe init: if designer didn't create some controls, skip
            try { RefreshLivrosGrid(); } catch { }
            try { RefreshAutoresGrid(); } catch { }
            try { RefreshCuponsGrid(); } catch { }
            try { RefreshVendasHistorico(); } catch { }
            try { LoadCombos(); } catch { }
        }

        private void WireRuntimeEvents()
        {
            // Attach events only if controls exist (prevents null-ref)
            if (btnLivroAdd != null) btnLivroAdd.Click += (s, e) => AddLivro();
            if (btnLivroEdit != null) btnLivroEdit.Click += (s, e) => EditLivro();
            if (btnLivroDelete != null) btnLivroDelete.Click += (s, e) => DeleteLivro();
            if (btnLivroRefresh != null) btnLivroRefresh.Click += (s, e) => RefreshLivrosGrid();

            if (btnAutorAdd != null) btnAutorAdd.Click += (s, e) => AddAutor();
            if (btnAutorEdit != null) btnAutorEdit.Click += (s, e) => EditAutor();
            if (btnAutorDelete != null) btnAutorDelete.Click += (s, e) => DeleteAutor();
            if (btnAutorRefresh != null) btnAutorRefresh.Click += (s, e) => RefreshAutoresGrid();

            if (btnEstoqueEntrada != null) btnEstoqueEntrada.Click += (s, e) => AjustarEstoque(true);
            if (btnEstoqueSaida != null) btnEstoqueSaida.Click += (s, e) => AjustarEstoque(false);
            if (btnEstoqueRefresh != null) btnEstoqueRefresh.Click += (s, e) => LoadCombos();

            if (btnCupomAdd != null) btnCupomAdd.Click += (s, e) => AddCupom();
            if (btnCupomDelete != null) btnCupomDelete.Click += (s, e) => DeleteCupom();
            if (btnCupomRefresh != null) btnCupomRefresh.Click += (s, e) => RefreshCuponsGrid();

            if (btnVendaAdicionarItem != null) btnVendaAdicionarItem.Click += (s, e) => AddItemToCart();
            if (btnVendaConcluir != null) btnVendaConcluir.Click += (s, e) => ConcluirVenda();

            if (btnRelTopVendidos != null) btnRelTopVendidos.Click += (s, e) => RelTopVendidos();
            if (btnRelEstoqueBaixo != null) btnRelEstoqueBaixo.Click += (s, e) => RelEstoqueBaixo();
        }

        // ------------------- Combos -------------------
        private void LoadCombos()
        {
            LoadAutoresToCombo();
            LoadLivrosToCombos();
        }

        private void LoadAutoresToCombo()
        {
            if (cmbLivroAutor == null) return;
            cmbLivroAutor.Items.Clear();
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "SELECT IdAutores, Nome_Aut FROM Autores ORDER BY Nome_Aut";
                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                            cmbLivroAutor.Items.Add(new ComboItem(r.GetInt32("IdAutores"), r.GetString("Nome_Aut")));
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao carregar autores: " + ex.Message); }
        }

        private void LoadLivrosToCombos()
        {
            if (cmbEstoqueLivro == null || cmbVendaLivro == null) return;
            cmbEstoqueLivro.Items.Clear();
            cmbVendaLivro.Items.Clear();
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "SELECT IdLivros, Titulo_Liv, Estoque_Liv, Preco_Liv FROM Livros ORDER BY Titulo_Liv";
                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            int id = r.GetInt32("IdLivros");
                            string title = r.GetString("Titulo_Liv");
                            int est = r.IsDBNull(r.GetOrdinal("Estoque_Liv")) ? 0 : r.GetInt32("Estoque_Liv");
                            decimal preco = r.IsDBNull(r.GetOrdinal("Preco_Liv")) ? 0 : r.GetDecimal("Preco_Liv");

                            cmbEstoqueLivro.Items.Add(new ComboItem(id, $"{id} - {title} (Est: {est})"));
                            cmbVendaLivro.Items.Add(new ComboItem(id, $"{id} - {title} | R$ {preco}"));
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao carregar livros: " + ex.Message); }
        }

        // ------------------- Livros CRUD -------------------
        private void AddLivro()
        {
            string titulo = GetTextIfNotPlaceholder(txtLivroTitulo);
            string categoria = GetTextIfNotPlaceholder(txtLivroCategoria);
            string precoText = GetTextIfNotPlaceholder(txtLivroPreco);
            string formato = cmbLivroFormato?.SelectedItem?.ToString() ?? "fisico";
            int estoque = nudLivroEstoque != null ? (int)nudLivroEstoque.Value : 0;

            if (string.IsNullOrWhiteSpace(titulo)) { MessageBox.Show("Informe o título."); return; }
            decimal preco = decimal.TryParse(precoText.Replace(',', '.'), out var p) ? p : 0;
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "INSERT INTO Livros (Titulo_Liv, Preco_Liv, Categoria_Liv, Formato, Estoque_Liv) VALUES (@t,@p,@c,@f,@e)";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@t", titulo);
                        cmd.Parameters.AddWithValue("@p", preco);
                        cmd.Parameters.AddWithValue("@c", categoria);
                        cmd.Parameters.AddWithValue("@f", formato);
                        cmd.Parameters.AddWithValue("@e", estoque);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Livro adicionado.");
                RefreshLivrosGrid();
                LoadLivrosToCombos();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao adicionar livro: " + ex.Message); }
        }

        private void EditLivro()
        {
            if (dgvLivros == null || dgvLivros.SelectedRows.Count == 0) { MessageBox.Show("Selecione um livro."); return; }
            int id = Convert.ToInt32(dgvLivros.SelectedRows[0].Cells["IdLivros"].Value);
            string titulo = GetTextIfNotPlaceholder(txtLivroTitulo);
            string categoria = GetTextIfNotPlaceholder(txtLivroCategoria);
            string precoText = GetTextIfNotPlaceholder(txtLivroPreco);
            string formato = cmbLivroFormato?.SelectedItem?.ToString() ?? "fisico";
            int estoque = nudLivroEstoque != null ? (int)nudLivroEstoque.Value : 0;
            decimal preco = decimal.TryParse(precoText.Replace(',', '.'), out var p) ? p : 0;
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "UPDATE Livros SET Titulo_Liv=@t, Preco_Liv=@p, Categoria_Liv=@c, Formato=@f, Estoque_Liv=@e WHERE IdLivros=@id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@t", titulo);
                        cmd.Parameters.AddWithValue("@p", preco);
                        cmd.Parameters.AddWithValue("@c", categoria);
                        cmd.Parameters.AddWithValue("@f", formato);
                        cmd.Parameters.AddWithValue("@e", estoque);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Livro atualizado.");
                RefreshLivrosGrid();
                LoadLivrosToCombos();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao editar livro: " + ex.Message); }
        }

        private void DeleteLivro()
        {
            if (dgvLivros == null || dgvLivros.SelectedRows.Count == 0) { MessageBox.Show("Selecione um livro."); return; }
            int id = Convert.ToInt32(dgvLivros.SelectedRows[0].Cells["IdLivros"].Value);
            if (MessageBox.Show("Confirma exclusão?", "Excluir", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("DELETE FROM Autores_Has_Livros WHERE IdLivros=@id", conn))
                    { cmd.Parameters.AddWithValue("@id", id); cmd.ExecuteNonQuery(); }
                    using (var cmd2 = new MySqlCommand("DELETE FROM Livros WHERE IdLivros=@id", conn))
                    { cmd2.Parameters.AddWithValue("@id", id); cmd2.ExecuteNonQuery(); }
                }
                MessageBox.Show("Livro excluído.");
                RefreshLivrosGrid();
                LoadLivrosToCombos();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao excluir livro: " + ex.Message); }
        }

        private void RefreshLivrosGrid()
        {
            if (dgvLivros == null) return;
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = @"
                        SELECT l.IdLivros, l.Titulo_Liv, l.Preco_Liv, l.Categoria_Liv, l.Formato, l.Estoque_Liv,
                        (SELECT GROUP_CONCAT(a.Nome_Aut SEPARATOR ', ') 
                         FROM Autores_Has_Livros ah 
                         JOIN Autores a ON ah.IdAutores = a.IdAutores 
                         WHERE ah.IdLivros = l.IdLivros) AS Autores
                        FROM Livros l ORDER BY l.Titulo_Liv";
                    using (var da = new MySqlDataAdapter(sql, conn))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        dgvLivros.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao carregar livros: " + ex.Message); }
        }

        // ------------------- Autores CRUD -------------------
        private void AddAutor()
        {
            string nome = GetTextIfNotPlaceholder(txtAutorNome);
            string nacional = GetTextIfNotPlaceholder(txtAutorNacionalidade);
            string email = GetTextIfNotPlaceholder(txtAutorEmail);
            if (string.IsNullOrWhiteSpace(nome)) { MessageBox.Show("Informe o nome."); return; }
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "INSERT INTO Autores (Nome_Aut, Nacionalidade_Aut, Email_Aut) VALUES (@n,@na,@e)";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@n", nome);
                        cmd.Parameters.AddWithValue("@na", nacional);
                        cmd.Parameters.AddWithValue("@e", email);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Autor adicionado.");
                RefreshAutoresGrid();
                LoadAutoresToCombo();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao adicionar autor: " + ex.Message); }
        }

        private void EditAutor()
        {
            if (dgvAutores == null || dgvAutores.SelectedRows.Count == 0) { MessageBox.Show("Selecione um autor."); return; }
            int id = Convert.ToInt32(dgvAutores.SelectedRows[0].Cells["IdAutores"].Value);
            string nome = GetTextIfNotPlaceholder(txtAutorNome);
            string nacional = GetTextIfNotPlaceholder(txtAutorNacionalidade);
            string email = GetTextIfNotPlaceholder(txtAutorEmail);
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "UPDATE Autores SET Nome_Aut=@n, Nacionalidade_Aut=@na, Email_Aut=@e WHERE IdAutores=@id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@n", nome);
                        cmd.Parameters.AddWithValue("@na", nacional);
                        cmd.Parameters.AddWithValue("@e", email);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Autor atualizado.");
                RefreshAutoresGrid();
                LoadAutoresToCombo();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao editar autor: " + ex.Message); }
        }

        private void DeleteAutor()
        {
            if (dgvAutores == null || dgvAutores.SelectedRows.Count == 0) { MessageBox.Show("Selecione um autor."); return; }
            int id = Convert.ToInt32(dgvAutores.SelectedRows[0].Cells["IdAutores"].Value);
            if (MessageBox.Show("Confirmar exclusão?", "Excluir", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("DELETE FROM Autores_Has_Livros WHERE IdAutores=@id", conn))
                    { cmd.Parameters.AddWithValue("@id", id); cmd.ExecuteNonQuery(); }
                    using (var cmd2 = new MySqlCommand("DELETE FROM Autores WHERE IdAutores=@id", conn))
                    { cmd2.Parameters.AddWithValue("@id", id); cmd2.ExecuteNonQuery(); }
                }
                MessageBox.Show("Autor excluído.");
                RefreshAutoresGrid();
                LoadAutoresToCombo();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao excluir autor: " + ex.Message); }
        }

        private void RefreshAutoresGrid()
        {
            if (dgvAutores == null) return;
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "SELECT IdAutores, Nome_Aut, Nacionalidade_Aut, Email_Aut FROM Autores ORDER BY Nome_Aut";
                    using (var da = new MySqlDataAdapter(sql, conn))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        dgvAutores.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao carregar autores: " + ex.Message); }
        }

        // ------------------- Estoque -------------------
        private void AjustarEstoque(bool entrada)
        {
            if (cmbEstoqueLivro == null || cmbEstoqueLivro.SelectedItem == null) { MessageBox.Show("Selecione um livro."); return; }
            int id = ((ComboItem)cmbEstoqueLivro.SelectedItem).Id;
            int qtd = (int)nudEstoqueQtd.Value;
            if (qtd <= 0) { MessageBox.Show("Quantidade inválida."); return; }

            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string op = entrada ? "+" : "-";
                    string sql = $"UPDATE Livros SET Estoque_Liv = Estoque_Liv {op} @q WHERE IdLivros=@id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@q", qtd);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Estoque ajustado.");
                RefreshLivrosGrid();
                LoadLivrosToCombos();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao ajustar estoque: " + ex.Message); }
        }

        // ------------------- Cupons -------------------
        private void AddCupom()
        {
            string codigo = GetTextIfNotPlaceholder(txtCupomCodigo);
            decimal desconto = nudCupomDesconto != null ? nudCupomDesconto.Value : 0;
            DateTime validade = dtpCupomValidade != null ? dtpCupomValidade.Value.Date : DateTime.Now.Date;
            int usoMax = nudCupomUsoMax != null ? (int)nudCupomUsoMax.Value : 1;

            if (string.IsNullOrWhiteSpace(codigo)) { MessageBox.Show("Informe o código."); return; }
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "INSERT INTO Cupons (Codigo, DescontoPercentual, DataValidade, UsoMaximo) VALUES (@c,@d,@v,@u)";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@c", codigo);
                        cmd.Parameters.AddWithValue("@d", desconto);
                        cmd.Parameters.AddWithValue("@v", validade.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@u", usoMax);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Cupom adicionado.");
                RefreshCuponsGrid();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao adicionar cupom: " + ex.Message); }
        }

        private void DeleteCupom()
        {
            if (dgvCupons == null || dgvCupons.SelectedRows.Count == 0) { MessageBox.Show("Selecione um cupom."); return; }
            int id = Convert.ToInt32(dgvCupons.SelectedRows[0].Cells["IdCupons"].Value);
            if (MessageBox.Show("Excluir cupom?", "Excluir", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("DELETE FROM Cupons WHERE IdCupons=@id", conn))
                    { cmd.Parameters.AddWithValue("@id", id); cmd.ExecuteNonQuery(); }
                }
                MessageBox.Show("Cupom excluído.");
                RefreshCuponsGrid();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao excluir cupom: " + ex.Message); }
        }

        private void RefreshCuponsGrid()
        {
            if (dgvCupons == null) return;
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "SELECT IdCupons, Codigo, DescontoPercentual, DataValidade, UsoMaximo FROM Cupons ORDER BY IdCupons DESC";
                    using (var da = new MySqlDataAdapter(sql, conn))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        dgvCupons.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao atualizar cupons: " + ex.Message); }
        }

        // ------------------- Vendas -------------------
        private void AddItemToCart()
        {
            if (cmbVendaLivro == null || cmbVendaLivro.SelectedItem == null) { MessageBox.Show("Selecione um livro."); return; }
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
                                if (dgvVendaCarrinho != null) dgvVendaCarrinho.DataSource = vendaCarrinho;
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

                    string insertVenda = "INSERT INTO Vendas (IdCliente, IdFuncionarios, IdFretes, Data_Ven, Quantidade_Ven, Valor_Total_Ven, Status_Ven) VALUES (NULL,NULL,NULL,@d,@q,@v,'Pago')";
                    using (var cmd = new MySqlCommand(insertVenda, conn))
                    {
                        cmd.Parameters.AddWithValue("@d", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@q", totalQtd);
                        cmd.Parameters.AddWithValue("@v", total);
                        cmd.ExecuteNonQuery();
                    }

                    long idVenda = GetLastInsertId(conn);
                    foreach (DataRow row in vendaCarrinho.Rows)
                    {
                        int idLivro = Convert.ToInt32(row["IdLivro"]);
                        int qtd = Convert.ToInt32(row["Quantidade"]);
                        decimal subtotal = Convert.ToDecimal(row["Subtotal"]);

                        using (var cmd2 = new MySqlCommand("INSERT INTO Livros_Has_Vendas (IdLivros, IdVendas, Quantidade, Subtotal) VALUES (@l,@v,@q,@s)", conn))
                        {
                            cmd2.Parameters.AddWithValue("@l", idLivro);
                            cmd2.Parameters.AddWithValue("@v", idVenda);
                            cmd2.Parameters.AddWithValue("@q", qtd);
                            cmd2.Parameters.AddWithValue("@s", subtotal);
                            cmd2.ExecuteNonQuery();
                        }
                        using (var cmd3 = new MySqlCommand("UPDATE Livros SET Estoque_Liv = Estoque_Liv - @q WHERE IdLivros=@l", conn))
                        {
                            cmd3.Parameters.AddWithValue("@q", qtd);
                            cmd3.Parameters.AddWithValue("@l", idLivro);
                            cmd3.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Venda concluída!");
                vendaCarrinho.Clear();
                if (dgvVendaCarrinho != null) dgvVendaCarrinho.DataSource = null;
                RefreshLivrosGrid();
                LoadLivrosToCombos();
                RefreshVendasHistorico();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao concluir venda: " + ex.Message); }
        }

        private void RefreshVendasHistorico()
        {
            if (dgvVendasHistorico == null) return;
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "SELECT IdVendas, Data_Ven, Quantidade_Ven, Valor_Total_Ven, Status_Ven FROM Vendas ORDER BY Data_Ven DESC LIMIT 200";
                    using (var da = new MySqlDataAdapter(sql, conn))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        dgvVendasHistorico.DataSource = dt;
                    }
                }
            }
            catch { /* silent */ }
        }

        // ------------------- Relatórios -------------------
        private void RelTopVendidos()
        {
            if (dgvRelatorios == null) return;
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
                        var dt = new DataTable();
                        da.Fill(dt);
                        dgvRelatorios.DataSource = dt;
                        tabControlMain.SelectedTab = tabRelatorios;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro no relatório: " + ex.Message); }
        }

        private void RelEstoqueBaixo()
        {
            if (dgvRelatorios == null) return;
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "SELECT IdLivros, Titulo_Liv, Estoque_Liv FROM Livros WHERE Estoque_Liv <= 5 ORDER BY Estoque_Liv ASC";
                    using (var da = new MySqlDataAdapter(sql, conn))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        dgvRelatorios.DataSource = dt;
                        tabControlMain.SelectedTab = tabRelatorios;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro no relatório: " + ex.Message); }
        }

        // ------------------- Helpers -------------------
        private string GetTextIfNotPlaceholder(TextBox tb)
        {
            if (tb == null) return "";
            return tb.ForeColor == System.Drawing.Color.Gray ? "" : tb.Text.Trim();
        }

        private long GetLastInsertId(MySqlConnection conn)
        {
            using (var cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", conn))
            {
                var o = cmd.ExecuteScalar();
                if (o != null && long.TryParse(o.ToString(), out long id)) return id;
                return -1;
            }
        }

        // Typed combo item
        private class ComboItem
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public ComboItem(int id, string text) { Id = id; Text = text; }
            public override string ToString() => Text;
        }
    }
}
