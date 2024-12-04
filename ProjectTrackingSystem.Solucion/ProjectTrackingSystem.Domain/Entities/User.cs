using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTrackingSystem.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Column("firstname")]
        public string FirstName { get; set; } = string.Empty;

        [Column("lastname")]
        public  string LastName { get; set; } = string.Empty;

        [Column("role")]
        public string  Role { get; set; } = string.Empty;

        public  ICollection<Task> Tasks { get; set; }
        public User()
        {
            Tasks = new List<Task>();
        }
    }
}
