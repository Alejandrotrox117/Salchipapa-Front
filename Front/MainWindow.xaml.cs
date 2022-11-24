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
        public static Employe session = null;
        public static bool admin { get { return session.account.admin; } }

        private SocketIO client;

        Pedidos pedidos;

        MenuMain menu;

        Caja caja;

        MainEmpleados empleados;
        Inicio inicio;
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IniciarSession();
            //string js = new JavaScriptSerializer().Serialize(new
            //{
            //    ci = "28498482",
            //    password = "$dra17"
            //});
            //var httpResponse = await Request.Post("session", js);
            //string result = await httpResponse.Content.ReadAsStringAsync();
            //session = JsonConvert.DeserializeObject<Employe>(result);
            if (session is not null)
            {
                TxtNombreUser.Text = session.ci;
                BtnPedidos.IsEnabled = session.account.rol == "ADMIN" || session.account.rol == "MESERO" || session.account.rol == "COCINERO";
                BtnMenu.IsEnabled = session.account.rol == "ADMIN" || session.account.rol == "COCINERO";
                BtnCaja.IsEnabled = session.account.rol == "ADMIN" || session.account.rol == "CAJERO";
                BtnAdministracion.IsEnabled = session.account.rol == "ADMIN";
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
        public void IniciarSession()
        {
            this.Hide();
            Session login = new Session();
            login.ShowDialog();
            session = login.session;
            this.Show();
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
            pedidos = new Pedidos(client);
            menu = new MenuMain();
            caja = new Caja();
            empleados = new MainEmpleados();
           
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
            TreeViewItem item = sender as TreeViewItem;
            switch (item.Name)
            {
                case "BtnPedidos":
                    MyFrame.NavigationService.Navigate(pedidos);
                    break;
                case "BtnCaja":
                    MyFrame.NavigationService.Navigate(caja);
                    break;
                case "BtnMenu":
                    MyFrame.NavigationService.Navigate(menu);
                    break;
                case "BtnAdministracion":
                    MyFrame.NavigationService.Navigate(empleados);
                    break;

            }
        
        }

    }
}
