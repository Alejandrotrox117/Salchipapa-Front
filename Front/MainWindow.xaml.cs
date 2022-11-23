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
using Front.Views.inicio;

namespace Front
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static employes session = null;
        public static employes Session { get { return session; } set { session = value; } }
        private SocketIO client;
        Pedidos pedidos;
        MenuMain menu;
        Caja caja;
        MainEmpleados empleados;
        Inicio inicio;
        public MainWindow()
        {
            InitializeComponent();
            if (session is null)
            {
                Session login = new Session();
                login.ShowDialog();
               session = login.session; 
                TxtNombreUser.Text = Session.ci;
            }
        }
        public async void SocketClient()
        {
            this.client = new SocketIO("http://192.168.0.110:3000");
            
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
            caja = new Caja();
            empleados = new MainEmpleados();
            menu = new MenuMain();
            inicio = new Inicio();
            MyFrame.NavigationService.Navigate(inicio);
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

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(pedidos);

        }

        private void TreeViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(menu);

        }

        private void ItemCaja_Selected(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(caja);
        }

        private void TreeViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            MyFrame.NavigationService.Navigate(empleados);
        }

        
    }
}
