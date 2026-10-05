namespace Logger;

public class LogFactory
{

    private string? _filePath;

    public void ConfigureFileLogger(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        _filePath = filePath;
    }

    public BaseLogger? CreateLogger(string className)
    {
        return _filePath == null ? null : (BaseLogger)new FileLogger(_filePath) { ClassName = className };
    }
}
