using System.Globalization;

namespace Logger;

public class FileLogger : BaseLogger
{
    private readonly string _filePath;

    public FileLogger(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        _filePath = filePath;
    }

    public override void Log(LogLevel level, string message)
    {
        string line = string.Create(CultureInfo.CurrentCulture, 
            $"{DateTime.Now} {base.ClassName} {level}: {message}{Environment.NewLine}");

        File.AppendAllText(_filePath, line);
    }
}