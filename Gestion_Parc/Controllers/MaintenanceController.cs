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
    public class MaintenanceController : Controller
    {
        private readonly IServiceRepositoryMaintenance<Maintenance> _serviceMaintenance;
        private readonly AppDbContxt _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceRepositoryBreakDown<BreakDown> _serviceBreakDown;

        public MaintenanceController(IServiceRepositoryMaintenance<Maintenance> serviceMaintenance,
                                     AppDbContxt db, UserManager<ApplicationUser> userManager,
                                     IServiceRepositoryBreakDown<BreakDown> serviceBreakDown)
        {
            _serviceMaintenance = serviceMaintenance;
            _db = db;
            _userManager = userManager;
            _serviceBreakDown = serviceBreakDown;
        }
        public IActionResult Index()
        {
            var maintenances = new MaintenanceViewModel
            {
                Maintenances = _serviceMaintenance.GetAll()
            };
            return View(maintenances);
        }

        public IActionResult Create(Guid id)
        {
            var breakDown = _serviceBreakDown.FindBy(id);
            var maintenance = new MaintenanceViewModel
            {
                NewMaintenance = new Maintenance
                {

                    BreakDown = breakDown,
                    User = breakDown.User,
                }
            };
            var currentMaintenance = new NewMaintenance
            {
                Material = breakDown.SubCategory,
                UserName = breakDown.User.FullName,
                Type = breakDown.Type,
                BreakDownId = breakDown.Id
            };
            ViewBag.maintenanceType = Helper.MaintenanceType.Select(s =>
                                  new SelectListItem { Value = s, Text = s }).ToList();
            return View(currentMaintenance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewMaintenance model)
        {
            if (ModelState.IsValid)
            {
                var maintenance = new Maintenance();
                maintenance.BreakDown.SubCategory = model.Material;
                maintenance.BreakDown.User.FullName = model.UserName;
                maintenance.Notes = model.Notes;
                maintenance.Type = model.Type;
                maintenance.BreakDownId = model.BreakDownId;
                
                var userId = _userManager.GetUserId(User);
                if(_serviceMaintenance.Save(maintenance, Guid.Parse(userId)))
                    SessionMsg(Helper.Success, "Commencer", "Maintenance en cours...");
                else
                    SessionMsg(Helper.Error, "Ne pas commencer", "Maintenance ne commencer!");

                return RedirectToAction("BreakDowns", "BreakDown");
            }
            return View(model);
        }

        
        public IActionResult Confirm(Guid Id)
        {
            if (_serviceMaintenance.Confirm(Id))
                return RedirectToAction("Index", "Maintenance");
            
            return RedirectToAction("Index", "Maintenance");
        }

        public void SessionMsg(string msgType, string title, string msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, msgType);
            HttpContext.Session.SetString(Helper.Title, title);
            HttpContext.Session.SetString(Helper.Msg, msg);
        }
    }
}