using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PL.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        #region Index
        public async Task<IActionResult> Index(string searchValue = "")
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                var users = userManager.Users;
                return View(users);
            }
            else
            {
                var user = await userManager.FindByEmailAsync(searchValue.Trim());
                var userInList = new List<ApplicationUser>() { user };

                return View(userInList);
            }
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
                return NotFound();

            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            return View(user);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(string id)
        {
            if (id is null)
                return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, ApplicationUser applicationUser)
        {
            if(id!= applicationUser.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var user= await userManager.FindByIdAsync(id);
                user.UserName= applicationUser.UserName;
                user.PhoneNumber= applicationUser.PhoneNumber;
                user.NormalizedUserName = applicationUser.UserName.ToUpper();

                var result=await userManager.UpdateAsync(user);
                if(result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View(applicationUser);
        }


        #endregion

        #region Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id is null)
                return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
                return RedirectToAction("Index");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return RedirectToAction("Index");

        }
        #endregion
    }
}
