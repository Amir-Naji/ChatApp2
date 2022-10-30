namespace Helpers;

public interface IConverters
{
    Task<string> StreamToString(Stream stream);

    Task<string> SimpleStreamToString(Stream stream);

    byte[] SimpleStringToStream(string message);
    
    byte[] StringMessageToByteArray(string message);
}