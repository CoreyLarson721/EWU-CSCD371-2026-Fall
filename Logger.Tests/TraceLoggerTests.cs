using System.Diagnostics;
using System.Globalization;


namespace Logger.Tests;

[TestClass]
public class TraceLoggerTests
{
    /// <summary>
    /// Used to test TraceLogger.Create
    /// Tests correctness of ClassName
    /// </summary>
    [TestMethod]
    public void TestCreateTraceLogger()
    {
        TraceLogger logger = TraceLogger.CreateLogger(nameof(TraceLoggerTests));
        Assert.EndsWith(nameof(TraceLoggerTests), logger.ClassName);
    }

    /// <summary>
    /// Used to test TraceLogger.Log output
    /// </summary>
    [TestMethod]
    public void TestTraceLoggerOutput()
    {
        string path = Path.GetTempFileName();
        TraceListener listener = new TextWriterTraceListener(path);

        Trace.Listeners.Add(listener); // Needed to read the output of Trace.Writeline
        Trace.AutoFlush = true;

        try
        {
            TraceLogger logger = TraceLogger.CreateLogger(nameof(TraceLoggerTests));

            logger.Log(LogLevel.Warning, "First");
            logger.Log(LogLevel.Error, "Second");
        }
        finally
        {
            Trace.Listeners.Remove(listener);
            listener.Dispose();
        }
        
        string[] lines = File.ReadAllLines(path);
            Assert.HasCount(2, lines);

        try
        {
            StringAssert.Contains(lines[0], nameof(TraceLoggerTests));
            StringAssert.Contains(lines[0], "Warning");
            StringAssert.Contains(lines[0], "First");
            StringAssert.Contains(lines[0], DateTime.Now.Year.ToString(CultureInfo.CurrentCulture));
            StringAssert.Contains(lines[1], "Error: Second");
        }
        finally
        {
            File.Delete(path);
        }

    }
}
