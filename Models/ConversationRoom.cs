using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class ConversationRoom
    {
        [Key]
        public string RoomName { get; set; } = string.Empty;
        public virtual ICollection<User> Users { get; set; }
    }
}
