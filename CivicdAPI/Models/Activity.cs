using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CivicdAPI.Models
{
  public class Activity
  {
    [Key]
    public int ID { get; set; }
    public string Description { get; set; }
    public virtual ICollection<UserActivity> UserActivities { get; set; }
  }
}