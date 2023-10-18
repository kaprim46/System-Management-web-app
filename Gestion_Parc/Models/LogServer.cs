using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Parc.Models
{
	public class LogServer : LogMaterial
	{
        public Guid MaterialId { get; set; }
        [ForeignKey("MaterialId")]
        public Server Server { get; set; }
    }
}

