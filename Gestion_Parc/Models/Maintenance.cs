using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Parc.Models
{
	public class Maintenance
	{
        public Guid Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string? Notes { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; } = string.Empty;

        
        public string UserId { get; set; } = string.Empty;
        public Guid BreakDownId { get; set; }


        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; } = new();

        [ForeignKey("BreakDownId")]
        public BreakDown BreakDown { get; set; } = new();
    }
}

