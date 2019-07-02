using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TuricorAPI.Controllers
{
    public class VehiculoController : ApiController
    {
        // GET: api/Vehiculo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Vehiculo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Vehiculo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Vehiculo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Vehiculo/5
        public void Delete(int id)
        {
        }
    }
}
