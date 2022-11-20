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

namespace Front.Views.Menu
{
    /// <summary>
    /// Lógica de interacción para MenuMain.xaml
    /// </summary>
    public partial class MenuMain : UserControl
    {
        public MenuMain()
        {
            InitializeComponent();
        }

        private void BtnExtras_Click(object sender, RoutedEventArgs e)
        {
            TabcontrolMenu.SelectedIndex = 2;
            PageToppings.GetToppings();
        }

        private void BtnTabCategoria_Click(object sender, RoutedEventArgs e)
        {
            TabcontrolMenu.SelectedIndex = 1;
            PageCategorias.Get();
        }

        private void GridMenu_Loaded(object sender, RoutedEventArgs e)
        {

            PageProductos.GetProductos();
        }

        private void BtnProducto_Click(object sender, RoutedEventArgs e)
        {
            TabcontrolMenu.SelectedIndex = 0;
            PageProductos.GetProductos();

        }
    }
}
