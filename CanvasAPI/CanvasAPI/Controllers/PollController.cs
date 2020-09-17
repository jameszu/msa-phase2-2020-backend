using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanvasAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            if (!PollExist(id))
            {
                return NotFound();
            }

            var the_poll = await _context.Poll.Include(p => p.PollOption).FirstAsync(p => p.Poll_ID == id);
            if (the_poll == null)
            {
                return NotFound();
            }

            return the_poll;
        }

        [Route("{id}/Stats")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PollVoteStatsInfo>>> GetPollWithStats(int id)
        {
            if (!PollExist(id))
            {
                return NotFound();
            }

            var the_poll = await _context.Poll.Include(p => p.PollOption).FirstAsync(p => p.Poll_ID == id);
            if (the_poll == null)
            {
                return NotFound();
            }

            int total_vote = 0;
            foreach (var op in the_poll.PollOption)
            {
                op.Vote = _context.PollOption.Include(op => op.Vote).FirstAsync(o => o.Option_ID == op.Option_ID).Result.Vote;
                total_vote += (op.Vote == null ? 0 : op.Vote.Count);
            }

            var result = the_poll.PollOption.Select(
                x => new PollVoteStatsInfo
                {
                    Poll_ID = the_poll.Poll_ID,
                    Pool_Title = the_poll.Pool_Title,
                    Option_ID = x.Option_ID,
                    Text = x.Text,
                    Count = x.Vote == null ? 0 : x.Vote.Count,
                    Percentage = (x.Vote == null ? 0 : x.Vote.Count) * 1.00 / (total_vote <= 0 ? 1 : total_vote)
                }
            ).ToList();

            return result;
        }

        public class PollVoteStatsInfo
        {
            public int Poll_ID { get; set; }
            public string Pool_Title { get; set; }
            public int Option_ID { get; set; }
            public string Text { get; set; }
            public int Count { get; set; }
            public double Percentage { get; set; }
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
