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
    public class AssetCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AssetCategory
        [Authorize(Roles = "Support,Manager,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.AssetCategory.ToListAsync());
        }

        // GET: AssetCategory/Details/5
        [Authorize(Roles = "Support,Manager,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetCategoryModel = await _context.AssetCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetCategoryModel == null)
            {
                return NotFound();
            }

            return View(assetCategoryModel);
        }

        [Authorize(Roles = "Manager,Admin")]
        // GET: AssetCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssetCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category")] AssetCategoryModel assetCategoryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetCategoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assetCategoryModel);
        }

        // GET: AssetCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetCategoryModel = await _context.AssetCategory.FindAsync(id);
            if (assetCategoryModel == null)
            {
                return NotFound();
            }
            return View(assetCategoryModel);
        }

        // POST: AssetCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category")] AssetCategoryModel assetCategoryModel)
        {
            if (id != assetCategoryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetCategoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetCategoryModelExists(assetCategoryModel.Id))
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
            return View(assetCategoryModel);
        }

        // GET: AssetCategory/Delete/5
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetCategoryModel = await _context.AssetCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetCategoryModel == null)
            {
                return NotFound();
            }

            return View(assetCategoryModel);
        }

        // POST: AssetCategory/Delete/5
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetCategoryModel = await _context.AssetCategory.FindAsync(id);
            _context.AssetCategory.Remove(assetCategoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetCategoryModelExists(int id)
        {
            return _context.AssetCategory.Any(e => e.Id == id);
        }
    }
}
