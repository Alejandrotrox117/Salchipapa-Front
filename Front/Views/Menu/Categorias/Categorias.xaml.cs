using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Nancy.Json;
using Entities;

namespace Front.Views.Menu.Categorias
{
    /// <summary>
    /// Lógica de interacción para Categorias.xaml
    /// </summary>
    public partial class Categorias : UserControl
    {
        public List<categories> categories;
        public string id { get; set; }

        public Categorias()
        {
            InitializeComponent();
        }
        //funcion cargar categorias
        public async void GetCategorias()
        {
            string RespondCategorie = await Request.Get("categories");
            List<categories> categories = JsonConvert.DeserializeObject<List<categories>>(RespondCategorie);

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
        public async void PostCategoria(object sender, RoutedEventArgs e)
        {
            string categoria = new JavaScriptSerializer().Serialize(new
            {
                name = DialogCategorias.TxtNombreCategoria.Text,
            });
            var Response = await Request.Post("categories/", categoria);
            if (Response.IsSuccessStatusCode)
            {
                TxtSnackbar.Text = "Se ha agregado correctamente";
                SnackBarNotificacion.IsActive = true;
                LimpiarEventos();
            }
            else
                MessageBox.Show("hubo un error");

        }
        //funcion actualizar categoria
        public async void PutCategoria(object sender, RoutedEventArgs e)
        {
            string categorias = new JavaScriptSerializer().Serialize(new
            {
                name = DialogCategorias.TxtNombreCategoria.Text,
            });
            var Response = await Request.Put("categories/"+id, categorias);
            if (Response.IsSuccessStatusCode)
            {
                TxtSnackbar.Text = "Se ha actualizado correctamente";
                SnackBarNotificacion.IsActive = true;
                LimpiarEventos();
            }
            else
            {
                string error = await Response.Content.ReadAsStringAsync();
                MessageBox.Show(error);
            }

        }
        //funcion eliminar categoria
        public async void DeleteCategoria(object sender, RoutedEventArgs e)
        {
            var Response = await Request.Delete("categories/"+id);
            if (Response.IsSuccessStatusCode)
            {
                TxtSnackbar.Text = "Se ha eliminado correctamente";
                SnackBarNotificacion.IsActive = true;
                LimpiarEventos();
            }
            else
                MessageBox.Show("hubo un error");

        }
        //evento click boton agregar categoria
        private void BtnAgregarCategoria_Click(object sender, RoutedEventArgs e)
        {
            DialogCategorias.TxtTituloDialgCategorias.Text = "Agregar Categoria";
            DialogHostCategorias.IsOpen = true;
            TxtTituloDrawerHostCategorias.Text = "¿Desea agregar una nueva categoria?";
            BtnConfirmarCategoriaDrawner.Click+=PostCategoria;
        }
        //evento click boton actualizar categoria
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            categories categorie = element.DataContext as categories;
            id = categorie._id;
         
            DialogCategorias.TxtNombreCategoria.Text = categorie.name;
            DialogCategorias.TxtTituloDialgCategorias.Text = "Actualizar Categoria";

            DialogHostCategorias.IsOpen = true;
            TxtTituloDrawerHostCategorias.Text = "¿Desea actualizar la categoria?";
            BtnConfirmarCategoriaDrawner.Click+=PutCategoria;
        }
        //evento click boton eliminar categoria
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            categories categorie = element.DataContext as categories;
            id = categorie._id;

            TxtTituloDrawerHostCategorias.Text = "¿Desea eliminar la categoria?";
            DrawerHostCategorias.IsBottomDrawerOpen = true;
            BtnConfirmarCategoriaDrawner.Click+=DeleteCategoria;
        }
        //evento click boton aceptar form categoria
        private void BtnAceptarDialogCategorias_Click(object sender, RoutedEventArgs e)
        {
            DialogHostCategorias.IsOpen = false;
            DrawerHostCategorias.IsBottomDrawerOpen = true;
        }
        //funcion limpiar form y drawner categoria
        private void LimpiarEventos()
        {
            DrawerHostCategorias.IsBottomDrawerOpen = false;
            BtnConfirmarCategoriaDrawner.Click-=PostCategoria;
            BtnConfirmarCategoriaDrawner.Click-=PutCategoria;
            BtnConfirmarCategoriaDrawner.Click-=DeleteCategoria;
            TxtTituloDrawerHostCategorias.Text = "";
        
        }
        //evento click boton cerrar form categoria
        private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        {
            LimpiarEventos();
        }
        //evento click boton cancelar drawner categoria
        private void BtnCancelarCategoriaDrawner_Click(object sender, RoutedEventArgs e)
        {

        }
        //evento click boton snackbar
        private void BtnSnackbar_Click(object sender, RoutedEventArgs e)
        {
            SnackBarNotificacion.IsActive = false;
        }
    }
}
