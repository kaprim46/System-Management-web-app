using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Gestion_Parc.ViewModel
{
	public class LoginViewModel
    {
		public string Email { get; set; } = string.Empty;
        [Display(Name = "Mot de passe")]
        public string Password { get; set; } = string.Empty;
		public bool RememberMe { get; set; }
	}
}

