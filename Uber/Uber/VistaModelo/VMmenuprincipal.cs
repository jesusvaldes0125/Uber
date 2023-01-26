using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using System.IO;
using Uber.Vistas.Registro;
using System.Windows.Input;

namespace Uber.VistaModelo
{
    public class VMmenuprincipal:BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        int _estado=0;
        #endregion
        #region CONSTRUCTOR
        public VMmenuprincipal(INavigation navigation)
        {
            Navigation = navigation;
            Validarauth();
        }
        #endregion
        #region OBJETOS
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }
        #endregion
        #region PROCESOS
        public async Task ProcesoAsyncrono()
        {

        }
        public int Autenticacion()
        {
            try
            {
                var ruta=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"auth.txt");
                _estado=Convert.ToInt32 (File.ReadAllText(ruta));
                return _estado;
            }
            catch (Exception)
            {

                return _estado;
            }
        }
        public void Validarauth()
        {
            Autenticacion();    
            if(_estado==0) 
                {
                    Application.Current.MainPage =new NavigationPage(new Empezar());
                } 
        }
        #endregion
        #region COMANDOS

        public ICommand ValidarCommand => new Command(Validarauth);
         
        
        #endregion
    }
}
