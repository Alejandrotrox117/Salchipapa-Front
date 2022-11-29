using Entities;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace Front
{
    public class Request
    {
        public static string url = "http://192.168.1.101:3000/api/";
        public static HttpClient client = new HttpClient();

        public static async Task<string> Get(string link)
        {
            //Peticion get al API mediante URL
            var response = await client.GetAsync(url + link);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
            {
                MessageBox.Show("error");
                return null;
            }
            //WebRequest oRequest = WebRequest.Create(url+link);
            //WebResponse oResponse = oRequest.GetResponse();
            //StreamReader sr = new StreamReader(oResponse.GetResponseStream());
            //return await sr.ReadToEndAsync();
        }
        public static async Task<HttpResponseMessage> Post(string link, string clase)
        {
            try
            {
                HttpContent content = new StringContent(clase, Encoding.UTF8, "application/json");
                var httpResponse = await client.PostAsync(url + link, content);
                return httpResponse;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public static async Task<HttpResponseMessage> Post(string link, string clase, string image, string filename)
        {
            try
            {
                var content = new MultipartFormDataContent
                {
                    { new StreamContent(File.OpenRead(image)), "img", filename },
                    { new StringContent(clase, Encoding.UTF8, "application/json"), "data"}
                };
                var httpResponse = await client.PostAsync(url + link, content);
                return httpResponse;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public static async Task<HttpResponseMessage> Put(string link, string clase)
        {
            try
            {

                HttpContent content = new StringContent(clase, Encoding.UTF8, "application/json");
                var httpResponse = await client.PutAsync(url + link, content);
                return httpResponse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public static async Task<HttpResponseMessage> Put(string link, string clase, string image, string filename)
        {
            try
            {
                var content = new MultipartFormDataContent
                {
                    { new StreamContent(File.OpenRead(image)), "img", filename },
                    { new StringContent(clase, Encoding.UTF8, "application/json"), "data"}
                };
                var httpResponse = await client.PutAsync(url + link, content);
                return httpResponse;

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public static async Task<HttpResponseMessage> Delete(string link)
        {
            try
            {
                var httpResponse = await client.DeleteAsync(url + link);
                return httpResponse;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
