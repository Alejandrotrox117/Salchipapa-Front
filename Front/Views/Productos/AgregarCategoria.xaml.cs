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

namespace Front.Views.Productos
{
    /// <summary>
    /// Lógica de interacción para AgregarCategoria.xaml
    /// </summary>
    public partial class AgregarCategoria : UserControl
    {
        public ObservableCollection<categories> categories;
        public ObservableCollection<productos> productos;
        private static readonly HttpClient client = new HttpClient();
        public AgregarCategoria()
        {
            InitializeComponent();
        }

        public async Task<string> GetHttp(string url)
        {
            WebRequest oRequest = WebRequest.Create(url);
            WebResponse oResponse = oRequest.GetResponse();
            StreamReader sr = new StreamReader(oResponse.GetResponseStream());
            return await sr.ReadToEndAsync();
        }

        private async void  Main()
        {
            string response = await GetHttp("http://localhost:3000/api/products?extendeData=true");
            string RespondCategorie = await GetHttp("http://localhost:3000/api/categories");
            string RespondTopping = await GetHttp("http://localhost:3000/api/toppings");

            List<Grupos> group = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Grupos>>(response);


            //List<categories> categorie = Newtonsoft.Json.JsonConvert.DeserializeObject<List<categories>>(RespondCategorie);
           List<Toppings> toppings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Toppings>>(RespondTopping);
            //productos = new ObservableCollection<productos>(products);
            //categories = new ObservableCollection<categories>(categorie);

            if (group  != null)
            {
                TabsProductos.ItemsSource = group;
                ListBoxToppings.ItemsSource = toppings;

                //ListToppings.ItemsSource = toppings;
            }
            else {
                MessageBox.Show("Error");
            }





        }
        private async void PostElement()
        {
            string link = ("http://localhost:3000/api/categories");
            categories categories = new categories()
            {
                name = TxtNombreCategoria.Text,
                
            };
            JavaScriptSerializer js = new JavaScriptSerializer();
            string DataSerializer = js.Serialize(categories);
            HttpContent content = new StringContent(DataSerializer, System.Text.Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(link, content);
            //evaluar si la solicitud ha sido exitosa
            var result = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Se han enviado los datos");
                itemsCardsCategorias.UpdateLayout();
            }
            else
            {
                MessageBox.Show("Error" + result);
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

        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            PostElement();
            DialogHostCategorias.IsOpen = false;
            
        }

       

        
        private void itemsCardsCategorias_Loaded(object sender, RoutedEventArgs e)
        {

           
        }

        private void BtnElimina_Click(object sender, RoutedEventArgs e)
        {
            itemsCardsCategorias.Focusable = true;
            itemsCardsCategorias.IsHitTestVisible = true;
            for (int i=0;i<itemsCardsCategorias.Items.Count;i++)
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
            ListToppings.Items.Add(CBToppings.Text);
        }

       

        private void CBToppings_KeyDown(object sender, KeyEventArgs e)
        {
            ListToppings.Items.Add(CBToppings.Text);
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
    }
}
