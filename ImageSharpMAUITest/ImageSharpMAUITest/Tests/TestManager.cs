namespace ImageSharpMAUITest.Tests;

public class TestManager
{
    public static ITest GetTest(TestTypes testType)
    {
        return testType switch
        {
            #if DEBUG
            TestTypes.DelayTest => new DelayTest(),
            #endif
            TestTypes.JpgLoadImageSharp => new JpgLoadImageSharp(),
            TestTypes.JpgLoadSkiaSharp => new JpgLoadSkiaSharp(),
            
            TestTypes.JpgLoadSaveImageSharp => new JpgLoadSaveImageSharp(),
            TestTypes.JpgLoadSaveSkiaSharp => new JpgLoadSaveSkiaSharp(),
            
            TestTypes.JpgResizeImageSharp => new JpgResizeImageSharp(),
            TestTypes.JpgResizeSkiaSharp => new JpgResizeSkiaSharp(),
            
            TestTypes.PngLoadImageSharp => new PngLoadImageSharp(),
            
            TestTypes.PngResizeImageSharp => new PngResizeImageSharp(),
            _ => throw new NotImplementedException()
        };
    }
}