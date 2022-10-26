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

namespace Front.Views.Productos
{
    /// <summary>
    /// Lógica de interacción para FormProducto.xaml
    /// </summary>
    public partial class FormProducto : UserControl
    {
        public ObservableCollection<productos> productos;
        string url = ("http://localhost:3000/api/products?extendeData=true");
        public FormProducto()
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




        private void BtnAgregarProducto_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
