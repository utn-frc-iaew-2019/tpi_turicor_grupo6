using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TuricorAPI.Datos;

namespace TuricorAPI.Controllers
{
    public class VehiculosController : ApiController
    {
        // GET: api/Vehiculo/5
        public ServiceReferenceReservaVehiculos.VehiculoModel[] Get(int idCiudad, string fechaRetiro, string fechaDevolucion)
        {
            var datosVehiculos = new DatosVehiculos();
            return datosVehiculos.consultarVehiculosDisponibles(idCiudad, DateTime.Parse(fechaRetiro), DateTime.Parse(fechaDevolucion));
        }

       


    }
}
