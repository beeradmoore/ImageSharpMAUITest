using System.Diagnostics;
using SixLabors.ImageSharp.Processing;

namespace ImageSharpMAUITest;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new MainPage());
    }
}