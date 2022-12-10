﻿using Entities;
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

namespace Front.Views.Caja.Pedidos_Finalizados.Ventas
{
    /// <summary>
    /// Lógica de interacción para Ventas.xaml
    /// </summary>
    public partial class Ventas : UserControl
    {
        public Ventas()
        {
            InitializeComponent();
        }
        public async void Get()
        {
            string response = await Request.Get("sales");
            List<Sales> returnedData = JsonConvert.DeserializeObject<List<Sales>>(response);
            if (returnedData != null)
            {
                CardsVentasTerminadas.ItemsSource = returnedData;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }        
    }
}
