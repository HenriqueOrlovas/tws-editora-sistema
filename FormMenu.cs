using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_tws
{
    public partial class FormMenu : Form
    {
        private Button btnHome;
        private Button btnLivros;
        private Button btnAutores;
        private Button btnEstoque;
        private Button btnVendas;
        private Button btnCupons;
        private Button btnRelatorios;

        private HomeControl homeControl;
        private LivrosControl livrosControl;
        private AutoresControl autoresControl;
        private EstoqueControl estoqueControl;
        private VendasControl vendasControl;
        private CuponsControl cuponsControl;
        private RelatoriosControl relatoriosControl;

        private bool menuAberto = true;
        private int larguraMenu = 200;

        private Color colorPrimary = ColorTranslator.FromHtml("#072E6A");

        public FormMenu()
        {
            InitializeComponent();
            CriarMenu();
            RegistrarEventos();
            LoadHome();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            btnToggleMenu.Left = this.ClientSize.Width - btnToggleMenu.Width - 10;
            btnToggleMenu.BringToFront();
        }

        private void CriarMenu()
        {
            btnHome = CriarBotao("Home", 120);
            btnLivros = CriarBotao("Livros", 170);
            btnAutores = CriarBotao("Autores", 220);
            btnEstoque = CriarBotao("Estoque", 270);
            btnVendas = CriarBotao("Vendas", 320);
            btnCupons = CriarBotao("Cupons", 370);
            btnRelatorios = CriarBotao("Relatórios", 420);

            pnlSidebar.Controls.Add(btnHome);
            pnlSidebar.Controls.Add(btnLivros);
            pnlSidebar.Controls.Add(btnAutores);
            pnlSidebar.Controls.Add(btnEstoque);
            pnlSidebar.Controls.Add(btnVendas);
            pnlSidebar.Controls.Add(btnCupons);
            pnlSidebar.Controls.Add(btnRelatorios);
        }

        private Button CriarBotao(string texto, int top)
        {
            Button b = new Button();
            b.Text = texto;
            b.Width = 170;
            b.Height = 40;
            b.Left = 15;
            b.Top = top;
            b.BackColor = colorPrimary;
            b.ForeColor = Color.White;
            b.FlatStyle = FlatStyle.Flat;
            b.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            b.TabStop = false;

            return b;
        }

        private void btnToggleMenu_Click(object sender, EventArgs e)
        {
            if (menuAberto == true)
            {
                pnlSidebar.Width = 50;
                btnToggleMenu.Text = "☰";
                menuAberto = false;

                btnHome.Text = "";
                btnLivros.Text = "";
                btnAutores.Text = "";
                btnEstoque.Text = "";
                btnVendas.Text = "";
                btnCupons.Text = "";
                btnRelatorios.Text = "";
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

        private void RegistrarEventos()
        {
            btnHome.Click += btnHome_Click;
            btnLivros.Click += btnLivros_Click;
            btnAutores.Click += btnAutores_Click;
            btnEstoque.Click += btnEstoque_Click;
            btnVendas.Click += btnVendas_Click;
            btnCupons.Click += btnCupons_Click;
            btnRelatorios.Click += btnRelatorios_Click;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            LoadHome();
        }

        private void btnLivros_Click(object sender, EventArgs e)
        {
            LoadLivros();
        }

        private void btnAutores_Click(object sender, EventArgs e)
        {
            LoadAutores();
        }

        private void btnEstoque_Click(object sender, EventArgs e)
        {
            LoadEstoque();
        }

        private void btnVendas_Click(object sender, EventArgs e)
        {
            LoadVendas();
        }

        private void btnCupons_Click(object sender, EventArgs e)
        {
            LoadCupons();
        }

        private void btnRelatorios_Click(object sender, EventArgs e)
        {
            LoadRelatorios();
        }

        private void LoadUserControl(UserControl uc)
        {
            pnlMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(uc);
            uc.BringToFront();
        }

        public void LoadHome()
        {
            if (homeControl == null)
            {
                homeControl = new HomeControl();
            }
            LoadUserControl(homeControl);
        }

        public void LoadLivros()
        {
            if (livrosControl == null)
            {
                livrosControl = new LivrosControl();
            }
            LoadUserControl(livrosControl);
        }

        public void LoadAutores()
        {
            if (autoresControl == null)
            {
                autoresControl = new AutoresControl();
            }
            LoadUserControl(autoresControl);
        }

        public void LoadEstoque()
        {
            if (estoqueControl == null)
            {
                estoqueControl = new EstoqueControl();
            }
            LoadUserControl(estoqueControl);
        }

        public void LoadVendas()
        {
            if (vendasControl == null)
            {
                vendasControl = new VendasControl();
            }
            LoadUserControl(vendasControl);
        }

        public void LoadCupons()
        {
            if (cuponsControl == null)
            {
                cuponsControl = new CuponsControl();
            }
            LoadUserControl(cuponsControl);
        }

        public void LoadRelatorios()
        {
            if (relatoriosControl == null)
            {
                relatoriosControl = new RelatoriosControl();
            }
            LoadUserControl(relatoriosControl);
        }

    }
}
