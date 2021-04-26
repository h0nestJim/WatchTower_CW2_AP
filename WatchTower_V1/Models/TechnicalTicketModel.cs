using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTower_V1.Models
{
    public class TechnicalTicketModel
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateOpened { get; set; }

        [Required]
        public bool isClosed { get; set; }
        
        //staff member generating the ticket
        [Required]
      
        public string UserName { get; set; }

        //person raising the issue
        [Required]
        public string UserId { get; set; }

        public virtual UserModel User { get; set; }



        [Required]
        public int AssetId { get; set; }

        public virtual ItemModel Asset { get; set; }

    }
}
