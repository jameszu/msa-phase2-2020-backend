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
            var user = await _context.User.Include(u => u.Poll).Include(u => u.Vote).FirstAsync(u => u.User_ID == id); //.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateName(User user)
        {
            object userAt;

            var users = await _context.User.Where(u => u.Name == user.Name).FirstOrDefaultAsync();
            if (users != null && users.User_ID > 0)
            {
                userAt = new { id = users.User_ID };
            }
            else
            {
                user.Poll = null;
                user.Vote = null;

                _context.User.Add(user);
                await _context.SaveChangesAsync();

                userAt = new { id = user.User_ID };
            }

            return CreatedAtAction(nameof(GetUser), userAt, User);
        }

        [Route("{id}/Poll")]
        [HttpPut]
        public async Task<ActionResult<User>> PutUserPoll(int id, User user)
        {
            if (id != user.User_ID)
            {
                return BadRequest();
            }

            var updateUser = await _context.User.FirstOrDefaultAsync(s => s.User_ID == user.User_ID);
            _context.Entry(updateUser).State = EntityState.Modified;

            updateUser.Name = user.Name;
            updateUser.Poll = user.Poll;

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

            return updateUser;
        }

        [Route("{id}/Vote")]
        [HttpPut]
        public async Task<ActionResult<User>> PutUserVote(int id, User user)
        {
            if (id != user.User_ID)
            {
                return BadRequest();
            }

            var updateUser = await _context.User.FirstOrDefaultAsync(s => s.User_ID == user.User_ID);
            _context.Entry(updateUser).State = EntityState.Modified;

            updateUser.Poll = null;
            updateUser.Vote = user.Vote;

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

            return updateUser;
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
