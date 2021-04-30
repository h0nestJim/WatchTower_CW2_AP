using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTower_V1.Models
{
    public class AssetViewModel
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string AssetCategory { get; set; }

        public string RoomNumber { get; set; }
        
        public string CampusName { get; set; }
       
       
    }
}
