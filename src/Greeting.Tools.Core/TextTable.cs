namespace Greeting.Tools.Core;

/// <summary>Renders rows as a plain-text table for command-line output.</summary>
public static class TextTable
{
    /// <summary>One line per row, columns padded to the widest cell and separated by two spaces.</summary>
    public static string Render(IEnumerable<IReadOnlyList<string>> rows) =>
        RenderTable(header: null, rows);

    /// <summary>
    /// One header line followed by one line per row. The header takes part in the column width
    /// calculation alongside the rows, and columns are padded to the widest cell and separated by
    /// two spaces.
    /// </summary>
    public static string Render(IReadOnlyList<string> header, IEnumerable<IReadOnlyList<string>> rows) =>
        RenderTable(header, rows);

    private static string RenderTable(IReadOnlyList<string>? header, IEnumerable<IReadOnlyList<string>> rows)
    {
        var materializedRows = rows.ToList();
        var widths = ColumnWidths(header, materializedRows);

        var lines = new List<string>();
        if (header is not null) lines.Add(RenderLine(header, widths));
        lines.AddRange(materializedRows.Select(row => RenderLine(row, widths)));

        return string.Join(Environment.NewLine, lines);
    }

    private static int[] ColumnWidths(IReadOnlyList<string>? header, IReadOnlyList<IReadOnlyList<string>> rows)
    {
        var allRows = header is null ? rows : rows.Append(header);
        var columnCount = allRows.Select(row => row.Count).DefaultIfEmpty(0).Max();

        var widths = new int[columnCount];
        foreach (var row in allRows)
        {
            for (var i = 0; i < row.Count; i++)
                widths[i] = Math.Max(widths[i], row[i].Length);
        }

        return widths;
    }

    private static string RenderLine(IReadOnlyList<string> cells, IReadOnlyList<int> widths) =>
        string.Join("  ", cells.Select((cell, i) => i == cells.Count - 1 ? cell : cell.PadRight(widths[i])));
}
