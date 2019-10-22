using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HM_WindowsForms
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void ToolStripButton1_Click_(object sender, EventArgs e)
        {
            Customers Clientes = new Customers();

            Clientes.MdiParent = this;
            Clientes.Show();
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            Customers Clientes = new Customers();

            Clientes.MdiParent = this;
            Clientes.Show();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            Items Articulos = new Items();

            Articulos.MdiParent = this;
            Articulos.Show();
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            Sales Ventas = new Sales();

            Ventas.MdiParent = this;
            Ventas.Show();
        }
    }
}
