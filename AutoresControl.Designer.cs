// AutoresControl.Designer.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Sistema_tws
{
    partial class AutoresControl
    {
        private IContainer components = null;
        internal DataGridView dgvAutores;
        internal TextBox txtAutorNome;
        internal TextBox txtAutorNacionalidade;
        internal TextBox txtAutorEmail;
        internal Button btnAutorAdd;
        internal Button btnAutorEdit;
        internal Button btnAutorDelete;
        internal Button btnAutorRefresh;

        private void InitializeComponent()
        {
            components = new Container();
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.WhiteSmoke;

            txtAutorNome = CreateTextBox(new Rectangle(24, 16, 420, 28), "Nome completo");
            txtAutorNacionalidade = CreateTextBox(new Rectangle(460, 16, 160, 28), "Nacionalidade");
            txtAutorEmail = CreateTextBox(new Rectangle(632, 16, 320, 28), "E-mail");

            btnAutorAdd = CreateBtn("Adicionar", new Rectangle(24, 56, 120, 34));
            btnAutorEdit = CreateBtn("Editar", new Rectangle(156, 56, 120, 34));
            btnAutorDelete = CreateBtn("Excluir", new Rectangle(288, 56, 120, 34));
            btnAutorRefresh = CreateBtn("Atualizar", new Rectangle(420, 56, 120, 34));

            dgvAutores = new DataGridView() { Left = 24, Top = 110, Width = 1060, Height = 560, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect, BackgroundColor = Color.White };

            this.Controls.AddRange(new Control[] { txtAutorNome, txtAutorNacionalidade, txtAutorEmail, btnAutorAdd, btnAutorEdit, btnAutorDelete, btnAutorRefresh, dgvAutores });
        }

        private TextBox CreateTextBox(Rectangle r, string placeholder) { var tb = new TextBox() { Left = r.Left, Top = r.Top, Width = r.Width, Height = r.Height, ForeColor = Color.Gray, Text = placeholder }; tb.GotFocus += (s, e) => { if (tb.ForeColor == Color.Gray) { tb.Text = ""; tb.ForeColor = Color.Black; } }; tb.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(tb.Text)) { tb.Text = placeholder; tb.ForeColor = Color.Gray; } }; return tb; }
        private Button CreateBtn(string text, Rectangle r) { var b = new Button() { Text = text, Left = r.Left, Top = r.Top, Width = r.Width, Height = r.Height, FlatStyle = FlatStyle.Flat, BackColor = ColorTranslator.FromHtml("#072E6A"), ForeColor = Color.White }; b.FlatAppearance.BorderSize = 0; return b; }
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
    }
}
