using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTower_V1.Models
{
    public class GeneralUpdateModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public string UserName { get; set; }

        
        public string ProfilePicture { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }

        [Required]
        public bool IsResolved { get; set; }

        [Required]
        public int TicketId { get; set; }
        public virtual GeneralTicketModel Ticket { get; set; }
    }
}
