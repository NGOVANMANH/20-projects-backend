public interface IChatHub
{
    Task SendMessageToClientAsync(string clientId, string message);
    Task SendMessageToGroupAsync(string groupId, string message);
}