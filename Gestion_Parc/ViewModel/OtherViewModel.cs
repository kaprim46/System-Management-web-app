using System;
using Gestion_Parc.Models;

namespace Gestion_Parc.ViewModel
{
	public class OtherViewModel
	{
        public List<Other> Others { get; set; } = new();
        public List<LogOther> LogOthers { get; set; } = new();
        public Other NewOther { get; set; } = new();
        public List<Brand> Brands { get; set; } = new();
        public List<Department> Departments { get; set; } = new();
    }
}

