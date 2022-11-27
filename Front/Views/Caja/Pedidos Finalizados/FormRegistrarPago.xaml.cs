using Entities;
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

namespace Front.Views.Caja.Pedidos_Finalizados
{
    /// <summary>
    /// Lógica de interacción para FormRegistrarPago.xaml
    /// </summary>
    public partial class FormRegistrarPago : UserControl
    {


        public  List<Orders> selecteds = new List<Orders>(); 
        public  List<Orders> Selecteds { get { return selecteds; } set { selecteds = value;} }
        public static float Total{ get; set; }
        public FormRegistrarPago()
        {
            InitializeComponent();
        }


        public  async void CargarLista()
        {
            ListPedidosFinalizados.ItemsSource = Selecteds;
            foreach(Orders i in Selecteds)
            {
                Total += i.total;
            }
            TxtMontoActual.Text=Total.ToString();
            string response = await Request.Get("payments");
            List<Payment> returnedData = JsonConvert.DeserializeObject<List<Payment>>(response);
            if (returnedData != null)
            {
                CbMetodoPago.ItemsSource = returnedData;
             }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private async void BtnBuscarCliente_Click(object sender, RoutedEventArgs e)
        {
            string response = await Request.Get("clients?ci=" + (! string.IsNullOrEmpty(TxtCiCliente.Text) ?  TxtCiCliente.Text : "\'\'" ));
            if (response != "null")
            {
                Client returnedData = JsonConvert.DeserializeObject<Client>(response);
                TxtNombreCliente.Text = returnedData.name + " " + returnedData.surname;
                TxtNombreCliente.Visibility = Visibility.Visible;
                LblErrorNombreCliente.Visibility = Visibility.Hidden;

            }
            else
            {
                LblErrorNombreCliente.Visibility = Visibility.Visible;
                TxtNombreCliente.Visibility = Visibility.Hidden;
            }
        }

        private void BtnAgregarListaPago_Click(object sender, RoutedEventArgs e)
        {
            ListPagos.Items.Add(CbMetodoPago.Text+TxtMonto.Text);
           
        }
    }
}
