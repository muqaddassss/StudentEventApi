using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEventAPI.Data;
using StudentEventAPI.Models;
using System.Linq;

namespace StudentEventAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/events
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events.ToListAsync();
            return Ok(events);
        }

        // GET: api/events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
                return NotFound();

            return Ok(@event);
        }

        // POST: api/events
        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event @event)
        {
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = @event.Id }, @event);
        }

        // PUT: api/events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event @event)
        {
            if (id != @event.Id)
                return BadRequest();

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Events.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
                return NotFound();

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ SEARCH: api/events/search?query=xyz
        [HttpGet("search")]
        public async Task<IActionResult> SearchEvents(string query)
        {
            var results = await _context.Events
                .Where(e => e.Name.Contains(query) || e.Venue.Contains(query))
                .ToListAsync();

            return Ok(results);
        }

        // ✅ FILTER by date: api/events/filter?date=2025-06-20
        [HttpGet("filter")]
        public async Task<IActionResult> FilterEvents([FromQuery] DateTime date)
        {
            var filteredEvents = await _context.Events
                .Where(e => e.Date.Date == date.Date)
                .ToListAsync();

            return Ok(filteredEvents);
        }
    }
}
