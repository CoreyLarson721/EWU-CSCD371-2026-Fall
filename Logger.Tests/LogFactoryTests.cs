namespace Logger.Tests;

[TestClass]
public class LogFactoryTests
{
    [TestMethod]
    public void CreateLoggerReturnsNullWhenNotConfigured()
    {
        Assert.IsNull(new LogFactory().CreateLogger(nameof(LogFactoryTests)));
    }

    [TestMethod]
    public void CreateLoggerReturnsFileLoggerWithClassNameWhenConfigured()
    {
        var factory = new LogFactory();
        factory.ConfigureFileLogger("log.txt");

        BaseLogger logger = factory.CreateLogger(nameof(LogFactoryTests));

        Assert.IsInstanceOfType<FileLogger>(logger);
        Assert.AreEqual(nameof(LogFactoryTests), logger.ClassName);
    }

    [TestMethod]
    public void ConfigureFileLoggerThrowsOnNullOrEmptyPath()
    {
        var factory = new LogFactory();
        Assert.ThrowsExactly<ArgumentNullException>(() => factory.ConfigureFileLogger(null!));
        Assert.ThrowsExactly<ArgumentException>(() => factory.ConfigureFileLogger(" "));
    }
}