using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TuricorAPI.Datos;
using TuricorAPI.Models;

namespace TuricorAPI.Controllers
{
    public class ReservasController : ApiController
    {
        private TuricorEntities db = new TuricorEntities();

        // GET: api/Reservas
        public IQueryable<Reserva> GetReservas()
        {
            return db.Reservas;
        }

        /*// GET: api/Reservas/5
        [ResponseType(typeof(Reserva))]
        public IHttpActionResult GetReserva(int codigoReserva)
        {
            Reserva reserva = db.Reservas.Find(codigoReserva);
            if (reserva == null)
            {
                return NotFound();
            }

            return Ok(reserva);
        }*/

        // GET: api/Reservas/5

        public ServiceReferenceReservaVehiculos.ReservaEntity GetReserva(string codigoReserva)
        {
            var datosReserva = new DatosReserva();
            return datosReserva.consultarReserva(codigoReserva);           
        }


        // PUT: api/Reservas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReserva(ServiceReferenceReservaVehiculos.ReservaEntity reservaSOAP)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Reserva reserva = new Reserva();
            reserva.CodigoReserva = reservaSOAP.CodigoReserva;
            reserva.Costo = reservaSOAP.TotalReserva;
            reserva.FechaReserva = reservaSOAP.FechaReserva;
            reserva.Id = reservaSOAP.Id;
            reserva.IdCiudad = db.Reservas.Find(reservaSOAP.Id).IdCiudad;
            reserva.IdCliente = db.Reservas.Find(reservaSOAP.Id).IdCliente;
            reserva.IdPais = db.Reservas.Find(reservaSOAP.Id).IdPais;
            reserva.IdVehiculoCiudad = reservaSOAP.VehiculoPorCiudadId;
            reserva.IdVendedor = db.Reservas.Find(reservaSOAP.Id).IdVendedor;
            reserva.PrecioVenta = db.Reservas.Find(reservaSOAP.Id).PrecioVenta;
            reserva.Estado = 0;
            db.Entry(reserva).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(reservaSOAP.CodigoReserva))
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

        /*// POST: api/Reservas
        [ResponseType(typeof(Reserva))]
        public IHttpActionResult PostReserva(ServiceReferenceReservaVehiculos.ReservaEntity reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reservas.Add(reserva);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReservaExists(reserva.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = reserva.Id }, reserva);
        }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservaExists(string codigoReserva)
        {
            return db.Reservas.Find(codigoReserva) != null;
        }
    }
}