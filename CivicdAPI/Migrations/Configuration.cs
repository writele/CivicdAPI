using CivicdAPI.Models;

namespace CivicdAPI.Migrations
{
  using System;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;

  internal sealed class Configuration : DbMigrationsConfiguration<CivicdAPI.Models.ApplicationDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(CivicdAPI.Models.ApplicationDbContext context)
    {
      //  This method will be called after migrating to the latest version.

      //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
      //  to avoid creating duplicate seed data. E.g.
      //
      //    context.People.AddOrUpdate(
      //      p => p.FullName,
      //      new Person { FullName = "Andrew Peters" },
      //      new Person { FullName = "Brice Lambson" },
      //      new Person { FullName = "Rowan Miller" }
      //    );
      //
      context.Activities.AddOrUpdate(
        p => p.DisplayTitle,
            new Activity
            {
              DisplayTitle = "City Council Meeting",
              Description = "Lorem ipsum dolor sit amet, eripuit lobortis sapientem pri no, ut sed delenit honestatis. An diceret copiosae pri, ius quas possit ea. Id pri partiendo salutatus disputando. Id nam minim minimum repudiare, ex harum commune interesset usu. Semper dissentiunt eum in. Simul graeco tacimates ius in."
            },
              new Activity
              {
                DisplayTitle = "Phone Bank",
                Description = "Lorem ipsum dolor sit amet, eripuit lobortis sapientem pri no, ut sed delenit honestatis. An diceret copiosae pri, ius quas possit ea. Id pri partiendo salutatus disputando. Id nam minim minimum repudiare, ex harum commune interesset usu. Semper dissentiunt eum in. Simul graeco tacimates ius in."
              }
          );
    }
  }
}
