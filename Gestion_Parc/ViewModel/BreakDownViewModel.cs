using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Gestion_Parc.DataDbContext;
using Gestion_Parc.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gestion_Parc.ViewModel
{
	public class BreakDownViewModel
	{
		public List<BreakDown> AllBreakDowns { get; set; } = new();
        public List<BreakDown> AllBreakDownsForEachUser { get; set; } = new();
        public List<BreakDown> AllReported { get; set; } = new();
        public List<BreakDown> AllProgressed { get; set; } = new();
        public List<BreakDown> AllCompleted { get; set; } = new();
        public NewBreakDown NNewBreakDown { get; set; } = new(); 
    }
	public class NewBreakDown
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
        [Display(Name = "Category de materiel")]
        public string Category { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Materiels")]
        public string SubCategory { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; } = string.Empty;

        [Display(Name = "Image")]
        [Column(TypeName = "varchar(250)")]
        public string ImageUser { get; set; } = string.Empty;

        [Display(Name = "Nom et Prenom")]
        [Column(TypeName = "varchar(50)")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Department")]
        [Column(TypeName = "varchar(50)")]
        public string DepartmentName { get; set; } = string.Empty;
    }
}

