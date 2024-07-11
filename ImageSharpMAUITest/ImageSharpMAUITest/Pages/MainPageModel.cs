using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ImageSharpMAUITest.Pages;

public partial class MainPageModel : ObservableObject
{
    WeakReference<MainPage> weakPage;
    
    public MainPageModel(MainPage page)
    {
        weakPage = new WeakReference<MainPage>(page);
    }

    [RelayCommand(AllowConcurrentExecutions = false)]
    async Task RunAllTestsAsync()
    {
        
    }


    [RelayCommand(AllowConcurrentExecutions = false)]
    async Task RunSpecificTestAsync()
    {
        if (weakPage.TryGetTarget(out MainPage? mainPage))
        {
            await mainPage.Navigation.PushAsync(new TestsListPage());
        }
    }


    [RelayCommand(AllowConcurrentExecutions = false)]
    async Task ViewLogsAsync()
    {
        if (weakPage.TryGetTarget(out MainPage? mainPage))
        {
            //await mainPage.Navigation.PushAsync(new TestsListPage());
        }
    }
}