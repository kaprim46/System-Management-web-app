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
    public class OtherController : Controller
    {
        private readonly AppDbContxt _db;
        private readonly IServiceRepositoryOther<Other> _serviceOther;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceRepositoryLogOther<LogOther> _serviceLogOther;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IServiceRepositoryDepartment<Department> _serviceDepartment;
        private readonly IServiceRepositoryBrand<Brand> _serviceBrand;

        public OtherController(AppDbContxt db, IServiceRepositoryOther<Other> serviceOther,
                                UserManager<ApplicationUser> userManager, IServiceRepositoryLogOther<LogOther> serviceLogOther,
                                RoleManager<IdentityRole> roleManager, IServiceRepositoryDepartment<Department> serviceDepartment,
                                IServiceRepositoryBrand<Brand> serviceBrand)
        {
            _db = db;
            _serviceOther = serviceOther;
            _userManager = userManager;
            _serviceLogOther = serviceLogOther;
            _roleManager = roleManager;
            _serviceDepartment = serviceDepartment;
            _serviceBrand = serviceBrand;
        }
        public IActionResult Index()
        {
            var Others = new OtherViewModel
            {
                LogOthers = _serviceLogOther.GetAll(),
                Others = _serviceOther.GetAll(),
            };
            return View(Others);
        }

        public IActionResult Create()
        {
            ViewBag.Colors = Enum.GetValues(typeof(Helper.Color))
                     .Cast<Helper.Color>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();
            var Others = new OtherViewModel
            {
                Departments = _serviceDepartment.GetAll(),
                Brands = _serviceBrand.GetAll(),
                NewOther = new Other()
            };
            return View(Others);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OtherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                if (_serviceOther.Save(model.NewOther) && _serviceLogOther.Save(model.NewOther.MaterialId, Guid.Parse(userId)))
                    SessionMsg(Helper.Success, "Ajouter", "Materiel ajouter avec succes");
                else
                    SessionMsg(Helper.Error, "Ne pas Ajouter", "Materiel ne pas ajouter!");

                return RedirectToAction("Index", "Other");
            }
            return RedirectToAction("Index", "Other");
        }

        public IActionResult Edit(Guid id)
        {
            ViewBag.Colors = Enum.GetValues(typeof(Helper.Color))
                     .Cast<Helper.Color>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();
            var Other = _serviceOther.FindBy(id);
            if (Other != null)
            {
                var updatedOther = new OtherViewModel
                {
                    NewOther = new Other
                    {
                        MaterialId = Other.MaterialId,
                        MaterialName = Other.MaterialName,
                        Description = Other.Description,
                        Department = Other.Department,
                        Brand = Other.Brand,
                        Color = Other.Color
                    },
                    Brands = _serviceBrand.GetAll(),
                    Departments = _serviceDepartment.GetAll()
                };
                return View(updatedOther);
            }
            return View(Other);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OtherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Other = model.NewOther;
                var userId = _userManager.GetUserId(User);
                var Id = model.NewOther.MaterialId;
                if (_serviceOther.Edit(Other) && _serviceLogOther.Update(Id, Guid.Parse(userId)))
                    SessionMsg(Helper.Success, "Modifier", "Materiel modifier avec succes");
                else
                    SessionMsg(Helper.Error, "Ne pas modifier", "Materiel ne pas modifier!");

                return RedirectToAction("Index", "Other");
            }
            ViewBag.Colors = Enum.GetValues(typeof(Helper.Color))
                     .Cast<Helper.Color>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();

            return View(model);
        }


        public IActionResult Delete(Guid id)
        {
            var userId = _userManager.GetUserId(User);
            if (_serviceOther.Delete(id) && _serviceLogOther.Delete(id, Guid.Parse(userId)))
            {
                SessionMsg(Helper.Success, "Supprimer", "Materiel supprimer avec succes");
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteLog(Guid id)
        {
            if (_serviceLogOther.DeleteLog(id))
                return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Index));
        }

        public void SessionMsg(string msgType, string title, string msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, msgType);
            HttpContext.Session.SetString(Helper.Title, title);
            HttpContext.Session.SetString(Helper.Msg, msg);
        }
    }
}