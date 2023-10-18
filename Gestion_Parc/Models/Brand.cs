using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gestion_Parc.Models
{
    public class Brand
	{
        
        public Guid BrandId { get; set; }

        [Display(Name = "Brand")]
        [Column(TypeName = "varchar(50)")]
        public string BrandName { get; set; } = string.Empty;

        public int CurrentState { get; set; }
    }
}

