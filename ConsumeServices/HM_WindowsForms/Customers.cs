using HM_Entities;
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
    public partial class Customers : Form
    {
        HMSrvService.HMServiceClient MiServicio = new HMSrvService.HMServiceClient();
        public Customers()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            string Msj1 = "Debe ingresar un ";
            string Msj2 = " valido";
            string a = "Rif", b = "Name", c = "Contact", d = " Email";
            if (string.IsNullOrEmpty(txtRif.Text) || string.IsNullOrWhiteSpace(txtRif.Text))
            {
                MessageBox.Show(Msj1+a+Msj2);
            }

            else if (string.IsNullOrEmpty(txtCustomerName.Text) || string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show(Msj1+b+Msj2);
            }

            else if (string.IsNullOrEmpty(txtContact.Text) || string.IsNullOrWhiteSpace(txtContact.Text))
            {
                MessageBox.Show(Msj1+c+Msj2);
            }

            else if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show(Msj1+d+Msj2);
            }

            else
            {
                string Mensaje = MiServicio.CreateCustomer(txtCustomerName.Text, txtContact.Text, txtEmail.Text, txtRif.Text);

                MessageBox.Show(Mensaje);
            }
            
        }

        private void BtnBorrar_Click(object sender, EventArgs e)
        {
            string Mensaje = MiServicio.DeleteCustomer(txtRif.Text);

            MessageBox.Show(Mensaje);

            txtRif.Text = "";
            txtCustomerName.Text = "";
            txtContact.Text = "";
            txtEmail.Text = "";

            txtRif.Focus();

        }       

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void BtnSearch_Click(object sender, EventArgs e)
        {
            Customer cliente = new Customer();

            Button boton = sender as Button;
            if (boton == null)
            {
                txtRif.Text = Convert.ToString(sender);
            }

            cliente = MiServicio.ValidarCustomerByRif(txtRif.Text);
            if (cliente == null || txtRif.Text == string.Empty)
            {
                MessageBox.Show("Registro no existe");
                List<Customer> ListaClientes = MiServicio.ListarCustomer("").ToList();
                dataGridCustomers.DataSource = ListaClientes;
                txtRif.Focus();
            }

            else
            {
                txtCustomerName.Text = cliente.CustomerName;
                txtContact.Text = cliente.Contact;
                txtEmail.Text = cliente.Email;
                txtCustomerId.Text = cliente.CustomerId.ToString();
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Formulario Limpiado");
            txtRif.Text = "";
            txtCustomerName.Text = "";
            txtContact.Text = "";
            txtEmail.Text = "";
            txtCustomerId.Text = "";
        }
    }
}
