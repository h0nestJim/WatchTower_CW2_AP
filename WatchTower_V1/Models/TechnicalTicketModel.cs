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
        public DateTime TimeOpened { get; set; }

        //staff member generating the ticket
        [Required]
        public string UserId { get; set; }

        public virtual UserModel Staff { get; set; }

        //staff or student that raised the ticket
        public string StaffStudentId { get; set; }

        [Required]
        public int ItemId { get; set; }

        public virtual ItemModel Assets { get; set; }

    }
}
