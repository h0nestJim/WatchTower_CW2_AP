using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTower_V1.Models
{
    public class RoomCampusViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RoomNumber { get; set; }

        [Required]
        public string Description { get; set; }

        //configure foreign key constraint
       
        public CampusModel Campus { get; set; }

    }
}
