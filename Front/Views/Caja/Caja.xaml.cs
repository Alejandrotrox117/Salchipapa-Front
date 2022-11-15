using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Net.Http;
using System.Net;
using Entities;
using Newtonsoft.Json;
using System.IO;
using Nancy.Json;
using System.Collections.ObjectModel;

namespace Front.Views.Caja
{
    /// <summary>
    /// Lógica de interacción para Caja.xaml
    /// </summary>
    public partial class Caja : UserControl
    {
        private static readonly HttpClient client = new HttpClient();
        private string _id = null;
        private List<clientes> listaClientes = new List<clientes>();
        public Caja()
        {
            InitializeComponent();
        }
      
        //METODO MAIN PARA DESEREALIZAR DATOS DEL JSON Y CONVERTIRLO A OBJETO C#
        public async void Main()
        {
            string respuesta = await Utilities.Get("clients/");
            //LISTA DE CLIENTES DESEREALIZADA PARA RETORNAR DATOS
            List<clientes> returnedData = JsonConvert.DeserializeObject<List<clientes>>(respuesta);
            //VALIDACION DE RETORNO DE DATOS 
            if (returnedData != null)
            {
                //ASIGNACION DE DATOS CONVERTIDOS AL ITEMS SOURCE DEL DATAGRID
                DataGridClientes.ItemsSource = returnedData;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        //METODO PARA BORRAR UN CLIENTE PASANDO POR PARAMETRO LA CEDULA COMO REQUISITO PARA EL INDICE
        //EL METODO DELETE DEBE USAR UN ID EN ESTE CASO ES LA CEDULA PARA BORRAR LA COLECCION
        public async void DeleteElement(string _id)
        {
            string url = ("http://localhost:3000/api/clients/" +_id);
            var httpResponse = await client.DeleteAsync(url);
            var result = await httpResponse.Content.ReadAsStringAsync();
            //CONDICION QUE ASEGURA EL PROCESO HA SIDO EXITOSO
            if (httpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Se ha eliminado correctamente");
            }
            else
            {
                MessageBox.Show("Error" + result);
            }
        }
        //ELIMINAR EL ELEMENTO DE MANERA VISUAL DEL DATAGRID 
        public void EliminateViewElement()
        {
            if (DataGridClientes.SelectedItems.Count > 0)
            {
                List<clientes> miLista = (List<clientes>)DataGridClientes.ItemsSource;
                //Certifica si la lista esta vacia
                if (miLista != null)
                {
                    for (int i = 0; i < DataGridClientes.SelectedItems.Count; i++)
                    {
                        //Obtiene el indice de la fila seleccionada mediante el boton
                        int indice = DataGridClientes.Items.IndexOf(DataGridClientes.SelectedItems[i]);
                        //Almacena en la variable item el item seleccionado del datagrid
                        clientes item = DataGridClientes.SelectedItem as clientes;
                        //En la variable id almacena la cedula y la pasa por parametros al metodo                 
                        Utilities.Delete("clients/",item.ci.ToString());
                        //Remueve la fila completa seleccionada solo visualmente
                        miLista.RemoveAt(indice);
                    }
                    DataGridClientes.ItemsSource = null;
                    DataGridClientes.ItemsSource = miLista;
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar por lo menos una fila.");
            }
        }
        
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            clientes cliente = element.DataContext as clientes;
            DialogHostClientes.IsOpen = true;
            this._id = cliente._id;
            TxtNombre.Text = cliente.name;
            TxtApellido.Text = cliente.surname;
            TxtCedula.Text = cliente.ci;
            string[] splitPhone = cliente.phone.Split('-');
            CBTelefono.Text = splitPhone[0]+'-';
            TxtTelefono.Text = splitPhone[1];
            TxtDireccion.Text = cliente.address;
            TxtTituloDialg.Text = "Editar Clientes";
            BtnEviarActualizar.Visibility = Visibility.Visible;
        }

        private async void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            string js = new JavaScriptSerializer().Serialize(new
            {
                ci = TxtCedula.Text,
                name = TxtNombre.Text,
                surname = TxtApellido.Text,
                address = TxtDireccion.Text,
                phone = CBTelefono.Text + TxtTelefono.Text,

            });
            var Response = await Utilities.Put("clients/" + _id + "/", js);
            if (Response.IsSuccessStatusCode)
                CerrarForm();
            else
                MessageBox.Show("hubo un error");

        }
        
        private async void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string js = new JavaScriptSerializer().Serialize(new
                {
                    ci = TxtCedula.Text,
                    name = TxtNombre.Text,
                    surname = TxtApellido.Text,
                    address = TxtDireccion.Text,
                    phone = CBTelefono.Text + TxtTelefono.Text,

                });
                var Response = await Utilities.Post("clients", js);
                if (Response.IsSuccessStatusCode)
                {
                    string sr = await Response.Content.ReadAsStringAsync();

                    clientes cliente = JsonConvert.DeserializeObject<clientes>(sr);
                    this.listaClientes.Add(cliente);
                    CerrarForm();
                }
                else
                    MessageBox.Show("hubo un error");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private void CerrarForm()
        {
            DialogHostClientes.IsOpen = false;
            BtnEnviar.Visibility = Visibility.Hidden;
            BtnEviarActualizar.Visibility = Visibility.Hidden;
        }

        private void DialogHostClientes_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            this.TxtNombre.Clear();
            this.TxtApellido.Clear();
            this.CBCedula.Text = "";
            this.CBTelefono.Text = "";
            this.TxtTelefono.Clear();
            this.TxtCedula.Clear();
            this.TxtDireccion.Clear();

        }

     
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Main();
        }

        private void BtnEliminar_Click_1(object sender, RoutedEventArgs e)
        {
            EliminateViewElement();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            TxtTituloDialg.Text = "Agregar Nuevo Cliente";
            BtnEnviar.Visibility = Visibility.Visible;
        }

        private void BtnCerrarForm_Click(object sender, RoutedEventArgs e)
        {
            CerrarForm();
        }
    }
}
