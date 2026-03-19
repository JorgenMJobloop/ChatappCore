using System.Net.Sockets;
using System.Text;

public sealed class ChatClient
{
    private readonly string _username;

    public ChatClient(string username)
    {
        _username = username ?? "Guest";
    }

    public async Task RunClientAsync(string ipAddress, int port, CancellationToken token)
    {
        using var client = new TcpClient();
        await client.ConnectAsync(ipAddress, port, token);

        Console.WriteLine($"Connected successfully to server at: {ipAddress}:/{port}");

        using var stream = client.GetStream();
        using var streamReader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
        using var streamWriter = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true)
        {
            AutoFlush = true
        };

        // Handles incomming traffic/packages/messages from the server
        var recieveTask = Task.Run(async () =>
        {
            while (!token.IsCancellationRequested)
            {
                var incomingData = streamReader.ReadLineAsync();

                if (incomingData is null)
                {
                    break;
                }

                Console.WriteLine($"Data sent from server: {incomingData}");
            }
        }, token);

        // handle outgoing traffic/packages/messages that is being sent back to the server
        while (!token.IsCancellationRequested)
        {
            // get userinput from the client that is connected.
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            // map messages to the ChatMessage model & use the message serializer service
            var message = new ChatMessage
            {
                Sender = _username,
                TextContents = input,
                TimeStamp = DateTime.Now
            };

            var json = ChatMessageSerializer.SerializeMessage(message);
            await streamWriter.WriteAsync(json);
        }

        await recieveTask;
    }
}