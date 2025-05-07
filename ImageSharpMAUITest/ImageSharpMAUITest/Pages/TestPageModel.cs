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
        
        stringBuilder.AppendLine("Test results:");
        for (int i = 0; i < testsToRun.Count; ++i)
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
            stringBuilder.AppendLine($"{testsToRun[i].TestName} - {testTime}ms");
        }
        stringBuilder.AppendLine("\n\nSummary:");
        foreach (var testType in testResults.Keys)
        {
            stringBuilder.AppendLine($"{testType}");
            stringBuilder.AppendLine($"Test runs: {testResults[testType].Count}");
            stringBuilder.AppendLine($"Min: {testResults[testType].Min()}ms");
            stringBuilder.AppendLine($"Max: {testResults[testType].Max()}ms");
            stringBuilder.AppendLine($"Average: {testResults[testType].Average()}ms");
            stringBuilder.AppendLine();
        }
        stringBuilder.AppendLine();
        
        
#if DEBUG
        stringBuilder.AppendLine("Configuration: Debug");
#else
        stringBuilder.AppendLine("Configuration: Release");
#endif
        
        stringBuilder.AppendLine($"OS Version: {DeviceInfo.Current.VersionString}");
        stringBuilder.AppendLine($"SkiaSharp Version: {SkiaSharp.SkiaSharpVersion.Native.ToString()}");

        var deviceType = "Unknown Device";
        var space = " ";
        // Add device information
        if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        { 
            if (DeviceInfo.Current.DeviceType == DeviceType.Physical)
            {
                deviceType = "iPhone";
            }
            else if (DeviceInfo.Current.DeviceType == DeviceType.Virtual)
            {
                deviceType = "iPhone Simulator";
            } 
        }
        else if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            if (DeviceInfo.Current.DeviceType == DeviceType.Physical)
            {
                deviceType = "Android";
            }
            else if (DeviceInfo.Current.DeviceType == DeviceType.Virtual)
            {
                deviceType = "Android Emulator";
            } 
        }
        else if (DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst)
        { 
            deviceType = "macOS";
        }
        else if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        { 
            deviceType = "Windows";
        }
        
        stringBuilder.Append($"| {string.Empty.PadRight(deviceType.Length, ' ')} ");
        foreach (var testType in testTypes)
        {
            stringBuilder.Append($"| {testType} ");
        }
        stringBuilder.AppendLine("|");

        stringBuilder.Append($"|-{string.Empty.PadRight(deviceType.Length, '-')}-");
        foreach (var testType in testTypes)
        {
            var title = testType.ToString();
            stringBuilder.Append($"|-{string.Empty.PadRight(title.Length, '-')}-");
        }
        stringBuilder.AppendLine("|");
        
        stringBuilder.Append($"| {deviceType} ");
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