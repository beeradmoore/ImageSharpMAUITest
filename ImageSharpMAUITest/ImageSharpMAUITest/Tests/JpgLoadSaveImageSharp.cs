using System.Diagnostics;
using SixLabors.ImageSharp;

namespace ImageSharpMAUITest.Tests;

public class JpgLoadSaveImageSharp : ITest
{
    public string TestName { get; } = "Jpg load and save (ImageSharp)";
    public TestTypes TestType { get; } = TestTypes.JpgLoadSaveImageSharp;

    public async Task<long> RunTest()
    {
        var outputPath = Helpers.GetTempJpg();
        
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.JpgTestImage).ConfigureAwait(false))
        {
            using (var image = await SixLabors.ImageSharp.Image.LoadAsync(stream).ConfigureAwait(false))
            {
                Console.WriteLine($"{image.Width}x{image.Height}");
                await image.SaveAsJpegAsync(outputPath).ConfigureAwait(false);
            }
        }

        stopWatch.Stop();

        return stopWatch.ElapsedMilliseconds;
    }
}