using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.IRepository;
using Gestion_Parc.Models;
using Gestion_Parc.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gestion_Parc.Controllers
{
    public class BreakDownController : Controller
    {
        private readonly AppDbContxt _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceRepositoryBreakDown<BreakDown> _serviceBreakDown;
        private readonly RoleManager<IdentityRole> _roleManager;

        public BreakDownController(AppDbContxt db, UserManager<ApplicationUser> userManager,
                                   IServiceRepositoryBreakDown<BreakDown> serviceBreakDown,
                                   RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _serviceBreakDown = serviceBreakDown;
            _roleManager = roleManager;
        }

        public IActionResult BreakDowns()
        {

            ViewBag.BreackDownType = Enum.GetValues(typeof(Helper.BreackDownType))
                     .Cast<Helper.BreackDownType>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();

            ViewBag.Category = Enum.GetValues(typeof(Helper.Category))
                     .Cast<Helper.Category>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();

            var breakDowns = new BreakDownViewModel
            {
                AllBreakDowns = _serviceBreakDown.GetAll(),
                AllBreakDownsForEachUser = _serviceBreakDown.GetAllForEachUser().ToList(),
                AllCompleted = _serviceBreakDown.GetAllCompleted().ToList(),
                AllProgressed = _serviceBreakDown.GetAllInProgress().ToList(),
                AllReported = _serviceBreakDown.GetAllReported().ToList(),
                NNewBreakDown = new NewBreakDown()
            };
            return View(breakDowns);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BreakDowns(BreakDownViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var newBreakDown = new BreakDown
                {
                    Description = model.NNewBreakDown.Description,
                    Category = model.NNewBreakDown.Category,
                    SubCategory = model.NNewBreakDown.SubCategory,
                    Type = model.NNewBreakDown.Type,
                    UserId = userId,
                };
                
                
                if(_serviceBreakDown.Save(newBreakDown, newBreakDown.UserId))
                    SessionMsg(Helper.Success, "Reclamer", "Panne reclamer avec succes");
                else
                    SessionMsg(Helper.Error, "Ne pas reclamer", "Panne ne pas reclamer!");

                return RedirectToAction("BreakDowns", "BreakDown");
            }
            return RedirectToAction("BreakDowns", "BreakDown");
        }

        public IActionResult GetComputer()
        {
            return Json(_db.Computers.OrderBy(n => n.MaterialName).ToList());
        }

        public IActionResult GetServer()
        {
            return Json(_db.Servers.OrderBy(n => n.MaterialName).ToList());
        }

        public IActionResult GetPrinter()
        {
            return Json(_db.Printers.OrderBy(n => n.MaterialName).ToList());
        }

        public IActionResult GetOther()
        {
            return Json(_db.Materials.OrderBy(n => n.MaterialName).ToList());
        }

        public void SessionMsg(string msgType, string title, string msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, msgType);
            HttpContext.Session.SetString(Helper.Title, title);
            HttpContext.Session.SetString(Helper.Msg, msg);
        }
    }
}