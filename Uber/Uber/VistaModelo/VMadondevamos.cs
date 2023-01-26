using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Uber.Datos;
using Uber.Modelo;
using Uber.Servicios;
using Uber.VistaModelo;
using Uber.Vistas.Navegacion;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Uber.VistaModelo
{
    public class VMadondevamos:BaseViewModel
    {
        #region VARIABLES
        List<GooglePlaceAutoCompletePrediction> _listadirecciones;
        private readonly IGoogleMapsApiService _googleMapsApi = new GoogleMapsApiService();
        string _txtorigen;
        string _txtdestino;
        string _txttarifaEmereg;
        string _txttarifa;
        string _txtbuscador;
        double ltOrigen = 0;
        double lgOrigen = 0;
        double ltDestino = 0;
        double lgDestino = 0;
        bool _selectorigen;
        bool _selectdestino;
        bool _enableTxtorigen;
        bool _enableTxtdestino;
        bool _visibleListdirec;
        bool _fijarenmapa;
        bool _visibleOfertar;
        Pin punto = new Pin();
        Xamarin.Forms.GoogleMaps.Map mapa;
        public GoogleMatrix ParametosMatrix { get; set; }
        #endregion
        #region CONSTRUCTOR
        public VMadondevamos(INavigation navigation, Xamarin.Forms.GoogleMaps.Map mapareferencia)
        {
            Navigation=navigation;
            mapa=mapareferencia;
            mapa.PropertyChanged += Mapa_PropertyChanged;
            EnableTxtorigen=false;
            EnableTxtdestino=false;
            Selectorigen=false;
            Selectdestino=false;
            VisibleListdirec=false;
            Fijarenmapa=false;
            VisibleOfertar = false;
            Txtbuscador = "";
            PinActual();
        }
        #endregion
        #region OBJETOS
        public string Txtbuscador
        {
            get { return _txtbuscador; }
            set { SetValue(ref _txtbuscador, value); }
        }
        public string TxttarifaEmereg
        {
            get { return _txttarifaEmereg; }
            set { SetValue(ref _txttarifaEmereg, value); }
        }
        public string Txttarifa
        {
            get { return _txttarifa; }
            set { SetValue(ref _txttarifa, value); }
        }
        public bool VisibleOfertar
        {
            get { return _visibleOfertar; }
            set { SetValue(ref _visibleOfertar, value); }
        }
        public bool Fijarenmapa
        {
            get { return _fijarenmapa; }
            set { SetValue(ref _fijarenmapa, value); }
        }
        public bool VisibleListdirec
        {
            get { return _visibleListdirec; }
            set { SetValue(ref _visibleListdirec, value); }
        }
        public bool Selectorigen
        {
            get { return _selectorigen; }
            set { SetValue(ref _selectorigen, value); }
        }
        public bool Selectdestino
        {
            get { return _selectdestino; }
            set { SetValue(ref _selectdestino, value); }
        }
        public bool EnableTxtorigen
        {
            get { return _enableTxtorigen; }
            set { SetValue(ref _enableTxtorigen, value); }
        }

        public bool EnableTxtdestino
        {
            get { return _enableTxtdestino; }
            set { SetValue(ref _enableTxtdestino, value); }
        }
        public string Txtorigen
        {
            get { return _txtorigen; }
            set { SetValue(ref _txtorigen, value); }
        }
        public string Txtdestino
        {
            get { return _txtdestino; }
            set { SetValue(ref _txtdestino, value); }
        }
        public List<GooglePlaceAutoCompletePrediction> Listadirecciones
        {
            get { return _listadirecciones; }
            set { SetValue(ref _listadirecciones, value); }
        }
        #endregion
        #region PROCESOS
        private void Agregartarifa()
        {
            Txttarifa = TxttarifaEmereg;
            Cerraroferta();
        }
        private void Cerraroferta()
        {
            VisibleOfertar = false;
        }
        private void VerOfertar()
        {
            VisibleOfertar = true;
        }
        [Obsolete]
        private async void Mapa_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Fijarenmapa==false)
            {
                return;
            }
            var m = (Xamarin.Forms.GoogleMaps.Map)sender;
            if (m.VisibleRegion!=null) 
            {
                if (Selectorigen == true)
                {
                    Txtorigen = await ObtenerDireccion(m.VisibleRegion.Center.Latitude, m.VisibleRegion.Center.Longitude);
                    ltOrigen = m.VisibleRegion.Center.Latitude;
                    lgOrigen = m.VisibleRegion.Center.Longitude;
                }
                if (Selectdestino == true)
                {
                    Txtdestino= await ObtenerDireccion(m.VisibleRegion.Center.Latitude, m.VisibleRegion.Center.Longitude);
                    ltDestino = m.VisibleRegion.Center.Latitude;
                    lgDestino = m.VisibleRegion.Center.Longitude;
                }
            }
        }
        private async Task<string>ObtenerDireccion(double lt, double lg)
        {
            try
            {
                Geocoder geoCoder = new Geocoder();
                Position position= new Position(lt, lg);
                IEnumerable<string> direcciones = await geoCoder.GetAddressesForPositionAsync(position);
                string direccion = direcciones.FirstOrDefault();
                return direccion;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }        
        private void FijarenMapa()
        {
            Fijarenmapa = true;
            VisibleListdirec = false;
            EnableTxtorigen= false;
            EnableTxtdestino= false;
        }
        public async Task PinActual()
        {
            punto = new Pin()
            {
                Label = "Tu ubicación",
                Type = PinType.Place,
                Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("pin.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "pin.png", WidthRequest = 64, HeightRequest = 64 }),
                Position = new Position(0, 0)

            };
            mapa.Pins.Add(punto);
            await GeolocalizacionActual();
        }
        public async Task GeolocalizacionActual()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location==null)
                {
                    location=await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy=GeolocationAccuracy.High,Timeout=TimeSpan.FromSeconds(30)
                    }); 
                    
                }
                if (location!=null)
                {
                    ltOrigen = location.Latitude;
                    lgOrigen = location.Longitude;
                    Txtorigen = "Tu ubicación";
                    var posicion = new Position(ltOrigen, lgOrigen);
                    punto.Position = new Position(ltOrigen, lgOrigen);
                    mapa.MoveToRegion(MapSpan.FromCenterAndRadius(posicion, Distance.FromMeters(500)));
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }        
        private async Task Buscardirecciones(string buscador)
        {
            var places = await _googleMapsApi.ApiPlaces(buscador);
            var placeResults = places.AutoCompletePlaces;
            if(placeResults!=null && placeResults.Count>0)
            {
                Listadirecciones = new List<GooglePlaceAutoCompletePrediction>(placeResults);
            }
        }
        private async Task SeleccionarDireccion(GooglePlaceAutoCompletePrediction parametros)
        {
            var coordenadas = await _googleMapsApi.ApiPlacesDetails(parametros.PlaceId);
            if (coordenadas != null)
            {
                if (Selectorigen==true)
                {
                    ltOrigen = coordenadas.Latitude;
                    lgOrigen = coordenadas.Longitude;
                    Txtorigen= coordenadas.Name;

                }
                if(Selectdestino== true)
                {
                    ltDestino= coordenadas.Latitude;
                    lgDestino= coordenadas.Longitude;
                    Txtdestino= coordenadas.Name;
                }
                VisibleListdirec = false;
            }
        }
        private void SeleccionarOrigen()
        {
            Selectorigen = true;
            Selectdestino = false;
            EnableTxtorigen= true;
            EnableTxtdestino= false;
            VisibleListdirec = true;
            Fijarenmapa = false;
            Txtbuscador = "";
        }
        private void SeleccionarDestino()
        {
            Selectorigen = false;
            Selectdestino = true;
            EnableTxtorigen = false;
            EnableTxtdestino = true;
            VisibleListdirec = true;
            Fijarenmapa = false;
            Txtbuscador = "";
        }
        private async void Insetarpedido()
        {
            var Coororigen = ltOrigen.ToString().Replace(",",".")+","+lgOrigen.ToString().Replace(",",".");
            var Coordestino = ltDestino.ToString().Replace(",", ".")+","+lgDestino.ToString().Replace(",", ".");
            ParametosMatrix = await _googleMapsApi.Calculardistanciatiempo(Coororigen, Coordestino);
            var funcion = new Dpedidos();
            var parametros = new Mpedidos();
            parametros.origen_lugar = Txtorigen;
            parametros.destino_lugar = Txtdestino;
            parametros.idchofer = "Modelo";
            parametros.iduser = "Modelo";
            parametros.lt_lg_origen= Coororigen;
            parametros.lt_lg_destino = Coordestino;
            parametros.estado="PENDIENTE";
            parametros.tiempo=ParametosMatrix.Rows[0].Elements[0].duration.text;
            parametros.tarifa=Txttarifa;
            parametros.distancia=ParametosMatrix.Rows[0].Elements[0].distance.value.ToString();
            await funcion.Insertarpedidos(parametros);
            await Navigation.PushAsync(new Esperaroferta(parametros));
          
        }

        private void Volverdebuscar()
        {
            VisibleListdirec = false;
        }

        #endregion
        #region COMANDOS
        public ICommand Volverdebuscarcommand => new Command(Volverdebuscar);
        public ICommand Insertarpedidocommand => new Command(Insetarpedido);
        public ICommand Agregartarifacommand => new Command(Agregartarifa);
        public ICommand CerrarOfertacommand => new Command(Cerraroferta);
        public ICommand VerOfertarcommand => new Command(VerOfertar);
        public ICommand Fijarenmapacommand => new Command(FijarenMapa);
        public ICommand SelectDireccioncommand => new Command<GooglePlaceAutoCompletePrediction>(async(p) => await SeleccionarDireccion(p));
        public ICommand Buscarcommand => new Command<string>(async (p) => await Buscardirecciones (p));
        public ICommand SelectOrigencommand => new Command(SeleccionarOrigen);
        public ICommand SelectDestinocommand => new Command(SeleccionarDestino);

        #endregion
    }
}
