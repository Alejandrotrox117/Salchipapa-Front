﻿using Entities;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace Front.Views.Clientes
{
    /// <summary>
    /// Lógica de interacción para FormCliente.xaml
    /// </summary>
    public partial class FormCliente : UserControl
    {
        public FormCliente()
        {
            InitializeComponent();
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {

        }


        async private void Main()
        {
            string url = ("http://localhost:3000/api/clients");

            var sol = new HttpClient();

            clientes prueba = new clientes(TxtCi.Text, TxtNombre.Text, TxtApellido.Text, TxtDir.Text, TxtTelef.Text)
            {
                Nombre = TxtNombre.Text,
                Apellido = TxtApellido.Text,
                Cedula = TxtCi.Text,
                Direccion = TxtDir.Text,
                Telefono = TxtTelef.Text

            };

            var data = JsonSerializer.Serialize<clientes>(prueba);
            HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var httpResponse = await sol.PostAsync(url, content);
            //evaluar si la soliciotud ha sido exitosa
            var result = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode)
            {

                MessageBox.Show("Se ha enviado los datos");
            }
            else
            {
                MessageBox.Show("Error" + result);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main();
        }

        private void BorderContenido_Initialized(object sender, EventArgs e)
        {

        }
    }
}

