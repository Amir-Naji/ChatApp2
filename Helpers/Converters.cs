using System.Text;

namespace Helpers;

public class Converters : IConverters
{
    private const int ByteSize = 4;

    public async Task<string> StreamToString(Stream stream)
    {
        var sizeBytes = await ReadStream(stream, ByteSize);
        var messageBytes = await ReadStream(
            stream,
            BitConverter.ToInt32(sizeBytes, 0));

        return EncodeString(messageBytes)
            .Where(c => c != '\0')
            .Aggregate<char, string>(null!, (current, c) => current + c);
    }

    public byte[] StringMessageToByteArray(string message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        var messageSize = messageBytes.Length;
        // add content length bytes to the original size
        var completeSize = messageSize + ByteSize;
        // create a buffer of the size of the complete message size
        var completeMsg = new byte[completeSize];

        // convert message size to bytes
        var sizeBytes = BitConverter.GetBytes(messageSize);
        // copy the size bytes and the message bytes to our overall message to be sent 
        sizeBytes.CopyTo(completeMsg, 0);
        messageBytes.CopyTo(completeMsg, ByteSize);
        return completeMsg;
    }

    public async Task<string> SimpleStreamToString(Stream stream)
    {
        var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }

    public byte[] SimpleStringToStream(string message)
    {
        return Encoding.UTF8.GetBytes(message);
    }

    private static async Task<byte[]> ReadStream(Stream stream, int byteSize)
    {
        var sizeBytes = new byte[byteSize];
        _ = await stream.ReadAsync(sizeBytes, 0, byteSize);
        return sizeBytes;
    }

    private static string EncodeString(byte[] messageBytes)
    {
        return Encoding.UTF8.GetString(messageBytes);
    }
}