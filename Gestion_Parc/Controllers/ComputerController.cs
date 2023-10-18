using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.IRepository;
using Gestion_Parc.IRepository.ServiceRepository;
using Gestion_Parc.Models;
using Gestion_Parc.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gestion_Parc.Controllers
{
    public class ComputerController : Controller
    {
        private readonly AppDbContxt _db;
        private readonly IServiceRepositoryComputer<Computer> _serviceComputer;
        private readonly IServiceRepositoryLogComputer<LogComputer> _serviceLogComputer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceRepositoryDepartment<Department> _serviceDepartment;
        private readonly IServiceRepositoryBrand<Brand> _serviceBrand;

        public ComputerController(AppDbContxt db, IServiceRepositoryComputer<Computer> serviceComputer,
                                  IServiceRepositoryLogComputer<LogComputer> serviceLogComputer,
                                  UserManager<ApplicationUser> userManager,
                                  IServiceRepositoryDepartment<Department> serviceDepartment,
                                  IServiceRepositoryBrand<Brand> serviceBrand)
        {
            _db = db;
            _serviceComputer = serviceComputer;
            _serviceLogComputer = serviceLogComputer;
            _userManager = userManager;
            _serviceDepartment = serviceDepartment;
            _serviceBrand = serviceBrand;
        }
        public IActionResult Index()
        {
            var computers = new ComputerViewModel
            {
                Computers = _serviceComputer.GetAll(),
                LogComputers = _serviceLogComputer.GetAll(),
                Brands = _serviceBrand.GetAll(),
                Departments = _serviceDepartment.GetAll()
            };
            return View(computers);
        }

        public IActionResult Create()
        {
            var colors = Enum.GetValues(typeof(Helper.Color))
                     .Cast<Helper.Color>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();
            var Oses = Enum.GetValues(typeof(Helper.Os))
                     .Cast<Helper.Os>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();
            var cpus = Enum.GetValues(typeof(Helper.Cpu))
                     .Cast<Helper.Cpu>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();
            var screens = Helper.ScreenResolution.Select(s => new SelectListItem { Value = s, Text = s }).ToList();
            var grphics = Helper.GraphicsCard.Select(g => new SelectListItem { Value = g, Text = g }).ToList();

            ViewBag.Cpu = cpus;
            ViewBag.Os = Oses;
            ViewBag.Colors = colors;
            ViewBag.Screens = screens;
            ViewBag.Graphics = grphics;
            var computers = new ComputerViewModel
            {
                Brands = _serviceBrand.GetAll(),
                Departments = _serviceDepartment.GetAll(),
                NewComputer = new Computer()
            };
            return View(computers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ComputerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var exist = _serviceComputer.FindBy(model.NewComputer.MaterialName);
                
                var userId = _userManager.GetUserId(User);
                if(_serviceComputer.Save(model.NewComputer) && _serviceLogComputer.Save(model.NewComputer.MaterialId, Guid.Parse(userId)))
                    SessionMsg(Helper.Success, "Ajouter", "Ordinateur Ajouter avec succes");
                else
                    SessionMsg(Helper.Error, "Ne pas ajouter", "Ordinateur ne pas Ajouter!");

                return RedirectToAction("Index", "Computer");
            }
            return RedirectToAction("Index", "Computer");
        }


        public IActionResult Edit(Guid id)
        {
            var colors = Enum.GetValues(typeof(Helper.Color))
                     .Cast<Helper.Color>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();
            var Oses = Enum.GetValues(typeof(Helper.Os))
                     .Cast<Helper.Os>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();
            var cpus = Enum.GetValues(typeof(Helper.Cpu))
                     .Cast<Helper.Cpu>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();
            var screens = Helper.ScreenResolution.Select(s => new SelectListItem { Value = s, Text = s }).ToList();
            var grphics = Helper.GraphicsCard.Select(g => new SelectListItem { Value = g, Text = g }).ToList();

            ViewBag.Cpu = cpus;
            ViewBag.Os = Oses;
            ViewBag.Colors = colors;
            ViewBag.Screens = screens;
            ViewBag.Graphics = grphics;

            var computer = _serviceComputer.FindBy(id);
            if (computer != null)
            {
                var newComputer = new ComputerViewModel
                {
                    NewComputer = new Computer
                    {
                        MaterialId = computer.MaterialId,
                        MaterialName = computer.MaterialName,
                        Memory = computer.Memory,
                        Color = computer.Color,
                        Cpu = computer.Cpu,
                        Description = computer.Description,
                        DiskSpace = computer.DiskSpace,
                        GraphicsCard = computer.GraphicsCard,
                        OS = computer.OS,
                        Processor = computer.Processor,
                        Brand = computer.Brand,
                        Department = computer.Department,
                        Quantity = computer.Quantity,
                        Screen = computer.Screen
                    },
                    Brands = _serviceBrand.GetAll(),
                    Departments = _serviceDepartment.GetAll()
                };
                return View(newComputer);
            }
            return View(computer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ComputerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var computer = model.NewComputer;
                var userId = _userManager.GetUserId(User);
                var Id = model.NewComputer.MaterialId;
                if (_serviceComputer.Edit(computer) && _serviceLogComputer.Update(Id, Guid.Parse(userId)))
                
                    SessionMsg(Helper.Success, "Modifier", "Ordinateur modifier avec succes");
                 else
                        SessionMsg(Helper.Error, "Ne pas modifier", "Ordinateur ne pas modifier!");

                return RedirectToAction("Index", "Computer");

            }
            var colors = Enum.GetValues(typeof(Helper.Color))
                     .Cast<Helper.Color>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();
            var Oses = Enum.GetValues(typeof(Helper.Os))
                     .Cast<Helper.Os>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();
            var cpus = Enum.GetValues(typeof(Helper.Cpu))
                     .Cast<Helper.Cpu>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();
            var screens = Helper.ScreenResolution.Select(s => new SelectListItem { Value = s, Text = s }).ToList();
            var grphics = Helper.GraphicsCard.Select(g => new SelectListItem { Value = g, Text = g }).ToList();

            ViewBag.Cpu = cpus;
            ViewBag.Os = Oses;
            ViewBag.Colors = colors;
            ViewBag.Screens = screens;
            ViewBag.Graphics = grphics;
            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            var userId = _userManager.GetUserId(User);
            if (_serviceComputer.Delete(id) && _serviceLogComputer.Delete(id, Guid.Parse(userId)))
            {
                SessionMsg(Helper.Success, "Supprimer", "Ordinateur supprimer avec succes");
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteLog(Guid id)
        {
            if (_serviceLogComputer.DeleteLog(id))
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