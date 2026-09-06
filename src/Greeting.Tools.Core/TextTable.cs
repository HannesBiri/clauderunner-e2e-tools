namespace Greeting.Tools.Core;

/// <summary>Renders rows as a plain-text table for command-line output.</summary>
public static class TextTable
{
    /// <summary>One line per row, each column padded to its widest cell.</summary>
    public static string Render(IEnumerable<IReadOnlyList<string>> rows)
    {
        var rowList = rows.ToList();
        if (rowList.Count == 0)
            return string.Empty;

        var columnCount = rowList.Max(r => r.Count);
        var widths = new int[columnCount];
        for (var column = 0; column < columnCount; column++)
            widths[column] = rowList.Max(r => column < r.Count ? r[column].Length : 0);

        return string.Join(Environment.NewLine, rowList.Select(r => string.Join("  ", r.Select(
            (cell, column) => column == r.Count - 1 ? cell : cell.PadRight(widths[column])))));
    }
}
