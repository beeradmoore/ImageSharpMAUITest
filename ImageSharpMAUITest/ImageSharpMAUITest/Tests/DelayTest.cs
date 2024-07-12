using System.Diagnostics;

namespace ImageSharpMAUITest.Tests;

#if DEBUG
public class DelayTest : ITest
{
    public string TestName { get; } = "Delay test (debug only)";
    public TestTypes TestType { get; } = TestTypes.DelayTest;
    public async Task<long> RunTest()
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        await Task.Delay(100);
        stopWatch.Stop();
        return stopWatch.ElapsedMilliseconds;
    }
}
#endif