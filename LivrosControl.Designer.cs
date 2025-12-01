// LivrosControl.Designer.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sistema_tws
{
    partial class LivrosControl
    {
        private IContainer components = null;
        internal DataGridView dgvLivros;
        internal TextBox txtLivroTitulo;
        internal TextBox txtLivroCategoria;
        internal TextBox txtLivroPreco;
        internal ComboBox cmbLivroFormato;
        internal ComboBox cmbLivroAutor;
        internal NumericUpDown nudLivroEstoque;
        internal Button btnLivroAdd;
        internal Button btnLivroEdit;
        internal Button btnLivroDelete;
        internal Button btnLivroRefresh;

        private Color colorPrimary = ColorTranslator.FromHtml("#072E6A");

        private void InitializeComponent()
        {
            components = new Container();
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            txtLivroTitulo = CreateTextBox(new Rectangle(24, 20, 360, 28), "Título do livro");
            txtLivroCategoria = CreateTextBox(new Rectangle(400, 20, 200, 28), "Categoria");
            txtLivroPreco = CreateTextBox(new Rectangle(612, 20, 140, 28), "Preço (ex: 39.90)");
            cmbLivroFormato = new ComboBox() { Left = 768, Top = 20, Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbLivroFormato.Items.AddRange(new string[] { "fisico", "ebook", "ambos" }); cmbLivroFormato.SelectedIndex = 2;
            cmbLivroAutor = new ComboBox() { Left = 900, Top = 20, Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };

            nudLivroEstoque = new NumericUpDown() { Left = 24, Top = 56, Width = 120, Minimum = 0, Maximum = 100000, Value = 0 };

            btnLivroAdd = CreateBtn("Adicionar", new Rectangle(160, 56, 110, 34));
            btnLivroEdit = CreateBtn("Editar", new Rectangle(280, 56, 110, 34));
            btnLivroDelete = CreateBtn("Excluir", new Rectangle(400, 56, 110, 34));
            btnLivroRefresh = CreateBtn("Atualizar", new Rectangle(520, 56, 110, 34));

            dgvLivros = new DataGridView() { Left = 24, Top = 110, Width = 1060, Height = 560, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

            this.Controls.AddRange(new Control[] {
                txtLivroTitulo, txtLivroCategoria, txtLivroPreco, cmbLivroFormato, cmbLivroAutor, nudLivroEstoque,
                btnLivroAdd, btnLivroEdit, btnLivroDelete, btnLivroRefresh, dgvLivros
            });
        }

        private TextBox CreateTextBox(Rectangle r, string placeholder)
        {
            var tb = new TextBox() { Left = r.Left, Top = r.Top, Width = r.Width, Height = r.Height, ForeColor = Color.Gray, Text = placeholder };
            tb.GotFocus += (s, e) => { if (tb.ForeColor == Color.Gray) { tb.Text = ""; tb.ForeColor = Color.Black; } };
            tb.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(tb.Text)) { tb.Text = placeholder; tb.ForeColor = Color.Gray; } };
            return tb;
        }

        private Button CreateBtn(string text, Rectangle r)
        {
            var b = new Button() { Text = text, Left = r.Left, Top = r.Top, Width = r.Width, Height = r.Height, BackColor = colorPrimary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }

        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
    }
}
