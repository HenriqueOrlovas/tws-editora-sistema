// CuponsControl.Designer.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_tws
{
    partial class CuponsControl
    {
        private IContainer components = null;
        internal DataGridView dgvCupons;
        internal TextBox txtCupomCodigo;
        internal NumericUpDown nudCupomDesconto;
        internal DateTimePicker dtpCupomValidade;
        internal NumericUpDown nudCupomUsoMax;
        internal Button btnCupomAdd;
        internal Button btnCupomDelete;
        internal Button btnCupomRefresh;

        private void InitializeComponent()
        {
            components = new Container();
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            txtCupomCodigo = CreateTextBox(new Rectangle(24, 16, 360, 28), "Código do cupom (ex: BEMVINDO5)");
            nudCupomDesconto = new NumericUpDown() { Left = 400, Top = 16, Width = 100, Minimum = 0, Maximum = 100, Value = 10 };
            dtpCupomValidade = new DateTimePicker() { Left = 520, Top = 16, Width = 140, Format = DateTimePickerFormat.Short };
            nudCupomUsoMax = new NumericUpDown() { Left = 680, Top = 16, Width = 120, Minimum = 1, Maximum = 10000, Value = 100 };
            btnCupomAdd = CreateBtn("Adicionar Cupom", new Rectangle(820, 12, 140, 34));
            btnCupomDelete = CreateBtn("Excluir Cupom", new Rectangle(980, 12, 140, 34));
            dgvCupons = new DataGridView() { Left = 24, Top = 64, Width = 1060, Height = 560, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };

            this.Controls.AddRange(new Control[] { txtCupomCodigo, nudCupomDesconto, dtpCupomValidade, nudCupomUsoMax, btnCupomAdd, btnCupomDelete, dgvCupons });
        }

        private TextBox CreateTextBox(Rectangle r, string placeholder) { var tb = new TextBox() { Left = r.Left, Top = r.Top, Width = r.Width, Height = r.Height, ForeColor = Color.Gray, Text = placeholder }; tb.GotFocus += (s, e) => { if (tb.ForeColor == Color.Gray) { tb.Text = ""; tb.ForeColor = Color.Black; } }; tb.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(tb.Text)) { tb.Text = placeholder; tb.ForeColor = Color.Gray; } }; return tb; }
        private Button CreateBtn(string t, Rectangle r) { var b = new Button() { Text = t, Left = r.Left, Top = r.Top, Width = r.Width, Height = r.Height, FlatStyle = FlatStyle.Flat, BackColor = ColorTranslator.FromHtml("#072E6A"), ForeColor = Color.White }; b.FlatAppearance.BorderSize = 0; return b; }
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
    }
}
