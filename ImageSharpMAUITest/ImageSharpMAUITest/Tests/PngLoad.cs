using System.Diagnostics;

namespace ImageSharpMAUITest.Tests;

public class PngLoad : ITest
{
    public string TestName { get; } = "Png load";
    
    public async Task<long> RunTest()
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        using (var stream = await FileSystem.OpenAppPackageFileAsync("Bike.png"))
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