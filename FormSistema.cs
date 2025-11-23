using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SeuProjeto
{
    public partial class FormSistema : Form
    {
        // colors used across code (same as designer)
        private readonly Color CorFundo = ColorTranslator.FromHtml("#CFCFCF");
        private readonly Color CorAzul = ColorTranslator.FromHtml("#072E6A");
        private readonly Color CorBranco = Color.White;

        public FormSistema()
        {
            InitializeComponent();
            WireEvents();
            ShowPanel("livros"); // abrir direto em Livros
        }

        private void WireEvents()
        {
            // menu navigation
            menuLivros.Click += (s, e) => ShowPanel("livros");
            menuAutores.Click += (s, e) => ShowPanel("autores");
            menuEstoque.Click += (s, e) => ShowPanel("estoque");
            menuVendas.Click += (s, e) => ShowPanel("vendas");
            menuRelatorios.Click += (s, e) => ShowPanel("relatorios");
            menuSair.Click += (s, e) => Application.Exit();

            // livros
            btnLivroAdicionar.Click += BtnLivroAdicionar_Click;
            btnLivroAtualizar.Click += (s, e) => LoadLivros();
            btnLivroExcluir.Click += BtnLivroExcluir_Click;
            btnLivroEditar.Click += BtnLivroEditar_Click;
            dgvLivros.CellDoubleClick += DgvLivros_CellDoubleClick;

            // autores
            btnAutorAdicionar.Click += BtnAutorAdicionar_Click;
            btnAutorAtualizar.Click += (s, e) => LoadAutores();
            btnAutorExcluir.Click += BtnAutorExcluir_Click;
            dgvAutores.CellDoubleClick += DgvAutores_CellDoubleClick;

            // estoque
            btnEstoqueEntrada.Click += BtnEstoqueEntrada_Click;
            btnEstoqueSaida.Click += BtnEstoqueSaida_Click;
            btnEstoqueAtualizar.Click += (s, e) => LoadLivrosInEstoqueCombo();

            // vendas
            btnRegistrarVenda.Click += BtnRegistrarVenda_Click;
            btnRegistrarVenda.MouseEnter += (s, e) => ((Button)s).BackColor = ControlPaint.Light(CorAzul);
            btnRegistrarVenda.MouseLeave += (s, e) => ((Button)s).BackColor = CorAzul;

            // relatorios
            btnRelTop.Click += BtnRelTop_Click;
            btnRelEstoqueBaixo.Click += BtnRelEstoqueBaixo_Click;

            // Add hover effects for primary buttons globally
            foreach (Control c in this.Controls)
            {
                WireHoverRecursive(c);
            }
        }

        private void WireHoverRecursive(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button b)
                {
                    b.MouseEnter += (s, e) => b.BackColor = ControlPaint.Light(CorAzul);
                    b.MouseLeave += (s, e) => b.BackColor = CorAzul;
                }
                // recurse
                if (c.HasChildren) WireHoverRecursive(c);
            }
        }

        private void ShowPanel(string name)
        {
            HideAllPanels();
            switch (name.ToLower())
            {
                case "livros":
                    panelLivros.Visible = true;
                    LoadLivros();
                    LoadAutoresInLivroCombo();
                    LoadLivrosIntoCombos();
                    break;
                case "autores":
                    panelAutores.Visible = true;
                    LoadAutores();
                    break;
                case "estoque":
                    panelEstoque.Visible = true;
                    LoadLivrosInEstoqueCombo();
                    break;
                case "vendas":
                    panelVendas.Visible = true;
                    LoadLivrosInVendaCombo();
                    LoadVendas();
                    break;
                case "relatorios":
                    panelRelatorios.Visible = true;
                    break;
            }
        }

        private void HideAllPanels()
        {
            panelLivros.Visible = false;
            panelAutores.Visible = false;
            panelEstoque.Visible = false;
            panelVendas.Visible = false;
            panelRelatorios.Visible = false;
        }

        // --------------- livros ----------------
        private void LoadLivros()
        {
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = @"
                        SELECT l.IdLivros, l.Titulo_Liv, l.Preco_Liv, l.Categoria_Liv, l.Formato, l.Estoque_Liv,
                               (SELECT GROUP_CONCAT(a.Nome_Aut SEPARATOR ', ')
                                FROM Autores_Has_Livros ah JOIN Autores a ON ah.IdAutores = a.IdAutores
                                WHERE ah.IdLivros = l.IdLivros) AS Autores
                        FROM Livros l
                        ORDER BY l.Titulo_Liv;";
                    using (var da = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvLivros.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao carregar livros: " + ex.Message); }
        }

        private void LoadAutoresInLivroCombo()
        {
            cmbLivroAutor.Items.Clear();
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "SELECT IdAutores, Nome_Aut FROM Autores ORDER BY Nome_Aut";
                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbLivroAutor.Items.Add(new ComboboxItem(reader.GetInt32("IdAutores"), reader.GetString("Nome_Aut")));
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void BtnLivroAdicionar_Click(object sender, EventArgs e)
        {
            string titulo = txtLivroTitulo.Text.Trim();
            string categoria = txtLivroCategoria.Text.Trim();
            decimal preco = 0;
            int estoque = 0;
            int autorId = -1;

            if (string.IsNullOrEmpty(titulo))
            {
                MessageBox.Show("Informe o título.");
                return;
            }

            decimal.TryParse(txtLivroPreco.Text.Replace(',', '.'), out preco);
            int.TryParse(txtLivroEstoque.Text, out estoque);
            if (cmbLivroAutor.SelectedItem is ComboboxItem it) autorId = it.Id;

            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "INSERT INTO Livros (Titulo_Liv, Preco_Liv, Categoria_Liv, Formato, Estoque_Liv) VALUES (@t,@p,@c,'fisico',@e)";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@t", titulo);
                        cmd.Parameters.AddWithValue("@p", preco);
                        cmd.Parameters.AddWithValue("@c", categoria);
                        cmd.Parameters.AddWithValue("@e", estoque);
                        cmd.ExecuteNonQuery();
                    }

                    long idLivro = CmdLastInsertId(conn);

                    if (autorId > 0)
                    {
                        string rel = "INSERT INTO Autores_Has_Livros (IdAutores, IdLivros) VALUES (@a,@l)";
                        using (var cmd2 = new MySqlCommand(rel, conn))
                        {
                            cmd2.Parameters.AddWithValue("@a", autorId);
                            cmd2.Parameters.AddWithValue("@l", idLivro);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Livro adicionado com sucesso.");
                ClearLivroInputs();
                LoadLivros();
                LoadLivrosIntoCombos();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao adicionar livro: " + ex.Message); }
        }

        private void BtnLivroExcluir_Click(object sender, EventArgs e)
        {
            if (dgvLivros.SelectedRows.Count == 0) { MessageBox.Show("Selecione um livro."); return; }
            int id = Convert.ToInt32(dgvLivros.SelectedRows[0].Cells["IdLivros"].Value);
            if (MessageBox.Show("Excluir livro selecionado?", "Confirma", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string delRel = "DELETE FROM Autores_Has_Livros WHERE IdLivros=@l";
                    using (var cmd = new MySqlCommand(delRel, conn))
                    {
                        cmd.Parameters.AddWithValue("@l", id);
                        cmd.ExecuteNonQuery();
                    }

                    string del = "DELETE FROM Livros WHERE IdLivros=@id";
                    using (var cmd2 = new MySqlCommand(del, conn))
                    {
                        cmd2.Parameters.AddWithValue("@id", id);
                        cmd2.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Livro excluído.");
                LoadLivros();
                LoadLivrosIntoCombos();
            }
            catch (Exception ex) { MessageBox.Show("Erro ao excluir: " + ex.Message); }
        }

        private void BtnLivroEditar_Click(object sender, EventArgs e)
        {
            if (dgvLivros.SelectedRows.Count == 0) { MessageBox.Show("Selecione um livro."); return; }
            int id = Convert.ToInt32(dgvLivros.SelectedRows[0].Cells["IdLivros"].Value);

            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "SELECT * FROM Livros WHERE IdLivros=@id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtLivroTitulo.Text = reader["Titulo_Liv"]?.ToString();
                                txtLivroCategoria.Text = reader["Categoria_Liv"]?.ToString();
                                txtLivroPreco.Text = reader["Preco_Liv"]?.ToString();
                                txtLivroEstoque.Text = reader["Estoque_Liv"]?.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao carregar livro: " + ex.Message); }
        }

        private void DgvLivros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BtnLivroEditar_Click(sender, e);
        }

        private void ClearLivroInputs()
        {
            txtLivroTitulo.Text = "";
            txtLivroCategoria.Text = "";
            txtLivroPreco.Text = "";
            txtLivroEstoque.Text = "";
            cmbLivroAutor.SelectedIndex = -1;
        }

        // --------------- autores ----------------
        private void LoadAutores()
        {
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "SELECT IdAutores, Nome_Aut, Nacionalidade_Aut, Email_Aut FROM Autores ORDER BY Nome_Aut";
                    using (var da = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvAutores.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao carregar autores: " + ex.Message); }
        }

        private void BtnAutorAdicionar_Click(object sender, EventArgs e)
        {
            string nome = txtAutorNome.Text.Trim();
            if (string.IsNullOrEmpty(nome)) { MessageBox.Show("Informe o nome do autor."); return; }

            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "INSERT INTO Autores (Nome_Aut) VALUES (@n)";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@n", nome);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Autor adicionado.");
                txtAutorNome.Text = "";
                LoadAutores();
                LoadAutoresInLivroCombo();
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void BtnAutorExcluir_Click(object sender, EventArgs e)
        {
            if (dgvAutores.SelectedRows.Count == 0) { MessageBox.Show("Selecione um autor."); return; }
            int id = Convert.ToInt32(dgvAutores.SelectedRows[0].Cells["IdAutores"].Value);
            if (MessageBox.Show("Excluir autor?", "Confirma", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string delRel = "DELETE FROM Autores_Has_Livros WHERE IdAutores=@a";
                    using (var cmd = new MySqlCommand(delRel, conn))
                    {
                        cmd.Parameters.AddWithValue("@a", id);
                        cmd.ExecuteNonQuery();
                    }
                    string del = "DELETE FROM Autores WHERE IdAutores=@id";
                    using (var cmd2 = new MySqlCommand(del, conn))
                    {
                        cmd2.Parameters.AddWithValue("@id", id);
                        cmd2.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Autor excluído.");
                LoadAutores();
                LoadAutoresInLivroCombo();
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void DgvAutores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAutores.SelectedRows.Count == 0) return;
            txtAutorNome.Text = dgvAutores.SelectedRows[0].Cells["Nome_Aut"].Value.ToString();
        }

        // --------------- estoque ----------------
        private void LoadLivrosInEstoqueCombo()
        {
            cmbEstoqueLivro.Items.Clear();
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "SELECT IdLivros, Titulo_Liv, Estoque_Liv FROM Livros ORDER BY Titulo_Liv";
                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbEstoqueLivro.Items.Add(new ComboboxItem(reader.GetInt32("IdLivros"), $"{reader.GetString("Titulo_Liv")} (Est: {reader.GetInt32("Estoque_Liv")})"));
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void BtnEstoqueEntrada_Click(object sender, EventArgs e) => AjustarEstoque(true);
        private void BtnEstoqueSaida_Click(object sender, EventArgs e) => AjustarEstoque(false);

        private void AjustarEstoque(bool entrada)
        {
            if (!(cmbEstoqueLivro.SelectedItem is ComboboxItem item)) { MessageBox.Show("Selecione um livro."); return; }
            int id = item.Id;
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
                MessageBox.Show("Estoque atualizado.");
                LoadLivros();
                LoadLivrosInEstoqueCombo();
                LoadLivrosInVendaCombo();
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        // --------------- vendas ----------------
        private void LoadLivrosInVendaCombo()
        {
            cmbVendaLivro.Items.Clear();
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "SELECT IdLivros, Titulo_Liv, Estoque_Liv, Preco_Liv FROM Livros WHERE Estoque_Liv > 0 ORDER BY Titulo_Liv";
                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbVendaLivro.Items.Add(new ComboboxItem(reader.GetInt32("IdLivros"), $"{reader.GetString("Titulo_Liv")} (Est: {reader.GetInt32("Estoque_Liv")}) | R$ {reader.GetDecimal("Preco_Liv")}"));
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void BtnRegistrarVenda_Click(object sender, EventArgs e)
        {
            if (!(cmbVendaLivro.SelectedItem is ComboboxItem it)) { MessageBox.Show("Selecione um livro."); return; }
            int idLivro = it.Id;
            int qtd = (int)nudVendaQtd.Value;
            if (qtd <= 0) { MessageBox.Show("Quantidade inválida."); return; }

            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    decimal preco = 0;
                    int estoque = 0;
                    using (var cmdp = new MySqlCommand("SELECT Preco_Liv, Estoque_Liv FROM Livros WHERE IdLivros=@id", conn))
                    {
                        cmdp.Parameters.AddWithValue("@id", idLivro);
                        using (var r = cmdp.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                preco = r.GetDecimal("Preco_Liv");
                                estoque = r.GetInt32("Estoque_Liv");
                                if (estoque < qtd) { MessageBox.Show("Estoque insuficiente."); return; }
                            }
                        }
                    }

                    decimal total = preco * qtd;

                    string insertVenda = "INSERT INTO Vendas (IdCliente, IdFuncionarios, IdFretes, Data_Ven, Quantidade_Ven, Valor_Total_Ven, Status_Ven) VALUES (NULL,NULL,NULL,@d,@q,@v,'Pago')";
                    using (var cmdv = new MySqlCommand(insertVenda, conn))
                    {
                        cmdv.Parameters.AddWithValue("@d", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmdv.Parameters.AddWithValue("@q", qtd);
                        cmdv.Parameters.AddWithValue("@v", total);
                        cmdv.ExecuteNonQuery();
                    }

                    long idVenda = CmdLastInsertId(conn);

                    string insRel = "INSERT INTO Livros_Has_Vendas (IdLivros, IdVendas, Quantidade, Subtotal) VALUES (@l,@v,@q,@s)";
                    using (var cmdrel = new MySqlCommand(insRel, conn))
                    {
                        cmdrel.Parameters.AddWithValue("@l", idLivro);
                        cmdrel.Parameters.AddWithValue("@v", idVenda);
                        cmdrel.Parameters.AddWithValue("@q", qtd);
                        cmdrel.Parameters.AddWithValue("@s", total);
                        cmdrel.ExecuteNonQuery();
                    }

                    string upEst = "UPDATE Livros SET Estoque_Liv = Estoque_Liv - @q WHERE IdLivros=@l";
                    using (var cmdup = new MySqlCommand(upEst, conn))
                    {
                        cmdup.Parameters.AddWithValue("@q", qtd);
                        cmdup.Parameters.AddWithValue("@l", idLivro);
                        cmdup.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Venda registrada.");
                LoadLivros();
                LoadLivrosInVendaCombo();
                LoadVendas();
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void LoadVendas()
        {
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "SELECT IdVendas, Data_Ven, Quantidade_Ven, Valor_Total_Ven, Status_Ven FROM Vendas ORDER BY Data_Ven DESC LIMIT 200";
                    using (var da = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvVendas.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao carregar vendas: " + ex.Message); }
        }

        // --------------- relatórios ----------------
        private void BtnRelTop_Click(object sender, EventArgs e)
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
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvRelatorios.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        private void BtnRelEstoqueBaixo_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = Conexao.Conectar())
                {
                    conn.Open();
                    string sql = "SELECT IdLivros, Titulo_Liv, Estoque_Liv FROM Livros WHERE Estoque_Liv <= 5 ORDER BY Estoque_Liv ASC";
                    using (var da = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvRelatorios.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Erro: " + ex.Message); }
        }

        // --------------- HELPERS ---------------
        private long CmdLastInsertId(MySqlConnection conn)
        {
            using (var cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", conn))
            {
                object o = cmd.ExecuteScalar();
                if (o != null && long.TryParse(o.ToString(), out long id)) return id;
                return -1;
            }
        }

        private void LoadLivrosIntoCombos()
        {
            LoadLivrosInEstoqueCombo();
            LoadLivrosInVendaCombo();
        }

        // helper combobox item class
        private class ComboboxItem
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public ComboboxItem(int id, string text) { Id = id; Text = text; }
            public override string ToString() => Text;
        }
    }
}
