using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_tws
{
    partial class FormMenu
    {
        private IContainer components = null;

        private Panel pnlSidebar;
        private Panel pnlMain;

        private Button btnHome, btnLivros, btnAutores, btnEstoque, btnVendas, btnCupons, btnRelatorios;
        private Button btnToggleMenu;

        private Label lblTitle;

        private Color colorBackground = ColorTranslator.FromHtml("#cfcfcf");
        private Color colorPrimary = ColorTranslator.FromHtml("#072E6A");

        private void InitializeComponent()
        {
            components = new Container();

            this.Text = "Sistema TWS";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(1200, 760);
            this.BackColor = colorBackground;
            this.Font = new Font("Segoe UI", 9F);

            // painel principal
            pnlMain = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke
            };

            // sidebar (direita)
            pnlSidebar = new Panel()
            {
                BackColor = colorPrimary,
                Width = 200,
                Dock = DockStyle.Right
            };

            // BOTÃO DE ABRIR/FECHAR MENU — FIXO
            btnToggleMenu = new Button()
            {
                Text = "✖",
                Width = 38,
                Height = 35,
                Top = 10,
                FlatStyle = FlatStyle.Flat,
                BackColor = colorPrimary,
                ForeColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnToggleMenu.FlatAppearance.BorderSize = 0;

            // repo final na load
            this.Load += (s, e) =>
            {
                btnToggleMenu.Left = this.ClientSize.Width - btnToggleMenu.Width - 10;
                btnToggleMenu.BringToFront();
            };

            btnToggleMenu.Click += btnToggleMenu_Click;

            // título
            lblTitle = new Label()
            {
                Text = "Editora TWS",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                AutoSize = true,
                Top = 60,
                Left = 15
            };

            // botões
            btnHome = CreateButton("Home", 120);
            btnLivros = CreateButton("Livros", 170);
            btnAutores = CreateButton("Autores", 220);
            btnEstoque = CreateButton("Estoque", 270);
            btnVendas = CreateButton("Vendas", 320);
            btnCupons = CreateButton("Cupons", 370);
            btnRelatorios = CreateButton("Relatórios", 420);

            pnlSidebar.Controls.AddRange(new Control[]
            {
                lblTitle,
                btnHome, btnLivros, btnAutores,
                btnEstoque, btnVendas,
                btnCupons, btnRelatorios
            });

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlSidebar);
            this.Controls.Add(btnToggleMenu);
        }

        private Button CreateButton(string text, int top)
        {
            var btn = new Button()
            {
                Text = text,
                Width = 170,
                Height = 40,
                Left = 15,
                Top = top,
                BackColor = colorPrimary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Light(colorPrimary);
            btn.MouseLeave += (s, e) => btn.BackColor = colorPrimary;

            return btn;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }
    }
}
