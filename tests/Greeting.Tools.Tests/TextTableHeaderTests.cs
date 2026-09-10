using Greeting.Tools.Core;

namespace Greeting.Tools.Tests;

public class TextTableHeaderTests
{
    [Fact]
    public void PadsDataColumnOutToAHeaderThatIsWiderThanEveryValueBeneathIt()
    {
        var rendered = TextTable.Render(["abc", "y"], [["a", "b"]]);

        Assert.Equal($"abc  y{Environment.NewLine}a    b", rendered);
    }

    [Fact]
    public void RendersTheHeaderAloneWhenThereAreNoDataRows()
    {
        var rendered = TextTable.Render(["Name", "Greeting"], []);

        Assert.Equal("Name  Greeting", rendered);
    }

    [Fact]
    public void PadsTheHeaderOutToAWidestValueComingFromADataCell()
    {
        var rendered = TextTable.Render(["a", "b"], [["ccc", "d"], ["x", "yy"]]);

        Assert.Equal($"a    b{Environment.NewLine}ccc  d{Environment.NewLine}x    yy", rendered);
    }
}
