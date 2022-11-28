using Entities;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Front.Views.Caja.Pedidos_Finalizados
{
    /// <summary>
    /// Lógica de interacción para PedidosFinalizados.xaml
    /// </summary>
    public partial class PedidosFinalizados : UserControl
    {
        public ObservableCollection<Orders> Orders { get; set; }

        public PedidosFinalizados()
        {
            InitializeComponent();
        }
        //funcion obtener Pedidos
        public async void Get()
        {
            string response = await Request.Get("orders?filter=true");
            Orders = JsonConvert.DeserializeObject<ObservableCollection<Orders>>(response);
            if (Orders != null)
            {
                itemCardTerminados.ItemsSource = Orders;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }



        private void BtnVentas_Click(object sender, RoutedEventArgs e)
        {
            TabcontrolFinalizados.SelectedIndex = 1;
            PageVentas.Get();
        }

        private void BtnPFinalizados_Click(object sender, RoutedEventArgs e)
        {
            Get();
            TabcontrolFinalizados.SelectedIndex = 0;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Get();
        }

        private void CheckboxFinalizado_Click(object sender, RoutedEventArgs e)
        {
            CheckBox check = sender as CheckBox;
            FrameworkElement element = e.Source as FrameworkElement;
            Orders order = element.DataContext as Orders;
            if (check.IsChecked.Value) {
                
                Formulario.Selecteds.Add(order);
            }
            else
            {
                Formulario.Selecteds.Remove(order);
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Formulario.CargarLista();
            DialogHost.IsOpen = true;
        }

        private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
            Formulario.LimpiarForm();
        }

        ////evento agregar PedidosFinalizados
        //public async void Agregar_Click(object sender, RoutedEventArgs e)
        //{
        //    string PedidosFinalizados = new JavaScriptSerializer().Serialize(new
        //    {
        //        ci = Formulario.CBCedula.Text + Formulario.TxtCedula.Text,
        //        name = Formulario.TxtNombre.Text,
        //        surname = Formulario.TxtApellido.Text,
        //        phone = Formulario.CBTelefono.Text + Formulario.TxtTelefono.Text,
        //        address = Formulario.TxtDireccion.Text
        //    });
        //    var response = await Request.Post("clients", cliente);
        //    string content = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        limpiarDrawner();
        //        abrirSnack("Se ha agredado correctamente", null);
        //    }
        //    else
        //    {
        //        DrawerHost.IsBottomDrawerOpen = false;
        //        Error error = JsonConvert.DeserializeObject<Error>(content);
        //        abrirSnack("Ha ocurrido un error", error);
        //    }
        //}
        //evento actualizar clientes
        //public async void Actualizar_Click(object sender, RoutedEventArgs e)
        //{
        //    string cliente = new JavaScriptSerializer().Serialize(new
        //    {
        //        ci = Formulario.CBCedula.Text + Formulario.TxtCedula.Text,
        //        name = Formulario.TxtNombre.Text,
        //        surname = Formulario.TxtApellido.Text,
        //        phone = Formulario.CBTelefono.Text + Formulario.TxtTelefono.Text,
        //        address = Formulario.TxtDireccion.Text
        //    });
        //    var response = await Request.Put("clients/" + id, cliente);
        //    string content = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        limpiarDrawner();
        //        abrirSnack("Se ha actualizado correctamente", null);
        //    }
        //    else
        //    {
        //        DrawerHost.IsBottomDrawerOpen = false;
        //        Error error = JsonConvert.DeserializeObject<Error>(content);
        //        abrirSnack("Ha ocurrido un error", error);
        //    }
        //}
        //evento eliminar clientes
        //public async void Eliminar_Click(object sender, RoutedEventArgs e)
        //{
        //    var response = await Request.Delete("clients/" + id);
        //    string content = await response.Content.ReadAsStringAsync();
        //    if (response.IsSuccessStatusCode)
        //    {
        //        limpiarDrawner();
        //        abrirSnack("Se ha eliminado correctamente", null);
        //    }
        //    else
        //    {
        //        DrawerHost.IsBottomDrawerOpen = false;
        //        Error error = JsonConvert.DeserializeObject<Error>(content);
        //        abrirSnack("Ha ocurrido un error", error);
        //    }
        //}
        //evento click btn agregar cliente
        //private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    Formulario.TxtTituloDialg.Text = "Agregar Cliente";
        //    TxtTituloDrawer.Text = "¿Desea agregar el cliente?";
        //    DialogHost.IsOpen = true;
        //    BtnConfirmarDrawner.Click += Agregar_Click;
        //    BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        //}
        ////evento click btn actualizar cliente
        //private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        //{
        //    FrameworkElement element = e.Source as FrameworkElement;
        //    Client client = element.DataContext as Client;
        //    id = client._id;
        //    Formulario.CargarForm(client);

        //    Formulario.TxtTituloDialg.Text = "Actualizar Cliente";
        //    TxtTituloDrawer.Text = "¿Desea actualizar el cliente?";
        //    DialogHost.IsOpen = true;
        //    BtnConfirmarDrawner.Click += Actualizar_Click;
        //    BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        //}
        //evento click boton eliminar cliente
        //private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        //{
        //    FrameworkElement element = e.Source as FrameworkElement;
        //    Client client = element.DataContext as Client;
        //    id = client._id;

        //    TxtTituloDrawer.Text = "¿Desea eliminar el cliente?";
        //    DrawerHost.IsBottomDrawerOpen = true;
        //    BtnConfirmarDrawner.Click += Eliminar_Click;
        //    BtnCancelarDrawner.Click += BtnCancelarDrawner_Click;
        //}
        //funcion abrir notificacion
        private void abrirSnack(string mensaje, Error error)
        {
            var bc = new BrushConverter();
            TxtSnackbar.Text = mensaje;
            SnackBarNotificacion.IsActive = true;
            if (error is null)
            {
                SnackBarNotificacion.Background = (Brush)bc.ConvertFrom("#00695c");
                BtnSnackbar.Click += BtnSnackbarCerrar_Click;
            }
            else
            {
                Formulario.MostrarErrores(error);
                SnackBarNotificacion.Background = (Brush)bc.ConvertFrom("#f44c58");
                BtnSnackbar.Click += BtnSnackbarAbrirForm_Click;
            }
        }
        //funcion limpiar drawner
        //private void limpiarDrawner()
        //{
        //    DrawerHost.IsBottomDrawerOpen = false;
        //    BtnConfirmarDrawner.Click -= Agregar_Click;
        //    BtnConfirmarDrawner.Click -= Actualizar_Click;
        //    BtnConfirmarDrawner.Click -= Eliminar_Click;
        //    BtnCancelarDrawner.Click -= BtnCancelarDrawnerAbrirForm_Click;
        //    BtnCancelarDrawner.Click -= BtnCancelarDrawner_Click;
        //}
        ////evento btn snack cerrar 
        private void BtnSnackbarCerrar_Click(object sender, RoutedEventArgs e)
        {
            Get();
            Formulario.LimpiarForm();
            SnackBarNotificacion.IsActive = false;
        }
        //evento btn snack abrir form 
        private void BtnSnackbarAbrirForm_Click(object sender, RoutedEventArgs e)
        {
            SnackBarNotificacion.IsActive = false;
            DialogHost.IsOpen = true;
        }
        //evento cierre snack siempre limpiar
        private void SnackBarNotificacion_IsActiveChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            if (!e.NewValue)
            {
                BtnSnackbar.Click -= BtnSnackbarCerrar_Click;
                BtnSnackbar.Click -= BtnSnackbarAbrirForm_Click;
            }
        }
        //evento click boton aceptar form
        private void BtnAceptarDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
            DrawerHost.IsBottomDrawerOpen = true;
        }
        //evento click boton cancelar form
        //private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        //{
        //    DialogHost.IsOpen = false;
        //    Formulario.LimpiarForm();
        //    //limpiarDrawner();
        //}
        //evento click boton cancelar drawner para abrir form
        private void BtnCancelarDrawnerAbrirForm_Click(object sender, RoutedEventArgs e)
        {
            DrawerHost.IsBottomDrawerOpen = false;
            DialogHost.IsOpen = true;
        }
        //evento click boton cancelar drawner para abrir form
        //private void BtnCancelarDrawner_Click(object sender, RoutedEventArgs e)
        //{
        //    Formulario.LimpiarForm();
        //    limpiarDrawner();
        //}

    }
}
