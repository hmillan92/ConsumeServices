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
    public partial class Sales : Form
    {
        HMSrvService.HMServiceClient MiServicio = new HMSrvService.HMServiceClient();
        List<SalesDetail> ListaArticulos = new List<SalesDetail>();
        
        public Sales()
        {
            InitializeComponent();
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Customer cliente = new Customer();
            cliente = MiServicio.ValidarCustomerByRif(txtRif.Text);

            if (cliente == null || txtRif.Text == string.Empty)
            {
                MessageBox.Show("Registro no existe");
                txtCustomerName.Text = string.Empty;
                txtContact.Text = string.Empty;         
                txtRif.Focus();
            }

            else
            {
                txtCustomerName.Text = cliente.CustomerName;
                txtContact.Text = cliente.Contact;
                txtCodigo.Enabled = true;
                txtCantidad.Enabled = true;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Item objItem = new Item();
            objItem = MiServicio.ValidarItemByCod(txtCodigo.Text);

            if (objItem != null)
            {
                SalesDetail DetalleVenta = new SalesDetail();
                {
                    DetalleVenta.Codigo = objItem.Codigo;
                    DetalleVenta.Description = objItem.Description;
                    DetalleVenta.Price = objItem.Price;
                    if (string.IsNullOrEmpty(txtCantidad.Text.ToString()))
                    {
                        txtCantidad.Text = "1";
                    }
                    DetalleVenta.Quantity = int.Parse(txtCantidad.Text);            
                    DetalleVenta.Total = objItem.Price * int.Parse(txtCantidad.Text);
                }
                EnviarItem(DetalleVenta);

                int TotalQuantity = CalcularTotalQuantity(ListaArticulos);
                decimal TotalDoc = CalcularTotalDoc(ListaArticulos);

                dataGridListaItem.DataSource = null;
                dataGridListaItem.DataSource = ListaArticulos;

                txtCantidad.Text = string.Empty;
                txtCodigo.Text = string.Empty;                
                lblTotalDoc.Text = TotalDoc.ToString("#,###.##");
                lblTotalQuantity.Text = TotalQuantity.ToString();
                txtCodigo.Focus();
            }

            else
            {
                MessageBox.Show("Este Articulo no existe");
            }                   
        }

        private int CalcularTotalQuantity(List<SalesDetail> listaArticulos)
        {
            int TotalQuantity = 0;
            foreach (var item in ListaArticulos)
            {
                TotalQuantity = item.Quantity + TotalQuantity;

            };
            return TotalQuantity;
        }

        private decimal CalcularTotalDoc(List<SalesDetail> listaArticulos)
        {
            decimal TotalDoc = 0;
            foreach (var item in ListaArticulos)
            {
                TotalDoc = item.Total + TotalDoc;
                
            };
            return TotalDoc;
        }

        public void EnviarItem(SalesDetail DetalleVenta)
        {
            if (ListaArticulos.Count == 0)
            {
                ListaArticulos.Add(DetalleVenta);
            }

            else
            {
                List<SalesDetail> ListaUpdate = new List<SalesDetail>();
                bool swobj = false;

                foreach (var item in ListaArticulos)
                {                  
                    SalesDetail ObjLista = new SalesDetail();
                    
                    if (item.Codigo == DetalleVenta.Codigo)
                    {
                        ObjLista.Codigo = item.Codigo;
                        ObjLista.Description = item.Description;
                        ObjLista.Quantity = item.Quantity + DetalleVenta.Quantity;
                        ObjLista.Price = item.Price;
                        ObjLista.Total = item.Total + DetalleVenta.Total;

                        ListaUpdate.Add(ObjLista);
                        swobj = true;
                        
                    }
                    else
                    {
                        ObjLista.Codigo = item.Codigo;
                        ObjLista.Description = item.Description;
                        ObjLista.Quantity = item.Quantity;
                        ObjLista.Price = item.Price;
                        ObjLista.Total = item.Total;

                        ListaUpdate.Add(ObjLista);
                    }
                    
                }

                if (swobj == true)
                {
                    ListaArticulos.Clear();
                    ListaArticulos.AddRange(ListaUpdate);
                }

                else
                {
                    ListaUpdate.Add(DetalleVenta);
                    ListaArticulos.Clear();
                    ListaArticulos.AddRange(ListaUpdate);
                }
               
            }
        }

        private void DataGridListaItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {           
            
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Menu menuForm = new Menu();
            string rif = txtRif.Text;
            Customers ClienteForm = new Customers();
            ClienteForm.BtnSearch_Click(txtRif.Text, e);           
            ClienteForm.Show();
            
        }
    }
}
