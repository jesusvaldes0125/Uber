using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Uber.VistaModelo;
using Uber.Vistas.MenuPrincipal;
using Xamarin.Forms;

namespace Uber.VistaModelo
{
    public class VMdigitarcodigo:BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        string _txtcodigo;
        string mensajerecibido;
        #endregion
        #region CONSTRUCTOR
        public VMdigitarcodigo(INavigation navigation, string codigo)
        {
            Navigation = navigation;
            mensajerecibido = codigo;

        }
        #endregion
        #region OBJETOS
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }
        public string Txtcodigo
        {
            get { return _txtcodigo; }
            set { SetValue(ref _txtcodigo, value); }
        }
        #endregion
        #region PROCESOS
        public void ProcesoSimple()
        {

        }
        public async void validarcodigo()
        {
            if (Txtcodigo == mensajerecibido)
            {
                Creararchivo();
                await Navigation.PushAsync(new Vmenuprincipal());
            }
            else
            {
                await DisplayAlert("Alerta", "Código incorrecto", "Ok");
            }
        }
        public void Creararchivo()
        {

            var ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "auth.txt");
            StreamWriter sw;
            string estado = "1";
            try
            {
                if (File.Exists(ruta) == false)
                {
                    sw = File.CreateText(ruta);
                    sw.WriteLine(estado);
                    sw.Flush();
                    sw.Close();
                }
                else
                {
                    File.Delete(ruta);
                    sw = File.CreateText(ruta);
                    sw.WriteLine(estado);
                    sw.Flush();
                    sw.Close();

                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        
        #endregion
        #region COMANDOS
        
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        public ICommand validarcommad => new Command(validarcodigo);

        #endregion

    }
}
