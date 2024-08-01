using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageSharpMAUITest.Tests;

namespace ImageSharpMAUITest.Pages;

public partial class MainPageModel : ObservableObject
{
    WeakReference<MainPage> weakPage;
    
    public MainPageModel(MainPage page)
    {
        Console.WriteLine("Vector.IsHardwareAccelerated: " + (System.Numerics.Vector.IsHardwareAccelerated ? "True" : "False"));
        weakPage = new WeakReference<MainPage>(page);
    }

    [RelayCommand(AllowConcurrentExecutions = false)]
    async Task RunAllTestsAsync()
    {
        if (weakPage.TryGetTarget(out MainPage? mainPage))
        {
            var testTypes = Enum.GetValues<TestTypes>();
            var testsToRun = new List<ITest>();
            foreach (var testType in testTypes)
            {
                for (int i = 0; i < 10; ++i)
                {
                    testsToRun.Add(TestManager.GetTest(testType));
                }
            }
            await mainPage.Navigation.PushModalAsync(new NavigationPage(new TestPage(testsToRun)));
        }
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
            await Task.Delay(1);
            //await mainPage.Navigation.PushAsync(new TestsListPage());
        }
    }
}