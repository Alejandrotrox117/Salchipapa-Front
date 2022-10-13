using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace Front
{
    public class Utilities
    {


        public void GetDeserealize(string url, DataGrid dataGrid)
        {
            async void Main()
            {
                string respuesta = await GetHttp(url);

                List<clientes> returnedData = JsonConvert.DeserializeObject<List<clientes>>(respuesta);

                if (returnedData != null)
                {
                    dataGrid.ItemsSource = returnedData;
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        public async Task<string> GetHttp(string url)
        {
            WebRequest oRequest = WebRequest.Create(url);
            WebResponse oResponse = oRequest.GetResponse();

            StreamReader sr = new StreamReader(oResponse.GetResponseStream());
            return await sr.ReadToEndAsync();

        }





    }
}
