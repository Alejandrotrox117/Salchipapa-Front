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

namespace Front.Views.Menu.Toppings
{
    /// <summary>
    /// Lógica de interacción para DialogToppings.xaml
    /// </summary>
    public partial class DialogToppings : UserControl
    {
        public DialogToppings()
        {
            InitializeComponent();
        }
        private void BtnEnviarTopping_Click(object sender, RoutedEventArgs e)
        {
            ToppingsMenu toppings= new ToppingsMenu();
            toppings.DrawerHostToppings.IsBottomDrawerOpen = true;

 
        }





    }
}
