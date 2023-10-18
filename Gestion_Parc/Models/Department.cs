using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Gestion_Parc.Models
{
    public class Department
	{

        public Guid DepartmentId { get; set; }

        [Display(Name = "Department")]
        [Column(TypeName ="varchar(50)")]
        public string DepartmentName { get; set; } = string.Empty;

        public int CurrentState { get; set; }
    }
}

