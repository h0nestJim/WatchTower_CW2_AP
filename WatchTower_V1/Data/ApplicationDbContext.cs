using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WatchTower_V1.Models;

namespace WatchTower_V1.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserModel>
    {
        public DbSet<RoomModel> Room { get; set; }
        public DbSet<CampusModel> Campus { get; set; }

        public DbSet<AssetCategoryModel> AssetCategory { get; set; }
        public DbSet<ItemModel> Item { get; set; }

        public DbSet<GeneralTicketModel> GeneralTickets { get; set; }

        public DbSet<GeneralUpdateModel> GeneralUpdates { get; set; }

        public DbSet<TechnicalUpdateModel> TechnicalUpdates { get; set; }
        public DbSet<TechnicalTicketModel> TechnicalTicket { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<WatchTower_V1.Models.UserViewModel> UserViewModel { get; set; }
    }
}
