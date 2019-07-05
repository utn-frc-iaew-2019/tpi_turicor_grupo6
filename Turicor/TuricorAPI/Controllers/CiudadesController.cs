using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TuricorAPI.Datos;

namespace TuricorAPI.Controllers
{
    public class CiudadesController : ApiController
    {
        // GET: api/ciudades/{idPais}
        public ServiceReferenceReservaVehiculos.CiudadEntity[] Get(int id)
        {
            var datosPaises = new DatosPaises();
            return datosPaises.consultarCiudades(id);
        }
    }
}
