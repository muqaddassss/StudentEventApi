using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentEventAPI.Models
{
    public class Participant
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        // ✅ Many-to-many relationship with Event via EventParticipant
        public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
    }
}
