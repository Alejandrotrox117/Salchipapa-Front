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
        string url = ("http://localhost:3000/api/products?extendeData=true");
        public AgregarCategoria()
        {
            InitializeComponent();
        }

        public async Task<string> GetHttp()
        {
            WebRequest oRequest = WebRequest.Create(url);
            WebResponse oResponse = oRequest.GetResponse();
            StreamReader sr = new StreamReader(oResponse.GetResponseStream());
            return await sr.ReadToEndAsync();
        }



        private async void Main()
        {
            string response = await GetHttp();
            List<productos> returnedData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<productos>>(response);
            productos = new ObservableCollection<productos>(returnedData);
            if (returnedData != null)
            {
                ItemsProductos.ItemsSource = returnedData;
            }
            else
            {
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

            }
            else
            {
                MessageBox.Show("Error" + result);
            }
        }



        private void BtnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Border_Initialized(object sender, EventArgs e)
        {
            Main();
        }

        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            PostElement();
        }
    }
}
