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

namespace Front.Views.Caja.Pagos
{
    /// <summary>
    /// Lógica de interacción para MetodosDePago.xaml
    /// </summary>
    public partial class MetodosDePago : UserControl
    {
        public string id { get; set; }
        public MetodosDePago()
        {
            InitializeComponent();
        }

        //funcion obtener Metodos de Pago
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

        //funcion subir metodos de pago
        public async void PostPagos(object sender, RoutedEventArgs e)
        {
            string Pagos = new JavaScriptSerializer().Serialize(new
            {
                name = FormPagos.TxtNombrePago.Text,
                money = FormPagos.TxtMoneda.Text,
            });
            var Response = await Request.Post("payments", Pagos);
            if (Response.IsSuccessStatusCode)
            {
                TxtSnackbarPagos.Text = "Se ha agregado correctamente";
                SnackBarNotificacionPagos.IsActive = true;
                LimpiarEventos();
            }
            else
                MessageBox.Show("hubo un error");

        }
        //funcion abrir forumluario de pagos
        private void BtnAbrirFormPagos_Click(object sender, RoutedEventArgs e)
        {
            DialogHostMetodosPago.IsOpen = true;
            FormPagos.TxtDialogPagos.Text = "Agregar Pago";
            TxtDrawerHostPagos.Text = "¿Desea agregar un nuevo Pago?";
            BtnConfirmarDrawnerPagos.Click += PostPagos;
        }
        private void BtnConfirmarDrawnerPagos_Click(object sender, RoutedEventArgs e)
        {

        }
        //funcion limpiar eventos
        private void LimpiarEventos()
        {
            DialogHostMetodosPago.IsOpen = false;
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

        
        private void BtnCancelarDrawnerPagos_Click(object sender, RoutedEventArgs e)
        {
            LimpiarEventos();
        }

        //PUT pagos
        public async void PutPagos(object sender, RoutedEventArgs e)
        {
            string pagos = new JavaScriptSerializer().Serialize(new
            {
                name = FormPagos.TxtNombrePago.Text,
                money = FormPagos.TxtMoneda.Text,
            });
            var Response = await Request.Put("payments/" + id, pagos);
            if (Response.IsSuccessStatusCode)
            {
                TxtSnackbarPagos.Text = "¡Se ha actualizado correctamente!";
                SnackBarNotificacionPagos.IsActive = true;
                LimpiarEventos();
            }
            else
            {
                string error = await Response.Content.ReadAsStringAsync();
                MessageBox.Show(error);
            }

        }

        //evento click boton actualizar metodos de pago
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            payments payment = element.DataContext as payments;
            id = payment._id;
            FormPagos.TxtNombrePago.Text = payment.name;
            FormPagos.TxtMoneda.Text = payment.money;
            FormPagos.TxtDialogPagos.Text = "Actualizar Metodo de pago";
            DialogHostMetodosPago.IsOpen = true;
            TxtDrawerHostPagos.Text = "¿Desea actualizar el metodo de pago?";
            BtnConfirmarDrawnerPagos.Click += PutPagos;
        }

        //evento cerrar formulario
        private void BtnCerrarFormularioPagos_Click(object sender, RoutedEventArgs e)
        {
            LimpiarEventos();
        }

        //DELETE PAGOS
        public async void DeletePagos(object sender, RoutedEventArgs e)
        {
            var Response = await Request.Delete("payments/" + id);
            if (Response.IsSuccessStatusCode)
            {
                TxtSnackbarPagos.Text = "Se ha eliminado correctamente";
                SnackBarNotificacionPagos.IsActive = true;
                LimpiarEventos();
                //ListBoxMetodosPagos.UpdateLayout();
                
            }
            else
                MessageBox.Show("hubo un error");
        }
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            payments payment = element.DataContext as payments;
            id = payment._id;
            TxtDrawerHostPagos.Text = "¿Desea eliminar el Método de pago?";
            DrawerHostPagos.IsBottomDrawerOpen = true;
            BtnConfirmarDrawnerPagos.Click += DeletePagos;
         }
    }
}
