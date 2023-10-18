using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Gestion_Parc.Models
{
	public class ApplicationUser:IdentityUser
	{
		[Required]
		[StringLength(50)]
		public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string ImageUser { get; set; } = string.Empty;
		public bool ActiveUser { get; set; }

        [Required]
        [StringLength(50)]
        public string DepartmentName { get; set; } = string.Empty;
    }
}

