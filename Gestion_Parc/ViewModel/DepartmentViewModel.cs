using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Gestion_Parc.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gestion_Parc.ViewModel
{
	public class DepartmentViewModel
	{
		public List<Department>? Departments { get; set; }
        public List<Department>? DepartmentsDeleted { get; set; }
        public Department NewDepartment { get; set; } = new();
        public NewDepartment NNewDepartment { get; set; } = new();
    }

    public class NewDepartment
    {
        public Guid? DepartmentId { get; set; }

        [Display(Name = "Department")]
        [Column(TypeName = "varchar(50)")]
        public string DepartmentName { get; set; } = string.Empty;

        public int CurrentState { get; set; }
    }

}

