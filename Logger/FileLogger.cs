using System.Globalization;

namespace Logger;

public class FileLogger : BaseLogger
{
    /// <summary>
    /// Path of log file
    /// </summary>
    private readonly string _filePath;

    /// <summary>
    /// Construct the file logger
    /// </summary>
    /// <param name="filePath"></param> the file path for the log file
    public FileLogger(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        _filePath = filePath;
    }

    /// <summary>
    /// Logs a message with a log level
    /// </summary>
    /// <param name="level"></param> log level
    /// <param name="message"></param> log message
    public override void Log(LogLevel level, string message)
    {
        string line = string.Create(CultureInfo.CurrentCulture, 
            $"{DateTime.Now} {base.ClassName} {level}: {message}{Environment.NewLine}");

        File.AppendAllText(_filePath, line);
    }
}