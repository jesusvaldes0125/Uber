using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Uber.Modelo;
using Uber.VistaModelo;
using Xamarin.Forms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Uber.Vistas.Registro;
using Uber.Datos;
using Rg.Plugins.Popup.Services;
using Uber.Vistas.Reutilizables;

namespace Uber.VistaModelo
{
    internal class VMcompletarreg : BaseViewModel
    {
        #region VARIABLES
        string _txtNumero;
        List<Mpaises> _listapaises;
        Mpaises _selectPaisDefault;
        Mpaises _selectPais;
        public GoogleUser _googleUserRecibe{get; set;}
        #endregion
        #region CONSTRUCTOR
        public VMcompletarreg(INavigation navigation, GoogleUser _googleUserTrae)
        {
            Navigation = navigation;
            _googleUserRecibe= _googleUserTrae;
            ObtenerDataPaisXpais();
        }
        #endregion
        #region OBJETOS
        public string TxtNumero
        {
            get { return _txtNumero; }
            set { SetValue(ref _txtNumero, value); }
        }
        public List<Mpaises> Listapaises
        {
            get { return _listapaises; }
            set { SetValue(ref _listapaises, value); }
        }

        public Mpaises SelectPaisDefault
        {
            get { return _selectPaisDefault; }
            set { SetValue(ref _selectPaisDefault, value); }
        }
        public Mpaises SelectPais
        {
            get { return _selectPais; }
            set { SetValue(ref _selectPais, value); }
        }


        #endregion
        #region PROCESOS
        public async void Enviarsms()
        {
            try
            {
                //string accountSid = Environment.GetEnvironmentVariable("AC67d3e30c11a7a318fa3b652b46730e1d");
                //string authToken = Environment.GetEnvironmentVariable("f0b9371fb638293c08a8e25d0f88394d");
                //TwilioClient.Init(accountSid, authToken);

                //var message = MessageResource.Create(
                //    body: "This is the ship that made the Kessel Run in fourteen parsecs?",
                //    from: new Twilio.Types.PhoneNumber("+17262042512"),
                //    to: new Twilio.Types.PhoneNumber("+573003930912"));
                //Console.WriteLine(message.Sid);
                #region GENERAR CODIGO ALEATORIO
                Random generador = new Random();
                String randomsms = generador.Next(0,9999).ToString("D4");
                #endregion

                var accountSid = "AC67d3e30c11a7a318fa3b652b46730e1d";
                var authToken = "f0b9371fb638293c08a8e25d0f88394d";
                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(
                    new PhoneNumber(SelectPaisDefault.Codigopais+TxtNumero));
                messageOptions.MessagingServiceSid = "MGd67973633b8f25dcb628a0d511bb782d";
                messageOptions.Body = "Usa "+randomsms+ "Para validar en Uber";

                var message = MessageResource.Create(messageOptions);
                Console.WriteLine(message.Body);
                await Navigation.PushAsync(new DigitarCodigo(randomsms));
            }
            catch (Exception ex)
            {

                await DisplayAlert("Alerta", ex.Message,"Ok");
            }
        }
        public void Mostrarpaises()
        {
            var funcion = new Dpaises();
            Listapaises = funcion.Mostarpaises();
        }
        private void ObtenerDataPaisXpais()
        {
            var funcion = new Dpaises();
            SelectPaisDefault = funcion.MostrarpaisesXnombre("Colombia");
            SelectPais= funcion.MostrarpaisesXnombre("Colombia");
        }
        private void Irlistapaises()
        {
            var popup = new Listapaises();
            popup.BindingContext = this;
            Mostrarpaises();
            PopupNavigation.Instance.PushAsync(popup);
        }
        private void SeleccionarPais(Mpaises parametros)
        {
            SelectPais = parametros;
        }
        private void Confirmarpais()
        {
            SelectPaisDefault = SelectPais;
            PopupNavigation.Instance.PopAsync();
        }
        private void Cancelar()
        {
            PopupNavigation.Instance.PopAsync();
        }
        private void Buscarpais(string buscador)
        {
            buscador = PrimerLetraMayus(buscador);
            var funcion = new Dpaises();
            var Lista= funcion.ListaMostarpaisesXnombre(buscador);
            if (string.IsNullOrWhiteSpace(buscador))
            {
                Listapaises = new List<Mpaises>();
                Mostrarpaises();
            }
            else
            {
                if (Lista.Count > 0)
                {
                    Listapaises = new List<Mpaises>();
                    Listapaises = Lista;
                }
            }
        }
        #endregion
        #region COMANDOS
        public ICommand BuscarCommand => new Command<string>(Buscarpais);
        public ICommand CancelarCommand => new Command(Cancelar);
        public ICommand ConfirmarCommand => new Command(Confirmarpais);
        public ICommand IrpaisesCommand => new Command(Irlistapaises);
        public ICommand SelectPaisCommand => new Command<Mpaises>(SeleccionarPais);
        public ICommand SiguienteCommand => new Command(Enviarsms);
        //public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}