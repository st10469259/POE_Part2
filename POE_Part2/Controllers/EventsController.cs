using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POE_Part2.Database;
using POE_Part2.Models;

namespace POE_Part2.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public EventsController(RaceDayContext context)
        {
            _context = context;
        }

        // GET: /api/events
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events
                .Include(e => e.Organiser)
                .ToListAsync();

            return Ok(events);
        }

        // GET: /api/events/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var raceEvent = await _context.Events
                .Include(e => e.Organiser)
                .FirstOrDefaultAsync(e => e.EventID == id);

            if (raceEvent == null)
            {
                return NotFound(new { message = "Event not found." });
            }

            return Ok(raceEvent);
        }

        // POST: /api/events
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Event raceEvent)
        {
            if (raceEvent == null)
            {
                return BadRequest(new { message = "Invalid event data." });
            }

            _context.Events.Add(raceEvent);
            await _context.SaveChangesAsync();

            return StatusCode(201, raceEvent);
        }
    }
}