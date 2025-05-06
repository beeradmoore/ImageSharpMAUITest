namespace ImageSharpMAUITest.Tests;

public enum TestTypes
{
    #if DEBUG
    DelayTest,
    #endif
    JpgLoadImageSharp,
    JpgLoadSaveImageSharp,
    JpgLoadSkiaSharp,
    JpgLoadSaveSkiaSharp,
    JpgResizeImageSharp,
    JpgResizeSkiaSharp,
    PngLoadImageSharp,
    PngResizeImageSharp,
}

public interface ITest
{
    public string TestName { get; }
    public TestTypes TestType { get; }
    
    Task<long> RunTest();
}