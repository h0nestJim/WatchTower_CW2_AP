using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTower_V1.Models
{
    public class GeneralTicketViewModel
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description{ get; set; }

        [Required]
        public DateTime DateOpened { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public bool isClosed { get; set; }


        [ForeignKey("UserId")]
        public string UserId{ get; set; }
        public IEnumerable<UserModel> DBUsers{ get; set; }

    

     
    }
}
