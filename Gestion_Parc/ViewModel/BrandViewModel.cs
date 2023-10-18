using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Gestion_Parc.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gestion_Parc.ViewModel
{
	public class BrandViewModel
	{
        public List<Brand> Brands { get; set; } = new();
        public List<LogBrand> LogBrands { get; set; } = new();
		public Brand NewBrand { get; set; } = new();
        public NewBrand NNewBrand { get; set; } = new();
    }
    public class NewBrand
    {

        public Guid? BrandId { get; set; }

        [Display(Name = "Marque")]
        [Column(TypeName = "varchar(50)")]
        public string BrandName { get; set; } = string.Empty;

        public int CurrentState { get; set; }
    }
}

