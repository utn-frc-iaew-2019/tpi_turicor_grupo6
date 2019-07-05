using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuricorAPI.ServiceReferenceReservaVehiculos;

namespace TuricorAPI.Models
{
    public class ReservaSOAP
    {
        private string apellidoNombreCliente;
        private DateTime fechaHoraDevolucion;
        private DateTime fechaHoraRetiro;
        private int idVehiculoCiudad;
        private string lugarDevolucion;
        private string lugarRetiro;
        private string nroDocumentoCliente;
        private string codigoReserva;
        private decimal totalReserva;


        public ReservaSOAP(ServiceReferenceReservaVehiculos.ReservaEntity reserva)
        {
            this.apellidoNombreCliente = reserva.ApellidoNombreCliente;
            this.fechaHoraDevolucion = reserva.FechaHoraDevolucion;
            this.fechaHoraRetiro = reserva.FechaHoraRetiro;
            this.idVehiculoCiudad = reserva.VehiculoPorCiudadId;
            this.lugarDevolucion = reserva.LugarDevolucion;
            this.lugarRetiro = reserva.LugarRetiro;
            this.nroDocumentoCliente = reserva.NroDocumentoCliente;
            this.totalReserva = reserva.TotalReserva;
            try { this.codigoReserva = reserva.CodigoReserva; }
            catch (System.Exception) { }

        }

        public string getApellidoNombreCliente()
        {
            return this.apellidoNombreCliente;
        }
        public DateTime getFechaHoraDevolucion()
        {
            return this.fechaHoraDevolucion;
        }
        public DateTime getFechaHoraRetiro()
        {
            return this.fechaHoraRetiro;
        }
        public int getIdVehiculoCiudad()
        {
            return this.idVehiculoCiudad;
        }
        public string getLugarDevolucion()
        {
            return this.lugarDevolucion;
        }
        public string getLugarRetiro()
        {
            return this.lugarRetiro;
        }
        public string getNroDocumentoCliente()
        {
            return this.nroDocumentoCliente;
        }

        public string getCodigoReserva()
        {
            return this.codigoReserva;
        }
    }
}