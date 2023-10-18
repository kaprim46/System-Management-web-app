using System;
using Gestion_Parc.Models;

namespace Gestion_Parc.ViewModel
{
    public class StatisticViewModel
    {
        public List<Computer> Computers { get; set; } = new();
        public List<LogComputer> PeriodComputers { get; set; } = new();
        public List<Printer> Printers { get; set; } = new();
        public List<Server> Servers { get; set; } = new();
        public List<Other> Others { get; set; } = new();
        public List<BreakDown> CopmuterBreakDowns { get; set; } = new();
        public List<BreakDown> ServerBreakDowns { get; set; } = new();
        public List<BreakDown> PrintersBreakDowns { get; set; } = new();
        public List<BreakDown> OthersBreakDowns { get; set; } = new();
        public List<Computer> UnusedComputer { get; set; } = new();
        public List<Printer> UnusedPrinter { get; set; } = new();
        public List<Server> UnusedServer { get; set; } = new();
        public List<Other> UnusedOther { get; set; } = new();
        public List<BreakDown> AllReported { get; set; } = new();
        public List<BreakDown> AllProgressed { get; set; } = new();
        public List<BreakDown> AllCompleted { get; set; } = new();
    }
}

