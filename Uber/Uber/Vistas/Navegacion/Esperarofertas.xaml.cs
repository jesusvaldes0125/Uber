using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uber.Modelo;
using Uber.VistaModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Uber.Vistas.Navegacion
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Esperaroferta : ContentPage
	{
		public Esperaroferta ()
		{
			InitializeComponent ();
			BindingContext = new VMesperarofertas(Navigation);
		}
	}
}