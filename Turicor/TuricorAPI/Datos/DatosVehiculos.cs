using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuricorAPI.Datos
{
    public class DatosVehiculos
    {
        private ServiceReferenceReservaVehiculos.Credentials credenciales;
        private ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient cliente;
        private String usuario = "grupo_nro6";
        private String contraseña = "nH74Xl1j8X";

        public DatosVehiculos()
        {
            credenciales = new ServiceReferenceReservaVehiculos.Credentials();
            cliente = new ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient();
            credenciales.UserName = usuario;
            credenciales.Password = contraseña;
        }

        public ServiceReferenceReservaVehiculos.VehiculoModel[] consultarVehiculosDisponibles(int idCiudad, DateTime fechaHoraRetiro, DateTime fechaHoraDevolucion)
        {
            var request = new ServiceReferenceReservaVehiculos.ConsultarVehiculosRequest();
            request.IdCiudad = idCiudad;
            request.FechaHoraRetiro = fechaHoraRetiro;
            request.FechaHoraDevolucion = fechaHoraDevolucion;

            var respuesta = cliente.ConsultarVehiculosDisponibles(credenciales, request);

            return respuesta.VehiculosEncontrados;
        }
    }
}