using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_Parc.Models
{
	public class Computer : Material
	{
        public int Memory { get; set; }

        [Display(Name = "Disque dure")]
        public int DiskSpace { get; set; }

        public float Processor { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Resolution d'ecran")]
        public string Screen { get; set; } = string.Empty;

        [Column(TypeName = "varchar(20)")]
        [Display(Name = "Systeme d'exploitation")]
        public string OS { get; set; } = string.Empty;

        [Column(TypeName = "varchar(100)")]
        [Display(Name = "CPU")]
        public string Cpu { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Carte graphic")]
        public string GraphicsCard { get; set; } = string.Empty;


    }
}

