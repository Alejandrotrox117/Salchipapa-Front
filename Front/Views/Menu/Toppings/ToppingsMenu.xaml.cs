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

namespace Front.Views.Menu
{
    /// <summary>
    /// Lógica de interacción para Toppings.xaml
    /// </summary>
    public partial class ToppingsMenu : UserControl
    {
       public Toppings.DialogToppings VentanaDialog ;
        public ToppingsMenu()
        {
            InitializeComponent();
           
        }
        //public async void Main()
        //{
         
        //    string RespondTopping = await Utilities.Get("toppings");
        //     List<ToppingsMenu> toppings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ToppingsMenu>>(RespondTopping);
           

        //    if (toppings != null)
        //    {
              
        //        DataGridToppings.ItemsSource = toppings;
              
        //    }
        //    else
        //    {
        //        MessageBox.Show("Error");
        //    }
 
        //}
       

        private void DataGridToppings_Initialized(object sender, EventArgs e)
        {
            //Main();
        }

        private void BtnAgregarTopping_Click(object sender, RoutedEventArgs e)
        {
           
           this.VentanaDialog.DialogHostToppings.IsOpen = true;
            
        }
    }
}
