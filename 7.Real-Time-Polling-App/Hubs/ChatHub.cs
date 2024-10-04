using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    public async Task SendBroadCastMessage(string user, string message) => await Clients.All.SendAsync("broadCastMessage", user, message);
}