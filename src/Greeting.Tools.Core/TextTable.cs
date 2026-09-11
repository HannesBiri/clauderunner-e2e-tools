namespace Greeting.Tools.Core;

/// <summary>Renders rows as a plain-text table for command-line output.</summary>
public static class TextTable
{
    /// <summary>One line per row, columns padded to the widest cell and separated by two spaces.</summary>
    public static string Render(IEnumerable<IReadOnlyList<string>> rows) => Layout(rows.ToList());

    private static string Layout(IReadOnlyList<IReadOnlyList<string>> lines)
    {
        var widths = new List<int>();
        foreach (var line in lines)
        {
            for (var i = 0; i < line.Count; i++)
            {
                if (i == widths.Count) widths.Add(0);
                widths[i] = Math.Max(widths[i], line[i].Length);
            }
        }

        return string.Join(Environment.NewLine, lines.Select(line => RenderLine(line, widths)));
    }

    private static string RenderLine(IReadOnlyList<string> line, IReadOnlyList<int> widths) =>
        string.Join("  ", line.Select((cell, i) => i == line.Count - 1 ? cell : cell.PadRight(widths[i])));
}
