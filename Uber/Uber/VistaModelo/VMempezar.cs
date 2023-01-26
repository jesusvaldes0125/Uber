using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Uber.Vistas.Registro;
using Xamarin.Forms;

namespace Uber.VistaModelo
{
    public class VMempezar:BaseViewModel
    {
        #region VARIABLES

        #endregion
        #region CONSTRUCTOR
        public VMempezar(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion
        #region OBJETOS
       
        #endregion
        #region PROCESOS
        private async void IrCrearcuenta()
        {
            await Navigation.PushAsync(new CrearCuenta());
        }
        #endregion
        #region COMANDOS

        public ICommand IrcrearcuentaCommand => new Command(IrCrearcuenta);
       
        #endregion
    }
}
