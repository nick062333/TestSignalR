using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    public void Send(string message, DateTime date)
    {
        Clients.All.SendAsync("Send", message, date);
    }

    public void ReceiveMessage(string user, string message)
    {
        //Clients.All.SendAsync("ReceiveMessage", user, message);
        Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
