using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_tws
{
    partial class HomeControl
    {
        private IContainer components = null;
        private Label lblWelcome;
        private FlowLayoutPanel flow;
        private Color colorBackground = ColorTranslator.FromHtml("#cfcfcf");
        private Color colorPrimary = ColorTranslator.FromHtml("#072E6A");

        private void InitializeComponent()
        {
            components = new Container();
            this.BackColor = Color.WhiteSmoke;

            lblWelcome = new Label();
            lblWelcome.Text = "Painel - Bem vindo à Editora TWS";
            lblWelcome.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblWelcome.ForeColor = colorPrimary;
            lblWelcome.AutoSize = true;
            lblWelcome.Left = 20;
            lblWelcome.Top = 20;

            flow = new FlowLayoutPanel();
            flow.Left = 20;
            flow.Top = 70;
            flow.Width = 880;
            flow.Height = 400;
            flow.AutoScroll = true;

            flow.Controls.Add(CreateTile("Livros", "Gerenciar catálogo de livros"));
            flow.Controls.Add(CreateTile("Autores", "Gerenciar autores"));
            flow.Controls.Add(CreateTile("Estoque", "Ajustar entradas e saídas"));
            flow.Controls.Add(CreateTile("Vendas", "Registrar vendas e visualizar histórico"));
            flow.Controls.Add(CreateTile("Cupons", "Gerenciar cupons de desconto"));
            flow.Controls.Add(CreateTile("Relatórios", "Ver relatórios e consultas"));

            this.Controls.Add(lblWelcome);
            this.Controls.Add(flow);
        }

        private Control CreateTile(string title, string subtitle)
        {
            Panel p = new Panel();
            p.Width = 260;
            p.Height = 120;
            p.BackColor = Color.White;
            p.Margin = new Padding(12);

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = colorPrimary;
            lblTitle.Left = 12;
            lblTitle.Top = 12;
            lblTitle.AutoSize = true;

            Label lblSub = new Label();
            lblSub.Text = subtitle;
            lblSub.Font = new Font("Segoe UI", 9F);
            lblSub.ForeColor = Color.Gray;
            lblSub.Left = 12;
            lblSub.Top = 44;
            lblSub.AutoSize = true;

            Button btn = new Button();
            btn.Text = "Abrir";
            btn.Left = 12;
            btn.Top = 72;
            btn.Width = 100;
            btn.Height = 30;
            btn.BackColor = colorPrimary;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.Tag = title;
            btn.Click += Tile_Click;

            p.Controls.Add(lblTitle);
            p.Controls.Add(lblSub);
            p.Controls.Add(btn);

            return p;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
    }
}
