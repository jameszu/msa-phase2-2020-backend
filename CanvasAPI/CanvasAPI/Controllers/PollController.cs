using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanvasAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CanvasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private AppDatabase _context;

        public PollController(AppDatabase context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Poll>>> GetPoll()
        {
            return await _context.Poll.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Poll>> GetPoll(int id)
        {
            var the_poll = await _context.Poll.FindAsync(id);

            if (the_poll == null)
            {
                return NotFound();
            }

            return the_poll;
        }
        [HttpPost]

        public async Task<IActionResult> CreatePoll(Poll poll)
        {
            _context.Poll.Add(poll);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPoll), new { id = poll.Poll_ID }, poll);
        }

        [HttpPut("Id")]
        //[Route("UpdateName")]
        public async Task<IActionResult> PutName(int id, Poll poll)
        {
            if (id != poll.Poll_ID)
            {
                return BadRequest();
            }

            var updatePoll = await _context.Poll.FirstOrDefaultAsync(s => s.Pool_Title == poll.Pool_Title);
            _context.Entry(updatePoll).State = EntityState.Modified;

            updatePoll.Pool_Title = poll.Pool_Title;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool PollExist(int id)
        {
            return _context.Poll.Any(e => e.Poll_ID == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Poll>> DeletePoll(int id)
        {
            var poll = await _context.Poll.FindAsync(id);
            if (poll == null)
            {
                return NotFound();
            }

            _context.Poll.Remove(poll);
            await _context.SaveChangesAsync();

            return poll;
        }
    }
}
