using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WatchTower_V1.Data;
using WatchTower_V1.Models;

namespace WatchTower_V1.Views
{
    public class CampusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CampusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CampusModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Campus.ToListAsync());
        }

      
        // GET: CampusModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CampusModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CampusModel campusModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campusModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campusModel);
        }

        // GET: CampusModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campusModel = await _context.Campus.FindAsync(id);
            if (campusModel == null)
            {
                return NotFound();
            }
            return View(campusModel);
        }

        // POST: CampusModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CampusModel campusModel)
        {
            if (id != campusModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campusModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampusModelExists(campusModel.Id))
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
            return View(campusModel);
        }

        // GET: CampusModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campusModel = await _context.Campus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campusModel == null)
            {
                return NotFound();
            }

            return View(campusModel);
        }

        // POST: CampusModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campusModel = await _context.Campus.FindAsync(id);
            _context.Campus.Remove(campusModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampusModelExists(int id)
        {
            return _context.Campus.Any(e => e.Id == id);
        }
    }
}
