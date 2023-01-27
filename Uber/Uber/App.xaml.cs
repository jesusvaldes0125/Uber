using System;
using Uber.Vistas.MenuPrincipal;
using Uber.Vistas.Navegacion;
using Uber.Vistas.Registro;
using Uber.Vistas.Reutilizables;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Uber
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Esperaroferta());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
