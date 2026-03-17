public class Chatapp : IChatapp
{
    private readonly Random _rng = new Random();
    private string _currentUser = "Guest";
    private readonly DateTime _sessionUptime = DateTime.Now;
    private readonly List<string> _responses = new List<string>()
    {
        "How can I help you?",
        "Is this what you were looking for?",
        "You can read more about this here:",
        "Goodbye",
    };

    private readonly List<string> _helpUrls = new List<string>()
    {
        "https://example.com",
        "https://example.com/article-one",
        "https://example.com/article-two"
    };

    public string GetCurrentUser()
    {
        return _currentUser;
    }

    public TimeSpan GetSessionUptime()
    {
        return DateTime.Now - _sessionUptime;
    }

    public void Log(string filePath, string logContent)
    {
        try
        {
            var entry = $"{DateTime.Now:u} {logContent}{Environment.NewLine}";
            File.AppendAllText(filePath, entry);
        }
        catch (IOException e)
        {
            Console.WriteLine($"An error occured when trying to log in: {e.Message}");
        }
    }

    public string Login(string username = "Guest")
    {
        _currentUser = string.IsNullOrWhiteSpace(username) ? "Guest" : username;
        return _currentUser;
    }

    public void RunApp()
    {
        // main loop
        Console.WriteLine("Hello, do you want to login? y/N");
        var userInput = Console.ReadLine();
        if (string.Equals(userInput, "y", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Enter username:");
            var username = Console.ReadLine();
            Login(username ?? "Guest");
        }
        Console.WriteLine($"Hello {GetCurrentUser()}");
        Console.WriteLine($"{_responses[_rng.Next(_responses.Count)]}");
    }

    public void RunInDebugMode()
    {
        Console.WriteLine("Debug mode started.");
        Console.WriteLine("To exit, press the enter key..");
        while (true)
        {
            Console.WriteLine($"Current user(s): {GetCurrentUser()}");
            Console.WriteLine($"Session uptime: {GetSessionUptime()}");

            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                break;
            }
        }
    }
}