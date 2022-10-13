using System;
using System.Collections;
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

namespace Front.Views.Stock
{
    /// <summary>
    /// Lógica de interacción para Stock.xaml
    /// </summary>
    public partial class Stock : UserControl
    {
        public Stock()
        {
            InitializeComponent();
            LoadListView();
        }

        public void LoadListView()
        {
            //this.ListProductos.Items.Add(new Producto { Name = "Pie de manzana", topping = "Flips,leche" });
            //this.ListProductos.Items.Add(new Producto { Name = "Café" });
            //this.ListProductos.Items.Add(new Producto { Name = "Torta" });
            //this.ListProductos.Items.Add(new Producto { Name = "Batido de fresa" });
            //this.ListProductos.Items.Add(new Producto { Name = "Batido de toddy" });

        }

        class Building
        {
            public List<Item> Items { get; }
            public string Name { get; }
        }

        class Item
        {
            public int Number { get; }
        }
        public class Producto
        {
            public string Name { get; set; }
            public string topping { get; set; }
        }

    }
}
