namespace Greeting.Tools.Core;

/// <summary>Renders rows as a plain-text table for command-line output.</summary>
public static class TextTable
{
    /// <summary>One line per row, each column padded to its widest cell and separated by two spaces.</summary>
    public static string Render(IEnumerable<IReadOnlyList<string>> rows) =>
        RenderRows(rows.ToList());

    private static string RenderRows(IReadOnlyList<IReadOnlyList<string>> rows)
    {
        if (rows.Count == 0)
        {
            return string.Empty;
        }

        var columnCount = rows.Max(r => r.Count);
        var widths = new int[columnCount];
        for (var column = 0; column < columnCount; column++)
        {
            widths[column] = rows.Max(r => column < r.Count ? r[column].Length : 0);
        }

        return string.Join(Environment.NewLine, rows.Select(r => RenderLine(r, widths)));
    }

    private static string RenderLine(IReadOnlyList<string> row, IReadOnlyList<int> widths)
    {
        var cells = row.Select((cell, i) => i == row.Count - 1 ? cell : cell.PadRight(widths[i]));
        return string.Join("  ", cells);
    }
}
