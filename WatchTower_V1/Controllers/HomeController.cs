using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WatchTower_V1.Data;
using WatchTower_V1.Models;

namespace WatchTower_V1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;
        

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var techTickets = await _context.TechnicalTicket.ToListAsync();
            var genTickets = await _context.GeneralTickets.ToListAsync();
            var myGenTickets = new List<GeneralTicketModel>();
            var myTechTickets = new List<TechnicalTicketModel>();

            int myTicketCount = 0;
            int activeTechTickets = 0;
            int activeGentickets = 0;
            

            foreach(var ticket in genTickets)
            {
                if (ticket.UserName == user.UserName && ticket.isClosed == false)
                {
                    myGenTickets.Add(ticket);
                    myTicketCount++;
                }

                if(ticket.isClosed == false)
                {
                    activeGentickets++;
                }
            }

            foreach (var ticket in techTickets)
            {
                if (ticket.UserName == user.UserName && ticket.isClosed == false)
                {
                    myTechTickets.Add(ticket);
                    myTicketCount++;
                }
                if (ticket.isClosed == false)
                {
                    activeTechTickets++;
                }
            }

            var dashboardModel = new DashboardViewModel();
            dashboardModel.CurrentUser = user;
            dashboardModel.ActiveGenTickets = activeGentickets;
            dashboardModel.ActiveTechTickets = activeTechTickets;
            dashboardModel.CombinedTickets = activeTechTickets + activeGentickets;
            dashboardModel.MyTicketCount = myTicketCount;
            dashboardModel.MyGenTickets = myGenTickets;
            dashboardModel.MyTechTickets = myTechTickets;
            dashboardModel.TechTickets = techTickets;
            dashboardModel.GenTickets = genTickets;

            

            return View(dashboardModel);
        }

        [Authorize(Roles="StaffStudent")]
        public IActionResult Privacy()
        {
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
