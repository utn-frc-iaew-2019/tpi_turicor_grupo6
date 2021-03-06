﻿using System;
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
using TuricorAPI.Datos;
using TuricorAPI.Models;

namespace TuricorAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ReservasController : ApiController
    {
        //Genero instancia del modelo TuricorEntities (Cliente, Reserva, Vendedor)
        private TuricorEntities db = new TuricorEntities();

        //APIs de MODELO BD PROPIO

        // GET: api/Reservas

        //public IQueryable<Reserva> GetReservasLocales()
        public IQueryable<Reserva> GetReservas()
        {
            return db.Reservas;
        }

        // GET: api/Reservas/?dni=&idVendedor=
        public IQueryable<Reserva> GetReservas(int dni, int idVendedor)
        {
            if (dni != -1)
            {
                if (db.Clientes.Where(n => n.NroDocumento == dni).Count() > 0)
                {
                    int idCliente = db.Clientes.Where(n => n.NroDocumento == dni).First().Id;
                    return db.Reservas.Where(r => r.IdCliente == idCliente);
                }
                else { return null; };
            }
            else if (idVendedor != -1)
            {
            if (db.Vendedors.Where(n => n.Id == idVendedor).Count() > 0)
            {
                return db.Reservas.Where(r => r.IdVendedor == idVendedor);
            }
            else { return null; };
        }
            else { return null; }
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

        //public ServiceReferenceReservaVehiculos.ReservaEntity GetReservasSoap(string codigoReserva)
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
                if (!ReservaExistsByCodReserva(reserva.CodigoReserva))
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

        //REAL POST
        // POST: api/Reservas
        [ResponseType(typeof(Reserva))]
        //public IHttpActionResult PostReserva(ServiceReferenceReservaVehiculos.ReservaEntity reserva, int idCiudad, int idPais, int idVendedor)
        public IHttpActionResult PostReserva(ServiceReferenceReservaVehiculos.ReservaEntity reserva, int idCiudad, int idPais, int idVendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var datosReserva = new DatosReserva();
            //Copio los datos del parametro "reserva" en un objeto ReservaSOAP
            //Debo generar objeto JavaScript con esos parámetros en ReservaSOAP
            var reservaSOAP = new ReservaSOAP(reserva);

            //Creo la reserva en el Servicio SOAP y obtengo la respuesta actualizada en formato ReservaEntity, ya con parametros que agrega el mismo post
            //como ser codigo de reserva y fecha de reserva.  
            ServiceReferenceReservaVehiculos.ReservaEntity reservaCreada = datosReserva.reservarVehiculo(reservaSOAP);

            //Ya con una ReservaEntity ReservaSOAP en entidad de mi modelo Reserva
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
                db.SaveChanges();
            }
            

            reservaNueva.CodigoReserva = reservaCreada.CodigoReserva;
            reservaNueva.Costo = reserva.TotalReserva / (decimal) 1.2;
            reservaNueva.Estado = 1;
            reservaNueva.FechaReserva = reservaCreada.FechaReserva;
            reservaNueva.IdCiudad = idCiudad;
            reservaNueva.IdCliente = this.GetCliente(int.Parse(reserva.NroDocumentoCliente)).Id;
            reservaNueva.IdPais = idPais;
            reservaNueva.IdVehiculoCiudad = reservaCreada.VehiculoPorCiudadId;
            //no se lo pasa reservaCreada
            reservaNueva.IdVendedor = idVendedor;
            //lo toma del parametro original
            reservaNueva.PrecioVenta = reserva.TotalReserva;
            //obtiene nombre del vendedor
            reservaNueva.Vendedor = GetVendedor(idVendedor);


            db.Reservas.Add(reservaNueva);

            

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
           
            {

                if (ReservaExistsByCodReserva(reserva.CodigoReserva))

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


        private bool ReservaExistsByCodReserva(string codigoReserva)
        {
            return db.Reservas.Where(n => n.CodigoReserva == codigoReserva).Count() > 0;
        }

        private bool ClienteExistsByDNI(int dni)
        {
            return db.Clientes.Where(n => n.NroDocumento == dni).Count() > 0;
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