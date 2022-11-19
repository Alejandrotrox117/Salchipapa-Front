using Entities;
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

namespace Front.Views.Menu.Productos
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : UserControl
    {
        public ObservableCollection<products> productos;
        public Productos()
        {
            InitializeComponent();
        }

        public async void GetProductos()
        {
            string responseGrupos = await Utilities.Get("products?extendeData=true");
            List<Grupos> group = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Grupos>>(responseGrupos);
            if (group != null)
            {
                TabsProductos.ItemsSource = group;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void BtnAbrirDialogProductos_Click(object sender, RoutedEventArgs e)
        {
            DialogHostProductos.IsOpen = true;
        }
    }
}
