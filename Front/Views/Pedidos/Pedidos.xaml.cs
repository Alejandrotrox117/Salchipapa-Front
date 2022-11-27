using Entities;
using Nancy.Json;
using Newtonsoft.Json;
using SocketIOClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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
                        switch (returned.status)
                        {
                            case "PROCESO":
                                Orders[index] = returned;
                                break;
                            case "LISTO":
                                Orders.RemoveAt(index);
                                Orders.Insert(index, returned);
                                break;
                            case "ENTREGADO":
                                Orders.RemoveAt(index);
                                break;
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
            string rol = MainWindow.session.account.rol;
            if (rol == "ADMIN" || rol == "COCINERO")
            {
                if (MainWindow.session._id == order.madeBy)
                {
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
            }
            MessageBox.Show("No puedes realizar esta accion");
        }

        private async void BtnAceptarPedido_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            Orders order = element.DataContext as Orders;
            string rol = MainWindow.session.account.rol;
            string body = "";
            switch (order.status)
            {
                case "NUEVO":
                    if (rol == "COCINERO" || rol == "ADMIN")
                    {
                        body = new JavaScriptSerializer().Serialize(new
                        {
                            status = "PROCESO"
                        });
                    }
                    break;
                case "LISTO":
                    if (rol == "MESERO" || rol == "ADMIN")
                    {
                        body = new JavaScriptSerializer().Serialize(new
                        {
                            status = "ENTREGADO"
                        });
                    }
                    break;
            }

            if (body != "")
            {
                var response = await Request.Put("orders/"+order.number.ToString(), body);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("error");
                }

            }
        }
    }
}
