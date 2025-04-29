using Microsoft.AspNetCore.SignalR;
using Timer = System.Timers.Timer;

public class GameTimerService
{
    private readonly IHubContext<GameHub> _hubContext;
    private readonly HashSet<string> _readyUsers = new();
    private Timer? _timer;
    private int _elapsedSeconds = 0;
    private const int RequiredPlayers = 2; 

    public GameTimerService(IHubContext<GameHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task PlayerReady(string connectionId)
    {
        _readyUsers.Add(connectionId);

        if (_readyUsers.Count == RequiredPlayers)
        {
            await StartRace();
        }
        else
        {
            await _hubContext.Clients.Client(connectionId).SendAsync("WaitingForOthers", RequiredPlayers - _readyUsers.Count);
        }
    }

    private async Task StartRace()
    {
        _elapsedSeconds = 0;
        await _hubContext.Clients.All.SendAsync("RaceStarted");

        _timer = new Timer(1000);
        _timer.Elapsed += async (sender, e) =>
        {
            _elapsedSeconds++;
            await _hubContext.Clients.All.SendAsync("UpdateRaceTimer", _elapsedSeconds);
        };
        _timer.AutoReset = true;
        _timer.Start();
    }

    public async Task EndRace()
    {
        if (_timer != null)
        {
            _timer.Stop();
            _timer.Dispose();
            _timer = null;
        }

        await _hubContext.Clients.All.SendAsync("RaceEnded", _elapsedSeconds);
        _readyUsers.Clear(); 
    }
}
