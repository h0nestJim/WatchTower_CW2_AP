using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTower_V1.Models
{
    public class UserViewModel:IdentityUser
    {
      

        [Required]
        public string FName { get; set; }

        [Required]
        public string SName { get; set; }

        public string JobTitle { get; set; }

        [Required]
        public byte[] ProfilePicture { get; set; }

       
    }
}
