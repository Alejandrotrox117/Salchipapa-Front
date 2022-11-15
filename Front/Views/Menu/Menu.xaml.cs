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
namespace Front.Views.Carta
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Carta : UserControl
    {
        public ObservableCollection<categories> categories;
        public ObservableCollection<products> productos;
        private static readonly HttpClient client = new HttpClient();
        public Carta()
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
        

        private childItem FindVisualChild<childItem>(DependencyObject obj)
           where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private void Border_Initialized(object sender, EventArgs e)
        {
            Main();
        }

        private void BtnEnviarCategoria_Click(object sender, RoutedEventArgs e)
        {
            string categorie = new JavaScriptSerializer().Serialize(new
            {
                name = TxtNombreCategoria.Text,
            });
            Utilities.Post("categories",categorie);
        }

        private void BtnElimina_Click(object sender, RoutedEventArgs e)
        {
            itemsCardsCategorias.Focusable = true;
            itemsCardsCategorias.IsHitTestVisible = true;
            for (int i = 0; i < itemsCardsCategorias.Items.Count; i++)
            {
                ListBoxItem myListBoxItem =
                (ListBoxItem)(itemsCardsCategorias.ItemContainerGenerator.ContainerFromItem(itemsCardsCategorias.Items[i]));

                // Getting the ContentPresenter of myListBoxItem
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);

                // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                Button myButton = (Button)myDataTemplate.FindName("BtnEliminar", myContentPresenter);

                // Do something to the DataTemplate-generated TextBlock

                myButton.Visibility = Visibility.Visible;

            }
        }

        private void AgregarTopping_Click(object sender, RoutedEventArgs e)
        {

        }



        private void CBToppings_KeyDown(object sender, KeyEventArgs e)
        {
            ListToppings.Items.Add(CBToppings.SelectedItem);
        }

        private void ListToppings_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ListToppings.Items.Count; i++)
            {
                ListBoxItem myListBoxItem =
    (ListBoxItem)(ListToppings.ItemContainerGenerator.ContainerFromItem(ListToppings.Items[i]));

                // Getting the ContentPresenter of myListBoxItem
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);

                // Finding textBlock from the DataTemplate that is set on that ContentPresenter
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                Button myButton = (Button)myDataTemplate.FindName("BtnEliminarTopping", myContentPresenter);

                // Do something to the DataTemplate-generated TextBlock
                myButton.Click += new RoutedEventHandler(BtnEliminarTopping_Click);

            }
        }

        private void BtnEliminarTopping_Click(object sender, RoutedEventArgs e)
        {

           
        }

        private void CbCategoriaProducto_Loaded(object sender, RoutedEventArgs e)
        {

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
                stock= Convert.ToInt32(TxtStockTopping.Text),
            });

            Utilities.Post("toppings", toppings);
            
        }

        private void CbCategoriaProducto_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(CbCategoriaProducto.Text);
        }

        private void CbCategoriaProducto_KeyDown(object sender, KeyEventArgs e)
        {

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

    }
}
