using Entities;
using Nancy.Json;
using Newtonsoft.Json;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Front.Views.Caja.Pedidos_Finalizados
{
    /// <summary>
    /// Lógica de interacción para PedidosFinalizados.xaml
    /// </summary>
    public partial class PedidosFinalizados : UserControl
    {
        public ObservableCollection<Orders> Orders { get; set; }
        private SocketIO client;
        public PedidosFinalizados(ref SocketIO client)
        {
            InitializeComponent();
            this.client = client;
        }
        //funcion obtener Pedidos
        public async void Get()
        {
            string response = await Request.Get("orders?filter=true");
            Orders = JsonConvert.DeserializeObject<ObservableCollection<Orders>>(response);
            if (Orders != null)
            {
                itemCardTerminados.ItemsSource = Orders;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private async void Agregar_Click(object sender, RoutedEventArgs e)
        {
            List<Orders> orders = new List<Orders>(Formulario.Selecteds);
            List<Payments> payments = new List<Payments>(Formulario.payments);
            string pago = new JavaScriptSerializer().Serialize(new
            {
               orders = orders.Select(order => new
               {
                   products = order.products.Select(product => new
                   {
                       product = product._id,
                       price = product.price,
                       toppings = product.toppings.Select(topping => new
                       {
                           topping = topping._id,
                           price = topping.price
                       })
                   }),
                   attendedBy = order.attendedBy._id,
                   madeBy = order.madeBy._id
               }),
               client = Formulario.client._id,
               sellerBy = MainWindow.session._id,
               payments = payments.Select(payment => new
               {
                   payment = payment.payment._id,
                   count = payment.count
               })
            });

            var response = await Request.Post("sales", pago);
            string message = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("ok");
                await client.EmitAsync("finishOrder", orders.Select(order => order.number));
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void BtnVentas_Click(object sender, RoutedEventArgs e)
        {
            TabcontrolFinalizados.SelectedIndex = 1;
            PageVentas.Get();
        }

        private void BtnPFinalizados_Click(object sender, RoutedEventArgs e)
        {
            Get();
            TabcontrolFinalizados.SelectedIndex = 0;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Get();
        }

        private void CheckboxFinalizado_Click(object sender, RoutedEventArgs e)
        {
            CheckBox check = sender as CheckBox;
            FrameworkElement element = e.Source as FrameworkElement;
            Orders order = element.DataContext as Orders;
            if (check.IsChecked.Value) {
                
                Formulario.Selecteds.Add(order);
            }
            else
            {
                Formulario.Selecteds.Remove(order);
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Formulario.CargarLista();
            DialogHost.IsOpen = true;
        }

        private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
            Formulario.LimpiarForm();
        }

        //funcion abrir notificacion
        //private void abrirSnack(string mensaje, Error error)
        //{
        //    var bc = new BrushConverter();
        //    TxtSnackbar.Text = mensaje;
        //    SnackBarNotificacion.IsActive = true;
        //    if (error is null)
        //    {
        //        SnackBarNotificacion.Background = (Brush)bc.ConvertFrom("#00695c");
        //        BtnSnackbar.Click += BtnSnackbarCerrar_Click;
        //    }
        //    else
        //    {
        //        Formulario.MostrarErrores(error);
        //        SnackBarNotificacion.Background = (Brush)bc.ConvertFrom("#f44c58");
        //        BtnSnackbar.Click += BtnSnackbarAbrirForm_Click;
        //    }
        //}
        ////funcion limpiar drawner
        //private void limpiarDrawner()
        //{
        //    DrawerHost.IsBottomDrawerOpen = false;
        //    BtnConfirmarDrawner.Click -= Agregar_Click;
        //    BtnConfirmarDrawner.Click -= Actualizar_Click;
        //    BtnConfirmarDrawner.Click -= Eliminar_Click;
        //    BtnCancelarDrawner.Click -= BtnCancelarDrawnerAbrirForm_Click;
        //    BtnCancelarDrawner.Click -= BtnCancelarDrawner_Click;
        //}

    }
}
