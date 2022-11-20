using Nancy.Json;
using Entities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Newtonsoft.Json;

namespace Front.Views.Menu
{
    /// <summary>
    /// Lógica de interacción para Toppings.xaml
    /// </summary>
    public partial class Topping : UserControl
    {

        private string id;
        public Topping()
        {
            InitializeComponent();
        }
        //funcion obtener clientes
        public async void Get()
        {
            string responseClients = await Request.Get("toppings");
            List<toppings> returnedDataClients = JsonConvert.DeserializeObject<List<toppings>>(responseClients);
            if (returnedDataClients != null)
            {
                DataGridToppings.ItemsSource = returnedDataClients;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
        //evento agregar clientes
        public async void Agregar_Click(object sender, RoutedEventArgs e)
        {
            decimal precio = Convert.ToDecimal(!string.IsNullOrEmpty(Formulario.TxtPrecioTopping.Text) ? Formulario.TxtPrecioTopping.Text : "0");
            string topping = new JavaScriptSerializer().Serialize(new
            {
                name = Formulario.TxtNombreTopping.Text,
                price = precio,
                stock = Formulario.CheckboxTp.IsChecked
            });
            var response = await Request.Post("toppings", topping);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                limpiarDrawner();
                abrirSnack("Se ha agredado correctamente", null);
            }
            else
            {
                DrawerHost.IsBottomDrawerOpen = false;
                MessageBox.Show(content);
                Errors error = JsonConvert.DeserializeObject<Errors>(content);
                abrirSnack("Ha ocurrido un error", error);
            }
        }
        //evento actualizar clientes
        public async void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            decimal precio = Convert.ToDecimal(!string.IsNullOrEmpty(Formulario.TxtPrecioTopping.Text) ? Formulario.TxtPrecioTopping.Text : "0");
            string topping = new JavaScriptSerializer().Serialize(new
            {
                name = Formulario.TxtNombreTopping.Text,
                price = precio,
                stock = Formulario.CheckboxTp.IsChecked
            });
            var response = await Request.Put("toppings/"+id, topping);
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
        //evento eliminar clientes
        public async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var response = await Request.Delete("toppings/"+id);
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
        //evento click btn agregar cliente
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Formulario.TxtTituloDialg.Text = "Agregar Topping";
            TxtTituloDrawer.Text = "¿Desea agregar el topping?";
            DialogHost.IsOpen = true;
            BtnConfirmarDrawner.Click += Agregar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }
        //evento click btn actualizar cliente
        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            toppings topping = element.DataContext as toppings;
            id = topping._id;
            Formulario.CargarForm(topping);

            Formulario.TxtTituloDialg.Text = "Actualizar Topping";
            TxtTituloDrawer.Text = "¿Desea actualizar el topping?";
            DialogHost.IsOpen = true;
            BtnConfirmarDrawner.Click += Actualizar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }
        //evento click boton eliminar cliente
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            toppings toppings = element.DataContext as toppings;
            id = toppings._id;

            TxtTituloDrawer.Text = "¿Desea eliminar el topping?";
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
