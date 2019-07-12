using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TuricorAPI.Datos;

namespace TuricorAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VehiculosController : ApiController
    {
        // GET: api/vehiculos/?idCiudad=&fechaRetiro=&fechaDevolucion=
        public ServiceReferenceReservaVehiculos.VehiculoModel[] Get(int idCiudad, string fechaRetiro, string fechaDevolucion)
        {
            var datosVehiculos = new DatosVehiculos();
            return datosVehiculos.consultarVehiculosDisponibles(idCiudad, DateTime.Parse(fechaRetiro), DateTime.Parse(fechaDevolucion));
        }
    }
}
