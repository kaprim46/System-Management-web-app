using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gestion_Parc.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gestion_Parc.ViewModel
{
	public class MaintenanceViewModel
	{
		public List<Maintenance> Maintenances { get; set; } = new();
		public Maintenance NewMaintenance { get; set; } = new();
    }
    public class NewMaintenance
	{
        [Display(Name = "Materiel")]
        public string Material { get; set; } = string.Empty;

        public Guid BreakDownId { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string? Notes { get; set; } = string.Empty;


        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; } = string.Empty;


        public string UserName { get; set; } = string.Empty;
    }
}

