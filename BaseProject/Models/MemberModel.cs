using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace BaseProject.Models
{
    public class MemberModel
    {
        public DateTime JoinDate { get; set; }

        [MaxLength(100)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        public string Sponsor { get; set; }

        public string LinkedIn { get; set; }
        public string Twitter { get; set; }

        public string Title { get; set; }

        [Display(Name = "Profession")]
        public string jobTitle { get; set; }

        public string Bio { get; set; }

        public Boolean isAdmin { get; set; }


    }
}