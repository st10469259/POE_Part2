using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POE_Part2.Database;
using POE_Part2.Models;

namespace POE_Part2.Controllers
{
    [Route("api/enrolments")]
    [ApiController]
    public class EnrolmentsController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public EnrolmentsController(RaceDayContext context)
        {
            _context = context;
        }

        // POST: /api/enrolments/{id}/enrol
        [HttpPost("{id}/enrol")]
        public async Task<IActionResult> EnrolInEvent(int id, [FromBody] EventEnrolment enrolment)
        {
            if (enrolment == null)
            {
                return BadRequest(new { message = "Invalid enrolment data." });
            }

            enrolment.EventID = id;

            var existingEnrolment = await _context.EventEnrolments
                .FirstOrDefaultAsync(e => e.EventID == id && e.ParticipantID == enrolment.ParticipantID);

            if (existingEnrolment != null)
            {
                return Conflict(new { message = "Participant is already enrolled in this event." });
            }

            _context.EventEnrolments.Add(enrolment);
            await _context.SaveChangesAsync();

            return StatusCode(201, enrolment);
        }

        // GET: /api/enrolments/event/{id}
        [HttpGet("event/{id}")]
        public async Task<IActionResult> GetEventEnrolments(int id)
        {
            var enrolments = await _context.EventEnrolments
                .Where(e => e.EventID == id)
                .Include(e => e.Participant)
                .ThenInclude(p => p.User)
                .Include(e => e.Category)
                .ToListAsync();

            return Ok(enrolments);
        }
    }
}