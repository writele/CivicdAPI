using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CivicdAPI.Models
{
  public enum LegalStatus
  {
    [Display(Name = "N/A")]
    RegularUser,
    [Display(Name = "Category 2")]
    Category2,
    [Display(Name = "Category 3")]
    Category3,
    [Display(Name = "Category 4")]
    Category4
  }
}