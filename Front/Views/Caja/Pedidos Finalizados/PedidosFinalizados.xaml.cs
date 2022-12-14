using Entities;
using Nancy.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Front.Views.Caja.Pedidos_Finalizados
{
    /// <summary>
    /// Lógica de interacción para PedidosFinalizados.xaml
    /// </summary>
    public partial class PedidosFinalizados : UserControl
    {
        public ObservableCollection<Orders> Orders { get; set; }
        public List<Orders> select { get; set; }
        public PedidosFinalizados()
        {
            InitializeComponent();
            select = new List<Orders>();

        }
        //funcion obtener Pedidos
        public async void Get()
        {
            select.Clear();
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
            float total = float.Parse(Formulario.TxtMontoActual.Text);
            float montoTotal = float.Parse(Formulario.txtMontoTotal.Text);

            if(total == montoTotal)
            {
            List<Orders> orders = new List<Orders>(Formulario.Selecteds);
            List<Payments> payments = new List<Payments>(Formulario.payments);
            string pago = new JavaScriptSerializer().Serialize(new
            {
               orders = orders.Select(order => new
               {
                   number = order.number,
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
               total = total,
               payments = payments.Select(payment => new
               {
                   payment = payment.payment._id,
                   count = payment.count
               })
            });

            var response = await Request.Post("sales", pago);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                abrirSnack("Se ha agredado correctamente", null);
            }
            else
            {
                Error error = JsonConvert.DeserializeObject<Error>(content);
                abrirSnack("Ha ocurrido un error", error);
            }
            }
            else
            {
                DrawerHost.IsBottomDrawerOpen = false;
                Error error = new Error()
                {
                    errors = new List<ErrorsList>()
                    {
                        new ErrorsList { property = "total"}
                    }
                };
                abrirSnack("Ha ocurrido un error", error);
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

                select.Add(order);
            }
            else
            {
                select.Remove(order);
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Formulario.Selecteds = new ObservableCollection<Orders>(select);
            Formulario.CargarLista();
            DialogHost.IsOpen = true;
        }
        //evento click boton aceptar form
        private void BtnAceptarDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
            DrawerHost.IsBottomDrawerOpen = true;
        }

        private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
            Formulario.LimpiarForm();
        }
        //evento click boton cancelar drawner para abrir form
        private void BtnCancelarDrawnerAbrirForm_Click(object sender, RoutedEventArgs e)
        {
            DrawerHost.IsBottomDrawerOpen = false;
            DialogHost.IsOpen = true;
        }
        //funcion abrir notificacion
        private void abrirSnack(string mensaje, Error error)
        {
            DrawerHost.IsBottomDrawerOpen = false;
            var bc = new BrushConverter();
            DrawerHost.IsBottomDrawerOpen = false;
            TxtSnackbar.Text = mensaje;
            SnackBarNotificacion.IsActive = true;
            if (error is null)
            {
                SnackBarNotificacion.Background = (Brush)bc.ConvertFrom("#00695c");
                BtnSnackbar.Click += BtnSnackbarCerrar_Click;
            }
            else
            {
                Formulario.MostrarErrores(error);
                SnackBarNotificacion.Background = (Brush)bc.ConvertFrom("#f44c58");
                BtnSnackbar.Click += BtnSnackbarAbrirForm_Click;
            }
        }
        //evento btn snack cerrar 
        private void BtnSnackbarCerrar_Click(object sender, RoutedEventArgs e)
        {
            Get();
            Formulario.LimpiarForm();
            SnackBarNotificacion.IsActive = false;
        }
        //evento btn snack abrir form 
        private void BtnSnackbarAbrirForm_Click(object sender, RoutedEventArgs e)
        {
            SnackBarNotificacion.IsActive = false;
            DialogHost.IsOpen = true;
        }
    }
}
