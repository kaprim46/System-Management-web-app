using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.IRepository;
using Gestion_Parc.IRepository.ServiceRepository;
using Gestion_Parc.Models;
using Gestion_Parc.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Gestion_Parc.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IServiceRepositoryComputer<Computer> _serviceComputer;
        private readonly IServiceRepositoryPrinter<Printer> _servicePrinter;
        private readonly IServiceRepositoryServer<Server> _serviceServer;
        private readonly IServiceRepositoryOther<Other> _serviceOther;
        private readonly IServiceRepositoryLogComputer<LogComputer> _serviceLogComputer;
        private readonly IServiceRepositoryLogPrinter<LogPrinter> _serviceLogPrinter;
        private readonly IServiceRepositoryLogServer<LogServer> _serviceLogServer;
        private readonly IServiceRepositoryLogOther<LogOther> _serviceLogOther;
        private readonly IServiceRepositoryBreakDown<BreakDown> _serviceBreakDown;

        public StatisticsController(IServiceRepositoryComputer<Computer> serviceComputer,
                                    IServiceRepositoryPrinter<Printer> servicePrinter,
                                    IServiceRepositoryServer<Server> serviceServer,
                                    IServiceRepositoryOther<Other> serviceOther,
                                    IServiceRepositoryLogComputer<LogComputer> serviceLogComputer,
                                    IServiceRepositoryLogPrinter<LogPrinter> serviceLogPrinter,
                                    IServiceRepositoryLogServer<LogServer> serviceLogServer,
                                    IServiceRepositoryLogOther<LogOther> serviceLogOther,
                                    IServiceRepositoryBreakDown<BreakDown> serviceBreakDown)
        {
            _serviceComputer = serviceComputer;
            _servicePrinter = servicePrinter;
            _serviceServer = serviceServer;
            _serviceOther = serviceOther;
            _serviceLogComputer = serviceLogComputer;
            _serviceLogPrinter = serviceLogPrinter;
            _serviceLogServer = serviceLogServer;
            _serviceLogOther = serviceLogOther;
            _serviceBreakDown = serviceBreakDown;
        }
        public IActionResult Materials()
        {
            var materials = new StatisticViewModel
            {
                Computers = _serviceComputer.GetAll(),
                Printers = _servicePrinter.GetAll(),
                Servers = _serviceServer.GetAll(),
                Others = _serviceOther.GetAll(),
                CopmuterBreakDowns = _serviceBreakDown.GetAllComputers(),
                PrintersBreakDowns = _serviceBreakDown.GetAllPrinters(),
                ServerBreakDowns = _serviceBreakDown.GetAllServers(),
                OthersBreakDowns = _serviceBreakDown.GetAllOthers(),
                UnusedComputer = _serviceComputer.GetAllUnUsed(),
                UnusedPrinter = _servicePrinter.GetAllUnUsed(),
                UnusedServer = _serviceServer.GetAllUnUsed(),
                UnusedOther = _serviceOther.GetAllUnUsed(),
                AllCompleted = _serviceBreakDown.GetAllCompleted(),
                AllProgressed = _serviceBreakDown.GetAllInProgress(),
                AllReported = _serviceBreakDown.GetAllReported()

            };
            return View(materials);
        }
    }
}