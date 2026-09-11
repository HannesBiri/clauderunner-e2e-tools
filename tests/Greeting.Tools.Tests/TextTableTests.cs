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
    public void LeavesNoTrailingWhitespaceWhenTheLastCellIsShorterThanItsColumn()
    {
        var rendered = TextTable.Render([["a", "bb"], ["ccc", "d"]]);

        Assert.Equal($"a    bb{Environment.NewLine}ccc  d", rendered);
    }
}
