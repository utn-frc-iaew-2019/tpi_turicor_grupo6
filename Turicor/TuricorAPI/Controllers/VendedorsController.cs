using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using TuricorAPI.Models;

namespace TuricorAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VendedorsController : ApiController
    {
        private TuricorEntities db = new TuricorEntities();

        // GET: api/Vendedors
        public IQueryable<Vendedor> GetVendedors()
        {
            return db.Vendedors;
        }
        /*
        // GET: api/Vendedors/5
        [ResponseType(typeof(Vendedor))]
        public IHttpActionResult GetVendedor(int id)
        {
            Vendedor vendedor = db.Vendedors.Find(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return Ok(vendedor);
        }

        // PUT: api/Vendedors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendedor(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendedor.Id)
            {
                return BadRequest();
            }

            db.Entry(vendedor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendedorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Vendedors
        [ResponseType(typeof(Vendedor))]
        public IHttpActionResult PostVendedor(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vendedors.Add(vendedor);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VendedorExists(vendedor.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vendedor.Id }, vendedor);
        }

        // DELETE: api/Vendedors/5
        [ResponseType(typeof(Vendedor))]
        public IHttpActionResult DeleteVendedor(int id)
        {
            Vendedor vendedor = db.Vendedors.Find(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            db.Vendedors.Remove(vendedor);
            db.SaveChanges();

            return Ok(vendedor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendedorExists(int id)
        {
            return db.Vendedors.Count(e => e.Id == id) > 0;
        }
        */
    }
}