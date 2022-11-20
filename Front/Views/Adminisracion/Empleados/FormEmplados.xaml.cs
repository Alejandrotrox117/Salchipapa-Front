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

namespace Front.Views.Adminisracion.Empleados
{
    /// <summary>
    /// Lógica de interacción para DialogEmpleados.xaml
    /// </summary>
    public partial class FormEmpleados : UserControl
    {
        public FormEmpleados()
        {
            InitializeComponent();
        }

        public void cargarForm(employes employe)
        {
            TxtNombreEmpleado.Text = employe.name;
            TxtCedulaEmpleado.Text = employe.ci;
            TxtApellidoEmpleado.Text = employe.surname;
            CbAccount.Text = employe.ci.ToString();
            TxtContra.Text = employe.account.password;
            CheckBoxPrivilegio.IsChecked = employe.account.admin;
        }


    }
}
