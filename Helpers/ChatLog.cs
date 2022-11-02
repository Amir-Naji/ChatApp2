using Serilog;

namespace Helpers;

public class ChatLog: IChatLog
{
    public ChatLog()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("Logs/logs.txt", rollingInterval:RollingInterval.Day)
            .CreateLogger();
    }

    public void LogInfo(string test)
    {
        Log.Information(test);
    }
}