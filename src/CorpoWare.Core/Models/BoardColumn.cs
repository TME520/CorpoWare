namespace CorpoWare.Models;

public enum BoardColumn
{
    Backlog = 0,
    Todo = 1,
    Doing = 2,
    Done = 3
}

public static class BoardColumnLabels
{
    public static string ToLabel(this BoardColumn column) => column switch
    {
        BoardColumn.Backlog => "BACKLOG",
        BoardColumn.Todo => "TODO",
        BoardColumn.Doing => "DOING",
        BoardColumn.Done => "DONE",
        _ => column.ToString().ToUpperInvariant()
    };
}
