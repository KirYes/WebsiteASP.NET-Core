using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace WebApplication2.Models
{
    public class User 
    {
        [Key]
        public string UserName { get; set; } = string.Empty;
        public ICollection<Connection>? Connections { get; set; }
        public virtual ICollection<ConversationRoom>? Rooms { get; set; }
    }
}
