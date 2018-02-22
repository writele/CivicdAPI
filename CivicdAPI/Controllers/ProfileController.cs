using CivicdAPI.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CivicdAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api")]
    public class ProfileController : ApiController
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public ProfileController()
        {
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        [Route("User/{userEmail}")]
        public async Task<UserViewModel> GetUserByEmail(string userEmail)
        {

            var matchedUser = context.Users.FirstOrDefault(user => user.Email == userEmail);
            if (await UserManager.IsInRoleAsync(matchedUser.Id, "User"))
            {
                //TODO: Specific exception message maybe?
                throw new Exception("Unable to Find Matching User");
            }

            return new UserViewModel
            {
                Email = matchedUser.Email,
                DisplayName = matchedUser.DisplayName,
                FirstName = matchedUser.FirstName,
                LastName = matchedUser.LastName,
                ProfileDescription = matchedUser.ProfileDescription,
                StreetAddressOne = matchedUser.Address?.StreetAddressOne,
                StreetAddressTwo = matchedUser.Address?.StreetAddressTwo,
                City = matchedUser.Address?.City,
                State = matchedUser.Address?.State,
                ZipCode = matchedUser.Address?.ZipCode,
                Tags = matchedUser.Tags?.Select(t => new Models.DTO.TagDTO
                {
                    Id = t.ID,
                    Name = t.Name
                })
            };

        }

        [HttpGet]
        [Route("Organizations/{organizationId}")]
        public async Task<OrganizationViewModel> GetOrganizationById(string organizationId)
        {
            var matchedOrganization = context.Users.FirstOrDefault(org => org.Id == organizationId);
            if(await UserManager.IsInRoleAsync(matchedOrganization.Id, "Organization"))
            {
                //TODO: specific exception message
                throw new Exception("Unable to Find Matching Organization.");
            }

            return new OrganizationViewModel
            {
                Email = matchedOrganization.Email,
                DisplayName = matchedOrganization.DisplayName,
                FirstName = matchedOrganization.FirstName,
                LastName = matchedOrganization.LastName,
                OrganizationCategory = matchedOrganization.Category,
                ProfileDescription = matchedOrganization.ProfileDescription,
                StreetAddressOne = matchedOrganization.Address?.StreetAddressOne,
                StreetAddressTwo = matchedOrganization.Address?.StreetAddressTwo,
                City = matchedOrganization.Address?.City,
                State = matchedOrganization.Address?.State,
                ZipCode = matchedOrganization.Address?.ZipCode,
                Tags = matchedOrganization.Tags?.Select(t => new Models.DTO.TagDTO
                {
                    Id = t.ID,
                    Name = t.Name
                })
            };
        }
    }

}