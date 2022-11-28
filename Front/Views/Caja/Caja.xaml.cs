using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Net.Http;
using System.Net;
using Entities;
using Newtonsoft.Json;
using System.IO;
using Nancy.Json;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.ComponentModel;

namespace Front.Views.Caja
{
    /// <summary>
    /// Lógica de interacción para Caja.xaml
    /// </summary>
    public partial class Caja : UserControl
    {
        
        private string _id = null;
        private List<Client> listaClientes = new List<Client>();
        public Caja()
        {
            InitializeComponent();

           
        }
       
        private void BtnTabClientes_Click(object sender, RoutedEventArgs e)
        {
            TabControlClientes.SelectedIndex=1;
            PageClientes.Get();
        }

        private void GridCaja_Loaded(object sender, RoutedEventArgs e)
        {
            PageClientes.Get();
        }

        private void BtnTabMetodosPagos_Click(object sender, RoutedEventArgs e)
        {
            TabControlClientes.SelectedIndex = 2;
            PagePagos.Get();
        }
        private void BtnPFinalizados_Click(object sender, RoutedEventArgs e)
        {
            TabControlClientes.SelectedIndex = 0;
        }
    }
}
