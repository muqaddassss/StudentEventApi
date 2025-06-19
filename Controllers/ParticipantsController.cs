using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEventAPI.Data;
using StudentEventAPI.Models;

namespace StudentEventAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ParticipantsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ POST: api/participants
        [HttpPost]
        public async Task<IActionResult> RegisterParticipant(Participant participant)
        {
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Participant registered successfully" });
        }

        // ✅ GET: api/participants/event/5
        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetParticipantsByEvent(int eventId)
        {
            var participants = await _context.EventParticipants
                .Where(ep => ep.EventId == eventId)
                .Include(ep => ep.Participant)
                .Select(ep => ep.Participant)
                .ToListAsync();

            return Ok(participants);
        }
    }
}
