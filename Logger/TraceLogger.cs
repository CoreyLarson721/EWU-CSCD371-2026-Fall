using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace Logger;

public class TraceLogger : BaseLogger, ILogger<TraceLogger>
{
    /// <summary>
    /// Creates a nre instance of a TraceLogger 
    /// </summary>
    /// <param name="className"></param> The name of the logged class
    /// <returns></returns> A new instance of TraceLogger class
    public static TraceLogger CreateLogger(string className)
    {
        return new TraceLogger() { ClassName = className};
    }

    /// <summary>
    /// Records a log message to a new line
    /// </summary>
    /// <param name="logLevel"></param> The log level
    /// <param name="message"></param> The message to be recorded
    public override void Log(LogLevel logLevel, string message)
    {
        Trace.WriteLine(string.Create(CultureInfo.CurrentCulture,
            $"{DateTime.Now} {base.ClassName} {logLevel}: {message}"));
    }
}
