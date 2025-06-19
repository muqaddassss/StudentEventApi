using System;
using System.ComponentModel.DataAnnotations;

namespace StudentEventAPI.Models
{
    public class Registration
    {
        public int Id { get; set; }

        [Required]
        public int ParticipantId { get; set; }

        [Required]
        public int EventId { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.Now;

        // Navigation Properties
        public Participant Participant { get; set; }
        public Event Event { get; set; }
    }
}
