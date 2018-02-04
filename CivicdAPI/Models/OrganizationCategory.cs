using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CivicdAPI.Models
{
  public enum OrganizationCategory
  {
    [Display(Name = "Regular User")]
    NA,
    [Display(Name = "Government")]
    Government,
    [Display(Name = "Educational")]
    Educational,
    [Display(Name = "Volunteer")]
    Volunteer,
    [Display(Name = "Neighborhood Association")]
    Neighborhood,
    [Display(Name = "Partisan Political")]
    Political,
    [Display(Name = "Non-partisan Political")]
    Cause
  }
}