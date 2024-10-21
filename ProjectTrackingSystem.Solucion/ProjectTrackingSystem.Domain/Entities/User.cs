﻿namespace ProjectTrackingSystem.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string  Role { get; set; }

        public required ICollection<Task> Tasks { get; set; }
    }
}
