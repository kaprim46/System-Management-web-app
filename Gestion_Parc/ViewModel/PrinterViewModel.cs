using System;
using System.Xml.Linq;
using Gestion_Parc.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gestion_Parc.ViewModel
{
	public class PrinterViewModel
	{
		public List<Printer> Printers { get; set; } = new();
		public List<LogPrinter> LogPrinters { get; set; } = new();
		public Printer NewPrinter { get; set; } = new();
        public List<Brand> Brands { get; set; } = new();
        public List<Department> Departments { get; set; } = new();
    }

	public static class Printers
	{
        static public string MaterialName { get; set; } = string.Empty;
        static public string Brand { get; set; } = string.Empty;
        static public string Description { get; set; } = string.Empty;
        static public string Color { get; set; } = string.Empty;
        static public int Quantity { get; set; }
        static public string Screen { get; set; } = string.Empty;
        static public string Department { get; set; } = string.Empty;
        static public string PrintingTechnology { get; set; } = string.Empty;
        static public string PrinterOutput { get; set; } = string.Empty;
        static public string MaximumPrintSpeed { get; set; } = string.Empty;
        static public string MaxPrintspeed { get; set; } = string.Empty;
    }
}

