using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEventAPI.Data;
using StudentEventAPI.Models;

namespace StudentEventAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistrationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] EventParticipant registration)
        {
            var ev = await _context.Events.FindAsync(registration.EventId);
            if (ev == null)
                return NotFound("Event not found.");

            var participant = await _context.Participants.FindAsync(registration.ParticipantId);
            if (participant == null)
                return NotFound("Participant not found.");

            bool alreadyRegistered = await _context.EventParticipants
                .AnyAsync(r => r.ParticipantId == registration.ParticipantId && r.EventId == registration.EventId);

            if (alreadyRegistered)
                return BadRequest("Already registered.");

            _context.EventParticipants.Add(registration);
            await _context.SaveChangesAsync();

            return Ok("Participant registered successfully.");
        }
    }
}
