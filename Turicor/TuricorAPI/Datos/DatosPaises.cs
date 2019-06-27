using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuricorAPI.Datos
{
    public class DatosPaises
    {
        private ServiceReferenceReservaVehiculos.Credentials credenciales;
        private ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient cliente;
        private String usuario = "grupo_nro6";
        private String contraseña = "nH74Xl1j8X";


        public DatosPaises()
        {
            credenciales.UserName = usuario;
            credenciales.Password = contraseña;
        }

        public ServiceReferenceReservaVehiculos.PaisEntity[] consultarPaises()
        {
            var respuesta = cliente.ConsultarPaises(credenciales);
            return respuesta.Paises;
        }
        
        public ServiceReferenceReservaVehiculos.CiudadEntity[] consultarCiudades(int id)
        {
            var request = new ServiceReferenceReservaVehiculos.ConsultarCiudadesRequest();
            request.IdPais = id;
            var respuesta = cliente.ConsultarCiudades(credenciales, request);
            return respuesta.Ciudades;
        }

    }
}