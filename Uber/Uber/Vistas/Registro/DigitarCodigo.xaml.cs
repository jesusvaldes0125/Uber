using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uber.VistaModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Uber.Vistas.Registro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DigitarCodigo : ContentPage
    {
        public DigitarCodigo(string codigo)
        {
            InitializeComponent();
            BindingContext= new VMdigitarcodigo(Navigation,codigo);
        }
    }
}