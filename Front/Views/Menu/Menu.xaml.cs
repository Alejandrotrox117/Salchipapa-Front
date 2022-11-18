using Entities;
using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
using System.Collections.ObjectModel;
using Nancy.Json;
namespace Front.Views.Menu
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public ObservableCollection<categories> categories;
        public ObservableCollection<products> productos;
        private string _id = null;
        private static readonly HttpClient client = new HttpClient();
        public Menu()
        {
            InitializeComponent();
        
        }

       
        private async void Main()
        {
            string responseGrupos = await Utilities.Get("products?extendeData=true");
            string RespondCategorie = await Utilities.Get("categories");
            string RespondTopping = await Utilities.Get("toppings");
            List<Grupos> group = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Grupos>>(responseGrupos);
            List<categories> categorie = Newtonsoft.Json.JsonConvert.DeserializeObject<List<categories>>(RespondCategorie);
            List<Toppings> toppings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Toppings>>(RespondTopping);
            categories = new ObservableCollection<categories>(categorie);

            if (group != null)
            {
                TabsProductos.ItemsSource = group;
                DataGridToppings.ItemsSource = toppings;
                itemsCardsCategorias.ItemsSource = categories;
                CbCategoriaProducto.ItemsSource = categorie;
                CBToppings.ItemsSource = toppings;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }


       

        private void Border_Initialized(object sender, EventArgs e)
        {
            Main();
        }

       

        private void CBToppings_KeyDown(object sender, KeyEventArgs e)
        {
            ListToppings.Items.Add(CBToppings.SelectedItem);
        }

        

        




        //EVENTOS CATEGORIAS
        
      
        private void BtnActualizarCategorias_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            categories categorie = element.DataContext as categories;
            DialogHostCategorias.IsOpen = true;
            this._id = categorie._id;
            TxtNombreCategoria.Text = categorie.name;
            TxtTituloDialgCategorias.Text = "Editar Categorías";
            this.BtnConfirmarCategoriaDrawner.Click += this.ActualizarCategoria_Click;
           
            this.TxtTituloDialgCategorias.Text = "Actualizar Cliente";
            this.TxtTituloDrawerHostCategorias.Text = "¿Desea actualizar esta categoría?";
            

        }

       
        private async void BtnActualizarCategoria_Click(object sender, RoutedEventArgs e)
        {
            string categorias = new JavaScriptSerializer().Serialize(new
            {
                name = TxtNombreCategoria.Text,
            });
            var Response = await Utilities.Put("categories/" + _id + "/", categorias);
            if (Response.IsSuccessStatusCode)
                DialogHostCategorias.IsOpen = false;
            else
                MessageBox.Show("hubo un error");
        }

        private void BtnAceptarDialogCategorias_Click(object sender, RoutedEventArgs e)
        {
            this.DialogHostCategorias.IsOpen = false;
            this.DrawerHostCategorias.IsBottomDrawerOpen = true;
        }
        private void BtnAgregarCategoria_Click(object sender, RoutedEventArgs e)
        {
            this.DialogHostCategorias.IsOpen = true;
            this.TxtTituloDialgCategorias.Text = "Agregar Nueva Categoría";
            this.TxtTituloDrawerHostCategorias.Text = "¿Desea agregar esta nueva categoría?";
            this.BtnConfirmarCategoriaDrawner.Click += this.EnviarCategoria_Click;
        }
        private async void EnviarCategoria_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string js = new JavaScriptSerializer().Serialize(new
                {
                    name = TxtNombreCategoria.Text
                });
                var Response = await Utilities.Post("categories", js);
                if (Response.IsSuccessStatusCode)
                {
                    string sr = await Response.Content.ReadAsStringAsync();
                    // List<categories> categorie = Newtonsoft.Json.JsonConvert.DeserializeObject<List<categories>>(sr);
                    DialogHostCategorias.IsOpen = false;
                    TxtNombreCategoria.Clear();

                }
                else
                    MessageBox.Show("hubo un error" + js);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async void EliminarCategoria_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string js = new JavaScriptSerializer().Serialize(new
                {
                    name = TxtNombreCategoria.Text,
                });
                var Response = await Utilities.Delete("categories/" + this._id + '/');
                if (Response.IsSuccessStatusCode)
                {
                    CerrarCategoria();
                }
                else
                {
                    string sr = await Response.Content.ReadAsStringAsync();
                    MessageBox.Show(sr);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async void ActualizarCategoria_Click(object sender, RoutedEventArgs e)
        {
            string js = new JavaScriptSerializer().Serialize(new
            {
                name = TxtNombreCategoria.Text,
            });
            var Response = await Utilities.Put("categories/" + _id + "/", js);
            if (Response.IsSuccessStatusCode)
                CerrarCategoria();
            else
                MessageBox.Show("hubo un error");

        }
        private void CerrarCategoria()
        {
            this._id = null;
            DialogHostCategorias.IsOpen = false;
            DrawerHostCategorias.IsBottomDrawerOpen = false;
            this.BtnConfirmarCategoriaDrawner.Click -= this.EliminarCategoria_Click;
            this.BtnConfirmarCategoriaDrawner.Click -= this.ActualizarCategoria_Click;
            this.BtnConfirmarCategoriaDrawner.Click -= this.EnviarCategoria_Click;
        }

        private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        {
            CerrarCategoria();
        }


        // PRODUCTOS
        private void BtnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            DialogHostProductos.IsOpen = true;
          
            this.TxtTituloDialogProductos.Text = "Agregar Nuevo Producto";
            this.TxtTituloDrawerProductos.Text = "¿Desea agregar este Producto?";
            //this.BtnConfirmarClienteDrawner.Click += this.EnviarCliente_Click;
           
        }
        private void BtnEnviarProducto_Click(object sender, RoutedEventArgs e)
        {
            List<Toppings> ListaToppings = new List<Toppings>();
            foreach (Toppings i in ListToppings.Items)
            {
                ListaToppings.Add(i);
            }
            categories categoria = CbCategoriaProducto.SelectedItem as categories;

            string producto = new JavaScriptSerializer().Serialize(new
            {
                name = TxtNombreProducto.Text,
                price = Convert.ToDouble(TxtPrecioProducto.Text),
                stock = Convert.ToInt32(TxtStockProducto.Text),
                description = TxtDescripcionProducto.Text,
                categorie = categoria._id,
                toppings = ListaToppings.Select(x => x._id)
            });


            Utilities.Post("products?extendeData=true", producto);

        }

        private void BtnEnviarTopping_Click(object sender, RoutedEventArgs e)
        {
            string toppings = new JavaScriptSerializer().Serialize(new
            {
                name = TxtNombreTopping.Text,
                price = Convert.ToDouble(TxtPrecioTopping.Text),
                stock = Convert.ToInt32(TxtStockTopping.Text),
            });

            Utilities.Post("toppings", toppings);

        }

        private void CbCategoriaProducto_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(CbCategoriaProducto.Text);
        }


        private void CbCategoriaProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categories x = CbCategoriaProducto.SelectedItem as categories;
            MessageBox.Show(x._id);
        }


        private void ListToppings_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            for (int i = 0; i < ListToppings.Items.Count; i++)
            {
                List<Toppings> top = ListToppings.Items[i] as List<Toppings>;

                MessageBox.Show(top.ToString());

            }
        }

        private void BtnAbrirDrawerProducto_Click(object sender, RoutedEventArgs e)
        {
            this.DialogHostProductos.IsOpen = false;
            this.DrawerHostProductos.IsBottomDrawerOpen = true;
            
        }
    }
}
