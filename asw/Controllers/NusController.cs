using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asw.Data;
using asw.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NusController : ControllerBase
    {
        private readonly My_db_s _context;

        public NusController(My_db_s context)
        {
            _context = context;
        }

        // GET: api/Nus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nu>>> Getnus()
        {
            return await _context.nus.ToListAsync();
        }

        // GET: api/Nus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nu>> GetNu(int id)
        {
            var nu = await _context.nus.FindAsync(id);

            if (nu == null)
            {
                return NotFound();
            }

            return nu;
        }

        // PUT: api/Nus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "RequireRoleSuperisor")]
        public async Task<IActionResult> PutNu(int id, Nu nu)
        {
            if (id != nu.ID)
            {
                return BadRequest();
            }

            _context.Entry(nu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NuExists(id))
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

        // POST: api/Nus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Nu>> PostNu(Nu nu)
        {
            _context.nus.Add(nu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNu", new { id = nu.ID }, nu);
        }

        // DELETE: api/Nus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNu(int id)
        {
            var nu = await _context.nus.FindAsync(id);
            if (nu == null)
            {
                return NotFound();
            }

            _context.nus.Remove(nu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NuExists(int id)
        {
            return _context.nus.Any(e => e.ID == id);
        }
    }
}
