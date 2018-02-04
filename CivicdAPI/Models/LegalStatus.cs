using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CivicdAPI.Models
{
  public enum LegalStatus
  {
    [Display(Name = "None or N/A")]
    NA,
    [Display(Name = "501(c)(3)")]
    Nonprofit
  }
}