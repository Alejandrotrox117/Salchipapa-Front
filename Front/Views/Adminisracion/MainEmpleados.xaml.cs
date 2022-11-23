using Entities;
using Nancy.Json;
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

namespace Front.Views.Empleados
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class MainEmpleados : UserControl
    {
        public MainEmpleados()
        {
            InitializeComponent();
        }

      

      
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            PageEmpleados.Get();

        }

        private void BtnTabEmpleado_Click(object sender, RoutedEventArgs e)
        {
            TabControlEmpleados.SelectedIndex = 0;
            PageEmpleados.Get();
        
        }

        private void BtnBitacora_Click(object sender, RoutedEventArgs e)
        {
            TabControlEmpleados.SelectedIndex = 1;
            PageBitacora.Get();
        }

        
    }
}
