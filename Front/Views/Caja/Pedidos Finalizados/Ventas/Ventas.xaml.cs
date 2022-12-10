using Entities;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        private async void Agregar_Click(object sender, RoutedEventArgs e)
        {
            float total = float.Parse(Formulario.TxtMontoActual.Text);
            float montoTotal = float.Parse(Formulario.txtMontoTotal.Text);

            if (total == montoTotal)
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

        public async void Get()
        {
            string response = await Request.Get("sales");
            List<Sales> returnedData = JsonConvert.DeserializeObject<List<Sales>>(response);
            Console.WriteLine(response);
            if (returnedData != null)
            {
                CardsVentasTerminadas.ItemsSource = returnedData;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            //Formulario.CargarLista(sele);
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
