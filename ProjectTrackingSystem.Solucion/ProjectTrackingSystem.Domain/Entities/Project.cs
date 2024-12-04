

using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTrackingSystem.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }

        // Inicializa con valores predeterminados
        public  string Name { get; set; } = string.Empty;
        public  string Description { get; set; } = string.Empty;

        [Column("start_date")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Column("end_date")] 
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);

        public  ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
