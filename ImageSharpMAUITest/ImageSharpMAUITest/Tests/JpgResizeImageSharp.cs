using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ImageSharpMAUITest.Tests;

public class JpgResizeImageSharp : ITest
{
    public string TestName { get; } = "Jpg resize (ImageSharp)";
    public TestTypes TestType { get; } = TestTypes.JpgResizeImageSharp;
    
    public async Task<long> RunTest()
    {
        var outputPath = Helpers.GetTempJpg();
        
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.JpgTestImage).ConfigureAwait(false))
        {
            using (var image = await SixLabors.ImageSharp.Image.LoadAsync(stream).ConfigureAwait(false))
            {
                image.Mutate(x => x.Resize(100, 200));
                await image.SaveAsJpegAsync(outputPath).ConfigureAwait(false);
            }
        }

        stopWatch.Stop();

        return stopWatch.ElapsedMilliseconds;
    }
}