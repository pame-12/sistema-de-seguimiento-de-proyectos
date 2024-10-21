

namespace ProjectTrackingSystem.Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public string? Status { get; set; }
        public DateTime DueDate { get; set; }

        public int ProjectId { get; set; }
        public required Project Project { get; set; }

        public int UserId { get; set; }
        public required User User { get; set; }
    }
}
