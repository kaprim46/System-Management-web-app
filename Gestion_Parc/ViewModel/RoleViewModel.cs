using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace Gestion_Parc.ViewModel
{
	public class RoleViewModel
	{
		public List<IdentityRole> Roles { get; set; } = new();
		public NewRole NewRole { get; set; } = new();
    }
    public class NewRole
    {
		public string? RoleId { get; set; }

        [Display(Name = "Service")]
        public string RoleName { get; set; } = string.Empty;
	}
}

