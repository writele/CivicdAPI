using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using CivicdAPI.Models;
using CivicdAPI.Providers;
using CivicdAPI.Results;

namespace CivicdAPI.Controllers
{
  [Authorize]
  [RoutePrefix("api")]
  public class UserController : ApiController
  {
    private const string LocalLoginProvider = "Local";
    private ApplicationUserManager _userManager;
    private ApplicationDbContext db = new ApplicationDbContext();

    public UserController()
    {
    }

    public UserController(ApplicationUserManager userManager,
      ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
    {
      UserManager = userManager;
      //AccessTokenFormat = accessTokenFormat;
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

    // POST api/Users
    [AllowAnonymous]
    [Route("Users")]
    [HttpPost]
    public async Task<IHttpActionResult> Register(UserViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var address = new Address()
      {
        StreetAddressOne = model.StreetAddressOne,
        StreetAddressTwo = model.StreetAddressTwo,
        City = model.City,
        State = model.State,
        ZipCode = model.ZipCode
      };

      var tags = new List<Tag>();

      foreach (var item in model.Tags)
      {
        var tag = db.Tags.FirstOrDefault(x => x.Name == item.Name);
        tags.Add(tag);
      }

      var user = new ApplicationUser()
      {
        UserName = model.Email,
        Email = model.Email,
        FirstName = model.FirstName,
        LastName = model.LastName,
        DisplayName = model.DisplayName,
        ProfileDescription = model.ProfileDescription,
        Address = address,
        Tags = tags
      };

      IdentityResult result = await UserManager.CreateAsync(user, model.Password.ToString());

      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      return Ok();
    }

    [HttpGet]
    [Route("Users/{userEmail}")]
    public async Task<UserViewModel> GetUserByEmail(string userEmail)
    {

      var matchedUser = await UserManager.FindByEmailAsync(userEmail).ConfigureAwait(false);
      if (matchedUser == null)
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

    private IHttpActionResult GetErrorResult(IdentityResult result)
    {
      if (result == null)
      {
        return InternalServerError();
      }

      if (!result.Succeeded)
      {
        if (result.Errors != null)
        {
          foreach (string error in result.Errors)
          {
            ModelState.AddModelError("", error);
          }
        }

        if (ModelState.IsValid)
        {
          // No ModelState errors are available to send, so just return an empty BadRequest.
          return BadRequest();
        }

        return BadRequest(ModelState);
      }

      return null;
    }
  }
}
