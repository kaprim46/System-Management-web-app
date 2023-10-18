using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Gestion_Parc.Models
{
	public class Printer : Material
	{
        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Technologe d'impression")]
        public string PrintingTechnology { get; set; } = string.Empty;

        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Sortie imprimante")]
        public string PrinterOutput { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Vitesse d'impression maximale ")]
        public string MaximumPrintSpeed { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Max vitesse ")]
        public string MaxPrintspeed  { get; set; } = string.Empty;
    }
}

