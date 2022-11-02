using Helpers;

namespace HelpersTests;

public class ChatLogTests
{
    private IChatLog _chatLog;

    public ChatLogTests()
    {
        _chatLog = new ChatLog();
    }

    [Test]
    public void LogInfo_GoodString_()
    {
        _chatLog.LogInfo("Test");
        Assert.True(FindLogFile());
    }

    private static bool FindLogFile()
    {
        return Directory.Exists("Logs/");
    }
}