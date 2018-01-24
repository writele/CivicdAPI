using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CivicdAPI.Models
{
  public class Activity
  {
    public Activity()
    {
      this.Tags = new HashSet<Tag>();
    }
    [Key]
    public int ID { get; set; }
    public string DisplayTitle { get; set; }
    [AllowHtml]
    public string Description { get; set; }
    public string Photo { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
    public ActivityCategory Category { get; set; }
    public Address Address { get; set; }
    public virtual ICollection<UserActivity> UserActivities { get; set; }
    public virtual ICollection<Tag> Tags { get; set; }
  }
}