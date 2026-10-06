namespace Logger;

public abstract class BaseLogger
{
    /// <summary>
    /// Logs Message
    /// </summary>
    /// <param name="logLevel"></param> Log level
    /// <param name="message"></param> Message for the log
    public abstract void Log(LogLevel logLevel, string message);

    /// <summary>
    /// The name of the logged class
    /// </summary>
    public required string ClassName { get; set; } = string.Empty;
}

