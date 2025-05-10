using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

namespace WebApplication2.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountController : ControllerBase
    {
        private readonly WebApplication2Context _context;
        public CountController(WebApplication2Context context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var room = await _context.Rooms
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.RoomName == "group2");
            if (room != null)
            {
                int userCount = room.Users.Count;
                return Ok(userCount);
            }
            else
            {
                return NotFound("Room not found");
            }
        }
    }
}
