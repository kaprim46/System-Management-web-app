using System;
using Gestion_Parc.Models;
using Microsoft.AspNetCore.Identity;

namespace Gestion_Parc.ViewModel
{
	public class HomeViewModel
	{
        public List<Printer> Printers { get; set; } = new();
        public List<Computer> Computers { get; set; } = new();
        public List<Server> Servers { get; set; } = new();
        public List<Other> Others { get; set; } = new();
        public List<IdentityRole> Roles { get; set; } = new();
        public List<ViewUser> Users { get; set; } = new();

    }
}

