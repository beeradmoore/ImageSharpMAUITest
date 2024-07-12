using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using ImageSharpMAUITest.Tests;

namespace ImageSharpMAUITest.Pages;

public partial class TestPage : ContentPage
{
    public TestPage(List<ITest> testsToRun)
    {
        InitializeComponent();
        BindingContext = new TestPageModel(this, testsToRun);
    }

    bool hasFirstAppeared = false;
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (hasFirstAppeared == false)
        {
            hasFirstAppeared = true;
            if (BindingContext is TestPageModel model)
            {
                model.RunTestsAsync().SafeFireAndForget();
            }
        }
    }
}