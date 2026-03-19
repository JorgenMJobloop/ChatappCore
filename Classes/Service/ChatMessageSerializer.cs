using System.Text.Json;
/// <summary>
/// Serializes plaintext to JSON, this is helpful, when trying to implement a messaging service over TCP or HTTP/HTTPS
/// </summary>
public static class ChatMessageSerializer
{
    public static string SerializeMessage(ChatMessage message)
    {
        return JsonSerializer.Serialize(message);
    }

    public static ChatMessage? DeserializeMessage(string jsonData)
    {
        return JsonSerializer.Deserialize<ChatMessage>(jsonData);
    }
}