using System.Collections.Generic;
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
      // Roles
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

      // Users
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

      // Tags
      context.Tags.AddOrUpdate(
        p => p.Name,
            new Tag
            {
              Name = "Tag1",
            },
            new Tag
            {
              Name = "Tag2",
            },
            new Tag
            {
              Name = "Tag3",
            },
            new Tag
            {
              Name = "Tag4",
            }
         );

      // Addresses
      context.Addresses.AddOrUpdate(
        p => p.ID,
        new Address()
        {
          StreetAddressOne = "101 Main Street",
          City = "Charlotte",
          State = "NC",
          ZipCode = "28215"
        },
        new Address()
        {
          StreetAddressOne = "2222 Ordinary Way",
          City = "Charlotte",
          State = "NC",
          ZipCode = "28277"
        }
          );

      // Activities
      context.Activities.AddOrUpdate(
        p => p.DisplayTitle,
        new Activity
        {
          DisplayTitle = "City Council Meeting",
          Description =
            "Lorem ipsum dolor sit amet, eripuit lobortis sapientem pri no, ut sed delenit honestatis. An diceret copiosae pri, ius quas possit ea. Id pri partiendo salutatus disputando. Id nam minim minimum repudiare, ex harum commune interesset usu. Semper dissentiunt eum in. Simul graeco tacimates ius in.",
          Category = ActivityCategory.Government,
          StartTime = new DateTimeOffset(2018, 05, 03, 13, 30, 00, new TimeSpan(0, 0, 0)),
          EndTime = new DateTimeOffset(2018, 05, 03, 14, 30, 00, new TimeSpan(0, 0, 0))
        },
        new Activity
        {
          DisplayTitle = "Phone Bank",
          Description =
            "Lorem ipsum dolor sit amet, eripuit lobortis sapientem pri no, ut sed delenit honestatis. An diceret copiosae pri, ius quas possit ea. Id pri partiendo salutatus disputando. Id nam minim minimum repudiare, ex harum commune interesset usu. Semper dissentiunt eum in. Simul graeco tacimates ius in.",
          Category = ActivityCategory.IndependentActivity,
          StartTime = new DateTimeOffset(2018, 05, 06, 16, 00, 00, new TimeSpan(0, 0, 0)),
          EndTime = new DateTimeOffset(2018, 05, 06, 17, 00, 00, new TimeSpan(0, 0, 0))
        },
        new Activity
        {
          DisplayTitle = "School Meeting",
          Description = "Lorem ipsum dolor sit amet, eripuit lobortis sapientem pri no, ut sed delenit honestatis. An diceret copiosae pri, ius quas possit ea. Id pri partiendo salutatus disputando. Id nam minim minimum repudiare, ex harum commune interesset usu. Semper dissentiunt eum in. Simul graeco tacimates ius in.",
          Category = ActivityCategory.School,
          StartTime = new DateTimeOffset(2018, 05, 08, 10, 30, 00, new TimeSpan(0, 0, 0)),
          EndTime = new DateTimeOffset(2018, 05, 08, 11, 30, 00, new TimeSpan(0, 0, 0))
        }
        );

      context.SaveChanges();

      // Add Tags to Activities
      var activity1 = context.Activities.Find(1);
      var activity2 = context.Activities.Find(2);
      var activity3 = context.Activities.Find(3);
      var tag1 = context.Tags.Find(1);
      var tag2 = context.Tags.Find(2);
      var tag3 = context.Tags.Find(3);
      var tag4 = context.Tags.Find(4);

      activity1.Tags.Add(tag1);
      activity1.Tags.Add(tag2);
      activity2.Tags.Add(tag3);
      activity3.Tags.Add(tag3);
      activity3.Tags.Add(tag1);
      activity3.Tags.Add(tag2);
      activity3.Tags.Add(tag4);

      // Add Address to Activity
      var address1 = context.Addresses.Find(1);
      var address2 = context.Addresses.Find(2);
      activity1.Address = address1;
      activity2.Address = address2;

      context.SaveChanges();

    }
  }
}
