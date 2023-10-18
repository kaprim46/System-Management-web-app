using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gestion_Parc.Models
{
	public class BreakDown
	{
		public Guid Id { get; set; }

        [DataType(DataType.DateTime)]
		public DateTime Date { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Display(Name = "Notes")]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        [Display(Name ="Material Category")]
        public string Category { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Materiels")]
        public string SubCategory { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; } = string.Empty;

       
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; } = new();
    }
}

