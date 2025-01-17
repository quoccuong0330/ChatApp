using ChatApp.Core.Interfaces;

namespace ChatApp.Application;

public class RoomService {
    private readonly INotificationService _notificationService;

    public RoomService(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task NotifyRoomAsync(string roomId, string message)
    {
        await _notificationService.SendMessageAsync(roomId, message);
    }
}