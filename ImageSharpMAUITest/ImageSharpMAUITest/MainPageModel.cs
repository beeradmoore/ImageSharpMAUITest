using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ImageSharpMAUITest;

public partial class MainPageModel : ObservableObject
{
    WeakReference<MainPage> weakPage;

    [ObservableProperty]
    string outputText = string.Empty;
    
    public MainPageModel(MainPage page)
    {
        weakPage = new WeakReference<MainPage>(page);
    }

    [RelayCommand(AllowConcurrentExecutions = false)]
    async Task LoadImageAsync()
    {
        OutputText += "\n";
        try
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            
            using (var stream = await FileSystem.OpenAppPackageFileAsync("sloth.jpg"))
            {
                using (var image = await SixLabors.ImageSharp.Image.LoadAsync(stream))
                {
                    OutputText += $"{image.Width}x{image.Height}\n";
                }
            }

            stopWatch.Stop();
            OutputText += $"{stopWatch.ElapsedMilliseconds}ms\n";
        }
        catch (Exception err)
        {
            OutputText += $"ERROR: {err.Message}\n";
            Debugger.Break();
        }
    }
}