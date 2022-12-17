using Entities;
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

namespace Front.Views.Caja.Pedidos_Finalizados.Ventas
{
    /// <summary>
    /// Lógica de interacción para DatagridVentas.xaml
    /// </summary>
    public partial class DatagridVentas : UserControl
    {
        public Sales sale { get; set; }
        private string id;
        private List<Sales> ListSales { get; set; }
        public DatagridVentas()
        {
            InitializeComponent();
        }
        public async void Get()
        {
            string response = await Request.Get("sales");
            List<Sales> returnedData = JsonConvert.DeserializeObject<List<Sales>>(response);
            Console.WriteLine(response);
            if (returnedData != null)
            {
                DataGridVentas.ItemsSource = returnedData;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void BtnConsultar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            Sales x = element.DataContext as Sales;
            sale = x;
            DialogDetalle.CargarDialog(sale);
            DialogHostConsultar.IsOpen = true;
        }

        private void BtnFiltroClick_Click(object sender, RoutedEventArgs e)
        {
            
            DateTime? InitialDate = DateInitial.SelectedDate;
            DateTime? FinalDate = DateFinal.SelectedDate;
            if (InitialDate != null && FinalDate != null)
            {
                List<Sales> ReportsFilter = ListSales.FindAll(report => report.createdAt >= InitialDate && report.createdAt <= FinalDate);
                DataGridVentas.ItemsSource = ReportsFilter;
            }
        }

        private async void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            float total = float.Parse(Formulario.TxtMontoActual.Text);
            float montoTotal = float.Parse(Formulario.txtMontoTotal.Text);

            if (total == montoTotal)
            {
                List<Payments> payments = new List<Payments>(Formulario.payments);
                string pago = new JavaScriptSerializer().Serialize(new
                {
                    orders = sale.orders.Select(order => new
                    {
                        number = order.number,
                        products = order.products.Select(product => new
                        {
                            product = product.product._id,
                            price = product.price,
                            toppings = product.toppings.Select(topping => new
                            {
                                topping = topping.topping._id,
                                price = topping.price
                            })
                        }),
                        attendedBy = order.attendedBy._id,
                        madeBy = order.madeBy._id
                    }),
                    client = Formulario.client._id,
                    sellerBy = sale.sellerBy._id,
                    total = sale.total,
                    payments = payments.Select(payment => new
                    {
                        payment = payment.payment._id,
                        count = payment.count
                    })
                });

                var response = await Request.Put("sales/" + sale._id, pago);
                string content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    limpiarDrawner();
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
        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var response = await Request.Delete("sales/" + sale._id);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                limpiarDrawner();
                abrirSnack("Se ha eliminado correctamente", null);
            }
            else
            {
                Error error = JsonConvert.DeserializeObject<Error>(content);
                abrirSnack("Ha ocurrido un error", error);
            }
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            Sales x = element.DataContext as Sales;
            sale = x;
            Formulario.CargarForm(sale);

            TxtTituloDrawer.Text = "¿Desea actualizar la venta?";
            DialogHost.IsOpen = true;
            BtnConfirmarDrawner.Click += Actualizar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            Sales x = element.DataContext as Sales;
            sale = x;

            TxtTituloDrawer.Text = "¿Desea eliminar la venta?";
            DrawerHost.IsBottomDrawerOpen = true;
            BtnConfirmarDrawner.Click += Eliminar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawner_Click;
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
        //funcion limpiar drawner
        private void limpiarDrawner()
        {
            DrawerHost.IsBottomDrawerOpen = false;
            BtnConfirmarDrawner.Click -= Actualizar_Click;
            BtnConfirmarDrawner.Click -= Eliminar_Click;
            BtnCancelarDrawner.Click -= BtnCancelarDrawnerAbrirForm_Click;
            BtnCancelarDrawner.Click -= BtnCancelarDrawner_Click;
        }
        //evento cierre snack siempre limpiar
        private void SnackBarNotificacion_IsActiveChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            if (!e.NewValue)
            {
                BtnSnackbar.Click -= BtnSnackbarCerrar_Click;
                BtnSnackbar.Click -= BtnSnackbarAbrirForm_Click;
            }
        }
        //evento click boton cancelar drawner para abrir form
        private void BtnCancelarDrawner_Click(object sender, RoutedEventArgs e)
        {
            Formulario.LimpiarForm();
            limpiarDrawner();
        }

        private void cerrarDialogDetalle(object sender, RoutedEventArgs e)
        {
            DialogHostConsultar.IsOpen = false;
        }
       
    }
}
