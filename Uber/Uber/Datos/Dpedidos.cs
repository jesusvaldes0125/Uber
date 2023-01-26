﻿using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uber.Conexiones;
using Uber.Modelo;

namespace Uber.Datos
{
    public class Dpedidos
    {
        public async Task<bool>Insertarpedidos(Mpedidos parametros)
        {
            await Constantes.firebase
            .Child("Pedidos")
            .PostAsync(new Mpedidos()
            {
                destino_lugar= parametros.destino_lugar,
                idchofer=parametros.idchofer,
                iduser=parametros.iduser,                
                lt_lg_destino=parametros.lt_lg_destino,
                lt_lg_origen=parametros.lt_lg_origen,
                estado = parametros.estado,
                origen_lugar =parametros.origen_lugar,
                tiempo=parametros.tiempo,
                tarifa=parametros.tarifa,
                distancia=parametros.distancia 
            });
          return true;
        }
    }
}
