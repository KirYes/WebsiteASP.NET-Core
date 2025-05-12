using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly WebApplication2Context _context;

        public ChatController(WebApplication2Context context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            if(!User.Identity.IsAuthenticated)
            {
                var messages = await _context.Messages
                .OrderBy(m => m.Timestamp)
                .Take(100)
                .ToListAsync();
                return Ok(messages);
            }
            else
            {
                var messages = await _context.Messages
                            .OrderByDescending(m => m.Timestamp)
                            .Take(5)
                            .ToListAsync();
                return Ok(messages);
            }


        
        }
    }

}
