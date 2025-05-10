using Microsoft.AspNetCore.SignalR;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using OpenAI.Chat;
using WebApplication2.Data;

namespace WebApplication2.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatClient chatClient;
        private ChatMessage? _chatMessage;
        private readonly WebApplication2Context? _context;

        public ChatHub(WebApplication2Context context, ChatClient _chatClient)
        {
            _context = context;
            chatClient = _chatClient;
        }
     


        public async Task SendMessage(string user, string message)
        {
            var chatMessag = new WebApplication2.Models.ChatMessage()
            {
                User = user,
                Text = message,
                Timestamp = DateTime.UtcNow
            };
            _context.Messages.Add(chatMessag);
            await _context.SaveChangesAsync();
            if (message.StartsWith("@gpt"))
            {
                await Clients.All.SendAsync("Question", user, message.Substring(4).Trim());
                _chatMessage  = new UserChatMessage(message);
                string result = chatClient.CompleteChat(_chatMessage).Value.Content[0].Text;
                var gptMessag = new WebApplication2.Models.ChatMessage()
                {
                    User = "ChatGPT",
                    Text = result,
                    Timestamp = DateTime.UtcNow
                };
                _context.Messages.Add(gptMessag);
                await _context.SaveChangesAsync();
                await Clients.All.SendAsync("Answer", user, result);
            }
            else
            {
                await Clients.All.SendAsync("ReceiveMessage", user, message);
            }
        }
    }
}
