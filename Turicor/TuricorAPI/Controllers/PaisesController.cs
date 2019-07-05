using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TuricorAPI.Datos;

namespace TuricorAPI.Controllers
{
    public class PaisesController : ApiController
    {
        // GET: api/paises
        public ServiceReferenceReservaVehiculos.PaisEntity[] Get()
        {
            var datosPaises = new DatosPaises();
            return datosPaises.consultarPaises();
        }
    }
}
