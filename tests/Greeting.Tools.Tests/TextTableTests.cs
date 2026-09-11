using Greeting.Tools.Core;

namespace Greeting.Tools.Tests;

public class TextTableTests
{
    [Fact]
    public void RendersOneLinePerRow()
    {
        var rendered = TextTable.Render([["a", "b"], ["c", "d"]]);

        Assert.Equal($"a  b{Environment.NewLine}c  d", rendered);
    }

    [Fact]
    public void RowsOnlyCallProducesNoHeaderLineAndNoTrailingWhitespace()
    {
        var rendered = TextTable.Render([["a", "b"], ["ccc", "d"]]);

        Assert.Equal($"a    b{Environment.NewLine}ccc  d", rendered);
        Assert.All(rendered.Split(Environment.NewLine), line => Assert.False(line.EndsWith(' ')));
    }

    [Fact]
    public void RowWithFewerCellsThanTheHeaderEndsAfterItsLastCell()
    {
        var rendered = TextTable.Render(["Name", "Greeting"], [["Ann", "Hi"], ["Bo"]]);

        Assert.Equal($"Name  Greeting{Environment.NewLine}Ann   Hi{Environment.NewLine}Bo", rendered);
    }

    [Fact]
    public void HeaderCellLongerThanEveryDataCellWidensTheColumn()
    {
        var rendered = TextTable.Render(["Name", "Greeting"], [["Al", "Hi"]]);

        Assert.Equal($"Name  Greeting{Environment.NewLine}Al    Hi", rendered);
    }

    [Fact]
    public void HeaderWithNoDataRowsRendersJustTheHeaderLine()
    {
        var rendered = TextTable.Render(["Name", "Greeting"], []);

        Assert.Equal("Name  Greeting", rendered);
    }
}
