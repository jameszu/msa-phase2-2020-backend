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
    public class UserController : ControllerBase
    {
        private AppDatabase _context;
        
        
        public UserController(AppDatabase context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }
        // GET: api/User/2
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var address = await _context.User.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }
        [HttpPost]

        public async Task<IActionResult> CreateName(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.User_ID }, User);
        }

        [HttpPut("Id")]
        //[Route("UpdateName")]
        public async Task<IActionResult> PutName(int id, User user)
        {
            if (id != user.User_ID)
            {
                return BadRequest();
            }

            var updateUser = await _context.User.FirstOrDefaultAsync(s => s.User_ID == user.User_ID);
            _context.Entry(updateUser).State = EntityState.Modified;

            updateUser.Name = user.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExist(id))
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

        private bool UserExist(int id)
        {
            return _context.User.Any(e => e.User_ID == id);
        }
    }
}
