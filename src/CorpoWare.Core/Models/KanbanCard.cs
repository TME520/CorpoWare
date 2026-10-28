namespace CorpoWare.Models;

public sealed class KanbanCard
{
    public Guid Id { get; set; }

    public string Title { get; set; } = "";

    public string Detail { get; set; } = "";

    public BoardColumn Column { get; set; }

    /// <summary>Set when this card was captured from the impromptu inbox.</summary>
    public Guid? ImpromptuItemId { get; set; }
}
