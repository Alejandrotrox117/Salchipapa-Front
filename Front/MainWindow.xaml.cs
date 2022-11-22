using Entities;
using Front.Views;

using Front.Views.Empleados;
using Front.Views.Menu;
using Front.Views.Pedidos;
using Newtonsoft.Json;
using SocketIOClient;
using SocketIOClient.JsonSerializer;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using Front.Views.Caja;
using Nancy.Json;

namespace Front
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private employes session = null;
        public employes Session { get { return this.session; } set { this.session = value; } }
        private SocketIO client;
        Pedidos pedidos;
        public MainWindow()
        {
            InitializeComponent();
            //if (session is null)
            //{
            //    Session login = new Session();
            //    login.ShowDialog();
            //    this.session = login.session;
            //    TxtNombreUser.Text = Session.ci;
            //}
        }
        public async void Iniciar()
        {
            string session = new JavaScriptSerializer().Serialize(new
            {

                ci = "28453511",
                password = "Hola.1"
            });
            var response = await Request.Post("session", session);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                employes content = JsonConvert.DeserializeObject<employes>(data);
                this.BtnCaja.IsEnabled = content.account.rol != "ADMIN" || content.account.rol != "CAJERO";
                MessageBox.Show("Inicio session");
            }
        }
        public async void SocketClient()
        {
            this.client = new SocketIO("http://localhost:3000");
            
            this.client.On("newOrder", async response =>
             {
                 this.Dispatcher.Invoke(() =>
                 {
                    SnackbarNotify.IsActive = true;                     
                 });
             });
            await this.client.ConnectAsync();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            RelojDigital();
            SocketClient();
            pedidos = new Pedidos(this.client);
            MyFrame.NavigationService.Navigate(pedidos);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Funcion para que la ventana se pueda mover usando el mouse
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
     
       
        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            //Para cerrar la aplicación
            Application.Current.Shutdown();
        }
        public void RelojDigital()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            txtFecha.Text = DateTime.Now.ToString("D");
        }
        void timer_Tick(object sender, EventArgs e)
        {
            LblHora.Content = DateTime.Now.ToLongTimeString();
        }
        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(new Views.Menu.MenuMain());
            
        }
       
        private void MyFrame_Loaded(object sender, RoutedEventArgs e)
        {
            SocketClient();
        }
        private void BtnPedidos_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(pedidos);
        }

        private void BtnCaja_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(new Caja());
        }

        private void BtnAdministracion_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(new MainEmpleados());
        }

        private void Principal_Loaded(object sender, RoutedEventArgs e)
        {
            Iniciar();
        }
    }
}
