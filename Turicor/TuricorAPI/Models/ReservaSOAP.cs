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
        private ServiceReferenceReservaVehiculos.LugarRetiroDevolucion lugarDevolucion;
        private ServiceReferenceReservaVehiculos.LugarRetiroDevolucion lugarRetiro;
        private string nroDocumentoCliente;

        public ReservaSOAP(string apellidoNombreCliente, DateTime fechaHoraDevolucion, DateTime fechaHoraRetiro, int idVehiculoCiudad, LugarRetiroDevolucion lugarDevolucion, LugarRetiroDevolucion lugarRetiro, string nroDocumentoCliente)
        {
            this.apellidoNombreCliente = apellidoNombreCliente;
            this.fechaHoraDevolucion = fechaHoraDevolucion;
            this.fechaHoraRetiro = fechaHoraRetiro;
            this.idVehiculoCiudad = idVehiculoCiudad;
            this.lugarDevolucion = lugarDevolucion;
            this.lugarRetiro = lugarRetiro;
            this.nroDocumentoCliente = nroDocumentoCliente;
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
        public LugarRetiroDevolucion getLugarDevolucion()
        {
            return this.lugarDevolucion;
        }
        public LugarRetiroDevolucion getLugarRetiro()
        {
            return this.lugarRetiro;
        }
        public string getNroDocumentoCliente()
        {
            return this.nroDocumentoCliente;
        }
    }
}