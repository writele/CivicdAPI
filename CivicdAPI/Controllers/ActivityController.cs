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
  [RoutePrefix("api/Activity")]
  public class ActivityController : ApiController
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    /// <summary>
    /// Get list of all activities in database.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    // GET: api/Activity
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

    // GET: api/Activity/5
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

    //// GET: api/Activity/
    //[Route("Category/{categoryName}")]
    //public IQueryable<ActivityDTO> GetCategory(string categoryName)
    //{
    //  var category = Enum.Parse(typeof(ActivityCategory), categoryName);

    //  var activities = from a in db.Activities
    //                   where a.Category == 
    //                   select new ActivityDTO()
    //                   {
    //                     Id = a.ID,
    //                     DisplayTitle = a.DisplayTitle,
    //                     Description = a.Description,
    //                     CategoryName = a.Category.ToString(),
    //                     PhotoURL = a.Photo,
    //                     StartTime = a.StartTime.ToString(),
    //                     EndTime = a.EndTime.ToString(),
    //                     StreetAddressOne = a.Address.StreetAddressOne,
    //                     StreetAddressTwo = a.Address.StreetAddressTwo,
    //                     City = a.Address.City,
    //                     State = a.Address.State,
    //                     Tags = from t in a.Tags
    //                            select new TagDTO()
    //                            {
    //                              Id = t.ID,
    //                              Name = t.Name
    //                            }
    //                   };
    //  return activities;
    //}

    // POST: api/Activity
    public void Post([FromBody]string value)
    {
    }

    // PUT: api/Activity/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE: api/Activity/5
    public void Delete(int id)
    {
    }
  }
}
