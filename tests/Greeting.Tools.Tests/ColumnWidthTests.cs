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
}
