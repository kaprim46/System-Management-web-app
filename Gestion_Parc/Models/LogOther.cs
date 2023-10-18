using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Parc.Models
{
	public class LogOther : LogMaterial
	{
        public Guid MaterialId { get; set; }
        [ForeignKey("MaterialId")]
        public Other Other { get; set; }
    }
}

