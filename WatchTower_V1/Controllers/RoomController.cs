using System;
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
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        // GET: Room
        [Authorize(Roles = "Support,Manager,Admin")]
        public async Task<IActionResult> Index(string type, string search)
        {
            var data = _context.Room.Include("Campus");
            var filter = Convert.ToBoolean(type);
            if (filter)
            {
                if (!String.IsNullOrEmpty(search))
                {

                    data = _context.Room.Include("Campus").Where(t => t.Campus.Name.ToLower() == search.ToLower());
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(search))
                {
                    data = _context.Room.Include("Campus").Where(t => t.RoomNumber.Contains(search));
                }
            }


           

            return View(await data.ToListAsync());
        }

        // GET: Room/Details/5
        [Authorize(Roles = "Support,Manager,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomModel = await _context.Room
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomModel == null)
            {
                return NotFound();
            }

            return View(roomModel);
        }

        // GET: Room/Create
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Create()
        {

            Console.WriteLine("Hello!");
            var campusRoomsViewModel = new CampusRoomsViewModel();
           
            campusRoomsViewModel.Campus = await _context.Campus.ToListAsync<CampusModel>();
/*
              var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();
            foreach (UserModel user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FirstName = user.Fname;
                thisViewModel.LastName = user.SName;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
            */

            return View(campusRoomsViewModel);
        }

        // POST: Room/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,RoomNumber,Description,CampusId")] RoomModel roomModel)
        {

            if (ModelState.IsValid)
            {
                _context.Add(roomModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roomModel);
        }




        // GET: Room/Edit/5
        [Authorize(Roles = "Support,Manager,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomModel = await _context.Room.FindAsync(id);
            if (roomModel == null)
            {
                return NotFound();
            }
            return View(roomModel);
        }

        // POST: Room/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Support,Manager,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,RoomNumber,Description")] RoomModel roomModel)
        {
            if (id != roomModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomModelExists(roomModel.Id))
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
            return View(roomModel);
        }

        // GET: Room/Delete/5
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomModel = await _context.Room
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomModel == null)
            {
                return NotFound();
            }

            return View(roomModel);
        }

        // POST: Room/Delete/5
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomModel = await _context.Room.FindAsync(id);
            _context.Room.Remove(roomModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool RoomModelExists(int id)
        {
            return _context.Room.Any(e => e.Id == id);
        }
    }
}
