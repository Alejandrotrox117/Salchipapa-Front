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

namespace Front.Views.Caja.Pedidos_Finalizados.Ventas
{
    /// <summary>
    /// Lógica de interacción para DialogDetalleVenta.xaml
    /// </summary>
    public partial class DialogDetalleVenta : UserControl
    {
        public DialogDetalleVenta()
        {
            InitializeComponent();
        }
        
        public void CargarDialog(Sales sale)
        {
           
            ItemControlDetalles.ItemsSource = sale.orders;
            TxtNombreCliente.Text = sale.client.name + " " + sale.client.surname;
            TxtCobradoPor.Text = sale.sellerBy.name;
            ListBoxPagos.ItemsSource = sale.payments;
            TxtTotal.Text = sale.total.ToString();
        }
        

    }
}
