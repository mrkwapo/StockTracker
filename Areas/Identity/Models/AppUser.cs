using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MultiUserMVC.Areas.Identity.Models
{
    public class AppUser : IdentityUser
    {
        //Adding custom properties
        [Required]
        [Display(Name="Full Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type of Shareholder")]
        public string Shareholder { get; set; } //indiviual investor or institutional investor

        [Required]
        [Display(Name = "Upload Photo")]
        public byte[] Photo { get; set; }

    }
}
