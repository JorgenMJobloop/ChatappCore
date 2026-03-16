public class Chatbot : IChatbot
{
    private Random _rng = new Random();
    private readonly List<string> _responses = new List<string>()
    {
        "Hello",
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

    public string GetCurrentuser()
    {
        var user = Login();
        return user;
    }

    public DateTime GetSessionUptime()
    {
        return DateTime.Now;
    }

    public void Logger(string filePath, string logContent)
    {
        if (!File.Exists(filePath))
        {
            return;
        }
        File.WriteAllText(filePath, logContent);
    }

    public string Login(string username = "Guest")
    {
        return username;
    }

    public void RunApp()
    {
        // main loop
        Console.WriteLine("Hello, do you want to login? y/N");
        var userInput = Console.ReadLine();
        if (userInput!.ToLower() == "y")
        {
            var username = Console.ReadLine();
            var login = Login(username!);
            Console.WriteLine($"{_responses[0]} {login}");
            Console.WriteLine($"{_responses[_rng.Next(_responses.Count)]}");
        }

        Console.WriteLine($"{_responses[0]} {GetCurrentuser()}");
        Console.WriteLine($"{_responses[_rng.Next(_responses.Count)]}");
    }

    public void RunInDebugMode()
    {
        throw new NotImplementedException();
    }
}