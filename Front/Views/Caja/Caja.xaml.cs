using MaterialDesignThemes.Wpf;
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
using System.ComponentModel;

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
            string responseClients = await Request.Get("clients/");
            string responsePay = await Request.Get("payments/");
            //LISTA DE CLIENTES DESEREALIZADA PARA RETORNAR DATOS
            List<clientes> returnedDataClients = JsonConvert.DeserializeObject<List<clientes>>(responseClients);
            List<payments> returnedPayment = JsonConvert.DeserializeObject<List<payments>>(responsePay);
            //VALIDACION DE RETORNO DE DATOS 
            if (returnedDataClients != null)
            {
                //ASIGNACION DE DATOS CONVERTIDOS AL ITEMS SOURCE DEL DATAGRID
                //DataGridClientes.ItemsSource = returnedDataClients;
                ListBoxMetodosPagos.ItemsSource= returnedPayment;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void BtnTabClientes_Click(object sender, RoutedEventArgs e)
        {
            TabControlClientes.SelectedIndex=0;
            PageClientes.GetClientes();
        }

        private void GridCaja_Loaded(object sender, RoutedEventArgs e)
        {
            PageClientes.GetClientes();
        }




        //evento aceptar dialgo form cliente
        //private void BtnAceptarDialog_Click(object sender, RoutedEventArgs e)
        //{
        //    this.DialogHostClientes.IsOpen = false;
        //    this.DrawerHostClientes.IsBottomDrawerOpen = true;
        //}
        //Evento click btn actualizar cliente
        //private void BtnActualizarCliente_Click(object sender, RoutedEventArgs e)
        //{
        //    FrameworkElement element = e.Source as FrameworkElement;
        //    clientes cliente = element.DataContext as clientes;
        //    this._id = cliente._id;

        //    TxtNombre.Text = cliente.name;
        //    TxtApellido.Text = cliente.surname;
        //    TxtCedula.Text = cliente.ci;
        //    string[] splitPhone = cliente.phone.Split('-');
        //    CBTelefono.Text = splitPhone[0]+'-';
        //    TxtTelefono.Text = splitPhone[1];
        //    TxtDireccion.Text = cliente.address;

        //    this.DialogHostClientes.IsOpen = true;
        //    this.TxtTituloDialg.Text = "Actualizar Cliente";
        //    this.TxtTituloDrawerCliente.Text = "¿Desea actualizar este cliente?";
        //    this.BtnConfirmarClienteDrawner.Click += this.ActualizarCliente_Click;

        //}
        ////evento actualizar cliente
        //        private async void ActualizarCliente_Click(object sender, RoutedEventArgs e)
        //{
        //    string js = new JavaScriptSerializer().Serialize(new
        //    {
        //        ci = TxtCedula.Text,
        //        name = TxtNombre.Text,
        //        surname = TxtApellido.Text,
        //        address = TxtDireccion.Text,
        //        phone = CBTelefono.Text + TxtTelefono.Text,

        //    });
        //    var Response = await Utilities.Put("clients/" + _id + "/", js);
        //    if (Response.IsSuccessStatusCode)
        //        CerrarCliente();
        //    else
        //        MessageBox.Show("hubo un error");

        //}

        //evento btn agregar cliente click
        //private void BtnAgregarCliente_Click(object sender, RoutedEventArgs e)
        //{

        //    this.DialogHostClientes.IsOpen = true;
        //    this.TxtTituloDialg.Text = "Agregar Nuevo Cliente";
        //    this.TxtTituloDrawerCliente.Text = "¿Desea agregar este cliente?";
        //    this.BtnConfirmarClienteDrawner.Click += this.EnviarCliente_Click;

        //}
        ////evento agregar cliente
        //private async void EnviarCliente_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string js = new JavaScriptSerializer().Serialize(new
        //        {
        //            ci = TxtCedula.Text,
        //            name = TxtNombre.Text,
        //            surname = TxtApellido.Text,
        //            address = TxtDireccion.Text,
        //            phone = CBTelefono.Text + TxtTelefono.Text,

        //        });
        //        var Response = await Utilities.Post("clients", js);
        //        if (Response.IsSuccessStatusCode)
        //        {

        //            CerrarCliente();
        //        }

        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //}

        //evento btn eliminar clente 
        //private void BtnEliminarCliente_Click(object sender, RoutedEventArgs e)
        //{
        //    FrameworkElement element = e.Source as FrameworkElement;
        //    clientes cliente = element.DataContext as clientes;
        //    this._id = cliente._id;

        //    DrawerHostClientes.IsBottomDrawerOpen = true;
        //    TxtTituloDrawerCliente.Text = "¿Está seguro de Eliminar este cliente?";
        //    this.BtnConfirmarClienteDrawner.Click += this.EliminarCliente_Click;

        //}
        ////evento eliminar cliente
        //private async void EliminarCliente_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string js = new JavaScriptSerializer().Serialize(new
        //        {
        //            ci = TxtCedula.Text,
        //            name = TxtNombre.Text,
        //            surname = TxtApellido.Text,
        //            address = TxtDireccion.Text,
        //            phone = CBTelefono.Text + TxtTelefono.Text,

        //        });
        //        var Response = await Utilities.Delete("clients/"+this._id+'/');
        //        if (Response.IsSuccessStatusCode)
        //        {
        //            CerrarCliente();
        //        }
        //        else
        //        {
        //            string sr = await Response.Content.ReadAsStringAsync();
        //            MessageBox.Show(sr);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
        ////funcion limpiar dialog
        //private void CerrarCliente()
        //{
        //    this._id = null;
        //    DialogHostClientes.IsOpen = false;
        //    DrawerHostClientes.IsBottomDrawerOpen = false;
        //    this.BtnConfirmarClienteDrawner.Click -= this.EliminarCliente_Click;
        //    this.BtnConfirmarClienteDrawner.Click -= this.ActualizarCliente_Click;
        //    this.BtnConfirmarClienteDrawner.Click -= this.EnviarCliente_Click;
        //}
        ////Evento btn cerrar form y drawner cliente
        //private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        //{
        //    CerrarCliente();
        //}
        ////evento cerrar diaglogo cliente 
        //private void DialogHostClientes_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        //{
        //    //this.TxtNombre.Clear();
        //    //this.TxtApellido.Clear();
        //    //this.CBCedula.Text = "";
        //    //this.CBTelefono.Text = "";
        //    //this.TxtTelefono.Clear();
        //    //this.TxtCedula.Clear();
        //    //this.TxtDireccion.Clear();

        //}

        ////FORMA PAGO
        ////boton aceptar form forma pago
        //private void BtnAceptarFormFormaPago_Click(object sender, RoutedEventArgs e)
        //{
        //    this.DialogHostMetodosPago.IsOpen = false;
        //    this.DrawerHostPagos.IsBottomDrawerOpen = true;
        //}
        ////Abrir formulario para agregar forma pago
        //private void BtnAbrirFormPagos_Click(object sender, RoutedEventArgs e)
        //{
        //    this.DialogHostMetodosPago.IsOpen = true;
        //    this.TituloDialogPagos.Text = "Agregar un nuevo Método de Pago";
        //    this.TxtTituloDrawerHostPagos.Text = "¿Esta seguro de agregar este método de pago?";
        //    this.BtnConfirmarDrawnerPagos.Click += this.EnviarPago_Click;
        //}
        ////evento agregar forma pago
        //private async void EnviarPago_Click(object sender, RoutedEventArgs e)
        //{

        //    try
        //    {
        //        string pago = new JavaScriptSerializer().Serialize(new
        //        {
        //            name = TxtNombrePago.Text,
        //            money = TxtMoneda.Text
        //        });
        //        var Response = await Utilities.Post("payments", pago);
        //        if (Response.IsSuccessStatusCode)
        //        {
        //            string sr = await Response.Content.ReadAsStringAsync();
        //            MessageBox.Show(sr);
        //            CerrarPago();
        //        }
        //        else
        //            MessageBox.Show("hubo un error");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
        ////evento boton actualizar METODOS DE PAGO
        //private  async void BtnAbrirFormEditarPagos_Click(object sender, RoutedEventArgs e)
        //{
        //    FrameworkElement element = e.Source as FrameworkElement;
        //    payments payment = element.DataContext as payments;
        //    DialogHostMetodosPago.IsOpen = true;
        //    this._id = payment._id;
        //    TxtNombrePago.Text = payment.name;
        //    TxtMoneda.Text = payment.money;
        //    TxtTituloDialg.Text = "Editar Métodos de pago";

        //    this.TxtTituloDrawerHostPagos.Text = "¿Esta seguro de actualizar este método de pago?";
        //    this.BtnConfirmarDrawnerPagos.Click += this.ActualizarPago_Click;

        //}
        ////evento actualizar forma pago
        //private async void ActualizarPago_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string payment = new JavaScriptSerializer().Serialize(new
        //        {
        //            name = TxtNombrePago.Text,
        //            money = TxtMoneda.Text

        //        });
        //        var Response = await Utilities.Put("payments/" + this._id + "/", payment);
        //        if (Response.IsSuccessStatusCode)
        //            CerrarPago();
        //        else
        //            MessageBox.Show("hubo un error");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        ////evento boton eliminar forma pago card
        //private void BtnAbrirDrawerEliminarPago_Click(object sender, RoutedEventArgs e)
        //{
        //    FrameworkElement element = e.Source as FrameworkElement;
        //    payments pay = element.DataContext as payments;
        //    this._id = pay._id;

        //    this.DrawerHostPagos.IsBottomDrawerOpen = true;
        //    this.TxtTituloDrawerHostPagos.Text = "¿Esta seguro de eliminar este método de pago?";
        //    this.BtnConfirmarDrawnerPagos.Click += this.EliminarPago_Click;
        //}
        ////Evento eliminar forma pago
        //private async void EliminarPago_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string payment = new JavaScriptSerializer().Serialize(new
        //        {
        //            name = TxtNombrePago.Text,
        //            money = TxtMoneda.Text

        //        });
        //        var Response = await Utilities.Delete("payments/" + this._id + "/");
        //        if (Response.IsSuccessStatusCode)
        //            CerrarPago();
        //        else
        //            MessageBox.Show("hubo un error");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
        ////Evento cancelar form y drawner
        //private void BtnCancelarDrawnerPagos_Click(object sender, RoutedEventArgs e)
        //{
        //    CerrarPago();
        //}
        //private void CerrarPago()
        //{
        //    this._id = null;
        //    this.TxtNombrePago.Clear();
        //    this.TxtMoneda.Clear();
        //    this.TabPagos.UpdateLayout();
        //    this.DialogHostMetodosPago.IsOpen = false;
        //    this.DrawerHostPagos.IsBottomDrawerOpen = false;
        //    this.BtnConfirmarDrawnerPagos.Click -= this.EliminarPago_Click;
        //    this.BtnConfirmarDrawnerPagos.Click -= this.ActualizarPago_Click;
        //    this.BtnConfirmarDrawnerPagos.Click -= this.EnviarPago_Click;
        //}

        //private void Grid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Main();



        //}






    }
}
