using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Parc.Models
{
    public class LogBrand
	{
        public Guid Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }

        public Guid BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
    }
}

