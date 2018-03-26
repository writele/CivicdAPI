using CivicdAPI.Controllers.Helpers;
using CivicdAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CivicdAPI.Controllers
{
  [Authorize]
  [RoutePrefix("api")]
  public class ActivityActionController : ApiController
  {
    [HttpPost]
    [Route("activities/{activityId:int}/rsvps/{userName}/")]
    public IHttpActionResult RSVP(int activityId, string userName)
    {

      using (var context = new ApplicationDbContext())
      {
        var rsvp = new UserActivity();
        var activity = context.Activities.FirstOrDefault(a => a.ID == activityId);
        var user = context.Users.FirstOrDefault(u => u.UserName == userName);
        if (user == null)
        {
          throw new Exception("Invalid Username");
        }
        if (activity == null)
        {
          throw new Exception("Invalid Activity Id");
        }

        rsvp.Activity = activity;
        rsvp.User = user;

        context.UserActivities.Add(rsvp);
        context.SaveChanges();

        return Ok();
      }
    }

    [HttpPost]
    [Route("activities/{activityId:int}/checkins/{userName}/")]
    public IHttpActionResult CheckInActivity(int activityId, string userName)
    {
      using (var context = new ApplicationDbContext())
      {
        var user = context.Users.FirstOrDefault(u => u.UserName == userName);
        if (user == null)
        {
          throw new Exception("Invalid Username");
        }

        var activity = context.UserActivities.FirstOrDefault(ua => ua.ActivityID == activityId && ua.User.Id == user.Id);
        if (activity == null)
        {
          throw new Exception("Invalid Activity or User is Not RSVP'd for this Activity");
        }

        activity.CheckedIn = true;
        context.SaveChanges();

        return Ok();
      }
    }

    [HttpPost]
    [Route("activities")]
    public ActivityViewModel CreateActivity(ActivityViewModel activity)
    {
      using (var context = new ApplicationDbContext())
      {
        var user = context.Users.Single(u => u.Id == User.Identity.GetUserId());
        var organization = context.Users.Single(u => u.Email == activity.Organization.Email);

        var userIsAdmin = User.IsInRole("Admin");
        var userIsOrg = User.IsInRole("Organization");
        if (User.IsInRole("User") || (userIsOrg && user.Id != organization.Id))
        {
          throw new HttpResponseException(HttpStatusCode.Forbidden);
        }

        var activityEntity = new Activity()
        {
          DisplayTitle = activity.DisplayTitle,
          Category = activity.CategoryName,
          StartTime = activity.StartTime,
          EndTime = activity.EndTime
        };

        if (!String.IsNullOrEmpty(activity.StreetAddressOne))
        {
          activityEntity.Address = new Address()
          {
            StreetAddressOne = activity.StreetAddressOne,
            StreetAddressTwo = activity.StreetAddressTwo,
            City = activity.City,
            State = activity.State,
            ZipCode = activity.ZipCode,
            Name = activity.AddressDisplayName
          };
        }

        var userActivity = new UserActivity()
        {
          Activity = activityEntity,
          User = user,
          Host = !userIsAdmin,
          CheckedIn = organization.Id == user.Id
        };

        context.Activities.Add(activityEntity);
        context.UserActivities.Add(userActivity);

        context.SaveChanges();
        activity.Id = activityEntity.ID;

        return activity;
      }
    }

    [HttpPut]
    [Route("activities/{activityId:int}")]
    public ActivityViewModel UpdateActivity(int activityId, ActivityViewModel activity)
    {
      using (var context = new ApplicationDbContext())
      {
        var user = context.Users.Single(u => u.Id == User.Identity.GetUserId());
        var organization = context.Users.Single(u => u.Email == activity.Organization.Email);

        var userIsAdmin = User.IsInRole("Admin");
        var userIsOrg = User.IsInRole("Organization");
        if (User.IsInRole("User") || (userIsOrg && user.Id != organization.Id))
        {
          throw new HttpResponseException(HttpStatusCode.Forbidden);
        }

        var activityEntity = context.UserActivities.FirstOrDefault(a => a.ActivityID == activityId && user.Id == a.UserID).Activity;
        if (activityEntity == null)
        {
          throw new Exception("Unable to Find Matching Activity");
        }

        activityEntity.DisplayTitle = activity.DisplayTitle;
        activityEntity.Category = activity.CategoryName;
        activityEntity.StartTime = activity.StartTime;
        activityEntity.EndTime = activity.EndTime;
        if (!String.IsNullOrEmpty(activity.StreetAddressOne))
        {
          if (activityEntity.Address == null)
          {
            activityEntity.Address = new Address()
            {
              StreetAddressOne = activity.StreetAddressOne,
              StreetAddressTwo = activity.StreetAddressTwo,
              City = activity.City,
              State = activity.State,
              ZipCode = activity.ZipCode,
              Name = activity.AddressDisplayName
            };
          }
          else
          {
            activityEntity.Address.StreetAddressOne = activity.StreetAddressOne;
            activityEntity.Address.StreetAddressTwo = activity.StreetAddressTwo;
            activityEntity.Address.City = activity.City;
            activityEntity.Address.State = activity.State;
            activityEntity.Address.ZipCode = activity.ZipCode;
            activityEntity.Address.Name = activity.AddressDisplayName;
          }
        }

        context.SaveChanges();
        return activity;
      }
    }

    [HttpDelete]
    [Route("activities/{activityId:int}")]
    public bool DeleteActivity(int activityId)
    {
      using (var context = new ApplicationDbContext())
      {
        var user = context.Users.Single(u => u.Id == User.Identity.GetUserId());
        var activityEntity = context.UserActivities.Include("Activities").FirstOrDefault(a => a.ActivityID == activityId);

        var organization = context.Users.Single(u => u.Email == activityEntity.User.Email);

        var userIsAdmin = User.IsInRole("Admin");
        var userIsOrg = User.IsInRole("Organization");
        if (User.IsInRole("User") || (userIsOrg && user.Id != organization.Id))
        {
          throw new HttpResponseException(HttpStatusCode.Forbidden);
        }
        if (activityEntity == null)
        {
          throw new Exception("Unable to Find Matching Activity");
        }

        context.Activities.Remove(activityEntity.Activity);
        context.UserActivities.Remove(activityEntity);

        context.SaveChanges();
        return true;
      }
    }
  }
}
