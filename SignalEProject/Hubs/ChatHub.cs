using Microsoft.AspNetCore.SignalR;
using SignalEProject.Contexts;
using SignalEProject.Models;

namespace SignalEProject.Hubs
{
    public class ChatHub:Hub
    {
        private readonly ChatDbContext _context;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ChatDbContext context, ILogger<ChatHub> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task Send(string userName, string message)
        {
            await Clients.Others.SendAsync("ReciveMessage", userName, message);

            var msg = new Message
            {
                UserName = userName,
                Text = message
            };


            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();
        }

        public async Task JoinGroup(string groupName, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.OthersInGroup(groupName).SendAsync("NewMemberJoin", userName, groupName);

            _logger.LogInformation(Context.ConnectionId);
        }


        public async Task SendToGroup(string groupName, string sender, string message)
        {
            await Clients.OthersInGroup(groupName).SendAsync("ReciveMessageFromGroup", sender, message);

            var msg = new Message
            {
                UserName = sender,
                Text = message
            };


            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();


        }
    }
}
