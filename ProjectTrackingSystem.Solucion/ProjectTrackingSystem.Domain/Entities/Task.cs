

using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTrackingSystem.Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public  string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        [Column("due_date")]
        public DateTime DueDate { get; set; }

        [Column("project_id")]
        public int ProjectId { get; set; } 

        public  Project Project { get; set; }

        [Column("user_id")]
        public int UserId { get; set; } 
        public  User User { get; set; } 



    }
}
