using CivicdAPI.Models;
using Microsoft.AspNet.Identity;

namespace CivicdAPI.Migrations
{
  using Microsoft.AspNet.Identity.EntityFramework;
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
      var roleManager = new RoleManager<IdentityRole>(
        new RoleStore<IdentityRole>(context));
      if (!context.Roles.Any(r => r.Name == "Admin"))
      {
        roleManager.Create(new IdentityRole { Name = "Admin" });
      }
      if (!context.Roles.Any(r => r.Name == "User"))
      {
        roleManager.Create(new IdentityRole { Name = "User" });
      }
      if (!context.Roles.Any(r => r.Name == "Organization"))
      {
        roleManager.Create(new IdentityRole { Name = "Organization" });
      }
      var userManager = new UserManager<ApplicationUser>(
        new UserStore<ApplicationUser>(context));
      if (!context.Users.Any(u => u.Email == "getcivicd@gmail.com"))
      {
        userManager.Create(new ApplicationUser
        {
          UserName = "getcivicd@gmail.com",
          Email = "getcivicd@gmail.com",
          FirstName = "Civicd",
          LastName = "API",
        }, "password1!");
      }

      var userId = userManager.FindByEmail("getcivicd@gmail.com").Id;
      userManager.AddToRole(userId, "Admin");

      context.Activities.AddOrUpdate(
        p => p.DisplayTitle,
            new Activity
            {
              DisplayTitle = "City Council Meeting",
              Description = "Lorem ipsum dolor sit amet, eripuit lobortis sapientem pri no, ut sed delenit honestatis. An diceret copiosae pri, ius quas possit ea. Id pri partiendo salutatus disputando. Id nam minim minimum repudiare, ex harum commune interesset usu. Semper dissentiunt eum in. Simul graeco tacimates ius in.",
              Category = ActivityCategory.Category1
            },
              new Activity
              {
                DisplayTitle = "Phone Bank",
                Description = "Lorem ipsum dolor sit amet, eripuit lobortis sapientem pri no, ut sed delenit honestatis. An diceret copiosae pri, ius quas possit ea. Id pri partiendo salutatus disputando. Id nam minim minimum repudiare, ex harum commune interesset usu. Semper dissentiunt eum in. Simul graeco tacimates ius in.",
                Category = ActivityCategory.Category2
              }
          );
    }
  }
}
