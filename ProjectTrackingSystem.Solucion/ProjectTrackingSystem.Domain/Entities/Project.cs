

namespace ProjectTrackingSystem.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public required ICollection<Task> Tasks { get; set; }
    }
}
