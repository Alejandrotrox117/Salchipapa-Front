using Entities;
using Microsoft.Graph;
using Newtonsoft.Json;
using SocketIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Front.Views.Pedidos
{
    /// <summary>
    /// Lógica de interacción para Pedidos.xaml
    /// </summary>
    public partial class Pedidos : UserControl
    {
        private List<Orders> Order;
        
        private SocketIO client;
        public Pedidos(SocketIO client)
        {
            InitializeComponent();
            this.client = client;
        }
        private async void Main()
        {
            string response = await Request.Get("orders");
             Order= JsonConvert.DeserializeObject<List<Orders>>(response);
          
            if (Order != null)
            {
                itemCardFlipper.ItemsSource = Order;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Main();
            client.On("newOrder", async response =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var returned = JsonConvert.DeserializeObject<List<Orders>>(response.ToString());
                    this.Order.Add(returned[0]);
                    itemCardFlipper.Focus();
                        
                });
            });
        }

        private void BtnFinalizado_click(object sender, RoutedEventArgs e)
        {
        }

        
    }
}
