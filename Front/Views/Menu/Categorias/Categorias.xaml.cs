using Entities;
using Nancy.Json;
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

namespace Front.Views.Menu.Categorias
{
    /// <summary>
    /// Lógica de interacción para Categorias.xaml
    /// </summary>
    public partial class Categorias : UserControl
    {
        public ObservableCollection<categories> categories;
        public string titulo { get; set; }
        public Categorias()
        {
            InitializeComponent();
        }
        public async void GetCategorias()
        {
            string RespondCategorie = await Request.Get("categories");
            List<categories> categorie = Newtonsoft.Json.JsonConvert.DeserializeObject<List<categories>>(RespondCategorie);
            categories = new ObservableCollection<categories>(categorie);

            if (categorie != null)
            {
                itemsCardsCategorias.ItemsSource = categories;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        public async void PostCategorias(object sender, RoutedEventArgs e)
        {
            string categorias = new JavaScriptSerializer().Serialize(new
            {
                name = DialogCategorias.TxtNombreCategoria.Text,
            });
            var Response = await Request.Post("categories/", categorias);
            if (Response.IsSuccessStatusCode)
            {
                TxtSnackbar.Text = "Se ha agregado correctamente";
                SnackBarNotificacion.IsActive = true;

            }
            else
                MessageBox.Show("hubo un error");

        }

        private void BtnSnackbar_Click(object sender, RoutedEventArgs e)
        {
            SnackBarNotificacion.IsActive = false;
        }

        private void BtnAgregarCategoria_Click(object sender, RoutedEventArgs e)
        {
            DialogHostCategorias.IsOpen = true;
            DialogCategorias.TxtTituloDialgCategorias.Text = "Agregar Categoria";
            TxtTituloDrawerHostCategorias.Text = "¿Desea agregar una nueva categoria?";
            BtnConfirmarCategoriaDrawner.Click+=PostCategorias;
        }

        private void BtnAceptarDialogCategorias_Click(object sender, RoutedEventArgs e)
        {
            DialogHostCategorias.IsOpen = false;
            DrawerHostCategorias.IsBottomDrawerOpen = true;
        }

        private void LimpiarEventos()
        {
            DrawerHostCategorias.IsBottomDrawerOpen = false;
            BtnConfirmarCategoriaDrawner.Click-=PostCategorias;
            TxtTituloDrawerHostCategorias.Text = "";
        
        }

        private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        {
            LimpiarEventos();
        }

        private void BtnCancelarCategoriaDrawner_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
