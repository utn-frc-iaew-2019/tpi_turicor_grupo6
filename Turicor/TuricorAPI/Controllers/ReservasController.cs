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
            ServiceReferenceReservaVehiculos.ReservaEntity reservaCancelada = datosReserva.cancelarReserva(reservaSOAP);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Reservas
        [ResponseType(typeof(Reserva))]
        public IHttpActionResult PostReserva(ServiceReferenceReservaVehiculos.ReservaEntity reserva, int idCiudad, int idPais, int idVendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var datosReserva = new DatosReserva();
            var reservaSOAP = new ReservaSOAP(reserva);
            ServiceReferenceReservaVehiculos.ReservaEntity reservaCreada = datosReserva.reservarVehiculo(reservaSOAP);

            var reservaNueva = new Reserva();
            if (this.ClienteExistsByDNI(int.Parse(reserva.NroDocumentoCliente)))
            {
                reservaNueva.Cliente = this.GetCliente(int.Parse(reserva.NroDocumentoCliente));
            }
            else {
                var clienteNuevo = new Cliente();
                clienteNuevo.Apellido = reserva.ApellidoNombreCliente.Split(',')[0];
                clienteNuevo.Nombre = reserva.ApellidoNombreCliente.Split(',')[1];
                clienteNuevo.NroDocumento = int.Parse(reserva.NroDocumentoCliente);
                db.Clientes.Add(clienteNuevo);
            }
            

            reservaNueva.CodigoReserva = reservaCreada.CodigoReserva;
            reservaNueva.Costo = reserva.TotalReserva;
            reservaNueva.Estado = 1;
            reservaNueva.FechaReserva = reservaCreada.FechaReserva;
            reservaNueva.IdCiudad = idCiudad;
            reservaNueva.IdCliente = int.Parse(reserva.NroDocumentoCliente);
            reservaNueva.IdPais = idPais;
            reservaNueva.IdVehiculoCiudad = reservaCreada.VehiculoPorCiudadId;
            reservaNueva.IdVendedor = idVendedor;
            reservaNueva.PrecioVenta = reserva.TotalReserva * (decimal) 1.2;
            reservaNueva.Vendedor = GetVendedor(idVendedor);


            db.Reservas.Add(reservaNueva);

            

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReservaExists(reserva.CodigoReserva))
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

        private bool ClienteExistsByDNI(int dni)
        {
            return db.Clientes.Where(n => n.NroDocumento == dni) != null;
        }

        private Cliente GetCliente(int dni)
        {
            return db.Clientes.Where(n => n.NroDocumento == dni).First();
        }

        private Vendedor GetVendedor(int id)
        {
            return db.Vendedors.Find(id);
        }
    }
}