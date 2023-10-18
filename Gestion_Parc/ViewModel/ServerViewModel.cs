using System;
using Gestion_Parc.Models;

namespace Gestion_Parc.ViewModel
{
	public class ServerViewModel
	{
        public List<Server> Servers { get; set; } = new();
        public List<LogServer> LogServers { get; set; } = new();
        public Server NewServer { get; set; } = new();
        public List<Brand> Brands { get; set; } = new();
        public List<Department> Departments { get; set; } = new();
    }
}

