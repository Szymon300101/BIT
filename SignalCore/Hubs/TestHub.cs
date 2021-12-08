using Microsoft.AspNetCore.SignalR;

namespace SignalCore.Hubs
{
    public class TestHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", user, "Sent");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
