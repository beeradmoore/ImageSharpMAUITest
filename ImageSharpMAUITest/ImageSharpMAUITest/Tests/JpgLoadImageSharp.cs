using System.Diagnostics;

namespace ImageSharpMAUITest.Tests;

public class JpgLoadImageSharp : ITest
{
    public string TestName { get; } = "Jpg load (ImageSharp)";
    public TestTypes TestType { get; } = TestTypes.JpgLoadImageSharp;

    public async Task<long> RunTest()
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.JpgTestImage).ConfigureAwait(false))
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