using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POE_Part2.Database;
using POE_Part2.Models;

namespace POE_Part2.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public ResultsController(RaceDayContext context)
        {
            _context = context;
        }

        // POST: /api/events/{id}/results
        [HttpPost("{id}/results")]
        public async Task<IActionResult> RecordResult(int id, [FromBody] Result result)
        {
            if (result == null)
            {
                return BadRequest(new { message = "Invalid result data." });
            }

            var enrolment = await _context.EventEnrolments
                .FirstOrDefaultAsync(e => e.EnrolmentID == result.EnrolmentID && e.EventID == id);

            if (enrolment == null)
            {
                return BadRequest(new { message = "Invalid enrolment record for this event." });
            }

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return StatusCode(201, result);
        }

        // GET: /api/events/{id}/results
        [HttpGet("{id}/results")]
        public async Task<IActionResult> GetEventResults(int id)
        {
            var results = await _context.Results
                .Include(r => r.EventEnrolment)
                .ThenInclude(ee => ee.Participant)
                .ThenInclude(p => p.User)
                .Where(r => r.EventEnrolment.EventID == id)
                .OrderBy(r => r.Position)
                .ToListAsync();

            if (!results.Any())
            {
                return NotFound(new { message = "No results found for this event." });
            }

            return Ok(results);
        }
    }
}