using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchTower_V1.Models
{
    public class DashboardViewModel
    {
        public UserModel CurrentUser { get; set; }

        public int ActiveGenTickets { get; set; }

        public int ActiveTechTickets { get; set; }

        public int CombinedTickets { get; set; }

        public int MyTicketCount { get; set; }

        public IEnumerable<GeneralTicketModel> MyGenTickets { get; set; }

        public IEnumerable<TechnicalTicketModel> MyTechTickets { get; set; }

        public IEnumerable<GeneralTicketModel> GenTickets { get; set; }

        public IEnumerable<TechnicalTicketModel> TechTickets { get; set; }
    }
}
