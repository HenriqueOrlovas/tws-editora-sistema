// HomeControl.Designer.cs
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
            lblWelcome = new Label() { Text = "Painel - Bem vindo à Editora TWS", Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = colorPrimary, AutoSize = true, Left = 20, Top = 20 };
            flow = new FlowLayoutPanel() { Left = 20, Top = 70, Width = 880, Height = 400, AutoScroll = true };

            // create some big shortcut buttons
            flow.Controls.Add(CreateTile("Livros", "Gerenciar catálogo de livros"));
            flow.Controls.Add(CreateTile("Autores", "Gerenciar autores"));
            flow.Controls.Add(CreateTile("Estoque", "Ajustar entradas e saídas"));
            flow.Controls.Add(CreateTile("Vendas", "Registrar vendas e visualizar histórico"));
            flow.Controls.Add(CreateTile("Cupons", "Gerenciar cupons de desconto"));
            flow.Controls.Add(CreateTile("Relatórios", "Ver relatórios e consultas"));

            this.Controls.AddRange(new Control[] { lblWelcome, flow });
        }

        private Control CreateTile(string title, string subtitle)
        {
            var p = new Panel() { Width = 260, Height = 120, BackColor = Color.White, Margin = new Padding(12) };
            var lblTitle = new Label() { Text = title, Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = colorPrimary, Left = 12, Top = 12, AutoSize = true };
            var lblSub = new Label() { Text = subtitle, Font = new Font("Segoe UI", 9F), ForeColor = Color.Gray, Left = 12, Top = 44, AutoSize = true };
            var btn = new Button() { Text = "Abrir", Left = 12, Top = 72, Width = 100, Height = 30, BackColor = colorPrimary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) =>
            {
                // route to parent FormMenu to open the control by simulating a click
                var form = this.FindForm() as FormMenu;
                if (form == null) return;
                switch (title)
                {
                    case "Livros": form.Controls.Find("btnLivros", true); form.GetType().GetMethod("LoadLivros", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(form, null); break;
                    case "Autores": form.GetType().GetMethod("LoadAutores", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(form, null); break;
                    case "Estoque": form.GetType().GetMethod("LoadEstoque", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(form, null); break;
                    case "Vendas": form.GetType().GetMethod("LoadVendas", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(form, null); break;
                    case "Cupons": form.GetType().GetMethod("LoadCupons", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(form, null); break;
                    case "Relatórios": form.GetType().GetMethod("LoadRelatorios", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(form, null); break;
                }
            };

            p.Controls.AddRange(new Control[] { lblTitle, lblSub, btn });
            return p;
        }

        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
    }
}
