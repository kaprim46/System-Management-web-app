using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Gestion_Parc.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gestion_Parc.ViewModel
{
	public class ComputerViewModel
	{
		public List<LogComputer> LogComputers { get; set; } = new();
		public List<Brand> Brands { get; set; } = new();
		public List<Department> Departments { get; set; } = new();
        public Computer NewComputer { get; set; } = new();
        public List<Computer> Computers { get; set; } = new();
    }
	public class NNewComputer
	{
        public Guid Id { get; set; }

        [Display(Name = "Nom de materiel")]
        [Column(TypeName = "varchar(50)")]
        public string MaterialName { get; set; } = string.Empty;

        [Display(Name = "Quantite")]
        public int Quantity { get; set; }

        [Display(Name = "Couleur")]
        [Column(TypeName = "varchar(20)")]
        public string Color { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Memoire")]
        public int Memory { get; set; }

        [Display(Name = "Disque dure")]
        public int DiskSpace { get; set; }

        [Display(Name = "Processeur")]
        public float Processor { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Resolution d'ecran")]
        public string Screen { get; set; } = string.Empty;

        [Column(TypeName = "varchar(20)")]
        [Display(Name = "system d'exploitation")]
        public string OS { get; set; } = string.Empty;

        [Column(TypeName = "varchar(100)")]
        [Display(Name = "CPU")]
        public string Cpu { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Carte Graphique")]
        public string GraphicsCard { get; set; } = string.Empty;

        [Display(Name = "Marque")]
        public string? Brand { get; set; }

        public string? Department { get; set; }
    }

    public static class Computers
    {
        static public string MaterialName { get; set; } = string.Empty;
        static public string Brand { get; set; } = string.Empty;
        static public string Description { get; set; } = string.Empty;
        static public int Memory { get; set; }
        static public string Color { get; set; } = string.Empty;
        static public float Processor { get; set; }
        static public string GraphicsCard { get; set; } = string.Empty;
        static public int DiskSpace { get; set; }
        static public int Quantity { get; set; }
        static public string Screen { get; set; } = string.Empty;
        static public string Department { get; set; } = string.Empty;
        static public string Cpu { get; set; } = string.Empty;
        static public string OS { get; set; } = string.Empty;
    }
}

