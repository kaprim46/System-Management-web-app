using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gestion_Parc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Gestion_Parc.IRepository;
using Gestion_Parc.ViewModel;
using Gestion_Parc.DataDbContext;

namespace Gestion_Parc.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IServiceRepositoryComputer<Computer> _serviceComputer;
    private readonly IServiceRepositoryPrinter<Printer> _servicePrinter;
    private readonly AppDbContxt _db;
    private readonly IServiceRepositoryServer<Server> _serviceServer;
    private readonly IServiceRepositoryOther<Other> _serviceOther;

    public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager,
                         RoleManager<IdentityRole> roleManager, IServiceRepositoryComputer<Computer> serviceComputer,
                         IServiceRepositoryPrinter<Printer> servicePrinter, AppDbContxt db,
                         IServiceRepositoryServer<Server> serviceServer,
                         IServiceRepositoryOther<Other> serviceOther)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _serviceComputer = serviceComputer;
        _servicePrinter = servicePrinter;
        _db = db;
        _serviceServer = serviceServer;
        _serviceOther = serviceOther;
    }
    
    public IActionResult Index()
    {
        var view = new HomeViewModel
        {
            Computers = _serviceComputer.GetAll(),
            Printers = _servicePrinter.GetAll(),
            Roles = _roleManager.Roles.ToList(),
            Users = _db.ViewUsers.ToList(),
            Servers = _serviceServer.GetAll(),
            Others = _serviceOther.GetAll()
        };
        return View(view);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Denied()
    {
        return View();
    }
}

