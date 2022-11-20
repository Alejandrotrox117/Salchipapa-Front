using Nancy.Json;
using Entities;
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
       
        public ToppingsMenu()
        {
            InitializeComponent();
           
        }
        
        public async void GetToppings()
        {
            string RespondTopping = await Request.Get("toppings");
            List<Entities.Toppings> toppings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Entities.Toppings>>(RespondTopping);
            if (toppings != null)
            {
                DataGridToppings.ItemsSource = toppings;
            }
            else
            {
                MessageBox.Show("Error");
            }

        }



        private void BtnAgregarTopping_Click(object sender, RoutedEventArgs e)
        {
           
        
            
        }
    }
}
