namespace Greeting.Tools.Core;

/// <summary>Renders rows as a plain-text table for command-line output.</summary>
public static class TextTable
{
    /// <summary>One line per row, columns padded to the widest cell and separated by two spaces.</summary>
    public static string Render(IEnumerable<IReadOnlyList<string>> rows) =>
        RenderTable(header: null, rows);

    /// <summary>
    /// The header as the first line, directly above the data, with no separator line. The header's cells
    /// take part in the same per-column width calculation as the data rows.
    /// </summary>
    public static string Render(IReadOnlyList<string> header, IEnumerable<IReadOnlyList<string>> rows) =>
        RenderTable(header, rows);

    private static string RenderTable(IReadOnlyList<string>? header, IEnumerable<IReadOnlyList<string>> rows)
    {
        var materialized = rows.ToList();
        var allRows = header is null
            ? materialized
            : new List<IReadOnlyList<string>> { header }.Concat(materialized).ToList();

        if (allRows.Count == 0)
        {
            return string.Empty;
        }

        var widths = new List<int>();
        foreach (var row in allRows)
        {
            for (var i = 0; i < row.Count; i++)
            {
                if (i >= widths.Count)
                {
                    widths.Add(row[i].Length);
                }
                else if (row[i].Length > widths[i])
                {
                    widths[i] = row[i].Length;
                }
            }
        }

        return string.Join(Environment.NewLine, allRows.Select(row => RenderRow(row, widths)));
    }

    private static string RenderRow(IReadOnlyList<string> row, IReadOnlyList<int> widths) =>
        string.Join("  ", row.Select((cell, i) => i == row.Count - 1 ? cell : cell.PadRight(widths[i])));
}
