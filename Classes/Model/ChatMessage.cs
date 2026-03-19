public sealed class ChatMessage
{
    public string Sender { get; init; } = string.Empty;
    public string TextContents { get; init; } = string.Empty;
    public DateTime TimeStamp { get; init; } = DateTime.UtcNow;
}