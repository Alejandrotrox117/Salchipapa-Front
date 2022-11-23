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

namespace Front.Views.Adminisracion.Bitacora
{
    /// <summary>
    /// Lógica de interacción para bitacora.xaml
    /// </summary>
    public partial class bitacora : UserControl
    {
        public bitacora()
        {
            InitializeComponent();
        }

        public async void Get()
        {
            string Response = await Request.Get("reports");
            List<Reports> report = JsonConvert.DeserializeObject<List<Reports>>(Response);

            if (report != null)
            {
                DataGridBitacora.ItemsSource = report;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }






    }
}
