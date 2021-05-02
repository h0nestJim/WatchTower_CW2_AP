﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WatchTower_V1.Data;
using WatchTower_V1.Models;

namespace WatchTower_V1.Views
{
    [Authorize]
    public class TechnicalTicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TechnicalTicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TechnicalTicket
        public async Task<IActionResult> Index(string status, string search)
        {
            var techTickets = (from t in _context.TechnicalTicket
                                  join u in _context.Users on t.UserId equals u.Id
                                  join a in _context.Item on t.AssetId equals a.Id
                                  join r in _context.Room on a.RoomId equals r.Id
                                  join c in _context.Campus on r.CampusId equals c.Id
                                  select new TechnicalTicketViewModel()
                                  {
                                      Id = t.Id,
                                      Title = t.Title,
                                      Description = t.Description,
                                      DateOpened = t.DateOpened,
                                      UserName = t.UserName,
                                      isClosed = t.isClosed,
                                      UserId = t.UserId,
                                      Stakeholder = u.UserName,
                                      CampusName = c.Name,
                                      RoomNumber = r.RoomNumber,
                                      AssetId = t.AssetId,
                                      AssetName = a.Name
                                  });


            //var generalTickets = from t in _context.GeneralTickets
            // select t;

            if (!String.IsNullOrEmpty(search))
            {
                techTickets = techTickets.Where(t => t.Title.Contains(search));
            }

            if (status != "null")
            {
                if (!String.IsNullOrEmpty(status))
                {
                    techTickets = techTickets.Where(t => t.isClosed == Convert.ToBoolean(status));
                }
            }
            return View(await techTickets.ToListAsync());
        }

        // GET: TechnicalTicket/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicalTicketModel = await _context.TechnicalTicket
                .FirstOrDefaultAsync(m => m.Id == id);


            if (technicalTicketModel == null)
            {
                return NotFound();
            }

            technicalTicketModel.Updates = await _context.TechnicalUpdates.Where(ticket => ticket.TicketId == id).ToListAsync();


            var technicalTicketViewModel = new TechnicalTicketViewModel();

            technicalTicketViewModel.Id = technicalTicketModel.Id;
            technicalTicketViewModel.Title = technicalTicketModel.Title;
            technicalTicketViewModel.Description = technicalTicketModel.Description;
            technicalTicketViewModel.DateOpened = technicalTicketModel.DateOpened;
            technicalTicketViewModel.UserName = technicalTicketModel.UserName;
            technicalTicketViewModel.isClosed = technicalTicketModel.isClosed;

            var user =  _context.Users.Find(technicalTicketModel.UserId);
            technicalTicketViewModel.Stakeholder = user.UserName;

            var asset = _context.Item.Find(technicalTicketModel.AssetId);
            technicalTicketViewModel.AssetName = asset.Name;

            var room = _context.Room.Find(asset.RoomId);
            technicalTicketViewModel.RoomNumber = room.RoomNumber;
            technicalTicketViewModel.RoomId = room.Id;

            var campus = _context.Campus.Find(room.CampusId);
            technicalTicketViewModel.CampusId = campus.Id;
            technicalTicketViewModel.CampusName = campus.Name;

            foreach(var update in technicalTicketModel.Updates)
            {
                technicalTicketViewModel.Updates.Add(update);
            }


            return View(technicalTicketViewModel);
        }



        [ActionName("Action")]
        public async Task<IActionResult> Update(int? id, string action, bool isresolved)
        {

            var technicalTicketModel = await _context.TechnicalTicket
                .FirstOrDefaultAsync(m => m.Id == id);
            if (technicalTicketModel == null)
            {
                return NotFound();
            }



            TechnicalUpdateModel update = new TechnicalUpdateModel();
            update.TimeStamp = DateTime.Now;
            update.UserName = User.Identity.Name;
            update.IsResolved = isresolved;
            update.Action = action;
            update.TicketId = technicalTicketModel.Id;
            //change later
            update.ProfilePicture = "/images/sampleprofile.png";

            if (isresolved)
            {
                technicalTicketModel.isClosed = true;
            }



            _context.Update(update);
            await _context.SaveChangesAsync();

            _context.Update(technicalTicketModel);

            await _context.SaveChangesAsync();

            technicalTicketModel.Updates.Add(update);

            return RedirectToAction(nameof(Details), new { id = id });


        }





        // GET: TechnicalTicket/Create
        public async Task<IActionResult> Create()
        {
            /*ViewData["AssetId"] = new SelectList(_context.Item, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            */


            var technicalTicketViewModel = new TechnicalTicketViewModel();

            technicalTicketViewModel.DBUsers = await _context.Users.ToListAsync();
            technicalTicketViewModel.UserName = User.Identity.Name;
            technicalTicketViewModel.DateOpened = DateTime.Now;
            technicalTicketViewModel.Campus = await _context.Campus.ToListAsync();
         


            return View(technicalTicketViewModel);
        }

        public async Task<IEnumerable<RoomModel>> GetRooms(int campusId)
        {

            IEnumerable<RoomModel> Room = await _context.Room.Where(x => x.CampusId == campusId).ToListAsync();


            return Room;
           
        }

        public async Task<IEnumerable<ItemModel>> GetAssets(int roomId)
        {

            IEnumerable<ItemModel> Assets = await _context.Item.Where(x => x.RoomId == roomId).ToListAsync();


            return Assets;

        }



        // POST: TechnicalTicket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,DateOpened,isClosed,UserName,UserId,AssetId")] TechnicalTicketModel technicalTicketModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technicalTicketModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetId"] = new SelectList(_context.Item, "Id", "Description", technicalTicketModel.AssetId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", technicalTicketModel.UserId);
            return View(technicalTicketModel);
        }

        // GET: TechnicalTicket/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicalTicketModel = await _context.TechnicalTicket.FindAsync(id);
            if (technicalTicketModel == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.Item, "Id", "Description", technicalTicketModel.AssetId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", technicalTicketModel.UserId);
            return View(technicalTicketModel);
        }

        // POST: TechnicalTicket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DateOpened,isClosed,UserName,UserId,AssetId")] TechnicalTicketModel technicalTicketModel)
        {
            if (id != technicalTicketModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technicalTicketModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechnicalTicketModelExists(technicalTicketModel.Id))
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
            ViewData["AssetId"] = new SelectList(_context.Item, "Id", "Description", technicalTicketModel.AssetId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", technicalTicketModel.UserId);
            return View(technicalTicketModel);
        }

        // GET: TechnicalTicket/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicalTicketModel = await _context.TechnicalTicket
                .Include(t => t.Asset)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (technicalTicketModel == null)
            {
                return NotFound();
            }

            return View(technicalTicketModel);
        }

        // POST: TechnicalTicket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technicalTicketModel = await _context.TechnicalTicket.FindAsync(id);
            _context.TechnicalTicket.Remove(technicalTicketModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechnicalTicketModelExists(int id)
        {
            return _context.TechnicalTicket.Any(e => e.Id == id);
        }
    }
}
