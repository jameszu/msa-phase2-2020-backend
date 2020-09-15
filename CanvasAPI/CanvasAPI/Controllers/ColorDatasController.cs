﻿using System.Threading.Tasks;
using CanvasAPI.Models;
using Microsoft.AspNetCore.Mvc;
using phase_2_back_end.Models;

namespace phase_2_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorDatasController : ControllerBase
    {
        private readonly AppDatabase _context;

        public ColorDatasController(AppDatabase context)
        {
            _context = context;
        }

        // GET: api/ColorDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ColorData>> GetColorData(int id)
        {
            var colorData = await _context.ColorData.FindAsync(id);

            if (colorData == null)
            {
                return NotFound();
            }

            return colorData;
        }
    }
}