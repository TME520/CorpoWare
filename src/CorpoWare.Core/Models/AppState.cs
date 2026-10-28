namespace CorpoWare.Models;

public sealed class AppState
{
    public List<ImpromptuItem> Impromptu { get; set; } = new();

    public List<KanbanCard> Cards { get; set; } = new();

    public List<Routine> Routines { get; set; } = new();
}
