using Microsoft.AspNetCore.SignalR;

public class GameHub : Hub
{
    private readonly GameTimerService _timerService;

    public GameHub(GameTimerService timerService)
    {
        _timerService = timerService;
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
}
