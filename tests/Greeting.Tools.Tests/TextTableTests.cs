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
}
