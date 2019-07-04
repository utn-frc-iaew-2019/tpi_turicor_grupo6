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
        public IHttpActionResult PutReserva(ServiceReferenceReservaVehiculos.ReservaEntity reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Reserva reservaModificada = new Reserva();
            reservaModificada.CodigoReserva = reserva.CodigoReserva;
            reservaModificada.Costo = reserva.TotalReserva;
            reservaModificada.FechaReserva = reserva.FechaReserva;
            reservaModificada.Id = reserva.Id;
            reservaModificada.IdCiudad = db.Reservas.Find(reserva.Id).IdCiudad;
            reservaModificada.IdCliente = db.Reservas.Find(reserva.Id).IdCliente;
            reservaModificada.IdPais = db.Reservas.Find(reserva.Id).IdPais;
            reservaModificada.IdVehiculoCiudad = reserva.VehiculoPorCiudadId;
            reservaModificada.IdVendedor = db.Reservas.Find(reserva.Id).IdVendedor;
            reservaModificada.PrecioVenta = db.Reservas.Find(reserva.Id).PrecioVenta;
            reservaModificada.Estado = 0;
            reservaModificada.Cliente = db.Reservas.Find(reserva.Id).Cliente;
            reservaModificada.Vendedor = db.Reservas.Find(reserva.Id).Vendedor;
            db.Entry(reservaModificada).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(reserva.CodigoReserva))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            var datosReserva = new DatosReserva();
            var reservaSOAP = new ReservaSOAP(reserva);
            datosReserva.cancelarReserva(reservaSOAP);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Reservas
        [ResponseType(typeof(Reserva))]
        public IHttpActionResult PostReserva(ServiceReferenceReservaVehiculos.ReservaEntity reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var reservaNueva = new Reserva();
            //reservaNueva.Cliente = reserva.ApellidoNombreCliente;

            //db.Reservas.Add(reserva);

            var datosReserva = new DatosReserva();
            var reservaSOAP = new ReservaSOAP(reserva);
            datosReserva.reservarVehiculo(reservaSOAP);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
           
            {
                if (ReservaExists(reserva.Id.ToString()))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = reserva.Id }, reserva);
        }

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