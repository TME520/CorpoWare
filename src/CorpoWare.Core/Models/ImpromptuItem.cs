namespace CorpoWare.Models;

/// <summary>
/// An ask that arrived outside a ticket tracker: a Slack DM, an email, a walk-in, or a verbal request.
/// </summary>
public sealed class ImpromptuItem
{
    public Guid Id { get; set; }

    public string Title { get; set; } = "";

    /// <summary>Slack DM, Email, Walk-in, or Verbal.</summary>
    public string Source { get; set; } = "";

    public string AskedBy { get; set; } = "";

    public string Context { get; set; } = "";

    public string FollowUpNote { get; set; } = "";

    public DateTimeOffset CreatedAt { get; set; }
}
