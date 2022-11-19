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

namespace Front.Views.Caja.Clientes
{
    /// <summary>
    /// Lógica de interacción para Clientes.xaml
    /// </summary>
    public partial class Clientes : UserControl
    {
        public Clientes()
        {
            InitializeComponent();
        }
        public async void GetClientes()
        {
            string responseClients = await Request.Get("clients/");
            //LISTA DE CLIENTES DESEREALIZADA PARA RETORNAR DATOS
            List<clientes> returnedDataClients = JsonConvert.DeserializeObject<List<clientes>>(responseClients);
            //VALIDACION DE RETORNO DE DATOS 
            if (returnedDataClients != null)
            {
                //ASIGNACION DE DATOS CONVERTIDOS AL ITEMS SOURCE DEL DATAGRID
                DataGridClientes.ItemsSource = returnedDataClients;
                }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void BtnAgregarCliente_Click(object sender, RoutedEventArgs e)
        {
            DialogHostClientes.IsOpen = true;
        }
    }
}
