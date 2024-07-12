using System.ComponentModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageSharpMAUITest.Tests;

namespace ImageSharpMAUITest.Pages;

public partial class TestsListPageModel : ObservableObject
{
    WeakReference<TestsListPage> weakPage;
    
    public List<ITest> Tests { get; } = new List<ITest>();
    
    public TestsListPageModel(TestsListPage page)
    {
        weakPage = new WeakReference<TestsListPage>(page);

        var testTypes = Enum.GetValues<TestTypes>();

        foreach (var testType in testTypes)
        {
            Tests.Add(TestManager.GetTest(testType));
        }
    }
    

    [RelayCommand(AllowConcurrentExecutions = false)]
    async Task RunTestAsync(ListView sourceListView)
    {
        if (weakPage.TryGetTarget(out TestsListPage? testsListPage))
        {
            if (sourceListView.SelectedItem is ITest selectedTest)
            {
                var testType = selectedTest.TestType;

                sourceListView.SelectedItem = null;

                var testsToRun = new List<ITest>(10);
                for (int i = 0; i < 10; i++)
                {
                    testsToRun.Add(TestManager.GetTest(testType));
                }

                await testsListPage.Navigation.PushModalAsync(new NavigationPage(new TestPage(testsToRun)));
            }
        }
    }

}