namespace ImageSharpMAUITest.Tests;

public enum TestTypes
{
    #if DEBUG
    DelayTest,
    #endif
    JpgLoad,
    JpgResize,
    PngLoad,
    PngResize,
}

public interface ITest
{
    public string TestName { get; }
    public TestTypes TestType { get; }
    
    Task<long> RunTest();
}