// VendasControl.Designer.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_tws
{
    partial class VendasControl
    {
        private IContainer components = null;
        internal ComboBox cmbVendaLivro;
        internal NumericUpDown nudVendaQtd;
        internal Button btnVendaAdicionarItem;
        internal Button btnVendaConcluir;
        internal DataGridView dgvVendaCarrinho;
        internal DataGridView dgvVendasHistorico;

        private void InitializeComponent()
        {
            components = new Container();
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            cmbVendaLivro = new ComboBox() { Left = 24, Top = 20, Width = 700, DropDownStyle = ComboBoxStyle.DropDownList };
            nudVendaQtd = new NumericUpDown() { Left = 740, Top = 20, Width = 120, Minimum = 1, Maximum = 1000, Value = 1 };
            btnVendaAdicionarItem = CreateBtn("Adicionar ao Carrinho", new Rectangle(880, 16, 160, 36));
            btnVendaConcluir = CreateBtn("Concluir Venda", new Rectangle(880, 60, 160, 36));

            dgvVendaCarrinho = new DataGridView() { Left = 24, Top = 110, Width = 640, Height = 520, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            dgvVendasHistorico = new DataGridView() { Left = 680, Top = 110, Width = 520, Height = 520, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };

            this.Controls.AddRange(new Control[] { cmbVendaLivro, nudVendaQtd, btnVendaAdicionarItem, btnVendaConcluir, dgvVendaCarrinho, dgvVendasHistorico });
        }

        private Button CreateBtn(string text, Rectangle r) { var b = new Button() { Text = text, Left = r.Left, Top = r.Top, Width = r.Width, Height = r.Height, FlatStyle = FlatStyle.Flat, BackColor = ColorTranslator.FromHtml("#072E6A"), ForeColor = Color.White }; b.FlatAppearance.BorderSize = 0; return b; }
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
    }
}
