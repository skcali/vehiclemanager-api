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
using VehicleManager.API.Data;
using VehicleManager.API.Models;

namespace VehicleManager.API.Controllers
{
    public class SalesController : ApiController
    {
        private VehicleManagerDataContext db = new VehicleManagerDataContext();

        // GET: api/Sales
        public IHttpActionResult GetSales()
        {
            var resultSet = db.Sales.Select(sale => new
            {
                sale.SaleId,
                sale.VehicleId,
                sale.CustomerId,
                sale.SalePrice,
                sale.InvoiceDate,
                sale.PaymentReceivedDate,
                CustomerName = sale.Customer.FirstName + " " + sale.Customer.LastName,
                VehicleName = sale.Vehicle.Year + " " + sale.Vehicle.Make + " " + sale.Vehicle.Model
            });
            return Ok(resultSet);
        }

        // GET: api/Sales/5
        [ResponseType(typeof(Sale))]
        public IHttpActionResult GetSale(int id)
        {
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                sale.SaleId,
                sale.VehicleId,
                sale.CustomerId,
                sale.SalePrice,
                sale.InvoiceDate,
                sale.PaymentReceivedDate,
            });
        }

        // PUT: api/Sales/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSale(int id, Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sale.SaleId)
            {
                return BadRequest();
            }


            var dbSale = db.Sales.Find(id);
            dbSale.SaleId = sale.SaleId;
            dbSale.VehicleId = sale.VehicleId;
            dbSale.CustomerId = sale.CustomerId;
            dbSale.SalePrice = sale.SalePrice;
            dbSale.InvoiceDate = sale.InvoiceDate;
            dbSale.PaymentReceivedDate = sale.PaymentReceivedDate;

            db.Entry(dbSale).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
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

        // POST: api/Sales
        [ResponseType(typeof(Sale))]
        public IHttpActionResult PostSale(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sales.Add(sale);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sale.SaleId }, sale);
        }

        // DELETE: api/Sales/5
        [ResponseType(typeof(Sale))]
        public IHttpActionResult DeleteSale(int id)
        {
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return NotFound();
            }

            db.Sales.Remove(sale);
            db.SaveChanges();

            return Ok(new
            {
                sale.SaleId,
                sale.VehicleId,
                sale.CustomerId,
                sale.SalePrice,
                sale.InvoiceDate,
                sale.PaymentReceivedDate,
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SaleExists(int id)
        {
            return db.Sales.Count(e => e.SaleId == id) > 0;
        }
    }
}