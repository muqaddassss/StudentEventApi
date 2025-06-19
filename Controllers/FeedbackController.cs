using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEventAPI.Data;
using StudentEventAPI.Models;
using StudentEventAPI.Models.DTOs; // ✅ This line is necessary for FeedbackDto

namespace StudentEventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FeedbackController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFeedback([FromBody] FeedbackDto feedbackDto)
        {
            var evnt = await _context.Events.FindAsync(feedbackDto.EventId);
            if (evnt == null)
                return NotFound("Event not found.");

            var participant = await _context.Participants.FindAsync(feedbackDto.ParticipantId);
            if (participant == null)
                return NotFound("Participant not found.");

            if (evnt.Date > DateTime.Now)
                return BadRequest("Feedback can only be submitted after the event date.");

            if (feedbackDto.Rating < 1 || feedbackDto.Rating > 5)
                return BadRequest("Rating must be between 1 and 5.");

            var feedback = new Feedback
            {
                EventId = feedbackDto.EventId,
                ParticipantId = feedbackDto.ParticipantId,
                Rating = feedbackDto.Rating,
                Comment = feedbackDto.Comment
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Feedback submitted successfully." });
        }
    }
}
