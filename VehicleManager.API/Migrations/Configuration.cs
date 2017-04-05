namespace VehicleManager.API.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VehicleManager.API.Data.VehicleManagerDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(VehicleManager.API.Data.VehicleManagerDataContext context)
        {
            string[] colors = new string[] { "Green", "Red", "Yellow", "Hot Pink" };
            string[] makes = new string[] { "Honda", "Toyota", "Ford", "Wilby" };
            string[] models = new string[] { "Mustang", "Civic", "Loucks", "Sentra" };
            string[] vehicleTypes = new string[] { "Wambam", "Sedan", "SUV" };

            if(context.Customers.Count() == 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    context.Customers.Add(new Models.Customer
                    {
                        EmailAddress = Faker.InternetFaker.Email(),
                        DateOfBirth = Faker.DateTimeFaker.BirthDay(),
                        FirstName = Faker.NameFaker.FirstName(),
                        LastName = Faker.NameFaker.LastName(),
                        Telephone = Faker.PhoneFaker.Phone()
                    });
                }
                context.SaveChanges();
            }

            if(context.Vehicles.Count() == 0)
            {
                for(int i = 0; i < 20; i++)
                {
                    context.Vehicles.Add(new Models.Vehicle
                    {
                        Make = Faker.ArrayFaker.SelectFrom(makes),
                        Model = Faker.ArrayFaker.SelectFrom(models),
                        Color = Faker.ArrayFaker.SelectFrom(colors),
                        RetailPrice = Faker.NumberFaker.Number(15000, 364000),
                        VehicleType = Faker.ArrayFaker.SelectFrom(vehicleTypes),
                        Year = Faker.DateTimeFaker.DateTime().Year
                    });
                }
                context.SaveChanges();
            }

            if(context.Sales.Count() == 0)
            {
                for(int i = 0; i < 20; i++)
                {
                    var vehicle = context.Vehicles.Find(Faker.NumberFaker.Number(1, 20));
                    var invoiceDate = Faker.DateTimeFaker.DateTime();

                    context.Sales.Add(new Sale
                    {
                        Customer = context.Customers.Find(Faker.NumberFaker.Number(1, 20)),
                        Vehicle = vehicle,
                        InvoiceDate = invoiceDate,
                        SalePrice = vehicle.RetailPrice,
                        PaymentReceivedDate = invoiceDate.AddDays(Faker.NumberFaker.Number(1, 14)),
                    });
                }
                context.SaveChanges();
            }
            
        }
    }
}
