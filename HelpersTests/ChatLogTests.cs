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
    public void InformationLog_GoodString_()
    {
        _chatLog.Log("Test");
    }
}