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
