using System.Diagnostics;

namespace ImageSharpMAUITest.Tests;

public class JpgLoad : ITest
{
    public string TestName { get; } = "Jpg load";

    public async Task<long> RunTest()
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        using (var stream = await FileSystem.OpenAppPackageFileAsync("forest_bridge.jpg"))
        {
            using (var image = await SixLabors.ImageSharp.Image.LoadAsync(stream))
            {
                Console.WriteLine($"{image.Width}x{image.Height}");
            }
        }

        stopWatch.Stop();

        return stopWatch.ElapsedMilliseconds;
    }
}