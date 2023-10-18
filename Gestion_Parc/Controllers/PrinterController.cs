using System;
using System.Collections.Generic;
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
    public class PrinterController : Controller
    {
        private readonly AppDbContxt _db;
        private readonly IServiceRepositoryPrinter<Printer> _servicePrinter;
        private readonly IServiceRepositoryLogPrinter<LogPrinter> _serviceLogPrinter;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceRepositoryBrand<Brand> _serviceBrand;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IServiceRepositoryDepartment<Department> _serviceDepartment;

        public PrinterController(AppDbContxt db, IServiceRepositoryPrinter<Printer> servicePrinter,
                                IServiceRepositoryLogPrinter<LogPrinter> serviceLogPrinter,
                                UserManager<ApplicationUser> userManager, IServiceRepositoryBrand<Brand> serviceBrand,
                                RoleManager<IdentityRole> roleManager, IServiceRepositoryDepartment<Department> serviceDepartment)
        {
            _db = db;
            _servicePrinter = servicePrinter;
            _serviceLogPrinter = serviceLogPrinter;
            _userManager = userManager;
            _serviceBrand = serviceBrand;
            _roleManager = roleManager;
            _serviceDepartment = serviceDepartment;
        }
        public IActionResult Index()
        {
            var printers = new PrinterViewModel
            {
                Printers = _servicePrinter.GetAll(),
                LogPrinters = _serviceLogPrinter.GetAll(),
                Brands = _serviceBrand.GetAll(),
                Departments = _serviceDepartment.GetAll(),
                NewPrinter = new Printer()
            };
            return View(printers);
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

            ViewBag.Output = Enum.GetValues(typeof(Helper.Output))
                     .Cast<Helper.Output>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();

            ViewBag.Technology = Enum.GetValues(typeof(Helper.Technology))
                     .Cast<Helper.Technology>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();
            var components = new PrinterViewModel
            {
                Brands = _serviceBrand.GetAll(),
                Departments = _serviceDepartment.GetAll(),
                NewPrinter = new Printer()
            };

            return View(components);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PrinterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                if (_servicePrinter.Save(model.NewPrinter) && _serviceLogPrinter.Save(model.NewPrinter.MaterialId, Guid.Parse(userId)))
                    SessionMsg(Helper.Success, "Saved", "Printer Saved successfully");
                else
                    SessionMsg(Helper.Error, "Not Saved", "Printer not saved!");

                return RedirectToAction("Index", "Printer");
            }
            return RedirectToAction("Index", "Printer");
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

            ViewBag.Output = Enum.GetValues(typeof(Helper.Output))
                     .Cast<Helper.Output>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();

            ViewBag.Technology = Enum.GetValues(typeof(Helper.Technology))
                     .Cast<Helper.Technology>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();

            var printer = _servicePrinter.FindBy(id);
            if (printer != null)
            {
                var existPrinter = new PrinterViewModel
                {
                    NewPrinter = new Printer
                    {
                        MaterialId = printer.MaterialId,
                        MaterialName = printer.MaterialName,
                        MaximumPrintSpeed = printer.MaximumPrintSpeed,
                        MaxPrintspeed = printer.MaxPrintspeed,
                        Brand = printer.Brand,
                        Department = printer.Department,
                        Color = printer.Color,
                        PrinterOutput = printer.PrinterOutput,
                        PrintingTechnology = printer.PrintingTechnology,
                        Description = printer.Description,
                        Quantity = printer.Quantity
                    },
                    Brands = _serviceBrand.GetAll(),
                    Departments = _serviceDepartment.GetAll()
                };
                return View(existPrinter);
            }
            
            return View(printer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PrinterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var printer = model.NewPrinter;
                var userId = _userManager.GetUserId(User);
                var Id = model.NewPrinter.MaterialId;
                if (_servicePrinter.Edit(printer) && _serviceLogPrinter.Update(Id, Guid.Parse(userId)))

                    SessionMsg(Helper.Success, "Updated", "Printer Updated successfully");
                else
                    SessionMsg(Helper.Error, "Not Updated", "Printer not Updated!");



                return RedirectToAction("Index", "Printer");
            }
            ViewBag.Colors = Enum.GetValues(typeof(Helper.Color))
                     .Cast<Helper.Color>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();

            ViewBag.Output = Enum.GetValues(typeof(Helper.Output))
                     .Cast<Helper.Output>()
                     .Select(c => new SelectListItem
                     {
                         Value = c.ToString(),
                         Text = c.ToString()
                     })
                     .ToList();

            ViewBag.Technology = Enum.GetValues(typeof(Helper.Technology))
                     .Cast<Helper.Technology>()
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
            if (_servicePrinter.Delete(id) && _serviceLogPrinter.Delete(id, Guid.Parse(userId)))
            {
                SessionMsg(Helper.Success, "Delete", "Printer Deleted successfully");
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteLog(Guid id)
        {
            if (_serviceLogPrinter.DeleteLog(id))
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