using System;
using System.Windows.Forms;

namespace SeuProjeto
{
    partial class FormSistema
    {
        private System.ComponentModel.IContainer components = null;

        // Menu e controles principais
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuCadastros;
        private System.Windows.Forms.ToolStripMenuItem menuLivros;
        private System.Windows.Forms.ToolStripMenuItem menuAutores;
        private System.Windows.Forms.ToolStripMenuItem menuConsultas;
        private System.Windows.Forms.ToolStripMenuItem menuEstoque;
        private System.Windows.Forms.ToolStripMenuItem menuVendas;
        private System.Windows.Forms.ToolStripMenuItem menuRelatorios;
        private System.Windows.Forms.ToolStripMenuItem menuSair;

        private System.Windows.Forms.Panel panelConteudo;

        // livros
        private System.Windows.Forms.Panel panelLivros;
        private System.Windows.Forms.DataGridView dgvLivros;
        private System.Windows.Forms.TextBox txtLivroTitulo;
        private System.Windows.Forms.ComboBox cmbLivroAutor;
        private System.Windows.Forms.TextBox txtLivroCategoria;
        private System.Windows.Forms.TextBox txtLivroPreco;
        private System.Windows.Forms.TextBox txtLivroEstoque;
        private System.Windows.Forms.Button btnLivroAdicionar;
        private System.Windows.Forms.Button btnLivroEditar;
        private System.Windows.Forms.Button btnLivroExcluir;
        private System.Windows.Forms.Button btnLivroAtualizar;

        // autores
        private System.Windows.Forms.Panel panelAutores;
        private System.Windows.Forms.DataGridView dgvAutores;
        private System.Windows.Forms.TextBox txtAutorNome;
        private System.Windows.Forms.Button btnAutorAdicionar;
        private System.Windows.Forms.Button btnAutorExcluir;
        private System.Windows.Forms.Button btnAutorAtualizar;

        // estoque
        private System.Windows.Forms.Panel panelEstoque;
        private System.Windows.Forms.ComboBox cmbEstoqueLivro;
        private System.Windows.Forms.NumericUpDown nudEstoqueQtd;
        private System.Windows.Forms.Button btnEstoqueEntrada;
        private System.Windows.Forms.Button btnEstoqueSaida;
        private System.Windows.Forms.Button btnEstoqueAtualizar;

        // vendas
        private System.Windows.Forms.Panel panelVendas;
        private System.Windows.Forms.ComboBox cmbVendaLivro;
        private System.Windows.Forms.NumericUpDown nudVendaQtd;
        private System.Windows.Forms.Button btnRegistrarVenda;
        private System.Windows.Forms.DataGridView dgvVendas;

        // relatorios
        private System.Windows.Forms.Panel panelRelatorios;
        private System.Windows.Forms.DataGridView dgvRelatorios;
        private System.Windows.Forms.Button btnRelTop;
        private System.Windows.Forms.Button btnRelEstoqueBaixo;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Colors
            var corFundo = System.Drawing.ColorTranslator.FromHtml("#CFCFCF");
            var corAzul = System.Drawing.ColorTranslator.FromHtml("#072E6A");
            var corBranco = System.Drawing.Color.White;

            // menuStrip
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuCadastros = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLivros = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAutores = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConsultas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEstoque = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVendas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRelatorios = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSair = new System.Windows.Forms.ToolStripMenuItem();

            this.panelConteudo = new System.Windows.Forms.Panel();

            // livros
            this.panelLivros = new System.Windows.Forms.Panel();
            this.dgvLivros = new System.Windows.Forms.DataGridView();
            this.txtLivroTitulo = new System.Windows.Forms.TextBox();
            this.cmbLivroAutor = new System.Windows.Forms.ComboBox();
            this.txtLivroCategoria = new System.Windows.Forms.TextBox();
            this.txtLivroPreco = new System.Windows.Forms.TextBox();
            this.txtLivroEstoque = new System.Windows.Forms.TextBox();
            this.btnLivroAdicionar = new System.Windows.Forms.Button();
            this.btnLivroEditar = new System.Windows.Forms.Button();
            this.btnLivroExcluir = new System.Windows.Forms.Button();
            this.btnLivroAtualizar = new System.Windows.Forms.Button();

            // autores
            this.panelAutores = new System.Windows.Forms.Panel();
            this.dgvAutores = new System.Windows.Forms.DataGridView();
            this.txtAutorNome = new System.Windows.Forms.TextBox();
            this.btnAutorAdicionar = new System.Windows.Forms.Button();
            this.btnAutorExcluir = new System.Windows.Forms.Button();
            this.btnAutorAtualizar = new System.Windows.Forms.Button();

            // estoque
            this.panelEstoque = new System.Windows.Forms.Panel();
            this.cmbEstoqueLivro = new System.Windows.Forms.ComboBox();
            this.nudEstoqueQtd = new System.Windows.Forms.NumericUpDown();
            this.btnEstoqueEntrada = new System.Windows.Forms.Button();
            this.btnEstoqueSaida = new System.Windows.Forms.Button();
            this.btnEstoqueAtualizar = new System.Windows.Forms.Button();

            // vendas
            this.panelVendas = new System.Windows.Forms.Panel();
            this.cmbVendaLivro = new System.Windows.Forms.ComboBox();
            this.nudVendaQtd = new System.Windows.Forms.NumericUpDown();
            this.btnRegistrarVenda = new System.Windows.Forms.Button();
            this.dgvVendas = new System.Windows.Forms.DataGridView();

            // relatorios
            this.panelRelatorios = new System.Windows.Forms.Panel();
            this.dgvRelatorios = new System.Windows.Forms.DataGridView();
            this.btnRelTop = new System.Windows.Forms.Button();
            this.btnRelEstoqueBaixo = new System.Windows.Forms.Button();

            // Menu structure
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuCadastros,
                this.menuConsultas,
                this.menuRelatorios,
                this.menuSair
            });
            this.menuCadastros.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuLivros,
                this.menuAutores
            });
            this.menuConsultas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuEstoque,
                this.menuVendas
            });

            this.menuCadastros.Text = "Cadastros";
            this.menuLivros.Text = "Livros";
            this.menuAutores.Text = "Autores";
            this.menuConsultas.Text = "Consultas";
            this.menuEstoque.Text = "Estoque";
            this.menuVendas.Text = "Vendas";
            this.menuRelatorios.Text = "Relatórios";
            this.menuSair.Text = "Sair";

            // apply menu colors
            this.menuStrip1.BackColor = corAzul;
            this.menuStrip1.ForeColor = corBranco;
            this.menuStrip1.RenderMode = ToolStripRenderMode.Professional;

            // panelConteudo
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(0, 28);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Size = new System.Drawing.Size(1100, 620);
            this.panelConteudo.BackColor = corFundo;

            // form base
            this.BackColor = corFundo;
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema TWS - Editora";

            // -------------------- LIVROS PANEL --------------------
            this.panelLivros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLivros.Visible = false;
            this.panelLivros.BackColor = corFundo;

            // inputs
            var txtStyleWidth = 300;
            this.txtLivroTitulo.Location = new System.Drawing.Point(20, 18);
            this.txtLivroTitulo.Width = txtStyleWidth;
            this.txtLivroTitulo.Name = "txtLivroTitulo";

            this.cmbLivroAutor.Location = new System.Drawing.Point(340, 18);
            this.cmbLivroAutor.Width = 220;
            this.cmbLivroAutor.Name = "cmbLivroAutor";

            this.txtLivroCategoria.Location = new System.Drawing.Point(570, 18);
            this.txtLivroCategoria.Width = 180;
            this.txtLivroCategoria.Name = "txtLivroCategoria";

            this.txtLivroPreco.Location = new System.Drawing.Point(20, 56);
            this.txtLivroPreco.Width = 120;
            this.txtLivroPreco.Name = "txtLivroPreco";

            this.txtLivroEstoque.Location = new System.Drawing.Point(160, 56);
            this.txtLivroEstoque.Width = 120;
            this.txtLivroEstoque.Name = "txtLivroEstoque";

            // buttons styled
            ApplyPrimaryButtonStyle(this.btnLivroAdicionar, "Adicionar", 300, 54, corAzul, corBranco);
            ApplyPrimaryButtonStyle(this.btnLivroEditar, "Editar", 410, 54, corAzul, corBranco);
            ApplyPrimaryButtonStyle(this.btnLivroExcluir, "Excluir", 520, 54, corAzul, corBranco);
            ApplyPrimaryButtonStyle(this.btnLivroAtualizar, "Atualizar", 640, 54, corAzul, corBranco);

            // datagrid livros
            this.dgvLivros.Location = new System.Drawing.Point(20, 100);
            this.dgvLivros.Size = new System.Drawing.Size(980, 480);
            this.dgvLivros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                  | System.Windows.Forms.AnchorStyles.Left)
                                  | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLivros.ReadOnly = true;
            this.dgvLivros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            StyleDataGridView(this.dgvLivros, corAzul);

            this.panelLivros.Controls.Add(this.txtLivroTitulo);
            this.panelLivros.Controls.Add(this.cmbLivroAutor);
            this.panelLivros.Controls.Add(this.txtLivroCategoria);
            this.panelLivros.Controls.Add(this.txtLivroPreco);
            this.panelLivros.Controls.Add(this.txtLivroEstoque);
            this.panelLivros.Controls.Add(this.btnLivroAdicionar);
            this.panelLivros.Controls.Add(this.btnLivroEditar);
            this.panelLivros.Controls.Add(this.btnLivroExcluir);
            this.panelLivros.Controls.Add(this.btnLivroAtualizar);
            this.panelLivros.Controls.Add(this.dgvLivros);

            // -------------------- AUTORES PANEL --------------------
            this.panelAutores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAutores.Visible = false;
            this.panelAutores.BackColor = corFundo;

            this.txtAutorNome.Location = new System.Drawing.Point(20, 18);
            this.txtAutorNome.Width = 420;
            this.txtAutorNome.Name = "txtAutorNome";

            ApplyPrimaryButtonStyle(this.btnAutorAdicionar, "Adicionar", 460, 16, corAzul, corBranco);
            ApplyPrimaryButtonStyle(this.btnAutorExcluir, "Excluir", 580, 16, corAzul, corBranco);
            ApplyPrimaryButtonStyle(this.btnAutorAtualizar, "Atualizar", 700, 16, corAzul, corBranco);

            this.dgvAutores.Location = new System.Drawing.Point(20, 60);
            this.dgvAutores.Size = new System.Drawing.Size(980, 520);
            this.dgvAutores.ReadOnly = true;
            this.dgvAutores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            StyleDataGridView(this.dgvAutores, corAzul);

            this.panelAutores.Controls.Add(this.txtAutorNome);
            this.panelAutores.Controls.Add(this.btnAutorAdicionar);
            this.panelAutores.Controls.Add(this.btnAutorExcluir);
            this.panelAutores.Controls.Add(this.btnAutorAtualizar);
            this.panelAutores.Controls.Add(this.dgvAutores);

            // -------------------- ESTOQUE PANEL --------------------
            this.panelEstoque.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEstoque.Visible = false;
            this.panelEstoque.BackColor = corFundo;

            this.cmbEstoqueLivro.Location = new System.Drawing.Point(20, 18);
            this.cmbEstoqueLivro.Width = 520;
            this.cmbEstoqueLivro.Name = "cmbEstoqueLivro";

            this.nudEstoqueQtd.Location = new System.Drawing.Point(560, 18);
            this.nudEstoqueQtd.Width = 80;

            ApplyPrimaryButtonStyle(this.btnEstoqueEntrada, "Entrada", 660, 16, corAzul, corBranco);
            ApplyPrimaryButtonStyle(this.btnEstoqueSaida, "Saída", 780, 16, corAzul, corBranco);
            ApplyPrimaryButtonStyle(this.btnEstoqueAtualizar, "Atualizar", 900, 16, corAzul, corBranco);

            this.panelEstoque.Controls.Add(this.cmbEstoqueLivro);
            this.panelEstoque.Controls.Add(this.nudEstoqueQtd);
            this.panelEstoque.Controls.Add(this.btnEstoqueEntrada);
            this.panelEstoque.Controls.Add(this.btnEstoqueSaida);
            this.panelEstoque.Controls.Add(this.btnEstoqueAtualizar);

            // -------------------- VENDAS PANEL --------------------
            this.panelVendas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVendas.Visible = false;
            this.panelVendas.BackColor = corFundo;

            this.cmbVendaLivro.Location = new System.Drawing.Point(20, 18);
            this.cmbVendaLivro.Width = 520;
            this.cmbVendaLivro.Name = "cmbVendaLivro";

            this.nudVendaQtd.Location = new System.Drawing.Point(560, 18);
            this.nudVendaQtd.Width = 80;
            this.nudVendaQtd.Minimum = 1;
            this.nudVendaQtd.Value = 1;

            ApplyPrimaryButtonStyle(this.btnRegistrarVenda, "Registrar Venda", 660, 16, corAzul, corBranco);

            this.dgvVendas.Location = new System.Drawing.Point(20, 60);
            this.dgvVendas.Size = new System.Drawing.Size(980, 520);
            this.dgvVendas.ReadOnly = true;
            this.dgvVendas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            StyleDataGridView(this.dgvVendas, corAzul);

            this.panelVendas.Controls.Add(this.cmbVendaLivro);
            this.panelVendas.Controls.Add(this.nudVendaQtd);
            this.panelVendas.Controls.Add(this.btnRegistrarVenda);
            this.panelVendas.Controls.Add(this.dgvVendas);

            // -------------------- RELATÓRIOS PANEL --------------------
            this.panelRelatorios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRelatorios.Visible = false;
            this.panelRelatorios.BackColor = corFundo;

            ApplyPrimaryButtonStyle(this.btnRelTop, "Top vendidos", 20, 18, corAzul, corBranco);
            ApplyPrimaryButtonStyle(this.btnRelEstoqueBaixo, "Estoque baixo", 160, 18, corAzul, corBranco);

            this.dgvRelatorios.Location = new System.Drawing.Point(20, 60);
            this.dgvRelatorios.Size = new System.Drawing.Size(980, 520);
            this.dgvRelatorios.ReadOnly = true;
            this.dgvRelatorios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            StyleDataGridView(this.dgvRelatorios, corAzul);

            this.panelRelatorios.Controls.Add(this.btnRelTop);
            this.panelRelatorios.Controls.Add(this.btnRelEstoqueBaixo);
            this.panelRelatorios.Controls.Add(this.dgvRelatorios);

            // Add panels to panelConteudo
            this.panelConteudo.Controls.Add(this.panelLivros);
            this.panelConteudo.Controls.Add(this.panelAutores);
            this.panelConteudo.Controls.Add(this.panelEstoque);
            this.panelConteudo.Controls.Add(this.panelVendas);
            this.panelConteudo.Controls.Add(this.panelRelatorios);

            // Add to form
            this.Controls.Add(this.panelConteudo);
            this.Controls.Add(this.menuStrip1);

            this.MainMenuStrip = this.menuStrip1;
        }

        #endregion

        // small helper methods used in InitializeComponent to keep designer tidy
        private void ApplyPrimaryButtonStyle(Button btn, string text, int x, int y, System.Drawing.Color bg, System.Drawing.Color fg)
        {
            btn.Location = new System.Drawing.Point(x, y);
            btn.Text = text;
            btn.Width = 120;
            btn.Height = 36;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bg;
            btn.ForeColor = fg;
            btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            // hover effect via events (added in code file if needed)
        }

        private void StyleDataGridView(DataGridView dgv, System.Drawing.Color headerColor)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = headerColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = System.Drawing.Color.White;
            dgv.BorderStyle = BorderStyle.FixedSingle;
            dgv.GridColor = System.Drawing.Color.LightGray;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
        }
    }
}
