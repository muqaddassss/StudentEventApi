namespace StudentEventAPI.Models
{
    public class EventParticipant
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int ParticipantId { get; set; }
        public Participant Participant { get; set; } = null!;
    }
}
