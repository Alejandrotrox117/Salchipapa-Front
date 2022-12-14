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
        private string id;
        public List<Employe> ListEmployes;
        public Empleados()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        //funcion obtener empleados
        public async void Get()
        {
            string response = await Request.Get("employes");
            List<Employe> returnedDataClients = JsonConvert.DeserializeObject<List<Employe>>(response);
            if (returnedDataClients != null)
            {
                DataGrid.ItemsSource = returnedDataClients;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }
        //evento agregar employes
        public async void Agregar_Click(object sender, RoutedEventArgs e)
        {
            string empleado = new JavaScriptSerializer().Serialize(new
            {
                ci = Formulario.CBCedula.Text + Formulario.TxtCedula.Text,
                name = Formulario.TxtNombre.Text,
                surname = Formulario.TxtApellido.Text,
                account = new
                {
                    password = Formulario.TxtContra.Text,
                    rol = Formulario.CbAccount.Text,
                    admin = Formulario.CheckBoxPrivilegio.IsChecked
                }
            });
            var response = await Request.Post("employes", empleado);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                limpiarDrawner();
                abrirSnack("Se ha agredado correctamente", null);
            }
            else
            {
                DrawerHost.IsBottomDrawerOpen = false;
                Error error = JsonConvert.DeserializeObject<Error>(content);
                abrirSnack("Ha ocurrido un error", error);
            }
        }
        //evento actualizar employes
        public async void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            string empleado = new JavaScriptSerializer().Serialize(new
            {
                ci = Formulario.CBCedula.Text + Formulario.TxtCedula.Text,
                name = Formulario.TxtNombre.Text,
                surname = Formulario.TxtApellido.Text,
                account = new
                {
                    password = Formulario.TxtContra.Text,
                    rol = Formulario.CbAccount.Text,
                    admin = Formulario.CheckBoxPrivilegio.IsChecked
                }
            });
            var response = await Request.Put("employes/"+id, empleado);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                limpiarDrawner();
                abrirSnack("Se ha actualizado correctamente", null);
            }
            else
            {
                DrawerHost.IsBottomDrawerOpen = false;
                Error error = JsonConvert.DeserializeObject<Error>(content);
                abrirSnack("Ha ocurrido un error", error);
            }
        }
        //evento eliminar employes
        public async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var response = await Request.Delete("employes/"+id);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                limpiarDrawner();
                abrirSnack("Se ha eliminado correctamente", null);
            }
            else
            {
                DrawerHost.IsBottomDrawerOpen = false;
                Error error = JsonConvert.DeserializeObject<Error>(content);
                abrirSnack("Ha ocurrido un error", error);
            }
        }
        //evento click btn agregar cliente
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Formulario.TxtTituloDialg.Text = "Agregar Empleado";
            TxtTituloDrawer.Text = "¿Desea agregar el empleado?";
            DialogHost.IsOpen = true;
            BtnConfirmarDrawner.Click += Agregar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }
        //evento click btn actualizar cliente
        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            Employe empleado = element.DataContext as Employe;
            id = empleado._id;
            Formulario.CargarForm(empleado);

            Formulario.TxtTituloDialg.Text = "Actualizar Empleado";
            TxtTituloDrawer.Text = "¿Desea actualizar el empleado?";
            DialogHost.IsOpen = true;
            BtnConfirmarDrawner.Click += Actualizar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawnerAbrirForm_Click;
        }
        //evento click boton eliminar cliente
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            Employe client = element.DataContext as Employe;
            id = client._id;

            TxtTituloDrawer.Text = "¿Desea eliminar el empleado?";
            DrawerHost.IsBottomDrawerOpen = true;
            BtnConfirmarDrawner.Click += Eliminar_Click;
            BtnCancelarDrawner.Click += BtnCancelarDrawner_Click;
        }
        //funcion abrir notificacion
        private void abrirSnack(string mensaje, Error error)
        {
            var bc = new BrushConverter();
            TxtSnackbar.Text = mensaje;
            SnackBarNotificacion.IsActive = true;
            if (error is null)
            {
                SnackBarNotificacion.Background = (Brush)bc.ConvertFrom("#00695c");
                BtnSnackbar.Click += BtnSnackbarCerrar_Click;
            }
            else
            {
                Formulario.MostrarErrores(error);
                SnackBarNotificacion.Background = (Brush)bc.ConvertFrom("#f44c58");
                BtnSnackbar.Click += BtnSnackbarAbrirForm_Click;
            }
        }
        //funcion limpiar drawner
        private void limpiarDrawner()
        {
            DrawerHost.IsBottomDrawerOpen = false;
            BtnConfirmarDrawner.Click -= Agregar_Click;
            BtnConfirmarDrawner.Click -= Actualizar_Click;
            BtnConfirmarDrawner.Click -= Eliminar_Click;
            BtnCancelarDrawner.Click -= BtnCancelarDrawnerAbrirForm_Click;
            BtnCancelarDrawner.Click -= BtnCancelarDrawner_Click;
        }
        //evento btn snack cerrar 
        private void BtnSnackbarCerrar_Click(object sender, RoutedEventArgs e)
        {
            Get();
            Formulario.LimpiarForm();
            SnackBarNotificacion.IsActive = false;
        }
        //evento btn snack abrir form 
        private void BtnSnackbarAbrirForm_Click(object sender, RoutedEventArgs e)
        {
            SnackBarNotificacion.IsActive = false;
            DialogHost.IsOpen = true;
        }
        //evento cierre snack siempre limpiar
        private void SnackBarNotificacion_IsActiveChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            if (!e.NewValue)
            {
                BtnSnackbar.Click -= BtnSnackbarCerrar_Click;
                BtnSnackbar.Click -= BtnSnackbarAbrirForm_Click;
            }
        }
        //evento click boton aceptar form
        private void BtnAceptarDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
            DrawerHost.IsBottomDrawerOpen = true;
        }
        //evento click boton cancelar form
        private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = false;
            Formulario.LimpiarForm();
            limpiarDrawner();
        }
        //evento click boton cancelar drawner para abrir form
        private void BtnCancelarDrawnerAbrirForm_Click(object sender, RoutedEventArgs e)
        {
            DrawerHost.IsBottomDrawerOpen = false;
            DialogHost.IsOpen = true;
        }
        //evento click boton cancelar drawner para abrir form
        private void BtnCancelarDrawner_Click(object sender, RoutedEventArgs e)
        {
            Formulario.LimpiarForm();
            limpiarDrawner();
        }
    }
}
