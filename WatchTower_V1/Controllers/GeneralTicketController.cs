﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WatchTower_V1.Data;
using WatchTower_V1.Models;

namespace WatchTower_V1.Views
{
    [Authorize]
    public class GeneralTicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GeneralTicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GeneralTicket
        public async Task<IActionResult> Index(string status, string search)
        {
            var generalTickets = (from t in _context.GeneralTickets
                              join u in _context.Users on t.UserId equals u.Id
                              select new GeneralTicketViewModel()
                              {
                                  Id = t.Id,
                                  Title = t.Title,
                                  Description = t.Description,
                                  DateOpened = t.DateOpened,
                                  UserName = t.UserName,
                                  isClosed = t.isClosed,
                                  UserId = t.UserId,
                                  Stakeholder = u.UserName
                              });


            //var generalTickets = from t in _context.GeneralTickets
                        // select t;

            if (!String.IsNullOrEmpty(search))
            {
                generalTickets = generalTickets.Where(t => t.Title.Contains(search));
            }

            if (status != "null")
            {
                if (!String.IsNullOrEmpty(status))
                {
                    generalTickets = generalTickets.Where(t => t.isClosed == Convert.ToBoolean(status));
                }
            }
            return View(await generalTickets.ToListAsync());


        }



        // GET: GeneralTicket/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generalTicketModel = await _context.GeneralTickets
                .FirstOrDefaultAsync(m => m.Id == id);


            if (generalTicketModel == null)
            {
                return NotFound();
            }

            var user =  _context.Users.Find(generalTicketModel.UserId);

            generalTicketModel.UserId = user.UserName;



            generalTicketModel.Updates = await _context.GeneralUpdates.Where(ticket => ticket.TicketId == id).ToListAsync();

            return View(generalTicketModel);
        }

    

        [ActionName("Action")]
        public async Task<IActionResult> Update(int? id, string action, bool isresolved)
        {
           
            var generalTicketModel = await _context.GeneralTickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generalTicketModel == null)
            {
                return NotFound();
            }

            var x = isresolved;

            GeneralUpdateModel update = new GeneralUpdateModel();
            update.TimeStamp = DateTime.Now;
            update.UserName = User.Identity.Name;
            update.IsResolved = isresolved;
            update.Action = action;
            update.TicketId = generalTicketModel.Id;
            //change later
            update.ProfilePicture = "/images/sampleprofile.png";

           if (isresolved)
            {
                generalTicketModel.isClosed = true;
            }

           

            _context.Update(update);
            await _context.SaveChangesAsync();

            _context.Update(generalTicketModel);

            await _context.SaveChangesAsync();

            generalTicketModel.Updates.Add(update);

            return RedirectToAction(nameof(Details), new { id = id });


        }



        public async Task<IActionResult> Create()
        {
             var generalTicketViewModel = new GeneralTicketViewModel();



             generalTicketViewModel.DBUsers = await _context.Users.ToListAsync();
            generalTicketViewModel.UserName = User.Identity.Name;
            generalTicketViewModel.DateOpened = DateTime.Now;

          

            //GET current logged in user ID or UserName
            /*
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    generalTicketCreateViewModel.StaffId = userIdClaim.Value;
                }
            }
            */
            return View(generalTicketViewModel);
        }



        // POST: GeneralTicket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,DateOpened,isClosed,UserName,UserId")] GeneralTicketModel generalTicketModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(generalTicketModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(generalTicketModel);
        }

        // GET: GeneralTicket/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generalTicketViewModel = new GeneralTicketViewModel();

            var model = await _context.GeneralTickets.FindAsync(id);
            var user = await _context.Users.FindAsync(model.UserId);

            generalTicketViewModel.Id = model.Id;
            generalTicketViewModel.isClosed = model.isClosed;
            generalTicketViewModel.Stakeholder = user.UserName;
            generalTicketViewModel.Title = model.Title;
            generalTicketViewModel.UserId = model.UserId;
            generalTicketViewModel.DateOpened = model.DateOpened;
            generalTicketViewModel.Description = model.Description;
            generalTicketViewModel.UserName = model.UserName;

            generalTicketViewModel.DBUsers = await _context.Users.ToListAsync();
           

            var generalTicketModel = await _context.GeneralTickets.FindAsync(id);
           
            return View(generalTicketViewModel);
        }

        // POST: GeneralTicket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DateOpened,isClosed,UserName,UserId")] GeneralTicketModel generalTicketModel)
        {
            var test = generalTicketModel.isClosed;
            if (id != generalTicketModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(generalTicketModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneralTicketModelExists(generalTicketModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(generalTicketModel);
        }

        // GET: GeneralTicket/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generalTicketModel = await _context.GeneralTickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generalTicketModel == null)
            {
                return NotFound();
            }

            return View(generalTicketModel);
        }

        // POST: GeneralTicket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var generalTicketModel = await _context.GeneralTickets.FindAsync(id);
            _context.GeneralTickets.Remove(generalTicketModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneralTicketModelExists(int id)
        {
            return _context.GeneralTickets.Any(e => e.Id == id);
        }
    }
}
