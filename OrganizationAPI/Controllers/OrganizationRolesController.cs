using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizationAPI.Data;
using OrganizationAPI.Models;

namespace OrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationRolesController : ControllerBase
    {
        private readonly DataContext _context;

        public OrganizationRolesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/OrganizationRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationRole>>> GetOrganizationRole()
        {
          if (_context.OrganizationRole == null)
          {
              return NotFound();
          }
            return await _context.OrganizationRole.Include(r => r.RoleUser).ToListAsync();
        }

        // GET: api/OrganizationRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationRole>> GetOrganizationRole(int id)
        {
          if (_context.OrganizationRole == null)
          {
              return NotFound();
          }
            var organizationRole = await _context.OrganizationRole.FindAsync(id);

            if (organizationRole == null)
            {
                return NotFound();
            }

            return organizationRole;
        }

        // PUT: api/OrganizationRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizationRole(int id, OrganizationRole organizationRole)
        {
            var found = await _context.OrganizationRole.FindAsync(id);

            if (found is null)
            {
                return NotFound();
            }

            try
            {
                found.RolesId = organizationRole.RolesId;
                found.RoleUser = organizationRole.RoleUser;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationRoleExists(id))
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

        // DELETE: api/OrganizationRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationRole(int id)
        {
            if (_context.OrganizationRole == null)
            {
                return NotFound();
            }
            var organizationRole = await _context.OrganizationRole.FindAsync(id);
            if (organizationRole == null)
            {
                return NotFound();
            }

            _context.OrganizationRole.Remove(organizationRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizationRoleExists(int id)
        {
            return (_context.OrganizationRole?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
