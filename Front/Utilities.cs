using Entities;
using Nancy.Json;
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
        public static string url = "http://localhost:3000/api/";
        public static HttpClient client = new HttpClient();


        public static async Task<string> Get(string link)
        {
            //Peticion get al API mediante URL
            WebRequest oRequest = WebRequest.Create(url+link);
            WebResponse oResponse = oRequest.GetResponse();
            StreamReader sr = new StreamReader(oResponse.GetResponseStream());
            return await sr.ReadToEndAsync();
        }

      

        public async static void Post(string link, string clase,string message)
        {
            HttpContent content = new StringContent(clase, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync(url+link, content);
            //evaluar si la solicitud ha sido exitosa
            string result = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show(message);
            }
            else
            {
                MessageBox.Show("Error:"+result);
            }
        }
    }
}
