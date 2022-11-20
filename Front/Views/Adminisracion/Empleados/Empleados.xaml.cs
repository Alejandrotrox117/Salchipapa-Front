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

namespace Front.Views.Adminisracion.Empleados
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class Empleados : UserControl
    {
        public Empleados()
        {
            InitializeComponent();
        }
        //private void BtnEnviarEmpleado_Click(object sender, RoutedEventArgs e)
        //{
        //    string empleado = new JavaScriptSerializer().Serialize(new
        //    {
        //        ci = TxtCedulaEmpleado.Text,
        //        name = TxtNombreEmpleado.Text,
        //        surname = TxtApellidoEmpleado.Text,
        //        account = CbAccount.Text,
        //        password = TxtContra.Text


        //    });
        //    Request.Post("clients", empleado);
        //}
    }
}
