namespace Greeting.Tools.Core;

/// <summary>Renders rows as a plain-text table for command-line output.</summary>
public static class TextTable
{
    /// <summary>One line per row, each column padded to its widest cell and separated by two spaces.</summary>
    public static string Render(IEnumerable<IReadOnlyList<string>> rows)
    {
        var rowList = rows.ToList();
        return rowList.Count == 0 ? string.Empty : RenderLayout(rowList);
    }

    /// <summary>
    /// The header line followed by one line per row, each column padded to its widest cell and separated by
    /// two spaces. The header takes part in the column-width calculation alongside every row, so a header
    /// longer than every cell beneath it still widens that column to the header's width. There is no separator
    /// line between the header and the data. With no rows, the result is the header line alone.
    /// </summary>
    public static string Render(IReadOnlyList<string> header, IEnumerable<IReadOnlyList<string>> rows)
    {
        var lines = new List<IReadOnlyList<string>> { header };
        lines.AddRange(rows);
        return RenderLayout(lines);
    }

    private static string RenderLayout(IReadOnlyList<IReadOnlyList<string>> lines)
    {
        var columnCount = lines.Max(r => r.Count);
        var widths = new int[columnCount];
        for (var column = 0; column < columnCount; column++)
        {
            widths[column] = lines.Max(r => column < r.Count ? r[column].Length : 0);
        }

        return string.Join(Environment.NewLine, lines.Select(r => RenderLine(r, widths)));
    }

    private static string RenderLine(IReadOnlyList<string> row, IReadOnlyList<int> widths)
    {
        var cells = row.Select((cell, i) => i == row.Count - 1 ? cell : cell.PadRight(widths[i]));
        return string.Join("  ", cells);
    }
}
