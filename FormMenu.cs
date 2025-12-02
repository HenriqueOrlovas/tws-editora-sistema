using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_tws
{
    public partial class FormMenu : Form
    {
        // BOTÕES DO MENU
        private Button btnHome, btnLivros, btnAutores, btnEstoque, btnVendas, btnCupons, btnRelatorios;

        // UserControls
        private HomeControl homeControl;
        private LivrosControl livrosControl;
        private AutoresControl autoresControl;
        private EstoqueControl estoqueControl;
        private VendasControl vendasControl;
        private CuponsControl cuponsControl;
        private RelatoriosControl relatoriosControl;

        // Menu
        private bool menuAberto = true;
        private int larguraMenu = 200;

        private Color colorPrimary = ColorTranslator.FromHtml("#072E6A");

        public FormMenu()
        {
            InitializeComponent();
            CriarMenu();
            WireEvents();
            LoadHome();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            btnToggleMenu.Left = this.ClientSize.Width - btnToggleMenu.Width - 10;
            btnToggleMenu.BringToFront();
        }

        // -------------------------- CRIAR MENU --------------------------

        private void CriarMenu()
        {
            btnHome = CreateButton("Home", 120);
            btnLivros = CreateButton("Livros", 170);
            btnAutores = CreateButton("Autores", 220);
            btnEstoque = CreateButton("Estoque", 270);
            btnVendas = CreateButton("Vendas", 320);
            btnCupons = CreateButton("Cupons", 370);
            btnRelatorios = CreateButton("Relatórios", 420);

            pnlSidebar.Controls.AddRange(new Control[]
            {
                btnHome, btnLivros, btnAutores, btnEstoque,
                btnVendas, btnCupons, btnRelatorios
            });
        }

        private Button CreateButton(string text, int top)
        {
            return new Button()
            {
                Text = text,
                Width = 170,
                Height = 40,
                Left = 15,
                Top = top,
                BackColor = colorPrimary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TabStop = false
            };
        }

        // -------------------------- MENU ABRIR/FECHAR --------------------------

        private void btnToggleMenu_Click(object sender, EventArgs e)
        {
            if (menuAberto)
            {
                pnlSidebar.Width = 50;
                btnToggleMenu.Text = "☰";
                menuAberto = false;

                // EVITA ERRO DE PICTUREBOX
                foreach (Control c in pnlSidebar.Controls)
                {
                    if (c is Button b)
                        b.Text = "";
                }
            }
            else
            {
                pnlSidebar.Width = larguraMenu;
                btnToggleMenu.Text = "✖";
                menuAberto = true;

                btnHome.Text = "Home";
                btnLivros.Text = "Livros";
                btnAutores.Text = "Autores";
                btnEstoque.Text = "Estoque";
                btnVendas.Text = "Vendas";
                btnCupons.Text = "Cupons";
                btnRelatorios.Text = "Relatórios";
            }

            btnToggleMenu.Left = this.ClientSize.Width - btnToggleMenu.Width - 10;
        }

        // -------------------------- EVENTOS DOS BOTÕES --------------------------

        private void WireEvents()
        {
            btnHome.Click += (s, e) => LoadHome();
            btnLivros.Click += (s, e) => LoadLivros();
            btnAutores.Click += (s, e) => LoadAutores();
            btnEstoque.Click += (s, e) => LoadEstoque();
            btnVendas.Click += (s, e) => LoadVendas();
            btnCupons.Click += (s, e) => LoadCupons();
            btnRelatorios.Click += (s, e) => LoadRelatorios();
        }

        // -------------------------- TROCA DE TELAS --------------------------

        private void LoadUserControl(UserControl uc)
        {
            pnlMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(uc);
            uc.BringToFront();
        }

        private void LoadHome()
        {
            if (homeControl == null) homeControl = new HomeControl();
            LoadUserControl(homeControl);
        }

        private void LoadLivros()
        {
            if (livrosControl == null) livrosControl = new LivrosControl();
            LoadUserControl(livrosControl);
        }

        private void LoadAutores()
        {
            if (autoresControl == null) autoresControl = new AutoresControl();
            LoadUserControl(autoresControl);
        }

        private void LoadEstoque()
        {
            if (estoqueControl == null) estoqueControl = new EstoqueControl();
            LoadUserControl(estoqueControl);
        }

        private void LoadVendas()
        {
            if (vendasControl == null) vendasControl = new VendasControl();
            LoadUserControl(vendasControl);
        }

        private void LoadCupons()
        {
            if (cuponsControl == null) cuponsControl = new CuponsControl();
            LoadUserControl(cuponsControl);
        }

        private void LoadRelatorios()
        {
            if (relatoriosControl == null) relatoriosControl = new RelatoriosControl();
            LoadUserControl(relatoriosControl);
        }
    }
}
