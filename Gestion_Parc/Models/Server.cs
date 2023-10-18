using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_Parc.Models
{
	public class Server : Material
	{
        [Display(Name = "Processeur")]
        [Column(TypeName = "varchar(50)")]
        public string Processor { get; set; } = string.Empty;

        [Display(Name = "Coeurs")]
        public int Cores { get; set; }

        [Display(Name = "Memoire")]
        public int Memory { get; set; } // in GB

        [Display(Name = "Stockage")]
        public int Storage { get; set; } // in GB
    }
}

