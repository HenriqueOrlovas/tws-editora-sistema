// EstoqueControl.Designer.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_tws
{
    partial class EstoqueControl
    {
        private IContainer components = null;
        internal ComboBox cmbEstoqueLivro;
        internal NumericUpDown nudEstoqueQtd;
        internal Button btnEstoqueEntrada;
        internal Button btnEstoqueSaida;
        internal Button btnEstoqueRefresh;

        private void InitializeComponent()
        {
            components = new Container();
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;
            cmbEstoqueLivro = new ComboBox() { Left = 24, Top = 24, Width = 720, DropDownStyle = ComboBoxStyle.DropDownList };
            nudEstoqueQtd = new NumericUpDown() { Left = 760, Top = 24, Width = 120, Minimum = 1, Maximum = 100000, Value = 1 };
            btnEstoqueEntrada = CreateBtn("Entrada (Adicionar)", new Rectangle(24, 64, 200, 36));
            btnEstoqueSaida = CreateBtn("Saída (Remover)", new Rectangle(240, 64, 200, 36));
            btnEstoqueRefresh = CreateBtn("Atualizar Lista", new Rectangle(460, 64, 160, 36));
            this.Controls.AddRange(new Control[] { cmbEstoqueLivro, nudEstoqueQtd, btnEstoqueEntrada, btnEstoqueSaida, btnEstoqueRefresh });
        }

        private Button CreateBtn(string t, Rectangle r) { var b = new Button() { Text = t, Left = r.Left, Top = r.Top, Width = r.Width, Height = r.Height, FlatStyle = FlatStyle.Flat, BackColor = ColorTranslator.FromHtml("#072E6A"), ForeColor = Color.White }; b.FlatAppearance.BorderSize = 0; return b; }
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
    }
}
