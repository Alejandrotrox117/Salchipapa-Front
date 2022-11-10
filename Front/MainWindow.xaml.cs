using Entities;
using Front.Views;
using Front.Views.Clientes;
using Front.Views.Empleados;
using Front.Views.Carta;
using Front.Views.Pedidos;
using Front.Views.Stock;
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

namespace Front
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SocketIO client;
        Pedidos pedidos;
        public MainWindow()
        {
            InitializeComponent();
           
        }

        public async Task<string> GetHttp()
        {
            //Funcion get para el socket
            string url = ("http://localhost:3000/api/socket");
            WebRequest oRequest = WebRequest.Create(url);
            WebResponse oResponse = oRequest.GetResponse();
            StreamReader sr = new StreamReader(oResponse.GetResponseStream());
            return await sr.ReadToEndAsync();
        }
        
        public async void SocketClient()
        {
            this.client = new SocketIO("http://192.168.1.105:3000");
            //this.client.OnConnected += (sender, arg) =>
            //{
            //    MessageBox.Show("Conectado al servidor");
            //};

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

        private void BtnPedidos_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(pedidos);
        }
        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(new Clientes());
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
            MyFrame.NavigationService.Navigate(new Carta());
            
        }
        private void BtnStock_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(new Stock());
        }
        private void MyFrame_Loaded(object sender, RoutedEventArgs e)
        {
            SocketClient();
        }
        private void BtnAceptarPedido_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(pedidos);
        }

        private void BtnEmpleados_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(new Empleados());
        }
    }
}
