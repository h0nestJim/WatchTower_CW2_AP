using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTower_V1.Models
{
    public class ItemModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

      


        //configure foreign key constraint
        public int AssetCategoryId { get; set; }
        public virtual AssetCategoryModel AssetCategory { get; set; }

        public int RoomId { get; set; }
        public virtual RoomModel Room { get; set; }


    }
}
