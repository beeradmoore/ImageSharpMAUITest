using System.Globalization;
using System.Runtime.InteropServices;

namespace ImageSharpMAUITest;

public static class Helpers
{
    public const string JpgTestImage = "forest_bridge.jpg";
    public const string PngTestImage = "Bike.png";
    
    public static string GetTempPng()
    {
        return Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture)}.png");
    }
    
    public static string GetTempJpg()
    {
        return Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture)}.jpg");
    }
}