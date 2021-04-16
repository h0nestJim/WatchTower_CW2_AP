using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTower_V1.Models
{
    public class CampusRoomsViewModel
    {
        [Key]
        public int id { get; set; }

        
        public string RoomNumber { get; set; }

        
        public string Description { get; set; }

        [ForeignKey("CampusId")]
        public int CampusId { get; set; }
        public IEnumerable<CampusModel> Campus { get; set; }
        
    }
}
