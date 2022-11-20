using Entities;
using Nancy.Json;
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

namespace Front.Views.Caja.Metodos_de_Pago
{
    /// <summary>
    /// Lógica de interacción para MetodosDePago.xaml
    /// </summary>
    public partial class MetodosDePago : UserControl
    {
        public MetodosDePago()
        {
            InitializeComponent();
        }


        public async void GetPagos()
        {
            string Response = await Request.Get("payments");
            List<payments> metodosDePagos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<payments>>(Response);
            if (metodosDePagos != null)
            {
                ListBoxMetodosPagos.ItemsSource = metodosDePagos;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        public async void PostPagos(object sender, RoutedEventArgs e)
        {
            string Pagos = new JavaScriptSerializer().Serialize(new
            {
                name = DialogPagos.TxtNombrePago.Text,
                money = DialogPagos.TxtMoneda.Text,
            });
            var Response = await Request.Post("payments/", Pagos);
            if (Response.IsSuccessStatusCode)
            {
                TxtSnackbarPagos.Text = "Se ha agregado correctamente";
                SnackBarNotificacionPagos.IsActive = true;
            }
            else
                MessageBox.Show("hubo un error");

        }

        private void BtnAbrirFormPagos_Click(object sender, RoutedEventArgs e)
        {
            DialogHostMetodosPago.IsOpen = true;
            DialogPagos.TxtDialogPagos.Text = "Agregar Pago";
            TxtDrawerHostPagos.Text = "¿Desea agregar un nuevo Pago?";
            BtnConfirmarDrawnerPagos.Click += PostPagos;
        }

        private void LimpiarEventos()
        {
            DrawerHostPagos.IsBottomDrawerOpen = false;
            BtnConfirmarDrawnerPagos.Click -= PostPagos;
            TxtDrawerHostPagos.Text = "";

        }
        private void BtnSnackbarPagos_Click(object sender, RoutedEventArgs e)
        {
            SnackBarNotificacionPagos.IsActive = false;

        }

        private void BtnAbrirDrawnerPagos_Click(object sender, RoutedEventArgs e)
        {
            DialogHostMetodosPago.IsOpen = false;
            DrawerHostPagos.IsBottomDrawerOpen = true;
        }

        private void BtnCerrarDrawnerPago_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnConfirmarDrawnerPagos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancelarDrawnerPagos_Click(object sender, RoutedEventArgs e)
        {
            LimpiarEventos();
        }
    }
}
