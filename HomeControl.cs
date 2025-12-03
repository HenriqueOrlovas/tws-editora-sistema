using System;
using System.Windows.Forms;

namespace Sistema_tws
{
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            InitializeComponent();
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            string title = "";
            if (btn.Tag != null)
            {
                title = btn.Tag.ToString();
            }

            FormMenu form = this.FindForm() as FormMenu;
            if (form == null) return;

            if (title == "Livros")
            {
                form.LoadLivros();
            }
            else if (title == "Autores")
            {
                form.LoadAutores();
            }
            else if (title == "Estoque")
            {
                form.LoadEstoque();
            }
            else if (title == "Vendas")
            {
                form.LoadVendas();
            }
            else if (title == "Cupons")
            {
                form.LoadCupons();
            }
            else if (title == "Relatórios")
            {
                form.LoadRelatorios();
            }
        }
    }
}
