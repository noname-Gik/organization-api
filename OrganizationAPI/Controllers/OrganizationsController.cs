using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizationAPI.Data;
using OrganizationAPI.Models;

namespace OrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly DataContext _context;

        public OrganizationsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Organizations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organizations>>> GetOrganizations()
        {
          if (_context.Organizations == null)
          {
              return NotFound();
          }
            return await _context.Organizations.Include(o => o.OrganizationRole).ThenInclude(r => r.RoleUser).ToListAsync();
        }

        // GET: api/Organizations/5
        [HttpGet("{name}")]
        public async Task<ActionResult<List<Organizations>>> GetOrganizations(string name)
        {
          if (_context.Organizations == null)
          {
              return NotFound();
          }
            var organizations = await _context.Organizations.Where(i => i.name.Contains(name)).Include(o => o.OrganizationRole).ThenInclude(r => r.RoleUser).ToListAsync();

            if (organizations == null)
            {
                return NotFound();
            }

            return organizations;
        }

        // PUT: api/Organizations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizations([FromRoute] int id, Organizations organizations)
        {
            var found = await _context.Organizations.FindAsync(id);

            if (found is null)
            {
                return NotFound();
            }

            try
            {
                found.name = organizations.name;
                found.OrganizationRole = organizations.OrganizationRole;
                // для метода PUT RoleUser не использовать - необходимо для декомпозиции
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationsExists(id))
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

        // POST: api/Organizations
        [HttpPost]
        public async Task<ActionResult<Organizations>> PostOrganizations(Organizations organizations)
        {
          if (_context.Organizations == null)
          {
              return Problem("Entity set 'DataContext.Organizations'  is null.");
          }
            _context.Organizations.Add(organizations);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganizations", new { id = organizations.ID }, organizations);
        }

        // DELETE: api/Organizations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizations(int id)
        {
            if (_context.Organizations == null)
            {
                return NotFound();
            }
            var organizations = await _context.Organizations.FindAsync(id);
            if (organizations == null)
            {
                return NotFound();
            }

            _context.Organizations.Remove(organizations);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizationsExists(int id)
        {
            return (_context.Organizations?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
