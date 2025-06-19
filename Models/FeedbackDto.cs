namespace StudentEventAPI.Models.DTOs
{
    public class FeedbackDto
    {
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
