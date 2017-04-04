namespace VehicleManager.API.Migrations
{
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
            
        }
    }
}
