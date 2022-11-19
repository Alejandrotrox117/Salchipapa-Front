﻿using System;
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

namespace Front.Views.Menu.Categorias
{
    /// <summary>
    /// Lógica de interacción para DialogHostCategorias.xaml
    /// </summary>
    public partial class DialogHostCategorias : UserControl
    {
        public string name { get; set; }

        public DialogHostCategorias()
        {
            InitializeComponent();
            this.DataContext = this;
            this.name = "";
                
        }



    }
}
