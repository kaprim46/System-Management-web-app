using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Parc.Models
{
	public class LogPrinter : LogMaterial
	{
        public Guid MaterialId { get; set; }
        [ForeignKey("MaterialId")]
        public Printer Printer { get; set; }
    }
}

