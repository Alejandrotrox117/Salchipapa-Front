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
        public DatagridVentas()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = true;
        }
    }
}
