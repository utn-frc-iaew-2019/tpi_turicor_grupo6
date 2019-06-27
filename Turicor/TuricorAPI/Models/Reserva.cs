using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuricorAPI.ServiceReferenceReservaVehiculos;

namespace TuricorAPI.Models
{
    public class Reserva
    {
        private string apellidoNombreCliente;
        private DateTime fechaHoraDevolucion;
        private DateTime fechaHoraRetiro;
        private int idVehiculoCiudad;
        private ServiceReferenceReservaVehiculos.LugarRetiroDevolucion lugarDevolucion;
        private ServiceReferenceReservaVehiculos.LugarRetiroDevolucion lugarRetiro;
        private string nroDocumentoCliente;

        public Reserva(string apellidoNombreCliente, DateTime fechaHoraDevolucion, DateTime fechaHoraRetiro, int idVehiculoCiudad, LugarRetiroDevolucion lugarDevolucion, LugarRetiroDevolucion lugarRetiro, string nroDocumentoCliente)
        {
            this.ApellidoNombreCliente = apellidoNombreCliente;
            this.FechaHoraDevolucion = fechaHoraDevolucion;
            this.FechaHoraRetiro = fechaHoraRetiro;
            this.IdVehiculoCiudad = idVehiculoCiudad;
            this.LugarDevolucion = lugarDevolucion;
            this.LugarRetiro = lugarRetiro;
            this.NroDocumentoCliente = nroDocumentoCliente;
        }

        public string ApellidoNombreCliente { get => apellidoNombreCliente; set => apellidoNombreCliente = value; }
        public DateTime FechaHoraDevolucion { get => fechaHoraDevolucion; set => fechaHoraDevolucion = value; }
        public DateTime FechaHoraRetiro { get => fechaHoraRetiro; set => fechaHoraRetiro = value; }
        public int IdVehiculoCiudad { get => idVehiculoCiudad; set => idVehiculoCiudad = value; }
        public LugarRetiroDevolucion LugarDevolucion { get => lugarDevolucion; set => lugarDevolucion = value; }
        public LugarRetiroDevolucion LugarRetiro { get => lugarRetiro; set => lugarRetiro = value; }
        public string NroDocumentoCliente { get => nroDocumentoCliente; set => nroDocumentoCliente = value; }
    }
}