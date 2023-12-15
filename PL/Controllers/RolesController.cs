using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RolesController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        #region Index
        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole identityRole)
        {
            if (ModelState.IsValid)
            {
                var result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View("Index");
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound();

            return View(role);
        }

        public async Task<IActionResult> Update(string id)
        {
            if (id == null)
                return NotFound();

            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound();

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, IdentityRole identityRole)
        {
            if (id != identityRole.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(id);
                role.Name = identityRole.Name;
                role.NormalizedName = role.Name.ToUpper();

                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View(identityRole);

        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();

            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound();

            var result=await roleManager.DeleteAsync(role);

            if(result.Succeeded)
                return RedirectToAction("Index");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return RedirectToAction("Index");
        }
        #endregion

        #region AddOrRemoveUsers
        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {
            var role = await roleManager.FindByIdAsync(RoleId);
            if(role is null)
                return NotFound();

            ViewBag.RoleId = RoleId;
            var users = new List<UserInRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var userInRole = new UserInRoleViewModel()
                {
                    UserId=user.Id,
                    UserEmail=user.Email,
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected = true;
                else
                    userInRole.IsSelected = false;

                users.Add(userInRole);

            }
            return View(users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleViewModel> models,string RoleId)
        {
            var role = await roleManager.FindByIdAsync(RoleId);

            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var item in models)
                {
                    var user = await userManager.FindByIdAsync(item.UserId);
                    if (user is not null)
                    {
                        if (item.IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                            await userManager.AddToRoleAsync(user, role.Name);
                        else if (!item.IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                            await userManager.RemoveFromRoleAsync(user, role.Name);

                    }

                }
                return RedirectToAction("Update", new { id = RoleId });
            }
            return View(models);
        }
        #endregion
    }
}
