using ChatApp.Core.Interfaces;

namespace ChatApp.Presentation.Hubs;

using Microsoft.AspNetCore.SignalR;

   public class ChatHub : Hub
   {
      public async Task SendMessage(string user, string message)
      {
         await Clients.All.SendAsync("ReceiveMessage", user, message);
      }

      public override Task OnConnectedAsync()
      {
         Console.WriteLine($"Client connected: {Context.ConnectionId}");
         return base.OnConnectedAsync();
      }

      public override Task OnDisconnectedAsync(Exception? exception)
      {
         Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
         return base.OnDisconnectedAsync(exception);
      }
}