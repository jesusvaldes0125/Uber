﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uber.VistaModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Uber.Vistas.MenuPrincipal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Vmenuprincipal : ContentPage
    {
        
        public Vmenuprincipal()
        {
            InitializeComponent();
            BindingContext = new VMmenuprincipal(Navigation);
        }
    }
}