using Entities;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Front.Views.Caja.Clientes
{
    /// <summary>
    /// Lógica de interacción para Clientes.xaml
    /// </summary>
    public partial class Clientes : UserControl
    {
        private string id;
        public Clientes()
        {
            InitializeComponent();
        }
        //funcion obtener clientes
        public async void GetClientes()
        {
            string responseClients = await Request.Get("clients");
            List<clientes> returnedDataClients = JsonConvert.DeserializeObject<List<clientes>>(responseClients);
            if (returnedDataClients != null)
            {    
                DataGridClientes.ItemsSource = returnedDataClients;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }//funcion obtener clientes
        public async void PostClientes(object sender, RoutedEventArgs e)
        {
            string cliente = new JavaScriptSerializer().Serialize(new
            {
                ci = FormClientes.CBCedula.Text + FormClientes.TxtCedula.Text,
                name = FormClientes.TxtNombre.Text,
                surname = FormClientes.TxtApellido.Text,
                phone = FormClientes.CBTelefono.Text + FormClientes.TxtTelefono.Text,
                address = FormClientes.TxtDireccion.Text
            }); 
            var response = await Request.Post("clients", cliente);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                limpiarDrawner();
                abrirSnack("Se ha agredado correctamente", null);
            }
            else
            {
                DrawerHostClientes.IsBottomDrawerOpen = false;
                Errors error = JsonConvert.DeserializeObject<Errors>(content);
                abrirSnack("Ha ocurrido un error", error);
            }
        }
        //funcion actualizar clientes
        public async void PutClientes(object sender, RoutedEventArgs e)
        {
            string cliente = new JavaScriptSerializer().Serialize(new
            {
                ci = FormClientes.CBCedula.Text + FormClientes.TxtCedula.Text,
                name = FormClientes.TxtNombre.Text,
                surname = FormClientes.TxtApellido.Text,
                phone = FormClientes.CBTelefono.Text + FormClientes.TxtTelefono.Text,
                address = FormClientes.TxtDireccion.Text
            });
            var response = await Request.Put("clients/"+id, cliente);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                limpiarDrawner();
                abrirSnack("Se ha actualizado correctamente", null);
            }
            else
            {
                DrawerHostClientes.IsBottomDrawerOpen = false;
                Errors error = JsonConvert.DeserializeObject<Errors>(content);
                abrirSnack("Ha ocurrido un error", error);
            }
        }
        //funcion eliminar clientes
        public async void DeleteClientes(object sender, RoutedEventArgs e)
        {
            var response = await Request.Delete("clients/"+id);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                limpiarDrawner();
                abrirSnack("Se ha eliminado correctamente", null);
            }
            else
            {
                DrawerHostClientes.IsBottomDrawerOpen = false;
                Errors error = JsonConvert.DeserializeObject<Errors>(content);
                abrirSnack("Ha ocurrido un error", error);
            }
        }
        //evento click btn agregar cliente
        private void BtnAgregarCliente_Click(object sender, RoutedEventArgs e)
        {
            FormClientes.TxtTituloDialg.Text = "Agregar Cliente";
            TxtTituloDrawerCliente.Text = "¿Desea agregar el cliente?";
            DialogHostClientes.IsOpen = true;
            BtnConfirmarClienteDrawner.Click += PostClientes;
            BtnCancelarClienteDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }
        //evento click btn actualizar cliente
        private void BtnActualizarCliente_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            clientes client = element.DataContext as clientes;
            id = client._id;
            FormClientes.CargarForm(client);

            FormClientes.TxtTituloDialg.Text = "Actualizar Cliente";
            TxtTituloDrawerCliente.Text = "¿Desea actualizar el cliente?";
            DialogHostClientes.IsOpen = true;
            BtnConfirmarClienteDrawner.Click += PutClientes;
            BtnCancelarClienteDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }
        //evento click boton eliminar cliente
        private void BtnEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            clientes client = element.DataContext as clientes;
            id = client._id;

            TxtTituloDrawerCliente.Text = "¿Desea eliminar el cliente?";
            DrawerHostClientes.IsBottomDrawerOpen = true;
            BtnConfirmarClienteDrawner.Click += DeleteClientes;
            BtnCancelarClienteDrawner.Click += BtnCancelarDrawner_Click;
        }
        //funcion abrir notificacion
        private void abrirSnack(string mensaje, Errors error)
        {
            var bc = new BrushConverter();
            TxtSnackbarClientes.Text = mensaje;
            SnackBarNotificacionClientes.IsActive = true;
            if (error is null)
            {
                SnackBarNotificacionClientes.Background = (Brush) bc.ConvertFrom("#00695c");
                BtnSnackbarClientes.Click += BtnSnackbarClientesCerrar_Click;
            }
            else
            {
                FormClientes.MostrarErrores(error);
                SnackBarNotificacionClientes.Background = (Brush) bc.ConvertFrom("#f44c58");
                BtnSnackbarClientes.Click += BtnSnackbarClientesAbrirForm_Click;
            }
        }
        private void limpiarDrawner()
        {
            DrawerHostClientes.IsBottomDrawerOpen = false;
            BtnConfirmarClienteDrawner.Click -= PostClientes;
            BtnConfirmarClienteDrawner.Click -= PutClientes;
            BtnConfirmarClienteDrawner.Click -= DeleteClientes;
            BtnCancelarClienteDrawner.Click -= BtnCancelarDrawnerAbrirForm_Click;
            BtnCancelarClienteDrawner.Click -= BtnCancelarDrawner_Click;
        }
        //evento btn snack cerrar 
        private void BtnSnackbarClientesCerrar_Click(object sender, RoutedEventArgs e)
        {
            GetClientes();
            SnackBarNotificacionClientes.IsActive = false;
        }
        //evento btn snack abrir form 
        private void BtnSnackbarClientesAbrirForm_Click(object sender, RoutedEventArgs e)
        {
            SnackBarNotificacionClientes.IsActive = false;
            DialogHostClientes.IsOpen = true;
        }
        //evento cierre snack siempre limpiar
        private void SnackBarNotificacionClientes_IsActiveChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            if (!e.NewValue)
            {
                BtnSnackbarClientes.Click -= BtnSnackbarClientesCerrar_Click;
                BtnSnackbarClientes.Click -= BtnSnackbarClientesAbrirForm_Click;
            }
        }
        //evento click boton aceptar form
        private void BtnAceptarDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogHostClientes.IsOpen = false;
            DrawerHostClientes.IsBottomDrawerOpen = true;
        }
        //evento click boton cancelar form
        private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        {
            DialogHostClientes.IsOpen = false;
            FormClientes.LimpiarForm();
            limpiarDrawner();
        }
        //evento click boton cancelar drawner para abrir form
        private void BtnCancelarDrawnerAbrirForm_Click(object sender, RoutedEventArgs e)
        {
            DrawerHostClientes.IsBottomDrawerOpen = false;
            DialogHostClientes.IsOpen = true;
        }
        //evento click boton cancelar drawner para abrir form
        private void BtnCancelarDrawner_Click(object sender, RoutedEventArgs e)
        {
            FormClientes.LimpiarForm();
            limpiarDrawner();
        }
    }
}
