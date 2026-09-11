using System.Collections;
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

    [Fact]
    public void HeaderCellLongerThanEveryValueWidensTheColumnForTheDataLines()
    {
        var rendered = TextTable.Render(["Name", "X"], [["a", "b"], ["c", "d"]]);

        Assert.Equal($"Name  X{Environment.NewLine}a     b{Environment.NewLine}c     d", rendered);
    }

    [Fact]
    public void HeaderWithNoDataRowsRendersOnlyTheHeaderLine()
    {
        var rendered = TextTable.Render(["Name", "Greeting"], []);

        Assert.Equal("Name  Greeting", rendered);
    }

    [Fact]
    public void RowWithFewerCellsThanTheHeaderDoesNotThrowOrDisturbTheWidthsItFills()
    {
        var rendered = TextTable.Render(["Name", "Greeting"], [["a"], ["ccc", "d"]]);

        Assert.Equal($"Name  Greeting{Environment.NewLine}a{Environment.NewLine}ccc   d", rendered);
    }

    [Fact]
    public void RendersCorrectlyWhenRowsCanOnlyBeEnumeratedOnce()
    {
        var rendered = TextTable.Render(new OneShotRows());

        Assert.Equal($"a    bb{Environment.NewLine}ccc  d", rendered);
    }

    [Fact]
    public void RendersCorrectlyWithHeaderWhenRowsCanOnlyBeEnumeratedOnce()
    {
        var rendered = TextTable.Render(["Name", "X"], new OneShotRows());

        Assert.Equal($"Name  X{Environment.NewLine}a     bb{Environment.NewLine}ccc   d", rendered);
    }

    /// <summary>An IEnumerable that throws if enumerated more than once, so a test using it fails on double enumeration.</summary>
    private sealed class OneShotRows : IEnumerable<IReadOnlyList<string>>
    {
        private bool _enumerated;

        public IEnumerator<IReadOnlyList<string>> GetEnumerator()
        {
            if (_enumerated) throw new InvalidOperationException("Enumerated more than once.");
            _enumerated = true;
            return Rows().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static IEnumerable<IReadOnlyList<string>> Rows()
        {
            yield return ["a", "bb"];
            yield return ["ccc", "d"];
        }
    }
}
