using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;

namespace Uber.Conexiones
{
    public class Constantes
    {
        public const string GoogleMapsApiKey = "AIzaSyDVjG6mTGOtdgQcuSzsxuSWFJ74C_otg6I";
        public static FirebaseClient firebase = new FirebaseClient("https://uber-curso-3e339-default-rtdb.firebaseio.com/");
    }
}
