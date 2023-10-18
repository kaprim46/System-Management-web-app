using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Gestion_Parc.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Parc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Roles()
        {
            var roles = new RoleViewModel
            {
                NewRole = new NewRole(),
                Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList()
            };
            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Roles(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Id = model.NewRole.RoleId,
                    Name = model.NewRole.RoleName
                };
                if (role.Id == null)
                {
                    role.Id = Guid.NewGuid().ToString();
                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                        SessionMsg(Helper.Success, "Created", "Role created successfuly");
                    else
                        SessionMsg(Helper.Error, "Not Created", "Role not created!");

                }
                else
                {
                    var roleUpdate = await _roleManager.FindByIdAsync(model.NewRole.RoleId.ToString());
                    roleUpdate.Id = model.NewRole.RoleId.ToString();
                    roleUpdate.Name = model.NewRole.RoleName;
                    var result = await _roleManager.UpdateAsync(roleUpdate);
                    if (result.Succeeded)
                        SessionMsg(Helper.Success, "Updated", "Role Updated successfuly");
                    else
                        SessionMsg(Helper.Error, "Not Updated", "Role not Updated!");
                }
            }
            return RedirectToAction(nameof(Roles));
        }

        
        public async Task<IActionResult> DeleteRole(string? id)
        {
            if (id != null)
            {
                var user = await _roleManager.FindByIdAsync(id);
                var result = await _roleManager.DeleteAsync(user);
                if (result.Succeeded)
                    SessionMsg(Helper.Success, "Deleted","Your file has been deleted!");
            }
            return RedirectToAction(nameof(Roles));
        }

        public void SessionMsg(string msgType, string title, string msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, msgType);
            HttpContext.Session.SetString(Helper.Title, title);
            HttpContext.Session.SetString(Helper.Msg, msg);
        }
    }
}