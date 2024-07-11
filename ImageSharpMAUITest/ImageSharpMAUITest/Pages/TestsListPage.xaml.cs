using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSharpMAUITest.Pages;

public partial class TestsListPage : ContentPage
{
    public TestsListPage()
    {
        InitializeComponent();
        BindingContext = new TestsListPageModel(this);
    }
}