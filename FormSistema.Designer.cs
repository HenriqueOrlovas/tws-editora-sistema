// FormSistema.Designer.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sistema_tws
{
    partial class FormSistema
    {
        private IContainer components = null;

        // Form controls
        private TabControl tabControlMain;
        private TabPage tabHome;
        private TabPage tabLivros;
        private TabPage tabAutores;
        private TabPage tabEstoque;
        private TabPage tabVendas;
        private TabPage tabCupons;
        private TabPage tabRelatorios;

        // Home
        private Label lblHomeTitle;

        // Livros controls
        private DataGridView dgvLivros;
        private TextBox txtLivroTitulo;
        private TextBox txtLivroCategoria;
        private TextBox txtLivroPreco;
        private ComboBox cmbLivroFormato;
        private ComboBox cmbLivroAutor;
        private NumericUpDown nudLivroEstoque;
        private Button btnLivroAdd;
        private Button btnLivroEdit;
        private Button btnLivroDelete;
        private Button btnLivroRefresh;

        // Autores controls
        private DataGridView dgvAutores;
        private TextBox txtAutorNome;
        private TextBox txtAutorNacionalidade;
        private TextBox txtAutorEmail;
        private Button btnAutorAdd;
        private Button btnAutorEdit;
        private Button btnAutorDelete;
        private Button btnAutorRefresh;

        // Estoque controls
        private ComboBox cmbEstoqueLivro;
        private NumericUpDown nudEstoqueQtd;
        private Button btnEstoqueEntrada;
        private Button btnEstoqueSaida;
        private Button btnEstoqueRefresh;
        private Label lblEstoqueTitle;

        // Vendas controls
        private ComboBox cmbVendaLivro;
        private NumericUpDown nudVendaQtd;
        private Button btnVendaAdicionarItem;
        private Button btnVendaConcluir;
        private DataGridView dgvVendaCarrinho;
        private DataGridView dgvVendasHistorico;
        private Label lblVendasTitle;

        // Cupons controls
        private DataGridView dgvCupons;
        private TextBox txtCupomCodigo;
        private NumericUpDown nudCupomDesconto;
        private DateTimePicker dtpCupomValidade;
        private NumericUpDown nudCupomUsoMax;
        private Button btnCupomAdd;
        private Button btnCupomDelete;
        private Button btnCupomRefresh;

        // Relatórios controls
        private DataGridView dgvRelatorios;
        private Button btnRelTopVendidos;
        private Button btnRelEstoqueBaixo;

        // Colors
        private Color colorBackground = ColorTranslator.FromHtml("#cfcfcf");
        private Color colorPrimary = ColorTranslator.FromHtml("#072E6A"); // Azul-escuro pedido

        // P/ rounded corners
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();

            // Form
            this.Text = "Sistema TWS - Editora";
            this.ClientSize = new Size(1200, 760);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = colorBackground;
            this.Font = new Font("Segoe UI", 9F);

            // TabControl
            tabControlMain = new TabControl()
            {
                Dock = DockStyle.Fill,
                Padding = new Point(12, 6)
            };

            tabHome = new TabPage("Home");
            tabLivros = new TabPage("Livros");
            tabAutores = new TabPage("Autores");
            tabEstoque = new TabPage("Estoque");
            tabVendas = new TabPage("Vendas");
            tabCupons = new TabPage("Cupons");
            tabRelatorios = new TabPage("Relatórios");

            tabControlMain.TabPages.AddRange(new TabPage[] {
                tabHome, tabLivros, tabAutores, tabEstoque, tabVendas, tabCupons, tabRelatorios
            });

            this.Controls.Add(tabControlMain);

            // ---------------- HOME ----------------
            lblHomeTitle = new Label()
            {
                Text = "Bem-vindo ao Painel Administrativo - Editora TWS",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = colorPrimary,
                AutoSize = true,
                Left = 24,
                Top = 24
            };
            tabHome.Controls.Add(lblHomeTitle);

            // ---------------- LIVROS ----------------
            dgvLivros = CreateGrid(new Rectangle(24, 190, 1120, 480));
            txtLivroTitulo = CreateTextBox(new Rectangle(24, 24, 320, 30), "Título do livro");
            txtLivroCategoria = CreateTextBox(new Rectangle(360, 24, 240, 30), "Categoria");
            txtLivroPreco = CreateTextBox(new Rectangle(620, 24, 140, 30), "Preço (ex: 39.90)");
            cmbLivroFormato = new ComboBox() { Left = 780, Top = 24, Width = 140, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbLivroFormato.Items.AddRange(new string[] { "fisico", "ebook", "ambos" });
            cmbLivroFormato.SelectedIndex = 2;
            cmbLivroAutor = new ComboBox() { Left = 930, Top = 24, Width = 214, DropDownStyle = ComboBoxStyle.DropDownList };
            nudLivroEstoque = new NumericUpDown() { Left = 24, Top = 64, Width = 120, Minimum = 0, Maximum = 100000, Value = 0 };

            btnLivroAdd = CreateActionButton("Adicionar Livro", new Rectangle(160, 64, 140, 34));
            btnLivroEdit = CreateActionButton("Editar Selecionado", new Rectangle(320, 64, 140, 34));
            btnLivroDelete = CreateActionButton("Excluir Selecionado", new Rectangle(480, 64, 140, 34));
            btnLivroRefresh = CreateActionButton("Atualizar Lista", new Rectangle(640, 64, 140, 34));

            tabLivros.Controls.AddRange(new Control[] {
                txtLivroTitulo, txtLivroCategoria, txtLivroPreco, cmbLivroFormato, cmbLivroAutor, nudLivroEstoque,
                btnLivroAdd, btnLivroEdit, btnLivroDelete, btnLivroRefresh, dgvLivros
            });

            // ---------------- AUTORES ----------------
            dgvAutores = CreateGrid(new Rectangle(24, 150, 1120, 520));
            txtAutorNome = CreateTextBox(new Rectangle(24, 24, 360, 30), "Nome completo do autor");
            txtAutorNacionalidade = CreateTextBox(new Rectangle(400, 24, 200, 30), "Nacionalidade");
            txtAutorEmail = CreateTextBox(new Rectangle(620, 24, 300, 30), "E-mail");

            btnAutorAdd = CreateActionButton("Adicionar Autor", new Rectangle(24, 64, 140, 34));
            btnAutorEdit = CreateActionButton("Editar Selecionado", new Rectangle(184, 64, 140, 34));
            btnAutorDelete = CreateActionButton("Excluir Selecionado", new Rectangle(344, 64, 140, 34));
            btnAutorRefresh = CreateActionButton("Atualizar Lista", new Rectangle(504, 64, 140, 34));

            tabAutores.Controls.AddRange(new Control[] {
                txtAutorNome, txtAutorNacionalidade, txtAutorEmail,
                btnAutorAdd, btnAutorEdit, btnAutorDelete, btnAutorRefresh, dgvAutores
            });

            // ---------------- ESTOQUE ----------------
            lblEstoqueTitle = new Label() { Text = "Estoque", Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = colorPrimary, Left = 24, Top = 16, AutoSize = true };
            cmbEstoqueLivro = new ComboBox() { Left = 24, Top = 56, Width = 640, DropDownStyle = ComboBoxStyle.DropDownList };
            nudEstoqueQtd = new NumericUpDown() { Left = 680, Top = 56, Width = 120, Minimum = 1, Maximum = 100000, Value = 1 };
            btnEstoqueEntrada = CreateActionButton("Entrada (Adicionar)", new Rectangle(820, 52, 160, 36));
            btnEstoqueSaida = CreateActionButton("Saída (Remover)", new Rectangle(990, 52, 160, 36));
            btnEstoqueRefresh = CreateActionButton("Atualizar", new Rectangle(820, 96, 160, 36));

            tabEstoque.Controls.AddRange(new Control[] {
                lblEstoqueTitle, cmbEstoqueLivro, nudEstoqueQtd, btnEstoqueEntrada, btnEstoqueSaida, btnEstoqueRefresh
            });

            // ---------------- VENDAS ----------------
            lblVendasTitle = new Label() { Text = "Vendas", Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = colorPrimary, Left = 24, Top = 16, AutoSize = true };
            cmbVendaLivro = new ComboBox() { Left = 24, Top = 56, Width = 640, DropDownStyle = ComboBoxStyle.DropDownList };
            nudVendaQtd = new NumericUpDown() { Left = 680, Top = 56, Width = 120, Minimum = 1, Maximum = 1000, Value = 1 };
            btnVendaAdicionarItem = CreateActionButton("Adicionar ao Carrinho", new Rectangle(820, 52, 200, 36));
            btnVendaConcluir = CreateActionButton("Concluir Venda", new Rectangle(1030, 52, 140, 36));

            dgvVendaCarrinho = CreateGrid(new Rectangle(24, 110, 650, 400));
            dgvVendasHistorico = CreateGrid(new Rectangle(700, 110, 444, 400));

            tabVendas.Controls.AddRange(new Control[] {
                lblVendasTitle, cmbVendaLivro, nudVendaQtd, btnVendaAdicionarItem, btnVendaConcluir,
                dgvVendaCarrinho, dgvVendasHistorico
            });

            // ---------------- CUPONS ----------------
            txtCupomCodigo = CreateTextBox(new Rectangle(24, 24, 360, 30), "Código do cupom (ex: BEMVINDO5)");
            nudCupomDesconto = new NumericUpDown() { Left = 400, Top = 24, Width = 100, Minimum = 0, Maximum = 100, Value = 10 };
            dtpCupomValidade = new DateTimePicker() { Left = 520, Top = 24, Width = 140, Format = DateTimePickerFormat.Short };
            nudCupomUsoMax = new NumericUpDown() { Left = 680, Top = 24, Width = 120, Minimum = 1, Maximum = 10000, Value = 100 };

            btnCupomAdd = CreateActionButton("Adicionar Cupom", new Rectangle(820, 20, 140, 34));
            btnCupomDelete = CreateActionButton("Excluir Cupom", new Rectangle(980, 20, 140, 34));
            btnCupomRefresh = CreateActionButton("Atualizar Lista", new Rectangle(1140, 20, 0, 34)); // won't show (space)

            dgvCupons = CreateGrid(new Rectangle(24, 70, 1120, 560));

            tabCupons.Controls.AddRange(new Control[] {
                txtCupomCodigo, nudCupomDesconto, dtpCupomValidade, nudCupomUsoMax,
                btnCupomAdd, btnCupomDelete, dgvCupons
            });

            // ---------------- RELATÓRIOS ----------------
            dgvRelatorios = CreateGrid(new Rectangle(24, 110, 1120, 520));
            btnRelTopVendidos = CreateActionButton("Top vendidos", new Rectangle(24, 56, 140, 34));
            btnRelEstoqueBaixo = CreateActionButton("Estoque baixo (<=5)", new Rectangle(184, 56, 160, 34));

            tabRelatorios.Controls.AddRange(new Control[] {
                btnRelTopVendidos, btnRelEstoqueBaixo, dgvRelatorios
            });
        }

        private TextBox CreateTextBox(Rectangle bounds, string placeholder)
        {
            var tb = new TextBox()
            {
                Left = bounds.Left,
                Top = bounds.Top,
                Width = bounds.Width,
                Height = bounds.Height,
                ForeColor = Color.Gray,
                Text = placeholder
            };
            tb.GotFocus += (s, e) =>
            {
                if (tb.ForeColor == Color.Gray) { tb.Text = ""; tb.ForeColor = Color.Black; }
            };
            tb.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(tb.Text)) { tb.Text = placeholder; tb.ForeColor = Color.Gray; }
            };
            return tb;
        }

        private Button CreateActionButton(string text, Rectangle bounds)
        {
            var b = new Button()
            {
                Text = text,
                Left = bounds.Left,
                Top = bounds.Top,
                Width = bounds.Width > 0 ? bounds.Width : 140,
                Height = bounds.Height > 0 ? bounds.Height : 34,
                FlatStyle = FlatStyle.Flat,
                BackColor = colorPrimary,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            b.FlatAppearance.BorderSize = 0;
            try
            {
                b.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, b.Width, b.Height, 10, 10));
            }
            catch { /* if platform doesn't allow */ }
            b.MouseEnter += (s, e) => b.BackColor = ControlPaint.Light(colorPrimary);
            b.MouseLeave += (s, e) => b.BackColor = colorPrimary;
            return b;
        }

        private DataGridView CreateGrid(Rectangle bounds)
        {
            var g = new DataGridView()
            {
                Left = bounds.Left,
                Top = bounds.Top,
                Width = bounds.Width,
                Height = bounds.Height,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };
            return g;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
    }
}
