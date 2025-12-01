using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_tws
{
    public partial class FormMenu : Form
    {
        // Referências dos UserControls
        private HomeControl homeControl;
        private LivrosControl livrosControl;
        private AutoresControl autoresControl;
        private EstoqueControl estoqueControl;
        private VendasControl vendasControl;
        private CuponsControl cuponsControl;
        private RelatoriosControl relatoriosControl;

        private bool menuAberto = true;
        private int larguraMenu = 200;

        public FormMenu()
        {
            InitializeComponent();
            WireEvents();
            LoadHome();
        }

        // BOTÃO QUE ESCONDE / MOSTRA O MENU
        private void btnToggleMenu_Click(object sender, EventArgs e)
        {
            if (menuAberto)
            {
                // FECHAR MENU (lado direito)
                pnlSidebar.Width = 50;
                btnToggleMenu.Text = "☰";
                menuAberto = false;

                foreach (Control c in pnlSidebar.Controls)
                {
                    if (c is Button b)
                        b.Text = "";
                }
            }
            else
            {
                // ABRIR MENU
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

            // **Reposicionar botão sempre à direita**
            btnToggleMenu.Left = this.ClientSize.Width - btnToggleMenu.Width - 10;
        }

        // Conectar botões às telas
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

        // Trocar telas
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
