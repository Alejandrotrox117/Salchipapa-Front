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

namespace Front.Views.Menu.Productos
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : UserControl
    {
        public ObservableCollection<product> productos;
        private string id;
        public Productos()
        {
            InitializeComponent();
        }
        //funcion obtener clientes
        public async void Get()
        {
            string Response = await Request.Get("products");
            List<Products> returnedData = JsonConvert.DeserializeObject<List<Products>>(Response);
            if (returnedData != null)
            {
                TabsProductos.ItemsSource = returnedData;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
        //evento agregar clientes
        public async void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Formulario.FileImg))
            {
                List<Entities.Topping> ListaToppings = new List<Entities.Topping>();
                foreach (Entities.Topping i in Formulario.ListToppings.Items)
                {
                    ListaToppings.Add(i);
                }
                Categorie categoria = Formulario.CbCategoriaProducto.SelectedItem as Categorie;
                string products = new JavaScriptSerializer().Serialize(new
                {
                    name = Formulario.TxtNombreProducto.Text,
                    description = Formulario.TxtDescripcionProducto.Text,
                    price = Convert.ToDecimal(!string.IsNullOrEmpty(Formulario.TxtPrecioProducto.Text) ? Formulario.TxtPrecioProducto.Text : "0"),
                    stock = Convert.ToInt32(!string.IsNullOrEmpty(Formulario.TxtStockProducto.Text) ? Formulario.TxtStockProducto.Text : "0"),
                    categorie = categoria is not null ? categoria._id : "",
                    toppings = ListaToppings.Select(x => x._id)
                });
                var response = await Request.Post("products", products, Formulario.FileImg, Formulario.TxtNombreProducto.Text + ".jpg");
                string content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    limpiarDrawner();
                    abrirSnack("Se ha agredado correctamente", null);
                }
                else
                {
                    DrawerHost.IsBottomDrawerOpen = false;
                    Error error = JsonConvert.DeserializeObject<Error>(content);
                    abrirSnack("Ha ocurrido un error", error);
                }
            }
            else
            {
                DrawerHost.IsBottomDrawerOpen = false;
                Error error = new Error()
                {
                    message = "img"
                };
                abrirSnack("Ha ocurrido un error", error);
            }
        }
        //evento actualizar clientes
        public async void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            List<Entities.Topping> ListaToppings = new List<Entities.Topping>();
            foreach (Entities.Topping i in Formulario.ListToppings.Items)
            {
                ListaToppings.Add(i);
            }
            Categorie categoria = Formulario.CbCategoriaProducto.SelectedItem as Categorie;
            string products = new JavaScriptSerializer().Serialize(new
            {
                name = Formulario.TxtNombreProducto.Text,
                description = Formulario.TxtDescripcionProducto.Text,
                price = Convert.ToDecimal(!string.IsNullOrEmpty(Formulario.TxtPrecioProducto.Text) ? Formulario.TxtPrecioProducto.Text : "0"),
                stock = Convert.ToInt32(!string.IsNullOrEmpty(Formulario.TxtStockProducto.Text) ? Formulario.TxtStockProducto.Text : "0"),
                categorie = categoria is not null ? categoria._id : "",
                toppings = ListaToppings.Select(x => x._id)
            });

            var response = await Request.Put("products/" + id, products, Formulario.FileImg, Formulario.TxtNombreProducto.Text + ".jpg");
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                limpiarDrawner();
                abrirSnack("Se ha actualizado correctamente", null);
            }
            else
            {
                DrawerHost.IsBottomDrawerOpen = false;
                Error error = JsonConvert.DeserializeObject<Error>(content);
                abrirSnack("Ha ocurrido un error", error);
            }
        }
        //evento eliminar clientes
        public async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var response = await Request.Delete("products/" + id);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                limpiarDrawner();
                abrirSnack("Se ha eliminado correctamente", null);
            }
            else
            {
                DrawerHost.IsBottomDrawerOpen = false;
                Error error = JsonConvert.DeserializeObject<Error>(content);
                abrirSnack("Ha ocurrido un error", error);
            }
        }
        //evento click btn agregar cliente
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Formulario.TxtTituloDialg.Text = "Agregar Producto";
            TxtTituloDrawer.Text = "¿Desea agregar el producto?";
            DialogHost.IsOpen = true;
            //Formulario.CargarCombobox();
            BtnConfirmarDrawner.Click += Agregar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }
        //evento click btn actualizar cliente
        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            product product = element.DataContext as product;
            id = product._id;
            //Formulario.CargarCombobox();
            Formulario.CargarForm(product);
            Formulario.TxtTituloDialg.Text = "Actualizar Producto";
            TxtTituloDrawer.Text = "¿Desea actualizar el producto?";
            DialogHost.IsOpen = true;
            BtnConfirmarDrawner.Click += Actualizar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }
        //evento click boton eliminar cliente
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {

            FrameworkElement element = e.Source as FrameworkElement;
            product product = element.DataContext as product;
            id = product._id;
            TxtTituloDrawer.Text = "¿Desea eliminar el producto?";
            DrawerHost.IsBottomDrawerOpen = true;
            BtnConfirmarDrawner.Click += Eliminar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawner_Click;
        }
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
