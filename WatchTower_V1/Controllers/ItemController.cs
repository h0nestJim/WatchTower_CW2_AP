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
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string search)
        {
            var assets = (from a in _context.Item
                          join ac in _context.AssetCategory on a.AssetCategoryId equals ac.Id
                          join r in _context.Room on a.RoomId equals r.Id
                          join c in _context.Campus on r.CampusId equals c.Id
                          select new AssetViewModel()
                          {
                              Id = a.Id,
                              Name = a.Name,
                              Description = a.Description,
                              AssetCategory = ac.Category,
                              RoomNumber = r.RoomNumber,
                              CampusName = c.Name
                          });
         

            if (!String.IsNullOrEmpty(search))
            {
                assets = assets.Where(a => a.Name.Contains(search));
            }



          
            return View(await assets.ToListAsync());


        }



        // GET: Item/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemModel = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemModel == null)
            {
                return NotFound();
            }

            var itemViewModel = new AssetViewModel();
            itemViewModel.Id = itemModel.Id;
            itemViewModel.AssetCategoryId = itemModel.AssetCategoryId;
            itemViewModel.Name = itemModel.Name;
            itemViewModel.RoomId = itemModel.RoomId;
            itemViewModel.Description = itemModel.Description;

            itemViewModel.Assetcategory = await _context.AssetCategory.ToListAsync();
            itemViewModel.Room = await _context.Room.ToListAsync();

            var assetCat = _context.AssetCategory.Find(itemModel.AssetCategoryId);
            var room = _context.Room.Find(itemModel.RoomId);

            itemViewModel.AssetCategory = assetCat.Category;
            itemViewModel.RoomNumber = room.RoomNumber;
            

            return View(itemViewModel);
        }

        // GET: Item/Create
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Create()
        {
           
            var assetAssetCategoriesViewModel = new AssetAssetCategoriesViewModel();

            assetAssetCategoriesViewModel.Categories = await _context.AssetCategory.ToListAsync();
            assetAssetCategoriesViewModel.Rooms = await _context.Room.ToListAsync();
            return View(assetAssetCategoriesViewModel);
           


        }

        // POST: Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,AssetCategoryId,RoomId")] ItemModel itemModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemModel);
        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemModel = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemModel == null)
            {
                return NotFound();
            }

            var itemViewModel = new AssetViewModel();
            itemViewModel.Id = itemModel.Id;
            itemViewModel.AssetCategoryId = itemModel.AssetCategoryId;
            itemViewModel.Name = itemModel.Name;
            itemViewModel.RoomId = itemModel.RoomId;
            itemViewModel.Description = itemModel.Description;

            itemViewModel.Assetcategory = await _context.AssetCategory.ToListAsync();
            itemViewModel.Room = await _context.Room.ToListAsync();


            return View(itemViewModel);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,AssetCategoryId,RoomId")] ItemModel itemModel)
        {
            if (id != itemModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemModelExists(itemModel.Id))
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
            return View(itemModel);
        }

        // GET: Item/Delete/5
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemModel = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemModel == null)
            {
                return NotFound();
            }

            return View(itemModel);
        }

        // POST: Item/Delete/5
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemModel = await _context.Item.FindAsync(id);
            _context.Item.Remove(itemModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemModelExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }
    }
}
