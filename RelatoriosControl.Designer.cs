// RelatoriosControl.Designer.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_tws
{
    partial class RelatoriosControl
    {
        private IContainer components = null;
        internal DataGridView dgvRelatorios;
        internal Button btnRelTopVendidos;
        internal Button btnRelEstoqueBaixo;

        private void InitializeComponent()
        {
            components = new Container();
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;
            btnRelTopVendidos = CreateBtn("Top vendidos", new Rectangle(24, 20, 140, 34));
            btnRelEstoqueBaixo = CreateBtn("Estoque baixo (<=5)", new Rectangle(180, 20, 180, 34));
            dgvRelatorios = new DataGridView() { Left = 24, Top = 70, Width = 1060, Height = 560, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            this.Controls.AddRange(new Control[] { btnRelTopVendidos, btnRelEstoqueBaixo, dgvRelatorios });
        }

        private Button CreateBtn(string t, Rectangle r) { var b = new Button() { Text = t, Left = r.Left, Top = r.Top, Width = r.Width, Height = r.Height, FlatStyle = FlatStyle.Flat, BackColor = ColorTranslator.FromHtml("#072E6A"), ForeColor = Color.White }; b.FlatAppearance.BorderSize = 0; return b; }
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
    }
}
