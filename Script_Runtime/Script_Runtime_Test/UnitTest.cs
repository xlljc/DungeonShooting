using Xunit.Abstractions;

public class UnitTest
{
    public static ITestOutputHelper Console;
    
    public UnitTest(ITestOutputHelper helper)
    {
        Console = helper;
    }
}