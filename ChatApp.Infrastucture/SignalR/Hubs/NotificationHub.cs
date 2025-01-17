using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Infrastucture.SignalR.Hubs;

public class NotificationHub : Hub{
    public async Task JoinRoom(string roomName) {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        await Clients.Group(roomName)
            .SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} has joined the room {roomName}.");
    }
    
}