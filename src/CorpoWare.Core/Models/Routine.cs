namespace CorpoWare.Models;

public sealed class RoutineStep
{
    public Guid Id { get; set; }

    public string Text { get; set; } = "";

    public bool IsDone { get; set; }
}

public sealed class Routine
{
    public Guid Id { get; set; }

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public List<RoutineStep> Steps { get; set; } = new();
}
