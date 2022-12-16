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
        private List<Reports> ReportsList { get; set; }
        public bitacora()
        {
            InitializeComponent();
        }
        //evento click filtro fecha
        private void BtnFiltroClick_Click(object sender, RoutedEventArgs e)
        {
            DateTime? InitialDate = DateInitial.SelectedDate;
            DateTime? FinalDate = DateFinal.SelectedDate;
            if(InitialDate != null && FinalDate != null)
            {
                List<Reports> ReportsFilter = ReportsList.FindAll(report => report.createdAt >= InitialDate && report.createdAt <= FinalDate);
                DataGridBitacora.ItemsSource = ReportsFilter;
            }
        }
        public async void Get()
        {
            string Response = await Request.Get("reports");
            List<Reports> report = JsonConvert.DeserializeObject<List<Reports>>(Response);
            if (report != null)
            {
                this.ReportsList = report;
                DataGridBitacora.ItemsSource = this.ReportsList;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

    }
}
