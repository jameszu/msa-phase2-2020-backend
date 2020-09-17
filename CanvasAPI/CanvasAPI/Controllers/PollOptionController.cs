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
    public class PollOptionController : ControllerBase
    {
        private AppDatabase _context;

        public PollOptionController(AppDatabase context)
        {
            _context = context;
        }

        [HttpGet]
        //[Route("GetOption")]
        public async Task<ActionResult<IEnumerable<PollOption>>> GetPollOption()
        {
            return await _context.PollOption.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PollOption>> GetPollOption(int id)
        {
            var the_pollOption = await _context.PollOption.Include(op => op.Vote).FirstAsync(op => op.Option_ID == id); //FindAsync(id);

            if (the_pollOption == null)
            {
                return NotFound();
            }

            return the_pollOption;
        }

        [Route("{id}/Vote")]
        [HttpPost]
        public async Task<ActionResult<PollOption>> AddVote(int id, PollOption pollOption)
        {
            if (id != pollOption.Option_ID)
            {
                return BadRequest();
            }

            var updatePollOption = await _context.PollOption.FirstOrDefaultAsync(s => s.Option_ID == pollOption.Option_ID);
            _context.Entry(updatePollOption).State = EntityState.Modified;

            updatePollOption.Vote = pollOption.Vote;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollOptionExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return updatePollOption;
        }

        private bool PollOptionExist(int id)
        {
            return _context.PollOption.Any(e => e.Option_ID == id);
        }
    }
}
