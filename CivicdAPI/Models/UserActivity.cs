using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace CivicdAPI.Models
{
    public class UserActivity
    {
        [Key, Column(Order = 0)]
        public int UserActivityId { get; set; }
        [Key, Column(Order = 1)]
        public int ActivityID { get; set; }
        [Key, Column(Order = 2)]
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Activity Activity { get; set; }

        public bool CheckedIn { get; set; }
        public bool Host { get; set; }
    }
}