public interface IChatbot
{
    /*
    Main methods
    */
    /// <summary>
    /// Log in a new user
    /// </summary>
    /// <param name="username">optional paramater, defaults to guest if not specified</param>
    /// <returns>string</returns>
    string Login(string username = "Guest");
    /// <summary>
    /// Get the current user of the application
    /// </summary>
    /// <returns>string</returns>
    string GetCurrentuser();
    /// <summary>
    /// Run the main application
    /// </summary>
    void RunApp();

    /*
    Application specific methods, utility methods & helper methods 
    */
    /// <summary>
    /// Get the uptime/lifetime of the current session
    /// </summary>
    /// <returns></returns>
    DateTime GetSessionUptime();
    /// <summary>
    /// Log information to a file
    /// </summary>
    /// <param name="filePath">path to the logfile</param>
    /// <param name="logContent">content of logfile</param>
    void Logger(string filePath, string logContent);

    /// <summary>
    /// Run the main application in debug mode
    /// </summary>
    void RunInDebugMode();
}