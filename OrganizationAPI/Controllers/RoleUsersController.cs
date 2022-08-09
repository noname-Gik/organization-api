using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizationAPI.Data;
using OrganizationAPI.Models;

namespace OrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleUsersController : ControllerBase
    {
        private readonly DataContext _context;

        public RoleUsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/RoleUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleUser>>> GetRoleUser()
        {
          if (_context.RoleUser == null)
          {
              return NotFound();
          }
            return await _context.RoleUser.ToListAsync();
        }

        // GET: api/RoleUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleUser>> GetRoleUser(int id)
        {
          if (_context.RoleUser == null)
          {
              return NotFound();
          }
            var roleUser = await _context.RoleUser.FindAsync(id);

            if (roleUser == null)
            {
                return NotFound();
            }

            return roleUser;
        }

        // PUT: api/RoleUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleUser(int id, RoleUser roleUser)
        {
            if (id != roleUser.ID)
            {
                return BadRequest();
            }

            _context.Entry(roleUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleUserExists(id))
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

        // DELETE: api/RoleUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleUser(int id)
        {
            if (_context.RoleUser == null)
            {
                return NotFound();
            }
            var roleUser = await _context.RoleUser.FindAsync(id);
            if (roleUser == null)
            {
                return NotFound();
            }

            _context.RoleUser.Remove(roleUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleUserExists(int id)
        {
            return (_context.RoleUser?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
