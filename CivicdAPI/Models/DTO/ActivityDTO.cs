using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CivicdAPI.Models.DTO
{
  public class ActivityDTO
  {
    public int Id { get; set; }
    public string DisplayTitle { get; set; }
    public string Description { get; set; }
  }
}