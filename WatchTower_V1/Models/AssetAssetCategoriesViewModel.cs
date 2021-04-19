using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTower_V1.Models
{
    public class AssetAssetCategoriesViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [ForeignKey("CategoryId")]
        public int AssetCategoryId { get; set; }

        public string Category { get; set; }
        public IEnumerable<AssetCategoryModel> Categories { get; set; }

        [ForeignKey("RoomId")]
        public int RoomId { get; set; }

        public string RoomNumber{ get; set; }
        public IEnumerable<RoomModel> Rooms { get; set; }
    }
}
