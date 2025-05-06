using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SkiaSharp;

namespace ImageSharpMAUITest.Tests;

public class JpgLoadSkiaSharp : ITest
{
    public string TestName { get; } = "Jpg load (SkiaSharp)";
    public TestTypes TestType { get; } = TestTypes.JpgLoadSkiaSharp;
    
    public async Task<long> RunTest()
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.JpgTestImage).ConfigureAwait(false))
        {
            using (var image = SKBitmap.Decode(stream))
            {
                Console.WriteLine($"{image.Width}x{image.Height}");
            }
        }

        stopWatch.Stop();

        return stopWatch.ElapsedMilliseconds;
    }
}