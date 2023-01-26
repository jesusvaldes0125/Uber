using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Uber.Modelo;
using Uber.Vistas.Registro;

namespace Uber.VistaModelo
{
    public class VMCrearCuenta:BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        private readonly IGoogleManager _googleManager;
        GoogleUser googleuserObtiene = new GoogleUser();

        #endregion
        #region CONSTRUCTOR
        public VMCrearCuenta(INavigation navigation)
        {
            Navigation = navigation;
            _googleManager = DependencyService.Get<IGoogleManager>();
            
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
        
        public void LoguearseConGmail()
        {
            _googleManager.Login(OnLoginComplete);

        }
        public async void OnLoginComplete(GoogleUser googleuserTrae, string message)
        {
            if(googleuserTrae != null) 
            {
                googleuserObtiene = googleuserTrae;
                string[] cadena = googleuserObtiene.Name.Split(' ');
                googleuserObtiene.Name = cadena[0];
                googleuserObtiene.Apellido = cadena[1];
                await Navigation.PushAsync(new CompletarReg(googleuserObtiene));
            }
            else
            {
                await DisplayAlert("Mensaje", message, "ok");
            }
        }

        #endregion
        #region COMANDOS
        public ICommand GmailCommand => new Command(LoguearseConGmail);
       
        #endregion
    }
}
