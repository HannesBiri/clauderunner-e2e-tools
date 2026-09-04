namespace Greeting.Tools.Core;

/// <summary>Renders rows as a plain-text table for command-line output.</summary>
public static class TextTable
{
    /// <summary>One line per row, columns separated by two spaces.</summary>
    public static string Render(IEnumerable<IReadOnlyList<string>> rows) =>
        string.Join(Environment.NewLine, rows.Select(r => string.Join("  ", r)));
}
