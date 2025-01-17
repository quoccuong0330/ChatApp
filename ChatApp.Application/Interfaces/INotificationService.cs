namespace ChatApp.Core.Interfaces;

public interface INotificationService {
        Task SendMessageAsync(string roomId, string message);
        Task ReceiveMessage( string message);
}