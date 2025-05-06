using System.Diagnostics;
using SixLabors.ImageSharp.Processing;
using SkiaSharp;

namespace ImageSharpMAUITest.Tests;

public class JpgResizeSkiaSharp : ITest
{
    public string TestName { get; } = "Jpg resize (SkiaSharp)";
    public TestTypes TestType { get; } = TestTypes.JpgResizeSkiaSharp;
    
    public async Task<long> RunTest()
    {
        var outputPath = Helpers.GetTempJpg();
        
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.JpgTestImage).ConfigureAwait(false))
        {
            using (var image = SKBitmap.Decode(stream))
            {
                using (var resizedImage = image.Resize(new SKSizeI(100, 200), SKSamplingOptions.Default))
                {
                    using (var data = resizedImage.Encode(SKEncodedImageFormat.Jpeg, 100))
                    {
                        await using (var outputStream = File.OpenWrite(outputPath))
                        {
                            data.SaveTo(outputStream);
                        }
                    }
                }
            }
        }

        stopWatch.Stop();

        return stopWatch.ElapsedMilliseconds;
    }
}