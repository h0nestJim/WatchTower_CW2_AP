using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTower_V1.Models
{
    public class UserModel: IdentityUser
    {
        [Required]
        public string Fname { get; set; }

        [Required]
        public string SName { get; set; }

        public byte[] ProfilePicture { get; set; }


    }
}
