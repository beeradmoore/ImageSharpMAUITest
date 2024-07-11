using System.ComponentModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageSharpMAUITest.Tests;

namespace ImageSharpMAUITest.Pages;

public partial class TestsListPageModel : ObservableObject
{
    TestTypes[] TestTypes { get; init; }
    
    WeakReference<TestsListPage> weakPage;
    
    public List<ITest> Tests { get; } = new List<ITest>();

    [ObservableProperty]
    ITest? selectedTest;
    
    public TestsListPageModel(TestsListPage page)
    {
        weakPage = new WeakReference<TestsListPage>(page);

        TestTypes = Enum.GetValues<TestTypes>();

        foreach (var testType in TestTypes)
        {
            Tests.Add(TestManager.GetTest(testType));
        }
    }
    

    [RelayCommand(AllowConcurrentExecutions = false)]
    async Task RunTestAsync(ListView sourceListView)
    {
        if (sourceListView.SelectedItem is ITest selectedTest)
        {
            sourceListView.SelectedItem = null;

            //await selectedTest.RunTest();
        }
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(SelectedTest))
        {
            if (SelectedTest == null)
            {
                return;
            }

            var testToRun = SelectedTest;
            SelectedTest = null;
        }
    }
}