using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuricorAPI.Models;

namespace TuricorAPI.Datos
{
    public class DatosReserva
    {
        private ServiceReferenceReservaVehiculos.Credentials credenciales;
        private ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient cliente;
        private String usuario = "grupo_nro6";
        private String contraseña = "nH74Xl1j8X";


        public ServiceReferenceReservaVehiculos.ReservaEntity consultarReserva(string codigoReserva)
        {
            var request = new ServiceReferenceReservaVehiculos.ConsultarReservasRequest();
            request.CodigoReserva = codigoReserva;
            var respuesta = cliente.ConsultarReserva(credenciales, request);
            return respuesta.Reserva;
        }

        public ServiceReferenceReservaVehiculos.ReservaEntity reservarVehiculo(Reserva reserva)
        {
            var request = new ServiceReferenceReservaVehiculos.ReservarVehiculoRequest();
            request.ApellidoNombreCliente = reserva.ApellidoNombreCliente;
            request.FechaHoraDevolucion = reserva.FechaHoraDevolucion;
            request.FechaHoraRetiro = reserva.FechaHoraRetiro;
            request.IdVehiculoCiudad = reserva.IdVehiculoCiudad;
            request.LugarDevolucion = reserva.LugarDevolucion;
            request.LugarRetiro = reserva.LugarRetiro;
            request.NroDocumentoCliente = reserva.NroDocumentoCliente;
            var respuesta = cliente.ReservarVehiculo(credenciales, request);
            return respuesta.Reserva;

        }


    }
}