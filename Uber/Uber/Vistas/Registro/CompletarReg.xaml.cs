using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uber.Modelo;
using Uber.VistaModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Uber.Vistas.Registro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompletarReg : ContentPage
    {
        public CompletarReg(GoogleUser parametros)
        {
            InitializeComponent();
            BindingContext = new VMcompletarreg(Navigation,parametros);
        }
    }
}