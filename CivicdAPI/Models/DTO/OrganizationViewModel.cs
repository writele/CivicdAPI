﻿using CivicdAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Web;

namespace CivicdAPI.Models
{
    public class OrganizationViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public SecureString Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public OrganizationCategory OrganizationCategory { get; set; }
        public string ProfileDescription { get; set; }
        public string StreetAddressOne { get; set; }
        public string StreetAddressTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public IEnumerable<TagDTO> Tags { get; set; }
    }
}