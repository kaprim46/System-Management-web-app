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
    public class ServerController : Controller
    {
        private readonly AppDbContxt _db;
        private readonly IServiceRepositoryServer<Server> _serviceServer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceRepositoryLogServer<LogServer> _serviceLogServer;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IServiceRepositoryDepartment<Department> _serviceDepartment;
        private readonly IServiceRepositoryBrand<Brand> _serviceBrand;

        public ServerController(AppDbContxt db, IServiceRepositoryServer<Server> serviceServer,
                                UserManager<ApplicationUser> userManager, IServiceRepositoryLogServer<LogServer> serviceLogServer,
                                RoleManager<IdentityRole> roleManager, IServiceRepositoryDepartment<Department> serviceDepartment,
                                IServiceRepositoryBrand<Brand> serviceBrand)
        {
            _db = db;
            _serviceServer = serviceServer;
            _userManager = userManager;
            _serviceLogServer = serviceLogServer;
            _roleManager = roleManager;
            _serviceDepartment = serviceDepartment;
            _serviceBrand = serviceBrand;
        }
        public IActionResult Index()
        {
            var servers = new ServerViewModel
            {
                LogServers = _serviceLogServer.GetAll(),
                Servers = _serviceServer.GetAll(),
            };
            return View(servers);
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
            var servers = new ServerViewModel
            {
                Departments = _serviceDepartment.GetAll(),
                Brands = _serviceBrand.GetAll(),
                NewServer = new Server()
            };
            return View(servers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                if(_serviceServer.Save(model.NewServer) && _serviceLogServer.Save(model.NewServer.MaterialId, Guid.Parse(userId)))
                    SessionMsg(Helper.Success, "Saved", "Server Saved successfully");
                else
                    SessionMsg(Helper.Error, "Not Saved", "Server not saved!");

                return RedirectToAction("Index", "Server");
            }
            return RedirectToAction("Index", "Server");
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
            var server = _serviceServer.FindBy(id);
            if (server != null)
            {
                var updatedServer = new ServerViewModel
                {
                    NewServer = new Server
                    {
                        MaterialId = server.MaterialId,
                        MaterialName = server.MaterialName,
                        Description = server.Description,
                        Department = server.Department,
                        Brand = server.Brand,
                        Color = server.Color,
                        Cores = server.Cores,
                        Memory =server.Memory,
                        Processor = server.Processor,
                        Storage = server.Storage,
                        Quantity = server.Quantity
                    },
                    Brands = _serviceBrand.GetAll(),
                    Departments = _serviceDepartment.GetAll()
                };
                return View(updatedServer);
            }
            return View(server);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ServerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var server = model.NewServer;
                var userId = _userManager.GetUserId(User);
                var Id = model.NewServer.MaterialId;
                if(_serviceServer.Edit(server) && _serviceLogServer.Update(Id, Guid.Parse(userId)))
                    SessionMsg(Helper.Success, "Updated", "Server Updated successfully");
                else
                    SessionMsg(Helper.Error, "Not Updated", "Server not Updated!");

                return RedirectToAction("Index", "Server");
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
            if (_serviceServer.Delete(id) && _serviceLogServer.Delete(id, Guid.Parse(userId)))
            {
                SessionMsg(Helper.Success, "Delete", "Server Deleted successfully");
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteLog(Guid id)
        {
            if (_serviceLogServer.DeleteLog(id))
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