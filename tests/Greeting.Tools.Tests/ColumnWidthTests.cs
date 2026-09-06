using Greeting.Tools.Core;

namespace Greeting.Tools.Tests;

/// <summary>
/// Seeded deliberately red on 2026-09-04 for the ClaudeRunner end-to-end test: ADR 0021 says a gate that was
/// already red parks before it pays for a fix session. This is the red gate.
/// </summary>
public class ColumnWidthTests
{
    [Fact]
    public void PadsEveryColumnToTheWidestCell()
    {
        var rendered = TextTable.Render([["a", "b"], ["ccc", "d"]]);

        Assert.Equal($"a    b{Environment.NewLine}ccc  d", rendered);
    }

    [Fact]
    public void WidensAColumnForItsWidestCellEvenWhenThatCellIsNotInTheFirstRow()
    {
        var rendered = TextTable.Render([["a", "1"], ["bbbb", "2"], ["c", "3"]]);

        Assert.Equal($"a     1{Environment.NewLine}bbbb  2{Environment.NewLine}c     3", rendered);
    }

    [Fact]
    public void NeverPadsTheLastCellOfARow()
    {
        var rendered = TextTable.Render([["a", "bbb"], ["cc", "d"]]);

        Assert.All(rendered.Split(Environment.NewLine), line => Assert.DoesNotMatch(@"\s$", line));
    }

    [Fact]
    public void RendersAnEmptySequenceAsAnEmptyString()
    {
        var rendered = TextTable.Render([]);

        Assert.Equal(string.Empty, rendered);
    }

    [Fact]
    public void RendersRaggedRowsWithoutThrowing()
    {
        var rendered = TextTable.Render([["a", "bb", "ccc"], ["d"], ["ee", "ff"]]);

        Assert.Equal($"a   bb  ccc{Environment.NewLine}d{Environment.NewLine}ee  ff", rendered);
    }
}
