
using Nancy.Json;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Front
{
    /// <summary>
    /// Lógica de interacción para Session.xaml
    /// </summary>
    public partial class Session : Window
    {
        public employes session = new employes();
        public Session()
        {
            InitializeComponent();
        }

        private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Post()
        {
            string link = "http://localhost:3000/api/session/";
            HttpClient client = new HttpClient();

            string js = new JavaScriptSerializer().Serialize(new
            {
                ci=TxtUsuario.Text,
                password=TxtContrasena.Text
            });
            HttpContent content = new StringContent(js,Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(link, content);
            //evaluar si la solicitud ha sido exitosa
            string result = await httpResponse.Content.ReadAsStringAsync();
            

            if (httpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Se ha iniciado sesion");

                this.session = Newtonsoft.Json.JsonConvert.DeserializeObject<employes>(result);

                this.Close();
                
            }
            else
            {
                MessageBox.Show("Error" + result);
            }
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Post();
        }
    }
}
