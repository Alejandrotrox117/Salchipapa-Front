using Entities;
using Newtonsoft.Json;
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

        public PedidosFinalizados()
        {
            InitializeComponent();
        }



        //funcion obtener Pedidos
        public async void Get()
        {
            string response = await Request.Get("orders?filter=false");
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



        private void BtnVentas_Click(object sender, RoutedEventArgs e)
        {
            TabcontrolFinalizados.SelectedIndex = 1;

        }

        private void BtnPFinalizados_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
