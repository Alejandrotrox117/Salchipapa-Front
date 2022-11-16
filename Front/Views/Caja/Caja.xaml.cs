﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Net.Http;
using System.Net;
using Entities;
using Newtonsoft.Json;
using System.IO;
using Nancy.Json;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Front.Views.Caja
{
    /// <summary>
    /// Lógica de interacción para Caja.xaml
    /// </summary>
    public partial class Caja : UserControl
    {
        private static readonly HttpClient client = new HttpClient();
        private string _id = null;
        private List<clientes> listaClientes = new List<clientes>();
        public Caja()
        {
            InitializeComponent();
        }
        //METODO MAIN PARA DESEREALIZAR DATOS DEL JSON Y CONVERTIRLO A OBJETO C#
        public async void Main()
        {
            string responseClients = await Utilities.Get("clients/");
            string responsePay = await Utilities.Get("payments/");
            //LISTA DE CLIENTES DESEREALIZADA PARA RETORNAR DATOS
            List<clientes> returnedDataClients = JsonConvert.DeserializeObject<List<clientes>>(responseClients);
            List<payments> returnedPayment = JsonConvert.DeserializeObject<List<payments>>(responsePay);
            //VALIDACION DE RETORNO DE DATOS 
            if (returnedDataClients != null)
            {
                //ASIGNACION DE DATOS CONVERTIDOS AL ITEMS SOURCE DEL DATAGRID
                DataGridClientes.ItemsSource = returnedDataClients;
                ListBoxMetodosPagos.ItemsSource= returnedPayment;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        //METODO PARA BORRAR UN CLIENTE PASANDO POR PARAMETRO LA CEDULA COMO REQUISITO PARA EL INDICE
        //ELIMINAR EL ELEMENTO DE MANERA VISUAL DEL DATAGRID 
        public void EliminateViewElement()
        {
            if (DataGridClientes.SelectedItems.Count > 0)
            {
                List<clientes> miLista = (List<clientes>)DataGridClientes.ItemsSource;
                //Certifica si la lista esta vacia
                if (miLista != null)
                {
                    for (int i = 0; i < DataGridClientes.SelectedItems.Count; i++)
                    {
                        //Obtiene el indice de la fila seleccionada mediante el boton
                        int indice = DataGridClientes.Items.IndexOf(DataGridClientes.SelectedItems[i]);
                        //Almacena en la variable item el item seleccionado del datagrid
                        clientes item = DataGridClientes.SelectedItem as clientes;
                        //En la variable id almacena la cedula y la pasa por parametros al metodo                 
                        Utilities.Delete("clients/",item.ci.ToString());
                        //Remueve la fila completa seleccionada solo visualmente
                        miLista.RemoveAt(indice);
                    }
                    DataGridClientes.ItemsSource = null;
                    DataGridClientes.ItemsSource = miLista;
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar por lo menos una fila.");
            }
        }
        
        private void BtnAbrirFormEditarCliente_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            clientes cliente = element.DataContext as clientes;
            DialogHostClientes.IsOpen = true;
            this._id = cliente._id;
            TxtNombre.Text = cliente.name;
            TxtApellido.Text = cliente.surname;
            TxtCedula.Text = cliente.ci;
            string[] splitPhone = cliente.phone.Split('-');
            CBTelefono.Text = splitPhone[0]+'-';
            TxtTelefono.Text = splitPhone[1];
            TxtDireccion.Text = cliente.address;
            TxtTituloDialg.Text = "Editar Clientes";
            BtnAbrirDrawerActualizarCliente.Visibility = Visibility.Visible;
            

        }

        private async void BtnEnviarClienteActualizado_Click(object sender, RoutedEventArgs e)
        {
            string js = new JavaScriptSerializer().Serialize(new
            {
                ci = TxtCedula.Text,
                name = TxtNombre.Text,
                surname = TxtApellido.Text,
                address = TxtDireccion.Text,
                phone = CBTelefono.Text + TxtTelefono.Text,

            });
            var Response = await Utilities.Put("clients/" + _id + "/", js);
            if (Response.IsSuccessStatusCode)
                CerrarForm();
            else
                MessageBox.Show("hubo un error");

        }
        
        //EVENTOS PARA BOTONES DE PANTALLA CLIENTES
        private async void BtnEnviarCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string js = new JavaScriptSerializer().Serialize(new
                {
                    ci = TxtCedula.Text,
                    name = TxtNombre.Text,
                    surname = TxtApellido.Text,
                    address = TxtDireccion.Text,
                    phone = CBTelefono.Text + TxtTelefono.Text,

                });
                var Response = await Utilities.Post("clients", js);
                if (Response.IsSuccessStatusCode)
                {
                    string sr = await Response.Content.ReadAsStringAsync();
                    clientes cliente = JsonConvert.DeserializeObject<clientes>(sr);
                    CerrarForm();
                }
                else
                    MessageBox.Show("hubo un error"+js);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Main();
        }

        private void CerrarForm()
        {
            DialogHostClientes.IsOpen = false;
            DrawerHostClientes.IsBottomDrawerOpen = false;
            BtnEnviarCliente.Visibility = Visibility.Hidden;
            BtnAbrirDrawerActualizarCliente.Visibility = Visibility.Hidden;
        }

        private void BtnAbrirDrawerActualizarCliente_Click(object sender, RoutedEventArgs e)
        {
            DrawerHostClientes.IsBottomDrawerOpen = true;
            TxtTituloDrawerHost.Text = "¿Desea Actualizar este nuevo cliente?";
            BtnEnviarClienteActualizado.Visibility = Visibility.Visible;
            BtnEnviarCliente.Visibility = Visibility.Hidden;
        }

        private void BtnAbrirFormularioAgregar_Click(object sender, RoutedEventArgs e)
        {

            DialogHostClientes.IsOpen = true;
            TxtTituloDialg.Text = "Agregar Nuevo Cliente";
            BtnAbrirDrawerClientes.Visibility = Visibility.Visible;

        }

        private void DialogHostClientes_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            this.TxtNombre.Clear();
            this.TxtApellido.Clear();
            this.CBCedula.Text = "";
            this.CBTelefono.Text = "";
            this.TxtTelefono.Clear();
            this.TxtCedula.Clear();
            this.TxtDireccion.Clear();

        }

        private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        {
            CerrarForm();
        }

        private void BtnAbrirDrawerClientes_Click(object sender, RoutedEventArgs e)
        {
            
            DrawerHostClientes.IsBottomDrawerOpen = true;
            TxtTituloDrawerHost.Text = "¿Desea agregar este nuevo cliente?";
            BtnEnviarCliente.Visibility = Visibility.Visible;
            
        }

        private void BtnAbrirDrawerEliminar_Click(object sender, RoutedEventArgs e)
        {
            DrawerHostClientes.IsBottomDrawerOpen = true;
            TxtTituloDrawerHost.Text = "¿Está seguro de Eliminar este cliente?";
            BtnEliminarCliente.Visibility = Visibility.Visible;
            BtnEnviarCliente.Visibility = Visibility.Hidden;

        }
        private void BtnEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
                EliminateViewElement();
        }
        //EVENTOS PARA BOTONES PANTALLA METODOS DE PAGO
        private  async void BtnAbrirFormEditarPagos_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            payments payment = element.DataContext as payments;
            DialogHostMetodosPago.IsOpen = true;
            this._id = payment._id;
            TxtNombrePago.Text = payment.name;
            TxtMoneda.Text = payment.money;
            TxtTituloDialg.Text = "Editar Métodos de pago";
            BtnAbrirDrawerAgregarPago.Visibility = Visibility.Hidden;
            BtnAbrirDrawerActualizarPago.Visibility = Visibility.Visible;

        }

        private async void BtnEnviarActualizarPago_Click(object sender, RoutedEventArgs e)
        {
            string payment = new JavaScriptSerializer().Serialize(new
            {
                name = TxtNombrePago.Text,
                money=TxtMoneda.Text
                
            });
            var Response = await Utilities.Put("payments/" + _id + "/", payment);
            if (Response.IsSuccessStatusCode)
                DialogHostMetodosPago.IsOpen = false;
            else
                MessageBox.Show("hubo un error");
        }

        private async void BtnEnviarPago_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string pago = new JavaScriptSerializer().Serialize(new
                {
                    name = TxtNombrePago.Text,
                    money=TxtMoneda.Text
                });
                var Response = await Utilities.Post("payments", pago);
                if (Response.IsSuccessStatusCode)
                {
                    string sr = await Response.Content.ReadAsStringAsync();
                    MessageBox.Show("Se ha agregado un nuevo pago");
                    DialogHostMetodosPago.IsOpen = false;
                    TxtNombrePago.Clear();
                    TxtMoneda.Clear();
                    TabPagos.UpdateLayout();
                    

                }
                else
                    MessageBox.Show("hubo un error");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BtnAbrirFormPagos_Click(object sender, RoutedEventArgs e)
        {
            DialogHostMetodosPago.IsOpen = true;
            TituloDialogPagos.Text = "Agregar un nuevo Método de Pago";
            BtnAbrirDrawerAgregarPago.Visibility = Visibility.Visible;
        }

        private async void BtnEliminarPago_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListBoxMetodosPagos.SelectedItems.Count > 0)
                {
                List<payments> pay = (List<payments>)ListBoxMetodosPagos.ItemsSource as List<payments>;
                    //Certifica si la lista esta vacia
                    if (pay != null)
                    {
                        for (int i = 0; i < ListBoxMetodosPagos.SelectedItems.Count; i++)
                        {
                            //Obtiene el indice de la fila seleccionada mediante el boton
                            int indice = ListBoxMetodosPagos.Items.IndexOf(ListBoxMetodosPagos.SelectedItems[i]);
                            //Almacena en la variable item el item seleccionado del datagrid
                            payments item = ListBoxMetodosPagos.Items.CurrentItem as payments;
                            //En la variable id almacena la cedula y la pasa por parametros al metodo                 
                            Utilities.Delete("payments/", item._id.ToString());
                            //Remueve la fila completa seleccionada solo visualmente
                            pay.RemoveAt(indice);
                        }
                        ListBoxMetodosPagos.ItemsSource = null;
                        ListBoxMetodosPagos.ItemsSource = pay;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex+"Error");
            }
        }
       
        private void BtnAbrirDrawerEliminarPago_Click(object sender, RoutedEventArgs e)
        {
            DrawerHostPagos.IsBottomDrawerOpen = true;
            TxtTituloDrawerHostPagos.Text = "¿Esta seguro de eliminar este método de pago?";
            BtnEliminarPago.Visibility = Visibility.Visible;
        }

        private void BtnAbrirDrawerAgregarPago_Click(object sender, RoutedEventArgs e)
        {
            DrawerHostPagos.IsBottomDrawerOpen = true;
            TxtTituloDrawerHostPagos.Text= "¿Desea agregar un método de pago nuevo?";
            BtnAgregarPago.Visibility = Visibility.Visible;
        }
    }
}
