using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Parc.Models
{
    public class LogComputer : LogMaterial
	{
        public Guid MaterialId { get; set; }
        [ForeignKey("MaterialId")]
        public Computer Computer { get; set; }
    }
}

