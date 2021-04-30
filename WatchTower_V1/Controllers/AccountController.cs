using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTower_V1.Data;
using WatchTower_V1.Models;

namespace WatchTower_V1.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(ApplicationDbContext context, UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET: AccountController
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersViewModel = new List<UserViewModel>();
            foreach(UserModel user in users)
            {
                var accountUserModel = new UserViewModel();
                accountUserModel.Id = user.Id;
                accountUserModel.FName = user.Fname;
                accountUserModel.SName = user.SName;
                accountUserModel.UserName = user.UserName;
                accountUserModel.JobTitle = user.JobTitle;
                accountUserModel.Email = user.Email;
               
                usersViewModel.Add(accountUserModel);
            }

            return View(usersViewModel);

        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userModel == null)
            {
                return NotFound();
            }

            var userViewModel = new UserViewModel();
            userViewModel.UserName = userModel.UserName;
            userViewModel.FName = userModel.Fname;
            userViewModel.SName = userModel.SName;
            userViewModel.JobTitle = userModel.JobTitle;
            userViewModel.Email = userModel.Email;

            return View(userViewModel);
        }

        // POST: /Delete/ffgty767gggt76
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteUserd(string id)
        {


            //get User Data from Userid
            var user = await _context.Users.FindAsync(id);



            //Gets list of Roles associated with current user
            var rolesForUser = await _userManager.GetRolesAsync(user);

            using (var transaction = _context.Database.BeginTransaction())
            {
                

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await _userManager.RemoveFromRoleAsync(user, item);
                    }
                }

                //Delete User
                await _userManager.DeleteAsync(user);

                TempData["Message"] = "User Deleted Successfully. ";
                TempData["MessageValue"] = "1";
                transaction.Commit();
            }

            return RedirectToAction("Index");
        }

    }


}
