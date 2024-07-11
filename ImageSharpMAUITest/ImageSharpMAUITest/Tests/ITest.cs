namespace ImageSharpMAUITest.Tests;

public enum TestTypes
{
    JpgLoad,
    JpgResize,
    PngLoad,
    PngResize,
}

public interface ITest
{
    public string TestName { get; }
    
    Task<long> RunTest();
}