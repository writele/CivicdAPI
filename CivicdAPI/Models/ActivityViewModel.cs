using CivicdAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CivicdAPI.Models
{
    public class ActivityViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string OrganizationUserName { get; set; }
        [Required]
        public string DisplayTitle { get; set; }
        [Required]
        public ActivityCategory CategoryName { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public string Description { get; set; }
        public DateTime EndTime { get; set; }
        public string AddressDisplayName { get; set; }
        public string StreetAddressOne { get; set; }
        public string StreetAddressTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        
        public IEnumerable<TagDTO> Tags { get; set; }
    }
}