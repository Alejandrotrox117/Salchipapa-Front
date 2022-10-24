using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
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
using Entities;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;

namespace Front.Views.Productos
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : UserControl
    {
        //string url = ("http://localhost:3000/api/products");
        string url = ("http://localhost:3000/api/orders");
        private ObservableCollection<Orders> Order;

        public Productos()
        {
            InitializeComponent();




            List<string> grupo = new List<string>();

            List<productos> items = new List<productos>();
            List<categories> categories = new List<categories>();


            List<categories> movimientos = new List<categories>();




            categories.Add(new categories()
            {
                name = "Batidos",
                products = new List<productos>()
                {
                    new productos{  name ="Oreo", price = 2,stock=3,description="Batido de Oreo" },
                    new productos{  name ="Fresa", price = 2,stock=3,description="Batido de Fresa" },
                    new productos{  name ="Piña", price = 2,stock=3,description="Batido de Piña" }
                
                
                },
                
                toppings = new List<Toppings>() { new Toppings { name = "Flips", price = 2 } }

            });

            categories.Add(new categories()
            {
                name = "Merengadas",
                products = new List<productos>() { new productos { name = "Limon", price = 2, stock = 3, description = "Batido de Oreo" } },
                toppings = new List<Toppings>() { new Toppings { name = "Flips", price = 2 } }

            });








            TabControlItem.ItemsSource = categories;
        }




        private async void Main()
        {
            string response = await GetHttp();
            List<Orders> returnedData = JsonConvert.DeserializeObject<List<Orders>>(response);
            Order = new ObservableCollection<Orders>(returnedData);
            if (returnedData != null)
            {
                TabControlItem.ItemsSource = Order;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }


        public async Task<string> GetHttp()
        {
            WebRequest oRequest = WebRequest.Create(url);
            WebResponse oResponse = oRequest.GetResponse();
            StreamReader sr = new StreamReader(oResponse.GetResponseStream());
            return await sr.ReadToEndAsync();
        }



        //private async void Main()
        //{
        //    string response = await GetHttp();
        //    List<productos> returnedData = JsonConvert.DeserializeObject<List<productos>>(response);
        //    productos = new ObservableCollection<productos>(returnedData);
        //    if (returnedData != null)
        //    {
        //        TabControlItem.ItemsSource = productos;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Error");
        //    }
        //}


        //public async Task<string> GetHttp()
        //{
        //    WebRequest oRequest = WebRequest.Create(url);
        //    WebResponse oResponse = oRequest.GetResponse();
        //    StreamReader sr = new StreamReader(oResponse.GetResponseStream());
        //    return await sr.ReadToEndAsync();
        //}









        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            


        }

        private void TabControlItem_Initialized(object sender, EventArgs e)
        {
            
        }
    }
}
