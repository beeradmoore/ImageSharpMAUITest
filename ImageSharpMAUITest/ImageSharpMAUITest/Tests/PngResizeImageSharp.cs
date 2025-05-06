using System.Diagnostics;
using SixLabors.ImageSharp.Processing;

namespace ImageSharpMAUITest.Tests;

public class PngResizeImageSharp : ITest
{
    public string TestName { get; } = "Png resize (ImageSharp)";
    public TestTypes TestType { get; } = TestTypes.PngResizeImageSharp;
    
    public async Task<long> RunTest()
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.PngTestImage).ConfigureAwait(false))
        {
            using (var image = await SixLabors.ImageSharp.Image.LoadAsync(stream).ConfigureAwait(false))
            {
                image.Mutate(x => x.Resize(100, 200));
            }
        }

        stopWatch.Stop();

        return stopWatch.ElapsedMilliseconds;
    }
}