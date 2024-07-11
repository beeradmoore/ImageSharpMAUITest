using System.Diagnostics;
using SixLabors.ImageSharp.Processing;

namespace ImageSharpMAUITest.Tests;

public class PngResize : ITest
{
    public string TestName { get; } = "Png resize";
    
    public async Task<long> RunTest()
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        using (var stream = await FileSystem.OpenAppPackageFileAsync("Bike.png"))
        {
            using (var image = await SixLabors.ImageSharp.Image.LoadAsync(stream))
            {
                image.Mutate(x => x.Resize(100, 200));
            }
        }

        stopWatch.Stop();

        return stopWatch.ElapsedMilliseconds;
    }
}