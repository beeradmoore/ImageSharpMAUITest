using System.Diagnostics;

namespace ImageSharpMAUITest;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageModel(this);
    }

}