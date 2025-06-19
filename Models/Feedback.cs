using System;
using System.ComponentModel.DataAnnotations;

namespace StudentEventAPI.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public int ParticipantId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        // Navigation
        public Event Event { get; set; }
        public Participant Participant { get; set; }
    }
}
