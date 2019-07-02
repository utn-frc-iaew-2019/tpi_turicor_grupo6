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


        public DatosReserva()
        {
            credenciales = new ServiceReferenceReservaVehiculos.Credentials();
            cliente = new ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient();
            credenciales.UserName = usuario;
            credenciales.Password = contraseña;
        }

        public ServiceReferenceReservaVehiculos.ReservaEntity consultarReserva(string codigoReserva)
        {
            var request = new ServiceReferenceReservaVehiculos.ConsultarReservasRequest();
            request.CodigoReserva = codigoReserva;
            var respuesta = cliente.ConsultarReserva(credenciales, request);
            return respuesta.Reserva;
        }

        public ServiceReferenceReservaVehiculos.ReservaEntity reservarVehiculo(ReservaSOAP reserva)
        {
            var request = new ServiceReferenceReservaVehiculos.ReservarVehiculoRequest();
            request.ApellidoNombreCliente = reserva.getApellidoNombreCliente();
            request.FechaHoraDevolucion = reserva.getFechaHoraDevolucion();
            request.FechaHoraRetiro = reserva.getFechaHoraRetiro();
            request.IdVehiculoCiudad = reserva.getIdVehiculoCiudad();
            request.LugarDevolucion = reserva.getLugarDevolucion();
            request.LugarRetiro = reserva.getLugarRetiro();
            request.NroDocumentoCliente = reserva.getNroDocumentoCliente();
            var respuesta = cliente.ReservarVehiculo(credenciales, request);
            return respuesta.Reserva;

        }


    }
}