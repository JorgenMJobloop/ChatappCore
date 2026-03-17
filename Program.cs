namespace ChatappCore;

class Program
{
    static void Main(string[] args)
    {
        var chatApp = new Chatapp();

        if (args.Length == 0)
        {
            Console.WriteLine("Usage: <app> - Run the app normally\n<debug> - Run the app in debug mode");
        }

        if (args.Length > 0 && args[0] == "app")
        {
            chatApp.RunApp();
        }

        if (args.Length > 0 && args[0] == "debug")
        {
            chatApp.RunInDebugMode();
        }
    }
}