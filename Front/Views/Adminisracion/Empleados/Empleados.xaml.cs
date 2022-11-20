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

namespace Front.Views.Adminisracion.Empleados
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class Empleados : UserControl
    {
        public string id { get; set; }  
        public Empleados()
        {
            InitializeComponent();
        }

        //funcion cargar 
        public async void Get()
        {
            string Response = await Request.Get("employes");
            List<employes> empleados = JsonConvert.DeserializeObject<List<employes>>(Response);

            if (empleados != null)
            {
                MessageBox.Show(empleados[0].account.rol);
                DataGrid.ItemsSource = empleados;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        //POST
        //funcion agregar categoria
        public async void Post(object sender, RoutedEventArgs e)
        {
            string empleados = new JavaScriptSerializer().Serialize(new
            {
                name = Formulario.TxtNombreEmpleado.Text,
                ci = Formulario.TxtCedulaEmpleado.Text,
                surname = Formulario.TxtApellidoEmpleado.Text,
                account = Formulario.CbAccount.Text,
                password = Formulario.TxtContra.Text

            });
            var Response = await Request.Post("employes/", empleados);
            if (Response.IsSuccessStatusCode)
            {
                TxtSnackbar.Text = "¡Se ha agregado correctamente!";
                SnackBarNotificacion.IsActive = true;
                LimpiarEventos();
            }
            else
                MessageBox.Show("hubo un error");

        }

        private void LimpiarEventos()
        {
            DrawerHost.IsBottomDrawerOpen = false;
            BtnConfirmarDrawner.Click -= Post;
            BtnConfirmarDrawner.Click -= Put;
            BtnConfirmarDrawner.Click -= Delete;
            TxtTituloDrawer.Text = "";

        }

        //AbrirFormulario
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Formulario.TxtDialog.Text = "Agregar Empleado";
            DialogHost.IsOpen = true;
            TxtTituloDrawer.Text = "¿Desea agregar un nuevo Empleado";
            BtnConfirmarDrawner.Click += Post;

        }
        //PUT EMPLEADOS
        public async void Put(object sender, RoutedEventArgs e)
        {
            string empleados = new JavaScriptSerializer().Serialize(new
            {
                name = Formulario.TxtNombreEmpleado.Text,
                ci = Formulario.TxtCedulaEmpleado.Text,
                surname = Formulario.TxtApellidoEmpleado.Text,
                account = Formulario.CbAccount.Text,
                password = Formulario.TxtContra.Text

            });
            var Response = await Request.Put("employes/" + id, empleados);
            if (Response.IsSuccessStatusCode)
            {
                TxtSnackbar.Text = "Se ha agregado correctamente";
                SnackBarNotificacion.IsActive = true;
                LimpiarEventos();
            }
            else
            {
                string error = await Response.Content.ReadAsStringAsync();
                MessageBox.Show(error);
            }

        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            employes employe = element.DataContext as employes;
            id = employe._id;
            Formulario.cargarForm(employe);
            Formulario.TxtDialog.Text = "Actualizar Empleado";
            DialogHost.IsOpen = true;
            TxtTituloDrawer.Text = "¿Desea actualizar el empleado?";
            BtnConfirmarDrawner.Click += Put;

        }

        //DELETE EMPLEADOS
        public async void Delete(object sender, RoutedEventArgs e)
        {
            var Response = await Request.Delete("employes/" + id);
            if (Response.IsSuccessStatusCode)
            {
                TxtSnackbar.Text = "Se ha eliminado correctamente";
                SnackBarNotificacion.IsActive = true;
                LimpiarEventos();
            }
            else
                MessageBox.Show("hubo un error");
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            employes employe = element.DataContext as employes;
            id = employe._id;
            TxtTituloDrawer.Text = "¿Desea eliminar el Empleado?";
            DrawerHost.IsBottomDrawerOpen = true;
            BtnConfirmarDrawner.Click += Delete;
        }

        private void BtnCancelarFormulario_Click(object sender, RoutedEventArgs e)
        {
            LimpiarEventos();
        }

        private void BtnAceptarFormulario_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
            DrawerHost.IsBottomDrawerOpen = true;
        }

        private void BtnCancelarDrawner_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancelarDrawner_Click_1(object sender, RoutedEventArgs e)
        {
            LimpiarEventos();
        }

        private void BtnConfirmarDrawner_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
