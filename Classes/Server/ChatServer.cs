using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;

public sealed class ChatServer
{
    /// <summary>
    /// Our main TCP listener object
    /// </summary>
    private readonly TcpListener _listener;
    private readonly ConcurrentDictionary<Guid, TcpClient> _clients = new ConcurrentDictionary<Guid, TcpClient>();

    public ChatServer(IPAddress ipAddress, int port)
    {
        _listener = new TcpListener(ipAddress, port);
    }

    /// <summary>
    /// Start the server and await client connections
    /// </summary>
    /// <param name="token">CancellationToken</param>
    /// <returns>Task</returns>
    public async Task StartServerAsync(CancellationToken token = default)
    {
        // start listening for clients
        _listener.Start();
        Console.WriteLine($"Server running on: {_listener.Server.LocalEndPoint}");

        while (!token.IsCancellationRequested)
        {
            var clientId = Guid.NewGuid();
            var tcpClient = await _listener.AcceptTcpClientAsync(token);

            _clients[clientId] = tcpClient;
            _ = HandleClientsAsync(clientId, tcpClient, token);
        }
    }

    private async Task HandleClientsAsync(Guid clientId, TcpClient client, CancellationToken token)
    {
        Console.WriteLine($"Client connected: {clientId}");

        try
        {
            using var stream = client.GetStream();
            using var streamReader = new StreamReader(stream, encoding: Encoding.UTF8);

            while (!token.IsCancellationRequested)
            {
                var line = await streamReader.ReadLineAsync(); // read each line from the network stream
                if (string.IsNullOrWhiteSpace(line))
                {
                    break;
                }
                Console.WriteLine($"Recieved: {line}");
            }
        }
        catch (IOException)
        {
            Console.WriteLine($"Client: {clientId} disconnected unexpectedly!");
        }
        finally
        {
            _clients.TryRemove(clientId, out _);
            client.Dispose();
            Console.WriteLine($"Client disconnected: {clientId}");
        }
    }

    private async Task BroadcastAsync(string message, Guid senderId, CancellationToken token)
    {
        var tasks = new List<Task>();

        foreach (var pair in _clients)
        {
            if (pair.Key == senderId)
            {
                continue;
            }

            tasks.Add(SendToClientAsync(pair.Value, message, token));
        }

        await Task.WhenAll(tasks);
    }

    private async Task SendToClientAsync(TcpClient client, string message, CancellationToken token)
    {
        if (!client.Connected)
        {
            return;
        }

        try
        {
            using var streamWriter = new StreamWriter(client.GetStream(), Encoding.UTF8, leaveOpen: true)
            {
                AutoFlush = true
            };

            await streamWriter.WriteAsync(message);
        }
        catch (IOException e)
        {
            Console.WriteLine($"Messages could not be fetched, an error occured on the server. {e.Message}");
        }
    }

}