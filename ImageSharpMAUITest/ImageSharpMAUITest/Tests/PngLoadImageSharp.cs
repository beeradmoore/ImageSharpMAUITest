using System.Diagnostics;

namespace ImageSharpMAUITest.Tests;

public class PngLoadImageSharp : ITest
{
    public string TestName { get; } = "Png load (ImageSharp)";
    public TestTypes TestType { get; } = TestTypes.PngLoadImageSharp;
    
    public async Task<long> RunTest()
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.PngTestImage).ConfigureAwait(false))
        {
            using (var image = await SixLabors.ImageSharp.Image.LoadAsync(stream).ConfigureAwait(false))
            {
                Console.WriteLine($"{image.Width}x{image.Height}");
            }
        }

        stopWatch.Stop();

        return stopWatch.ElapsedMilliseconds;
    }
}