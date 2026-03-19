using System.Net.Sockets;

namespace ChatappCore;

class Program
{
    static async Task Main(string[] args)
    {
        var server = new ChatServer(System.Net.IPAddress.Any, 9001);

        var client = new ChatClient("John Doe");

        if (args.Length == 0)
        {
            Console.WriteLine("Usage: <server> - Run the server\n<client> - Run the client");
        }

        if (args.Length > 0 && args[0] == "server")
        {
            await server.StartServerAsync();
        }

        if (args.Length > 0 && args[0] == "client")
        {
            await client.RunClientAsync("127.0.0.1", 9001, default);
        }
    }
}