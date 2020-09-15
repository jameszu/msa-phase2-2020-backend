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
    }
}
