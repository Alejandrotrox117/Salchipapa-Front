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
using MaterialDesignThemes.Wpf;
namespace Front.Views.Productos
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : UserControl
    {
        public Productos()
        {
            InitializeComponent();
            List<pedir> items = new List<pedir>();

            items.Add(new pedir() { producto = "Batido pie de limon", descripcion = "batido limon", cantidad = "3" });

            items.Add(new pedir() { producto = "Batido de fresa", descripcion = "Leche condensada", cantidad = "2" });
            itemsPedidos.ItemsSource = items;
        }

        public string Categorie { get; internal set; }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            FormProducto formProducto = new FormProducto();


        }
        public class pedir
        {
            public string pedido { get; set; }
            public string categoria { get; set; }

            public string producto { get; set; }

            public string descripcion { get; set; }
            public string cantidad { get; set; }

        }
    }
}
