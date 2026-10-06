using System.Globalization;

namespace Logger;

public static class BaseLoggerExtensions
{
    /// <summary>
    /// Extension method for error level log shortcut
    /// </summary>
    /// <param name="logger"></param> BaseLogger
    /// <param name="message"></param> Logger message
    /// <param name="args"></param> additional arguments
    public static void Error(this BaseLogger logger, string message, params object?[] args) =>
        LogMessage(logger, LogLevel.Error, message, args);


    /// <summary>
    /// Extension method for warning level log shortcut
    /// </summary>
    /// <param name="logger"></param> BaseLogger
    /// <param name="message"></param> Logger message
    /// <param name="args"></param> additional arguments
    public static void Warning(this BaseLogger logger, string message, params object?[] args) =>
        LogMessage(logger, LogLevel.Warning, message, args);

    /// <summary>
    /// Extension method for information level log shortcut
    /// </summary>
    /// <param name="logger"></param> BaseLogger
    /// <param name="message"></param> Logger message
    /// <param name="args"></param> Additional arguments
    public static void Information(this BaseLogger logger, string message, params object?[] args) =>
        LogMessage(logger, LogLevel.Information, message, args);

    /// <summary>
    /// Extension method for debug level log shortcut
    /// </summary>
    /// <param name="logger"></param> BaseLogger
    /// <param name="message"></param> Logger message
    /// <param name="args"></param> Additional arguments
    public static void Debug(this BaseLogger logger, string message, params object?[] args) =>
        LogMessage(logger, LogLevel.Debug, message, args);

    /// <summary>
    /// Helper method that calls an exception if the logger is null. 
    /// </summary>
    /// <param name="logger"></param> BaseLogger
    /// <param name="level"></param> Log level
    /// <param name="message"></param> Logger message
    /// <param name="args"></param> Additional arguments
    private static void LogMessage(BaseLogger logger, LogLevel level, string message, object?[] args)
    {
        ArgumentNullException.ThrowIfNull(logger);

        logger.Log(level, string.Format(CultureInfo.CurrentCulture, message, args));
    }
}