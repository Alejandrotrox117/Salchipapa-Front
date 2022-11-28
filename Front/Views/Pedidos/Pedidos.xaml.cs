using Entities;
using MaterialDesignThemes.Wpf;
using Nancy.Json;
using Newtonsoft.Json;
using SocketIOClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Front.Views.Pedidos
{
    /// <summary>
    /// Lógica de interacción para Pedidos.xaml
    /// </summary>
    public partial class Pedidos : UserControl
    {
        private Snackbar notify;
        public ObservableCollection<Orders> Orders { get; set; }

        private SocketIO client;

        public Pedidos(ref SocketIO client, ref Snackbar notify)
        {
            InitializeComponent();
            this.client = client;
            this.DataContext = this;
            this.notify = notify;
        }
        private async void Main()
        {
            string response = await Request.Get("orders?filter=true");
            Orders = JsonConvert.DeserializeObject<ObservableCollection<Orders>>(response);
            if (Orders != null)
            {
                itemCardFlipper.ItemsSource = Orders;
            }
            else
            {
                MostrarError("Error inesperado", true);
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
                    notify.IsActive = true;
                });
            });
            client.On("takeOrder", async response =>
            {
                this.Dispatcher.Invoke(() =>
                {

                    var returned = JsonConvert.DeserializeObject<List<Orders>>(response.ToString())[0];
                    var lista = new List<Orders>(Orders);
                    int index = lista.FindIndex(i => i.number == returned.number);

                    this.Orders.RemoveAt(index);
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

            if (MainWindow.session._id == order.madeBy)
            {
                string body = new JavaScriptSerializer().Serialize(new
                {
                    status = "LISTO"
                });

                var response = await Request.Put("orders/"+order.number.ToString(), body);
                if (response.IsSuccessStatusCode)
                {
                    MostrarError("El pedido "+order.number.ToString()+" ha cambiado de estatus", false);
                } 
                else
                {
                    MostrarError("Error inesperado", true);
                }
            }
            else
            {
                MostrarError("No puedes realizar esta acción", true);
            }
        }

        private async void BtnAceptarPedido_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            Orders order = element.DataContext as Orders;
            switch (order.status)
            {
                case "NUEVO":
                    if (MainWindow.session.account.rol == "COCINERO" || MainWindow.session.account.rol == "ADMIN")
                    {
                        string body = new JavaScriptSerializer().Serialize(new
                        {
                            status = order.status == "NUEVO" ? "PROCESO" : "ENTREGADO"
                        });

                        var response = await Request.Put("orders/"+order.number.ToString(), body);
                        if (response.IsSuccessStatusCode)
                        {
                            MostrarError("El pedido "+order.number.ToString()+" ha cambiado de estatus", false);
                        }
                        {
                            MostrarError("Error inesperado", true);
                        }
                    }
                    else
                    {
                        MostrarError("No puedes realizar esta acción", true);
                    }
                    break;
                case "LISTO":
                    if (MainWindow.session.account.rol == "MESERO" || MainWindow.session.account.rol == "ADMIN")
                    {
                        string body = new JavaScriptSerializer().Serialize(new
                        {
                            status = order.status == "NUEVO" ? "PROCESO" : "ENTREGADO"
                        });

                        var response = await Request.Put("orders/"+order.number.ToString(), body);
                        if (response.IsSuccessStatusCode)
                        {
                            MostrarError("El pedido "+order.number.ToString()+" ha cambiado de estatus", false);
                        }
                        else
                        {
                            MostrarError("Error inesperado", true);
                        }
                    }
                    else
                    {
                        MostrarError("No puedes realizar esta acción", true);
                    }
                    break;
            }
        }

        private void MostrarError(string massage, bool error)
        {
            if(error)
            {
                SnackBarNotificacion.Background = (Brush)new BrushConverter().ConvertFrom("#f44c58");
            }
            TxtSnackbar.Text = massage;
            SnackBarNotificacion.IsActive = true;
        }
        private void BtnSnackbar_Click(object sender, RoutedEventArgs e)
        {
            SnackBarNotificacion.IsActive = false;
            SnackBarNotificacion.Background = (Brush)new BrushConverter().ConvertFrom("#00695c");
        }

        private async void BtnCancelarPedido_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            Orders order = element.DataContext as Orders;
            string response = await Request.Get("orders/" + order.number.ToString());
            Orders cancelOrder = JsonConvert.DeserializeObject<Orders>(response);
            if (cancelOrder != null)
            {
                MostrarError("El pedido "+order.number.ToString()+" se ha eliminado", false);
            }
            else
            {
                MostrarError("Error inesperado", true);
            }
        }
    }
}
