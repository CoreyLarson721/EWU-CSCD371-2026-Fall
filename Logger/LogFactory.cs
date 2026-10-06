namespace Logger;

public class LogFactory
{

    private string? _filePath;

    /// <summary>
    /// Configures FileLogger by setting its file path. 
    /// </summary>
    /// <param name="filePath"></param>
    public void ConfigureFileLogger(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        _filePath = filePath;
    }

    /// <summary>
    /// Factory class to create a new logger using the configured _filePath
    /// </summary>
    /// <param name="className"></param> Name of the class
    /// <returns></returns>
    public BaseLogger? CreateLogger(string className)
    {
        return _filePath == null ? null : (BaseLogger)new FileLogger(_filePath) { ClassName = className };
    }
}
