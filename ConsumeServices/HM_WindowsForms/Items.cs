using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HM_Entities;

namespace HM_WindowsForms
{
    public partial class Items : Form
    {
        HMSrvService.HMServiceClient MiServicio = new HMSrvService.HMServiceClient();
        public Items()
        {
            InitializeComponent();
        }

        private void Items_Load(object sender, EventArgs e)
        {

        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            string Msj1 = "Debe ingresar un ";
            string Msj2 = " valido";
            string a = "Codigo", b = "Description", c = "Price";
            if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show(Msj1 + a + Msj2);
            }

            else if (string.IsNullOrEmpty(txtDescription.Text) || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show(Msj1 + b + Msj2);
            }

            else if (string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show(Msj1 + c + Msj2);
            }

            else
            {
                try
                {
                    string Mensaje = MiServicio.CreateItem(txtCodigo.Text, txtDescription.Text, decimal.Parse(txtPrice.Text));
                    MessageBox.Show(Mensaje);
                }
                catch (Exception ex)
                {
                   MessageBox.Show(ex.Message);
                }               
            }
        }

        private void BtnBorrar_Click(object sender, EventArgs e)
        {
            string Mensaje = MiServicio.DeleteItem(txtCodigo.Text);

            MessageBox.Show(Mensaje);

            txtCodigo.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";

            txtCodigo.Focus();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Item articulos = new Item();
            articulos = MiServicio.ValidarItemByCod(txtCodigo.Text);

            if (articulos == null || txtCodigo.Text == string.Empty)
            {
                MessageBox.Show("Registro no existe");
                List<Item> ListaArticulos = MiServicio.ListarItem("").ToList();
                dataGridItems.DataSource = ListaArticulos;
                txtCodigo.Focus();
            }

            else
            {
                txtCodigo.Text = articulos.Codigo;
                txtDescription.Text = articulos.Description;
                txtPrice.Text = articulos.Price.ToString();
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
        }
    }
}
