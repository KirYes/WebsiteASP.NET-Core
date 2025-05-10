namespace WebApplication2.Models
{
    public class Connection
    {
        public string ConnectionId { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
        public bool Connected { get; set; }
    }
}
