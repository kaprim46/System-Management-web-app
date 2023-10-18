using System;
namespace Gestion_Parc.Models
{
	public class LogMaterial
	{
        public Guid Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
    }
}

