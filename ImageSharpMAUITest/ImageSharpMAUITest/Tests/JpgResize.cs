using System.Diagnostics;
using SixLabors.ImageSharp.Processing;

namespace ImageSharpMAUITest.Tests;

public class JpgResize : ITest
{
    public string TestName { get; } = "Jpg resize";
    
    public async Task<long> RunTest()
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        using (var stream = await FileSystem.OpenAppPackageFileAsync("forest_bridge.jpg"))
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