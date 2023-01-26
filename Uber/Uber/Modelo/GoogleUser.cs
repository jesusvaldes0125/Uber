﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Uber.Modelo
{
     public class GoogleUser
    {
        public string Name { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public Uri Picture { get; set; }
    }
    public interface IGoogleManager
    {
        void Login(Action<GoogleUser,string> OnLoginComplete);
        void Logout();
    }


}
