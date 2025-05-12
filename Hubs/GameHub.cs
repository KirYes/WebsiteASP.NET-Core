using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using System.Linq;
using WebApplication2.Migrations;
[Authorize]
public class GameHub : Hub
{
    private readonly GameTimerService _timerService;
    private readonly WebApplication2Context? _context;

    public GameHub(WebApplication2Context context, GameTimerService timerService)
    {
        _timerService = timerService;
        _context = context;
    }

    public Task PlayerReady()
    {
        return _timerService.PlayerReady(Context.ConnectionId);
    }

    public Task EndRace()
    {
        return _timerService.EndRace();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }

    public override async Task OnConnectedAsync()
    {
            var user = _context.Users
                .Include(u => u.Rooms)
                .SingleOrDefault(u => u.UserName == Context.User.Identity.Name);
            if (user == null)
            {
                user = new WebApplication2.Models.User()
                {
UserName = Context.User.Identity.Name
                };
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            else
            {
                foreach(var item in user.Rooms)
                {
                   await Groups.AddToGroupAsync(Context.ConnectionId, item.RoomName);
                }
            }
        await base.OnConnectedAsync();
    }
    public async Task AddToGroup(string groupName)
    {
        var room = _context.Rooms
    .Include(r => r.Users)
    .FirstOrDefault(r => r.RoomName == groupName);
        if (room != null)
        {
            //var user = new WebApplication2.Models.User()
            //{
            //    UserName = Context.User.Identity.Name
            //};
            //_context.Users.Attach(user);
            var user = _context.Users
         .FirstOrDefault(u => u.UserName == Context.User.Identity.Name);
            if (room.Users.Count >= 2)
            {
                return;
            }
            room.Users.Add(user);
            _context.SaveChanges();
            int userCount = room.Users.Count;
            string count = userCount.ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.All.SendAsync("Join", $"{Context.ConnectionId} has joined the group {groupName}.", count);
            if(userCount ==2)
            {
                await Clients.Group(groupName).SendAsync("StartG", groupName);
            }
        }

    }
    public async Task RemoveFromGroup(string groupName)
    {
        var room = _context.Rooms
            .Include(r => r.Users)
            .FirstOrDefault(r => r.RoomName == groupName);
        if (room != null)
        {
            //var user = new WebApplication2.Models.User()
            //{
            //    UserName = Context.User.Identity.Name
            //};
            //_context.Users.Attach(user);
            var user = _context.Users
         .FirstOrDefault(u => u.UserName == Context.User.Identity.Name);
            room.Users.Remove(user);
            _context.SaveChanges();
            int userCount = room.Users.Count;
            string count = userCount.ToString();
            await Clients.All.SendAsync("Leave", $"{Context.ConnectionId} has left the group {groupName}.", count);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }

    public async Task StartGame(string groupName)
    {
        var room = _context.Rooms
            .Include(r => r.Users)
            .FirstOrDefault(r => r.RoomName == groupName);
        if (room != null)
        {
            await Clients.Group(groupName).SendAsync("StartGame", $"{Context.ConnectionId} has started the game.");
        }
    }

    public async Task DrawGame(object ctx, string groupName)
    {
        await Clients.Group(groupName).SendAsync("Dra",ctx);
    }
}