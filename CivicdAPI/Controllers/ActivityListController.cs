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
    [Authorize]
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

        [Route("tags/{tagName}")]
        public IEnumerable<ActivityDTO> GetByTag(string tagName)
        {
            var tag = db.Tags.Include("Activities").FirstOrDefault(t => t.Name == tagName);
            if (tag == null)
            {
                throw new Exception("No matching tags found");
            }

            return tag.Activities.Select(act => new ActivityDTO
            {
                AddressDisplayName = act.Address.Name,
                CategoryName = Enum.GetName(typeof(ActivityCategory), act.Category),
                City = act.Address.City,
                Description = act.Description,
                DisplayTitle = act.DisplayTitle,
                EndTime = act.EndTime.ToString(),
                Id = act.ID,
                PhotoURL = act.Photo,
                StartTime = act.StartTime.ToString(),
                State = act.Address.State,
                StreetAddressOne = act.Address.StreetAddressOne,
                StreetAddressTwo = act.Address.StreetAddressTwo,
                ZipCode = act.Address.ZipCode,
                Tags = act.Tags.Select(t => new TagDTO
                {
                    Id = t.ID,
                    Name = t.Name
                })
            });
        }

        public IQueryable<ActivityDTO> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return db.Activities
                .Include("Tags")
                .Where(act => act.StartTime <= startDate && act.EndTime >= endDate)
                .Select(act => new ActivityDTO
                {
                    AddressDisplayName = act.Address.Name,
                    CategoryName = Enum.GetName(typeof(ActivityCategory), act.Category),
                    City = act.Address.City,
                    Description = act.Description,
                    DisplayTitle = act.DisplayTitle,
                    EndTime = act.EndTime.ToString(),
                    Id = act.ID,
                    PhotoURL = act.Photo,
                    StartTime = act.StartTime.ToString(),
                    State = act.Address.State,
                    StreetAddressOne = act.Address.StreetAddressOne,
                    StreetAddressTwo = act.Address.StreetAddressTwo,
                    ZipCode = act.Address.ZipCode,
                    Tags = act.Tags.Select(t => new TagDTO
                    {
                        Id = t.ID,
                        Name = t.Name
                    })
                });
        }

        // GET: api/Activities/
        [Route("categories/{categoryName}")]
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

        [Route("organization/{organizationUserName}")]
        public IQueryable<ActivityDTO> GetByOrganization(string organizationUserName)
        {
            var activities = db.UserActivities
                .Include("Activity")
                .Include("User")
                .Where(ua => ua.User.UserName == organizationUserName)
                .Select(ua => ua.Activity);

            return activities.Select(act => new ActivityDTO
            {
                AddressDisplayName = act.Address.Name,
                CategoryName = Enum.GetName(typeof(ActivityCategory), act.Category),
                City = act.Address.City,
                Description = act.Description,
                DisplayTitle = act.DisplayTitle,
                EndTime = act.EndTime.ToString(),
                Id = act.ID,
                PhotoURL = act.Photo,
                StartTime = act.StartTime.ToString(),
                State = act.Address.State,
                StreetAddressOne = act.Address.StreetAddressOne,
                StreetAddressTwo = act.Address.StreetAddressTwo,
                ZipCode = act.Address.ZipCode,
                Tags = act.Tags.Select(t => new TagDTO
                {
                    Id = t.ID,
                    Name = t.Name
                })
            });
        }
    }
}