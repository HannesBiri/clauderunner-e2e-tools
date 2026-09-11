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
}
