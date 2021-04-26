using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTower_V1.Models
{
    public class GeneralTicketModel
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
        //person creating the ticket
        public string UserName { get; set; }

        [Required]
        public string UserId { get; set; }
   
        public virtual UserModel User {get;set; }


        public List<GeneralUpdateModel> Updates;

        public GeneralTicketModel()
        {
            Updates = new List<GeneralUpdateModel>();
           
        }
    }
}
