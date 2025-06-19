using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentEventAPI.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        // ✅ Many-to-many relationship with Participant via EventParticipant
        public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
    }
}
