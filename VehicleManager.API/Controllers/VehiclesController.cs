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
    public class VehiclesController : ApiController
    {
        private VehicleManagerDataContext db = new VehicleManagerDataContext();

        // GET: api/Vehicles
        public IHttpActionResult GetVehicles()
        {
            var resultSet = db.Vehicles.Select(vehicle => new
            {
                vehicle.VehicleId,
                vehicle.Make,
                vehicle.Model,
                vehicle.Year,
                vehicle.Color,
                vehicle.VehicleType,
                vehicle.RetailPrice 
            });
            return Ok(resultSet);
        }

        // GET: api/Vehicles/5
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult GetVehicle(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                vehicle.VehicleId,
                vehicle.Make,
                vehicle.Model,
                vehicle.Year,
                vehicle.Color,
                vehicle.VehicleType,
                vehicle.RetailPrice
            });
        }

        // PUT: api/Vehicles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVehicle(int id, Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicle.VehicleId)
            {
                return BadRequest();
            }

            var dbVehicle = db.Vehicles.Find(id);
            dbVehicle.VehicleId = vehicle.VehicleId;
            dbVehicle.Make = vehicle.Make;
            dbVehicle.Model = vehicle.Model;
            dbVehicle.Year = vehicle.Year;
            dbVehicle.Color = vehicle.Color;
            dbVehicle.VehicleType = vehicle.VehicleType;
            dbVehicle.RetailPrice = vehicle.RetailPrice;

            db.Entry(dbVehicle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // POST: api/Vehicles
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult PostVehicle(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vehicle.VehicleId }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult DeleteVehicle(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            db.Vehicles.Remove(vehicle);
            db.SaveChanges();

            return Ok(new
            {
                vehicle.VehicleId,
                vehicle.Make,
                vehicle.Model,
                vehicle.Year,
                vehicle.Color,
                vehicle.VehicleType,
                vehicle.RetailPrice
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

        private bool VehicleExists(int id)
        {
            return db.Vehicles.Count(e => e.VehicleId == id) > 0;
        }
    }
}