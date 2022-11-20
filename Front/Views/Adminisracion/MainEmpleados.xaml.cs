﻿using Entities;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Front.Views.Empleados
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class MainEmpleados : UserControl
    {
        public MainEmpleados()
        {
            InitializeComponent();
        }

        public async void Main()
        {
            string respuesta = await Request.Get("employes/");
            //LISTA DE CLIENTES DESEREALIZADA PARA RETORNAR DATOS
            List<employes> returnedData = JsonConvert.DeserializeObject<List<employes>>(respuesta);
            //VALIDACION DE RETORNO DE DATOS 
            if (returnedData != null)
            {
                //ASIGNACION DE DATOS CONVERTIDOS AL ITEMS SOURCE DEL DATAGRID
                //DataGridEmpleados.ItemsSource = returnedData;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void DataGridEmpleados_Initialized(object sender, EventArgs e)
        {
            Main();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            PageEmpleados.Get();

        }

        private void BtnTabEmpleado_Click(object sender, RoutedEventArgs e)
        {
            TabControlEmpleados.SelectedIndex = 0;
            PageEmpleados.Get();
        }
    }
}