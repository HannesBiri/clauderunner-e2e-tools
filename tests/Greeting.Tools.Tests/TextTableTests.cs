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
    public void ReturnsEmptyStringForNoRows()
    {
        var rendered = TextTable.Render([]);

        Assert.Equal(string.Empty, rendered);
    }

    [Fact]
    public void DoesNotPadTheLastCellOnALine()
    {
        var rendered = TextTable.Render([["aaa", "b"], ["c", "d"]]);

        Assert.Equal($"aaa  b{Environment.NewLine}c    d", rendered);
    }

    [Fact]
    public void EndsShortRowsEarlyWithoutThrowing()
    {
        var rendered = TextTable.Render([["aaa", "b", "c"], ["d"]]);

        Assert.Equal($"aaa  b  c{Environment.NewLine}d", rendered);
    }

    [Fact]
    public void HeaderLongerThanEveryCellWidensTheColumn()
    {
        var rendered = TextTable.Render(["Name", "b"], [["a", "d"]]);

        Assert.Equal($"Name  b{Environment.NewLine}a     d", rendered);
    }

    [Fact]
    public void HeaderWithNoRowsRendersJustTheHeaderLine()
    {
        var rendered = TextTable.Render(["Name", "Greeting"], []);

        Assert.Equal("Name  Greeting", rendered);
    }

    [Fact]
    public void HeaderOverloadEndsShortRowsEarlyWithoutThrowing()
    {
        var rendered = TextTable.Render(["Name", "Greeting", "Extra"], [["a"], ["bb", "c"]]);

        Assert.Equal(
            $"Name  Greeting  Extra{Environment.NewLine}a{Environment.NewLine}bb    c",
            rendered);
    }
}
