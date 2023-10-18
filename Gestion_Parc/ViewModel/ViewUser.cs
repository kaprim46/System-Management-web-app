using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Gestion_Parc.ViewModel
{
	[NotMapped]
    public class ViewUser
    {
		public string? Id { get; set; }

        [Display(Name = "Nom et prenom")]
        public string? FullName { get; set; }

        public string? Email { get; set; }

        [Display(Name = "Image")]
        public string? ImageUser { get; set; }

        [Display(Name = "Active Utlisateur")]
        public bool ActiveUser { get; set; }

        [Display(Name = "Service")]
        public string? Role { get; set; }
        public string? Department { get; set; }
    }
}

