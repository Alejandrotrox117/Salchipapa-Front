using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Front.Views.Clientes;
using System.Net.Http;
using System.Net;
using Entities;
using Newtonsoft.Json;
using System.IO;

namespace Front.Views.Clientes
{
    /// <summary>
    /// Lógica de interacción para Clientes.xaml
    /// </summary>
    public partial class Clientes : UserControl
    {
        private static readonly HttpClient client = new HttpClient();
        string url = ("http://localhost:3000/api/clients");

        public Clientes()
        {
            InitializeComponent();
        }
        private void DataGridClientes_Loaded(object sender, RoutedEventArgs e)
        {
            Main();

        }

        public async void Main()
        {
            string respuesta = await GetHttp();

            List<clientes> returnedData = JsonConvert.DeserializeObject<List<clientes>>(respuesta);

            if (returnedData != null)
            {

                DataGridClientes.ItemsSource = returnedData;

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


        public async void DeleteHttp(string ci)
        {





            string urll = ("http://localhost:3000/api/clients/" + ci);
            var httpResponse = await client.DeleteAsync(urll);
            var result = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode)
            {

                MessageBox.Show(result + "Se ha eliminado correctamente");
            }
            else
            {
                MessageBox.Show(result + "Error");
            }

        }

        public void eliminar()
        {
            if (DataGridClientes.SelectedItems.Count > 0)
            {
                List<clientes> miLista = (List<clientes>)DataGridClientes.ItemsSource;

                if (miLista != null)
                {
                    for (int i = 0; i < DataGridClientes.SelectedItems.Count; i++)
                    {
                        int indice = DataGridClientes.Items.IndexOf(DataGridClientes.SelectedItems[i]);

                        miLista.RemoveAt(indice);
                        foreach (clientes p in miLista)
                        {

                            DeleteHttp(p.Cedula);
                        }

                    }

                    DataGridClientes.ItemsSource = null;
                    DataGridClientes.ItemsSource = miLista;
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar por lo menos una fila.");
            }
        }


        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            FormCliente form = new FormCliente();
            FrameClientes.Content = form;

        }



        private void DataGridClientes_Initialized(object sender, EventArgs e)
        {
            Main();
        }


        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {


            eliminar();
        }
    }
}
