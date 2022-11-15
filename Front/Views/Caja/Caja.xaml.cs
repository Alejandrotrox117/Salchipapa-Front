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
                        DeleteElement(item._id);
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
       

        async private void PutElement(string id)
        {
            string link = ("http://localhost:3000/api/clients/" + id);
            clientes cliente = new clientes()
            {
                name = TxtNombre.Text,
                surname = TxtApellido.Text,
                ci = CBCedula.Text + TxtCedula.Text,
                address = TxtDireccion.Text,
                phone = CBTelefono.Text + TxtTelefono.Text

            };
            JavaScriptSerializer js = new JavaScriptSerializer();
            string data = js.Serialize(cliente);
            HttpContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var httpResponse = await client.PutAsync(link, content);
            //evaluar si la solicitud ha sido exitosa
            var result = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Se han editado los datos correctamente");
            }
            else
            {
                MessageBox.Show("Error" + result);
            }
        }

        public void PutViewElement()
        {
            if (DataGridClientes.SelectedItems.Count > 0)
            {
                List<clientes> miLista = (List<clientes>)DataGridClientes.ItemsSource;
                //Certifica si la lista esta vacia
                if (miLista != null)
                {
                    for (int i = 0; i < DataGridClientes.SelectedItems.Count; i++)
                    {
                        clientes item = DataGridClientes.SelectedItem as clientes;
                        //EVALUA SI LA LISTA ESTA VACIA Y CARGA EL FORMULARIO CON LOS DATOS
                        //DADA LA CONDICION

                        if (item != null)
                        {
                            DialogHostClientes.IsOpen = true;
                            TxtNombre.Text = item.name;
                            TxtApellido.Text = item.surname;
                            TxtCedula.Text = item.ci;
                            TxtTelefono.Text = item.phone;
                            TxtDireccion.Text = item.address;
                        }
                    }
                    DataGridClientes.ItemsSource = null;
                    DataGridClientes.ItemsSource = miLista;
                }
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            //METODO QUE ABRE EL FORMULARIO Y EL INDEX DE LA FILA DE LOS CLIENTES
            PutViewElement();
            TxtTituloDialg.Text = "Editar Clientes";
        }

      
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            EliminateViewElement();
        }

        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            string js = new JavaScriptSerializer().Serialize(new
            {
                ci = TxtCedula.Text,
                name = TxtNombre.Text,
                surname = TxtApellido.Text,
                address = TxtDireccion.Text,
                phone = CBTelefono.Text + TxtTelefono.Text,

            });
            Utilities.Post("clients", js);
          
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
    }
}
