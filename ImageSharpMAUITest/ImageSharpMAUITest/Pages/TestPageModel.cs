using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageSharpMAUITest.Tests;

namespace ImageSharpMAUITest.Pages;

public partial class TestPageModel : ObservableObject
{
    WeakReference<TestPage> weakPage;

    [ObservableProperty]
    string title = "Running tests...";
    [ObservableProperty]
    int currentTest = 0;
    
    [ObservableProperty]
    string currentTestName = string.Empty;
    
    public int TotalTests { get; init; }

    [ObservableProperty]
    bool isRunningTests = true;
    
    [ObservableProperty]
    string resultText = string.Empty;

    List<ITest> testsToRun;
    
    Dictionary<TestTypes, List<long>> testResults = new Dictionary<TestTypes, List<long>>();
    
    public TestPageModel(TestPage page, List<ITest> testsToRun)
    {
        weakPage = new WeakReference<TestPage>(page);
        TotalTests = testsToRun.Count;
        this.testsToRun = testsToRun;
    }

    public async Task RunTestsAsync()
    {
        var stringBuilder = new StringBuilder();
#if DEBUG
        stringBuilder.AppendLine("Configuration: Debug");
#else
        stringBuilder.AppendLine("Configuration: Release");
#endif
        
        stringBuilder.AppendLine("Test results:");
        for (var i = 0; i < testsToRun.Count; ++i)
        {
            CurrentTest = i + 1;
            CurrentTestName = testsToRun[i].TestName;
            var currentTestType = testsToRun[i].TestType;
            var testTime = await testsToRun[i].RunTest();
            if (testResults.ContainsKey(currentTestType) == false)
            {
                testResults.Add(currentTestType, new List<long>());
            }
            testResults[currentTestType].Add(testTime);
            stringBuilder.AppendLine($"{testsToRun[i].TestName} - {testTime:F1}ms");
        }
        stringBuilder.AppendLine("\n\nSummary:");
        
        
        var testTypes = testResults.Keys.ToList();
        
        foreach (var testType in testTypes)
        {
            stringBuilder.AppendLine($"{testType}");
            stringBuilder.AppendLine($"Test runs: {testResults[testType].Count}");
            stringBuilder.AppendLine($"Min: {testResults[testType].Min():F1}ms");
            stringBuilder.AppendLine($"Max: {testResults[testType].Max():F1}ms");
            stringBuilder.AppendLine($"Average: {testResults[testType].Average():F1}ms");
            stringBuilder.AppendLine();
        }
        stringBuilder.AppendLine();
        
        foreach (var testType in testTypes)
        {
            stringBuilder.Append($"| {testType} ");
        }
        stringBuilder.AppendLine("|");
        
        foreach (var testType in testTypes)
        {
            var title = testType.ToString();
            var padding = "--";
            stringBuilder.Append($"|-{padding.PadRight(title.Length, '-')}-");
        }
        stringBuilder.AppendLine("|");

        foreach (var testType in testTypes)
        {
            var title = testType.ToString();
            var result = $"{testResults[testType].Average():F1}ms";
            stringBuilder.Append($"| {result.PadRight(title.Length, ' ')} ");
        }
        stringBuilder.AppendLine("|");
        
        ResultText = stringBuilder.ToString();
        

        Console.WriteLine(ResultText);
        
        IsRunningTests = false;
        Title = "Results";
    }

    [RelayCommand(AllowConcurrentExecutions = false)]
    async Task DoneAsync()
    {
        if (weakPage.TryGetTarget(out TestPage? testPage))
        {
            await testPage.Navigation.PopModalAsync();
        }
    }

    [RelayCommand(AllowConcurrentExecutions = false)]
    async Task ShareAsync()
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = ResultText,
            Title = "Share results"
        });
    }
}