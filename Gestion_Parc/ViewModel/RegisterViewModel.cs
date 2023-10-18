using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gestion_Parc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Parc.ViewModel
{
    [NotMapped]
    public class RegisterViewModel
    {
        public NewRegisterViewModel NewRegister { get; set; } = new();
        public List<ViewUser> Users { get; set; } = new();
        public List<IdentityRole> Roles { get; set; } = new();
        public List<Department> Departments { get; set; } = new();
    }

    [NotMapped]
    public class NewRegisterViewModel
	{
        public string? Code { get; set; }

        [Required]
		[Display(Name ="Nom et prenom")]
		[StringLength(50)]
		public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;

        //[Required]
        [Display(Name = "Image")]
        [StringLength(250)]
        public string ImageUser { get; set; } = string.Empty;

        [Display(Name = "Active Utlisateur")]
        public bool ActiveUser { get; set; }

        [Display(Name = "Service")]
        public string? RoleName { get; set; }

        [Display(Name = "Department")]
        public string? DepartmentName { get; set; }

        [Required]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirmer Mot de passe")]
        public string ComparePassword { get; set; } = string.Empty;
	}
    /*
    public class ListViewModel
    {
        
        public string? Code { get; set; }
        public List<ApplicationUser> UsersList { get; set; }
    }
    */
    [NotMapped]
    public class EditViewModel
    {
        public string? Code { get; set; }

        [Required]
        [Display(Name = "Nom et prenom")]
        [StringLength(50)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;

        //[Required]
        [Display(Name = "Image")]
        [StringLength(250)]
        public string? ImageUser { get; set; } = string.Empty;

        [Display(Name = "Active Utlisateur")]
        public bool ActiveUser { get; set; }

        public string? RoleName { get; set; }

        [Display(Name = "Department")]
        public string? DepartmentName { get; set; }

        [Display(Name = "Mot de passe")]
        public string? Password { get; set; } = string.Empty;

        
        [Compare("Password")]
        [Display(Name = "Confirmer Mot de passe")]
        public string? ComparePassword { get; set; } = string.Empty;
    }
    
}

