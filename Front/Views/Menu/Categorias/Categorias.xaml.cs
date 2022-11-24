using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Nancy.Json;
using Entities;
using System.Windows.Media;
using System.IO;

namespace Front.Views.Menu.Categorias
{
    /// <summary>
    /// Lógica de interacción para Categorias.xaml
    /// </summary>
    public partial class Categorias : UserControl
    {
        public List<Categorie> categories;
        public string id { get; set; }
        public Categorias()
        {
            InitializeComponent();
        }
        //funcion cargar categorias
        public async void Get()
        {
            string RespondCategorie = await Request.Get("categories");
            List<Categorie> categories = JsonConvert.DeserializeObject<List<Categorie>>(RespondCategorie);

            if (categories != null)
            {
                itemsCardsCategorias.ItemsSource = categories;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
        //funcion agregar categoria
        public async void Agregar_Click(object sender, RoutedEventArgs e)
        {
            string categoria = new JavaScriptSerializer().Serialize(new
            {
                name = Formulario.TxtNombreCategoria.Text,
            });
            var response = await Request.Post("categories", categoria, Formulario.FileImg, Formulario.TxtNombreCategoria.Text+".jpg");
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
        //funcion actualizar categoria
        public async void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            string categoria = new JavaScriptSerializer().Serialize(new
            {
                name = Formulario.TxtNombreCategoria.Text,
            });
            var response = await Request.Put("categories/" + id, categoria, Formulario.FileImg, Formulario.TxtNombreCategoria.Text+".jpg");
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
        //funcion eliminar categoria
        public async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var response = await Request.Delete("categories/"+id);
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
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Formulario.TxtTituloDialg.Text = "Agregar Categoria";
            TxtTituloDrawer.Text = "¿Desea agregar el categoria?";
            DialogHost.IsOpen = true;
            BtnConfirmarDrawner.Click += Agregar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }
        //evento click btn actualizar categoria
        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            Categorie categoria = element.DataContext as Categorie;
            id = categoria._id;
            Formulario.CargarForm(categoria);
            Formulario.TxtTituloDialg.Text = "Actualizar Categoria";
            TxtTituloDrawer.Text = "¿Desea actualizar el categoria?";
            DialogHost.IsOpen = true;
            BtnConfirmarDrawner.Click += Actualizar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }
        //evento click boton eliminar categoria
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            Categorie categoria= element.DataContext as Categorie;
            id = categoria._id;

            TxtTituloDrawer.Text = "¿Desea eliminar el categoria?";
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
