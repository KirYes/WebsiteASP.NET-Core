using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class WebApplication2Context : IdentityDbContext<IdentityUser>
    {
        public WebApplication2Context (DbContextOptions<WebApplication2Context> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<WebApplication2.Models.Movie> Movie { get; set; } = default!;
        public DbSet<WebApplication2.Models.ChatMessage> Messages { get; set; } = default!;
        public new DbSet<WebApplication2.Models.User> Users { get; set; } = default!;
        public DbSet<WebApplication2.Models.Connection> Connections { get; set; } = default!;
        public DbSet<WebApplication2.Models.ConversationRoom> Rooms { get; set; } = default!;
    }
}

