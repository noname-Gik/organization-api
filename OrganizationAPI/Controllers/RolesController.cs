using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizationAPI.Data;
using OrganizationAPI.Models;

namespace OrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DataContext _context;

        public RolesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles()
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            return await _context.Roles.ToListAsync();
        }

        // GET: api/Roles/name
        [HttpGet("{name}")]
        public async Task<ActionResult<List<Roles>>> GetRoles(string name)
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            var roles = await _context.Roles.Where(i => i.name.Contains(name)).ToListAsync();

            if (roles == null)
            {
                return NotFound();
            }

            return roles;
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoles([FromRoute] int id, Roles roles)
        {
            var found = await _context.Roles.FindAsync(id);

            if (found is null)
            {
                return NotFound();
            }

            try
            {
                found.name = roles.name;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolesExists(id))
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

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<Roles>> PostRoles(Roles roles)
        {
          if (_context.Roles == null)
          {
              return Problem("Entity set 'DataContext.Roles'  is null.");
          }
            _context.Roles.Add(roles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoles", new { id = roles.ID }, roles);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoles(int id)
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }
            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(roles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolesExists(int id)
        {
            return (_context.Roles?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
