using Nancy.Json;
using Entities;
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
using Newtonsoft.Json;

namespace Front.Views.Menu
{
    /// <summary>
    /// Lógica de interacción para Toppings.xaml
    /// </summary>
    public partial class ToppingsMenu : UserControl
    {

        public string id;  
        public ToppingsMenu()
        {
            InitializeComponent();
           
        }
        //Gets
        //Obtener Toppings
        public async void Get()
        {
            string RespondTopping = await Request.Get("toppings");
            List<Entities.Toppings> toppings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Entities.Toppings>>(RespondTopping);
            if (toppings != null)
            {
                DataGridToppings.ItemsSource = toppings;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }




        //Delete
        //evento click boton eliminar categoria
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            Entities.Toppings toppings = element.DataContext as Entities.Toppings;
            id = toppings._id;
            TxtDrawer.Text = "¿Desea eliminar el Topping?";
            DrawerHost.IsBottomDrawerOpen = true;
            BtnConfirmarDrawner.Click += Eliminar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawner_Click;
        }
        //funcion eliminar categoria
        public async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var response = await Request.Delete("categories/" + id);
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

        private void limpiarDrawner()
        {
            DrawerHost.IsBottomDrawerOpen = false;
            BtnConfirmarDrawner.Click -= Agregar_Click;
            BtnConfirmarDrawner.Click -= Actualizar_Click;
            BtnConfirmarDrawner.Click -= Eliminar_Click;
            BtnCancelarDrawner.Click -= BtnCancelarDrawnerAbrirForm_Click;
            BtnCancelarDrawner.Click -= BtnCancelarDrawner_Click;
        }

        //Post
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = true;
            Formulario.TxtDialog.Text = "Agregar Toppings";
            TxtDrawer.Text = "¿Desea agregar este topping?";
            BtnConfirmarDrawner.Click += Agregar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }
        public async void Agregar_Click(object sender, RoutedEventArgs e)
        {
            string toppings = new JavaScriptSerializer().Serialize(new
            {
                name = Formulario.TxtNombreTopping.Text,
                price = Convert.ToDecimal( string.IsNullOrEmpty(Formulario.TxtPrecioTopping.Text) ? Formulario.TxtPrecioTopping.Text : 0),
                stock = Formulario.CheckboxTp.IsChecked

            }); 
            var response = await Request.Post("toppings", toppings);
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

        //funcion actualizar categoria
        public async void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            string toppings = new JavaScriptSerializer().Serialize(new
            {
                name = Formulario.TxtNombreTopping.Text,
                price = Convert.ToDecimal(string.IsNullOrEmpty(Formulario.TxtPrecioTopping.Text) ? Formulario.TxtPrecioTopping.Text : 0),
                stock = Formulario.CheckboxTp.IsChecked
            });
            var response = await Request.Put("toppings/" + id, toppings);
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
        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            Entities.Toppings toppings = element.DataContext as Entities.Toppings;
            id = toppings._id;
            Formulario.CargarForm(toppings);

            Formulario.TxtDialog.Text = "Actualizar Toppings";
            TxtDrawer.Text = "¿Desea actualizar el Topping?";
            DialogHost.IsOpen = true;
            BtnConfirmarDrawner.Click += Actualizar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }

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
