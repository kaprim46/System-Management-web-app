using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Drawing2D;

namespace Gestion_Parc.Models
{
    public class Material
	{
        
        public virtual Guid MaterialId { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Display(Name = "Nom de materiel")]
        public string MaterialName { get; set; } = string.Empty;
        public int Quantity { get; set; }

        [Display(Name = "Couleur")]
        [Column(TypeName = "varchar(20)")]
        public string Color { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string Description { get; set; } = string.Empty;

        public int CurrentState { get; set; }

        public string Department { get; set; } = string.Empty;

        [Display(Name = "Marque")]
        public string Brand { get; set; } = string.Empty;

    }
}

