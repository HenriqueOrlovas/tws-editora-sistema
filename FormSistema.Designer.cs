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
            this.panelLivros = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtLivroTitulo = new System.Windows.Forms.TextBox();
            this.cmbLivroAutor = new System.Windows.Forms.ComboBox();
            this.txtLivroCategoria = new System.Windows.Forms.TextBox();
            this.txtLivroPreco = new System.Windows.Forms.TextBox();
            this.txtLivroEstoque = new System.Windows.Forms.TextBox();
            this.btnLivroAdicionar = new System.Windows.Forms.Button();
            this.btnLivroEditar = new System.Windows.Forms.Button();
            this.btnLivroExcluir = new System.Windows.Forms.Button();
            this.btnLivroAtualizar = new System.Windows.Forms.Button();
            this.dgvLivros = new System.Windows.Forms.DataGridView();
            this.panelAutores = new System.Windows.Forms.Panel();
            this.txtAutorNome = new System.Windows.Forms.TextBox();
            this.btnAutorAdicionar = new System.Windows.Forms.Button();
            this.btnAutorExcluir = new System.Windows.Forms.Button();
            this.btnAutorAtualizar = new System.Windows.Forms.Button();
            this.dgvAutores = new System.Windows.Forms.DataGridView();
            this.panelEstoque = new System.Windows.Forms.Panel();
            this.cmbEstoqueLivro = new System.Windows.Forms.ComboBox();
            this.nudEstoqueQtd = new System.Windows.Forms.NumericUpDown();
            this.btnEstoqueEntrada = new System.Windows.Forms.Button();
            this.btnEstoqueSaida = new System.Windows.Forms.Button();
            this.btnEstoqueAtualizar = new System.Windows.Forms.Button();
            this.panelVendas = new System.Windows.Forms.Panel();
            this.cmbVendaLivro = new System.Windows.Forms.ComboBox();
            this.nudVendaQtd = new System.Windows.Forms.NumericUpDown();
            this.btnRegistrarVenda = new System.Windows.Forms.Button();
            this.dgvVendas = new System.Windows.Forms.DataGridView();
            this.panelRelatorios = new System.Windows.Forms.Panel();
            this.btnRelTop = new System.Windows.Forms.Button();
            this.btnRelEstoqueBaixo = new System.Windows.Forms.Button();
            this.dgvRelatorios = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            this.panelLivros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLivros)).BeginInit();
            this.panelAutores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAutores)).BeginInit();
            this.panelEstoque.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEstoqueQtd)).BeginInit();
            this.panelVendas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVendaQtd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendas)).BeginInit();
            this.panelRelatorios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelatorios)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(46)))), ((int)(((byte)(106)))));
            this.menuStrip1.ForeColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCadastros,
            this.menuConsultas,
            this.menuRelatorios,
            this.menuSair});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(1100, 24);
            this.menuStrip1.TabIndex = 1;
            // 
            // menuCadastros
            // 
            this.menuCadastros.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLivros,
            this.menuAutores});
            this.menuCadastros.Name = "menuCadastros";
            this.menuCadastros.Size = new System.Drawing.Size(71, 20);
            this.menuCadastros.Text = "Cadastros";
            // 
            // menuLivros
            // 
            this.menuLivros.Name = "menuLivros";
            this.menuLivros.Size = new System.Drawing.Size(115, 22);
            this.menuLivros.Text = "Livros";
            // 
            // menuAutores
            // 
            this.menuAutores.Name = "menuAutores";
            this.menuAutores.Size = new System.Drawing.Size(115, 22);
            this.menuAutores.Text = "Autores";
            // 
            // menuConsultas
            // 
            this.menuConsultas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEstoque,
            this.menuVendas});
            this.menuConsultas.Name = "menuConsultas";
            this.menuConsultas.Size = new System.Drawing.Size(71, 20);
            this.menuConsultas.Text = "Consultas";
            // 
            // menuEstoque
            // 
            this.menuEstoque.Name = "menuEstoque";
            this.menuEstoque.Size = new System.Drawing.Size(116, 22);
            this.menuEstoque.Text = "Estoque";
            // 
            // menuVendas
            // 
            this.menuVendas.Name = "menuVendas";
            this.menuVendas.Size = new System.Drawing.Size(116, 22);
            this.menuVendas.Text = "Vendas";
            // 
            // menuRelatorios
            // 
            this.menuRelatorios.Name = "menuRelatorios";
            this.menuRelatorios.Size = new System.Drawing.Size(71, 20);
            this.menuRelatorios.Text = "Relatórios";
            // 
            // menuSair
            // 
            this.menuSair.Name = "menuSair";
            this.menuSair.Size = new System.Drawing.Size(38, 20);
            this.menuSair.Text = "Sair";
            // 
            // panelConteudo
            // 
            this.panelConteudo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.panelConteudo.Controls.Add(this.panelLivros);
            this.panelConteudo.Controls.Add(this.panelAutores);
            this.panelConteudo.Controls.Add(this.panelEstoque);
            this.panelConteudo.Controls.Add(this.panelVendas);
            this.panelConteudo.Controls.Add(this.panelRelatorios);
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(0, 24);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Size = new System.Drawing.Size(1100, 626);
            this.panelConteudo.TabIndex = 0;
            // 
            // panelLivros
            // 
            this.panelLivros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.panelLivros.Controls.Add(this.pictureBox1);
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
            this.panelLivros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLivros.Location = new System.Drawing.Point(0, 0);
            this.panelLivros.Name = "panelLivros";
            this.panelLivros.Size = new System.Drawing.Size(1100, 626);
            this.panelLivros.TabIndex = 0;
            this.panelLivros.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::tws_sistema.Properties.Resources.slogan_Tws1;
            this.pictureBox1.Location = new System.Drawing.Point(1006, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(74, 76);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // txtLivroTitulo
            // 
            this.txtLivroTitulo.Location = new System.Drawing.Point(20, 18);
            this.txtLivroTitulo.Name = "txtLivroTitulo";
            this.txtLivroTitulo.Size = new System.Drawing.Size(300, 20);
            this.txtLivroTitulo.TabIndex = 0;
            // 
            // cmbLivroAutor
            // 
            this.cmbLivroAutor.Location = new System.Drawing.Point(340, 18);
            this.cmbLivroAutor.Name = "cmbLivroAutor";
            this.cmbLivroAutor.Size = new System.Drawing.Size(220, 21);
            this.cmbLivroAutor.TabIndex = 1;
            // 
            // txtLivroCategoria
            // 
            this.txtLivroCategoria.Location = new System.Drawing.Point(570, 18);
            this.txtLivroCategoria.Name = "txtLivroCategoria";
            this.txtLivroCategoria.Size = new System.Drawing.Size(180, 20);
            this.txtLivroCategoria.TabIndex = 2;
            // 
            // txtLivroPreco
            // 
            this.txtLivroPreco.Location = new System.Drawing.Point(20, 56);
            this.txtLivroPreco.Name = "txtLivroPreco";
            this.txtLivroPreco.Size = new System.Drawing.Size(120, 20);
            this.txtLivroPreco.TabIndex = 3;
            // 
            // txtLivroEstoque
            // 
            this.txtLivroEstoque.Location = new System.Drawing.Point(160, 56);
            this.txtLivroEstoque.Name = "txtLivroEstoque";
            this.txtLivroEstoque.Size = new System.Drawing.Size(120, 20);
            this.txtLivroEstoque.TabIndex = 4;
            // 
            // btnLivroAdicionar
            // 
            this.btnLivroAdicionar.Location = new System.Drawing.Point(0, 0);
            this.btnLivroAdicionar.Name = "btnLivroAdicionar";
            this.btnLivroAdicionar.Size = new System.Drawing.Size(75, 23);
            this.btnLivroAdicionar.TabIndex = 5;
            // 
            // btnLivroEditar
            // 
            this.btnLivroEditar.Location = new System.Drawing.Point(0, 0);
            this.btnLivroEditar.Name = "btnLivroEditar";
            this.btnLivroEditar.Size = new System.Drawing.Size(75, 23);
            this.btnLivroEditar.TabIndex = 6;
            // 
            // btnLivroExcluir
            // 
            this.btnLivroExcluir.Location = new System.Drawing.Point(0, 0);
            this.btnLivroExcluir.Name = "btnLivroExcluir";
            this.btnLivroExcluir.Size = new System.Drawing.Size(75, 23);
            this.btnLivroExcluir.TabIndex = 7;
            // 
            // btnLivroAtualizar
            // 
            this.btnLivroAtualizar.Location = new System.Drawing.Point(0, 0);
            this.btnLivroAtualizar.Name = "btnLivroAtualizar";
            this.btnLivroAtualizar.Size = new System.Drawing.Size(75, 23);
            this.btnLivroAtualizar.TabIndex = 8;
            // 
            // dgvLivros
            // 
            this.dgvLivros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLivros.Location = new System.Drawing.Point(20, 100);
            this.dgvLivros.Name = "dgvLivros";
            this.dgvLivros.ReadOnly = true;
            this.dgvLivros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLivros.Size = new System.Drawing.Size(1880, 1006);
            this.dgvLivros.TabIndex = 9;
            // 
            // panelAutores
            // 
            this.panelAutores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.panelAutores.Controls.Add(this.txtAutorNome);
            this.panelAutores.Controls.Add(this.btnAutorAdicionar);
            this.panelAutores.Controls.Add(this.btnAutorExcluir);
            this.panelAutores.Controls.Add(this.btnAutorAtualizar);
            this.panelAutores.Controls.Add(this.dgvAutores);
            this.panelAutores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAutores.Location = new System.Drawing.Point(0, 0);
            this.panelAutores.Name = "panelAutores";
            this.panelAutores.Size = new System.Drawing.Size(1100, 626);
            this.panelAutores.TabIndex = 1;
            this.panelAutores.Visible = false;
            // 
            // txtAutorNome
            // 
            this.txtAutorNome.Location = new System.Drawing.Point(20, 18);
            this.txtAutorNome.Name = "txtAutorNome";
            this.txtAutorNome.Size = new System.Drawing.Size(420, 20);
            this.txtAutorNome.TabIndex = 0;
            // 
            // btnAutorAdicionar
            // 
            this.btnAutorAdicionar.Location = new System.Drawing.Point(0, 0);
            this.btnAutorAdicionar.Name = "btnAutorAdicionar";
            this.btnAutorAdicionar.Size = new System.Drawing.Size(75, 23);
            this.btnAutorAdicionar.TabIndex = 1;
            // 
            // btnAutorExcluir
            // 
            this.btnAutorExcluir.Location = new System.Drawing.Point(0, 0);
            this.btnAutorExcluir.Name = "btnAutorExcluir";
            this.btnAutorExcluir.Size = new System.Drawing.Size(75, 23);
            this.btnAutorExcluir.TabIndex = 2;
            // 
            // btnAutorAtualizar
            // 
            this.btnAutorAtualizar.Location = new System.Drawing.Point(0, 0);
            this.btnAutorAtualizar.Name = "btnAutorAtualizar";
            this.btnAutorAtualizar.Size = new System.Drawing.Size(75, 23);
            this.btnAutorAtualizar.TabIndex = 3;
            // 
            // dgvAutores
            // 
            this.dgvAutores.Location = new System.Drawing.Point(20, 60);
            this.dgvAutores.Name = "dgvAutores";
            this.dgvAutores.ReadOnly = true;
            this.dgvAutores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAutores.Size = new System.Drawing.Size(980, 520);
            this.dgvAutores.TabIndex = 4;
            // 
            // panelEstoque
            // 
            this.panelEstoque.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.panelEstoque.Controls.Add(this.cmbEstoqueLivro);
            this.panelEstoque.Controls.Add(this.nudEstoqueQtd);
            this.panelEstoque.Controls.Add(this.btnEstoqueEntrada);
            this.panelEstoque.Controls.Add(this.btnEstoqueSaida);
            this.panelEstoque.Controls.Add(this.btnEstoqueAtualizar);
            this.panelEstoque.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEstoque.Location = new System.Drawing.Point(0, 0);
            this.panelEstoque.Name = "panelEstoque";
            this.panelEstoque.Size = new System.Drawing.Size(1100, 626);
            this.panelEstoque.TabIndex = 2;
            this.panelEstoque.Visible = false;
            // 
            // cmbEstoqueLivro
            // 
            this.cmbEstoqueLivro.Location = new System.Drawing.Point(20, 18);
            this.cmbEstoqueLivro.Name = "cmbEstoqueLivro";
            this.cmbEstoqueLivro.Size = new System.Drawing.Size(520, 21);
            this.cmbEstoqueLivro.TabIndex = 0;
            // 
            // nudEstoqueQtd
            // 
            this.nudEstoqueQtd.Location = new System.Drawing.Point(560, 18);
            this.nudEstoqueQtd.Name = "nudEstoqueQtd";
            this.nudEstoqueQtd.Size = new System.Drawing.Size(80, 20);
            this.nudEstoqueQtd.TabIndex = 1;
            // 
            // btnEstoqueEntrada
            // 
            this.btnEstoqueEntrada.Location = new System.Drawing.Point(0, 0);
            this.btnEstoqueEntrada.Name = "btnEstoqueEntrada";
            this.btnEstoqueEntrada.Size = new System.Drawing.Size(75, 23);
            this.btnEstoqueEntrada.TabIndex = 2;
            // 
            // btnEstoqueSaida
            // 
            this.btnEstoqueSaida.Location = new System.Drawing.Point(0, 0);
            this.btnEstoqueSaida.Name = "btnEstoqueSaida";
            this.btnEstoqueSaida.Size = new System.Drawing.Size(75, 23);
            this.btnEstoqueSaida.TabIndex = 3;
            // 
            // btnEstoqueAtualizar
            // 
            this.btnEstoqueAtualizar.Location = new System.Drawing.Point(0, 0);
            this.btnEstoqueAtualizar.Name = "btnEstoqueAtualizar";
            this.btnEstoqueAtualizar.Size = new System.Drawing.Size(75, 23);
            this.btnEstoqueAtualizar.TabIndex = 4;
            // 
            // panelVendas
            // 
            this.panelVendas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.panelVendas.Controls.Add(this.cmbVendaLivro);
            this.panelVendas.Controls.Add(this.nudVendaQtd);
            this.panelVendas.Controls.Add(this.btnRegistrarVenda);
            this.panelVendas.Controls.Add(this.dgvVendas);
            this.panelVendas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVendas.Location = new System.Drawing.Point(0, 0);
            this.panelVendas.Name = "panelVendas";
            this.panelVendas.Size = new System.Drawing.Size(1100, 626);
            this.panelVendas.TabIndex = 3;
            this.panelVendas.Visible = false;
            // 
            // cmbVendaLivro
            // 
            this.cmbVendaLivro.Location = new System.Drawing.Point(20, 18);
            this.cmbVendaLivro.Name = "cmbVendaLivro";
            this.cmbVendaLivro.Size = new System.Drawing.Size(520, 21);
            this.cmbVendaLivro.TabIndex = 0;
            // 
            // nudVendaQtd
            // 
            this.nudVendaQtd.Location = new System.Drawing.Point(560, 18);
            this.nudVendaQtd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVendaQtd.Name = "nudVendaQtd";
            this.nudVendaQtd.Size = new System.Drawing.Size(80, 20);
            this.nudVendaQtd.TabIndex = 1;
            this.nudVendaQtd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnRegistrarVenda
            // 
            this.btnRegistrarVenda.Location = new System.Drawing.Point(0, 0);
            this.btnRegistrarVenda.Name = "btnRegistrarVenda";
            this.btnRegistrarVenda.Size = new System.Drawing.Size(75, 23);
            this.btnRegistrarVenda.TabIndex = 2;
            // 
            // dgvVendas
            // 
            this.dgvVendas.Location = new System.Drawing.Point(20, 60);
            this.dgvVendas.Name = "dgvVendas";
            this.dgvVendas.ReadOnly = true;
            this.dgvVendas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVendas.Size = new System.Drawing.Size(980, 520);
            this.dgvVendas.TabIndex = 3;
            // 
            // panelRelatorios
            // 
            this.panelRelatorios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.panelRelatorios.Controls.Add(this.btnRelTop);
            this.panelRelatorios.Controls.Add(this.btnRelEstoqueBaixo);
            this.panelRelatorios.Controls.Add(this.dgvRelatorios);
            this.panelRelatorios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRelatorios.Location = new System.Drawing.Point(0, 0);
            this.panelRelatorios.Name = "panelRelatorios";
            this.panelRelatorios.Size = new System.Drawing.Size(1100, 626);
            this.panelRelatorios.TabIndex = 4;
            this.panelRelatorios.Visible = false;
            // 
            // btnRelTop
            // 
            this.btnRelTop.Location = new System.Drawing.Point(0, 0);
            this.btnRelTop.Name = "btnRelTop";
            this.btnRelTop.Size = new System.Drawing.Size(75, 23);
            this.btnRelTop.TabIndex = 0;
            // 
            // btnRelEstoqueBaixo
            // 
            this.btnRelEstoqueBaixo.Location = new System.Drawing.Point(0, 0);
            this.btnRelEstoqueBaixo.Name = "btnRelEstoqueBaixo";
            this.btnRelEstoqueBaixo.Size = new System.Drawing.Size(75, 23);
            this.btnRelEstoqueBaixo.TabIndex = 1;
            // 
            // dgvRelatorios
            // 
            this.dgvRelatorios.Location = new System.Drawing.Point(20, 60);
            this.dgvRelatorios.Name = "dgvRelatorios";
            this.dgvRelatorios.ReadOnly = true;
            this.dgvRelatorios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRelatorios.Size = new System.Drawing.Size(980, 520);
            this.dgvRelatorios.TabIndex = 2;
            // 
            // FormSistema
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(207)))), ((int)(((byte)(207)))));
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.panelConteudo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormSistema";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema TWS - Editora";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelConteudo.ResumeLayout(false);
            this.panelLivros.ResumeLayout(false);
            this.panelLivros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLivros)).EndInit();
            this.panelAutores.ResumeLayout(false);
            this.panelAutores.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAutores)).EndInit();
            this.panelEstoque.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudEstoqueQtd)).EndInit();
            this.panelVendas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudVendaQtd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendas)).EndInit();
            this.panelRelatorios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelatorios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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

        private PictureBox pictureBox1;
    }
}
