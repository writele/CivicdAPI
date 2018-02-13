using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CivicdAPI.Models;
using CivicdAPI.Models.DTO;
using Newtonsoft.Json;

namespace CivicdAPI.Controllers
{
  [RoutePrefix("api/Activities")]
  public class ActivityListController : ApiController
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    /// <summary>
    /// Get list of all activities in database.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    // GET: api/Activities
    public IQueryable<ActivityDTO> Get()
    {

      var activities = from a in db.Activities
                       select new ActivityDTO()
                       {
                         Id = a.ID,
                         DisplayTitle = a.DisplayTitle,
                         Description = a.Description,
                         CategoryName = a.Category.ToString(),
                         PhotoURL = a.Photo,
                         StartTime = a.StartTime.ToString(),
                         EndTime = a.EndTime.ToString(),
                         AddressDisplayName = a.Address.Name,
                         StreetAddressOne = a.Address.StreetAddressOne,
                         StreetAddressTwo = a.Address.StreetAddressTwo,
                         City = a.Address.City,
                         State = a.Address.State,
                         ZipCode = a.Address.ZipCode,
                         Tags = from t in a.Tags
                                select new TagDTO()
                                {
                                  Id = t.ID,
                                  Name = t.Name
                                }
                       };
      return activities;
    }

    // GET: api/Activities/5
    public IQueryable<ActivityDTO> Get(int id)
    {
      var activities = from a in db.Activities
                       where a.ID == id
                       select new ActivityDTO()
                       {
                         Id = a.ID,
                         DisplayTitle = a.DisplayTitle,
                         Description = a.Description,
                         CategoryName = a.Category.ToString(),
                         PhotoURL = a.Photo,
                         StartTime = a.StartTime.ToString(),
                         EndTime = a.EndTime.ToString(),
                         AddressDisplayName = a.Address.Name,
                         StreetAddressOne = a.Address.StreetAddressOne,
                         StreetAddressTwo = a.Address.StreetAddressTwo,
                         City = a.Address.City,
                         State = a.Address.State,
                         ZipCode = a.Address.ZipCode,
                         Tags = from t in a.Tags
                                select new TagDTO()
                                {
                                  Id = t.ID,
                                  Name = t.Name
                                }
                       };
      return activities;
    }
public IQueryable<ActivityDTO> GetTime(int activityTime)
{
    DateTime RightNow = DateTime.Now;
    var activities = from a in db.Activities
                        where (a.Begins <= RightNow) && (a.Expires >= RightNow)
                        select new ActivityDTO()
                        {
                            Id = a.ID,
                            DisplayTitle = a.DisplayTitle,
                            Description = a.Description,
                            CategoryName = a.Category.ToString(),
                            PhotoURL = a.Photo,
                            StartTime = a.StartTime.ToString(),
                            EndTime = a.EndTime.ToString(),
                            AddressDisplayName = a.Address.Name,
                            StreetAddressOne = a.Address.StreetAddressOne,
                            StreetAddressTwo = a.Address.StreetAddressTwo,
                            City = a.Address.City,
                            State = a.Address.State,
                            ZipCode = a.Address.ZipCode,
                            Tags = from t in a.Tags
                                select new TagDTO()
                                {
                                    Id = t.ID,
                                    Name = t.Name
                                }
                        };
    return activities;
}


        // GET: api/Activities/
        [Route("Category/{categoryName}")]
    public IQueryable<ActivityDTO> GetCategory(string categoryName)
    {
      var categoryInt = (ActivityCategory)Enum.Parse(typeof(ActivityCategory), categoryName, true);
      var categoryEnum = (ActivityCategory)Enum.ToObject(typeof(ActivityCategory), categoryInt);

      var activities = from a in db.Activities
                       where a.Category == categoryEnum
                       select new ActivityDTO()
                       {
                         Id = a.ID,
                         DisplayTitle = a.DisplayTitle,
                         Description = a.Description,
                         CategoryName = a.Category.ToString(),
                         PhotoURL = a.Photo,
                         StartTime = a.StartTime.ToString(),
                         EndTime = a.EndTime.ToString(),
                         StreetAddressOne = a.Address.StreetAddressOne,
                         StreetAddressTwo = a.Address.StreetAddressTwo,
                         City = a.Address.City,
                         State = a.Address.State,
                         Tags = from t in a.Tags
                                select new TagDTO()
                                {
                                  Id = t.ID,
                                  Name = t.Name
                                }
                       };
      return activities;
    }

  }
}