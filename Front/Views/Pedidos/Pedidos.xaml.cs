using Entities;
using Microsoft.Graph;
using Nancy.Json;
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
        public ObservableCollection<Orders> Orders { get; set; }
        
        private SocketIO client;
        public Pedidos(ref SocketIO client)
        {
            InitializeComponent();
            this.client = client;
            this.DataContext = this;
        }
        private async void Main()
        {
            string response = await Request.Get("orders");
            Orders = JsonConvert.DeserializeObject<ObservableCollection<Orders>>(response);
          
            if (Orders != null)
            {
                itemCardFlipper.ItemsSource = Orders;
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
                    var returned = JsonConvert.DeserializeObject<List<Orders>>(response.ToString())[0];
                    this.Orders.Add(returned);
                    itemCardFlipper.Focus();
                        
                });
            });
            client.On("changeOrder", async response =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var returned = JsonConvert.DeserializeObject<List<Orders>>(response.ToString())[0];
                    var lista = new List<Orders>(Orders);

                    int index = lista.FindIndex(i => i.number == returned.number);

                    if (index != -1)
                    {
                        if (returned.IsProgress)
                        {
                            Orders[index] = returned;
                        }
                        else
                        {
                            Orders.RemoveAt(index);
                            Orders.Add(returned);

                        }
                    }

                    itemCardFlipper.Focus();

                });
            });
        }

        private async void BtnFinalizado_click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            Orders order = element.DataContext as Orders;
            string body = new JavaScriptSerializer().Serialize(new
            {
                status = "LISTO"
            });

            var response = await Request.Put("orders/"+order.number.ToString(), body);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("error");
            }
        }

        private async void BtnAceptarPedido_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            Orders order = element.DataContext as Orders;
            string body = new JavaScriptSerializer().Serialize(new
            {
                status = "PROCESO"
            });

            var response = await Request.Put("orders/"+order.number.ToString(), body);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("error");
            }
        }
    }
}
