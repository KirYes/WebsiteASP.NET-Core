namespace WebApplication2.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string User { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
