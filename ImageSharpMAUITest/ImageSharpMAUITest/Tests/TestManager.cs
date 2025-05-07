namespace ImageSharpMAUITest.Tests;

public class TestManager
{
    public static ITest GetTest(TestTypes testType)
    {
        return testType switch
        {
            #if DEBUG
            TestTypes.DelayTest => new DelayTest(),
            #endif
            TestTypes.JpgLoad => new JpgLoad(),
            TestTypes.JpgResize => new JpgResize(),
            TestTypes.PngLoad => new PngLoad(),
            TestTypes.PngResize => new PngResize(),
            _ => throw new NotImplementedException()
        };
    }
}