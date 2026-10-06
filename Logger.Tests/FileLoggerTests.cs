using System.Globalization;
namespace Logger.Tests;

[TestClass]
public class FileLoggerTests
{
    /// <summary>
    /// Used for testing the log file output
    /// </summary>
    [TestMethod]
    public void LogAppendsMessagesOnSeparateLines()
    {
        string path = Path.GetTempFileName();
        try
        {
            var logger = new FileLogger(path) { ClassName = nameof(FileLoggerTests) };

            logger.Log(LogLevel.Warning, "First");
            logger.Log(LogLevel.Error, "Second");

            string[] lines = File.ReadAllLines(path);
            Assert.HasCount(2, lines);
            StringAssert.Contains(lines[0], nameof(FileLoggerTests));
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

    /// <summary>
    /// Used for testing constructor
    /// </summary>
    [TestMethod]
    public void ConstructorThrowsOnNullPath()
    {
        Assert.ThrowsExactly<ArgumentNullException>(
            () => new FileLogger(null!) { ClassName = nameof(FileLoggerTests) });
    }
}