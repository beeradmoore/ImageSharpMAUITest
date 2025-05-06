using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SkiaSharp;

namespace ImageSharpMAUITest.Tests;

public class JpgLoadSaveSkiaSharp : ITest
{
    public string TestName { get; } = "Jpg load and save (SkiaSharp)";
    public TestTypes TestType { get; } = TestTypes.JpgLoadSkiaSharp;
    
    public async Task<long> RunTest()
    {
        var outputPath = Helpers.GetTempJpg();
        
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.JpgTestImage).ConfigureAwait(false))
        {
            using (var image = SKBitmap.Decode(stream))
            {
                Console.WriteLine($"{image.Width}x{image.Height}");
                using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 100))
                {
                    await using (var fileStream = File.OpenWrite(outputPath))
                    {
                        data.SaveTo(fileStream);
                    }
                }
            }
        }

        stopWatch.Stop();

        return stopWatch.ElapsedMilliseconds;
    }
}