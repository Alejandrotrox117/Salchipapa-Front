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

namespace Front.Views.Caja.Pagos
{
    /// <summary>
    /// Lógica de interacción para MetodosDePago.xaml
    /// </summary>
    public partial class MetodosDePago : UserControl
    {
        private string id;
        public MetodosDePago()
        {
            InitializeComponent();
        }
        //funcion obtener payments
        public async void Get()
        {
            string response = await Request.Get("payments");
            List<payments> returnedData = JsonConvert.DeserializeObject<List<payments>>(response);
            if (returnedData != null)
            {
                ListBoxMetodosPagos.ItemsSource = returnedData;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
        //evento agregar payments
        public async void Agregar_Click(object sender, RoutedEventArgs e)
        {
            string pago = new JavaScriptSerializer().Serialize(new
            {
                
                name = Formulario.TxtNombrePago.Text,
                money = Formulario.CBMoneda.Text
            });
            var response = await Request.Post("payments", pago);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                limpiarDrawner();
                abrirSnack("Se ha agredado correctamente", null);
            }
            else
            {
                DrawerHost.IsBottomDrawerOpen = false;
                Errors error = JsonConvert.DeserializeObject<Errors>(content);
                abrirSnack("Ha ocurrido un error", error);
            }
        }
        //evento actualizar payments
        public async void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            string pago = new JavaScriptSerializer().Serialize(new
            {

                name = Formulario.TxtNombrePago.Text,
                money = Formulario.CBMoneda.Text
            });
            var response = await Request.Put("payments/"+id, pago);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                limpiarDrawner();
                abrirSnack("Se ha actualizado correctamente", null);
            }
            else
            {
                DrawerHost.IsBottomDrawerOpen = false;
                Errors error = JsonConvert.DeserializeObject<Errors>(content);
                abrirSnack("Ha ocurrido un error", error);
            }
        }
        //evento eliminar pago
        public async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var response = await Request.Delete("payments/"+id);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                limpiarDrawner();
                abrirSnack("Se ha eliminado correctamente", null);
            }
            else
            {
                DrawerHost.IsBottomDrawerOpen = false;
                Errors error = JsonConvert.DeserializeObject<Errors>(content);
                abrirSnack("Ha ocurrido un error", error);
            }
        }
        //evento click btn agregar pago
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Formulario.TxtTituloDialg.Text = "Agregar Método de Pago";
            TxtTituloDrawer.Text = "¿Desea agregar el método de pago?";
            DialogHost.IsOpen = true;
            BtnConfirmarDrawner.Click += Agregar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }
        //evento click btn actualizar pago
        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            payments pago = element.DataContext as payments;
            id = pago._id;
            Formulario.CargarForm(pago);

            Formulario.TxtTituloDialg.Text = "Actualizar Método de Pago";
            TxtTituloDrawer.Text = "¿Desea actualizar el método de pago?";
            DialogHost.IsOpen = true;
            BtnConfirmarDrawner.Click += Actualizar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }
        //evento click boton eliminar pago
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            payments pago = element.DataContext as payments;
            id = pago._id;

            TxtTituloDrawer.Text = "¿Desea eliminar el método de pago?";
            DrawerHost.IsBottomDrawerOpen = true;
            BtnConfirmarDrawner.Click += Eliminar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawner_Click;
        }
        //funcion abrir notificacion
        private void abrirSnack(string mensaje, Errors error)
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
        private void limpiarDrawner()
        {
            DrawerHost.IsBottomDrawerOpen = false;
            BtnConfirmarDrawner.Click -= Agregar_Click;
            BtnConfirmarDrawner.Click -= Actualizar_Click;
            BtnConfirmarDrawner.Click -= Eliminar_Click;
            BtnCancelarDrawner.Click -= BtnCancelarDrawnerAbrirForm_Click;
            BtnCancelarDrawner.Click -= BtnCancelarDrawner_Click;
        }
        //evento btn snack cerrar 
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
        private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
            Formulario.LimpiarForm();
            limpiarDrawner();
        }
        //evento click boton cancelar drawner para abrir form
        private void BtnCancelarDrawnerAbrirForm_Click(object sender, RoutedEventArgs e)
        {
            DrawerHost.IsBottomDrawerOpen = false;
            DialogHost.IsOpen = true;
        }
        //evento click boton cancelar drawner para abrir form
        private void BtnCancelarDrawner_Click(object sender, RoutedEventArgs e)
        {
            Formulario.LimpiarForm();
            limpiarDrawner();
        }
    }
}
