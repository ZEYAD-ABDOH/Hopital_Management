using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asw.Data;
using asw.Model;
using Microsoft.AspNetCore.Authorization;

namespace Client.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DepartlsController : ControllerBase
    {
        private readonly My_db_s _context;

        public DepartlsController(My_db_s context)
        {
            _context = context;
        }

        // GET: api/Departls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departl>>> Getdepartls()
        {
            return await _context.departls.ToListAsync();
        }

        // GET: api/Departls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Departl>> GetDepartl(int id)
        {
            var departl = await _context.departls.FindAsync(id);

            if (departl == null)
            {
                return NotFound();
            }

            return departl;
        }

        // PUT: api/Departls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartl(int id, Departl departl)
        {
            if (id != departl.ID)
            {
                return BadRequest();
            }

            _context.Entry(departl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartlExists(id))
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

        // POST: api/Departls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Departl>> PostDepartl(Departl departl)
        {
            _context.departls.Add(departl);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartl", new { id = departl.ID }, departl);
        }

        // DELETE: api/Departls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartl(int id)
        {
            var departl = await _context.departls.FindAsync(id);
            if (departl == null)
            {
                return NotFound();
            }

            _context.departls.Remove(departl);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartlExists(int id)
        {
            return _context.departls.Any(e => e.ID == id);
        }
    }
}
