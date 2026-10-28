using CorpoWare.Models;

namespace CorpoWare.Persistence;

/// <summary>
/// Sample rows written the first time <c>data/state.json</c> is created.
/// Fixed ids keep the inbox items linked to their Kanban cards.
/// </summary>
public static class SeedData
{
    public static readonly Guid Q3SlideId = Guid.Parse("4f1c2a10-7b3e-4d5a-9c11-0a1b2c3d4e01");
    public static readonly Guid VendorPoId = Guid.Parse("4f1c2a10-7b3e-4d5a-9c11-0a1b2c3d4e02");
    public static readonly Guid PrinterId = Guid.Parse("4f1c2a10-7b3e-4d5a-9c11-0a1b2c3d4e03");
    public static readonly Guid OutageNoteId = Guid.Parse("4f1c2a10-7b3e-4d5a-9c11-0a1b2c3d4e04");

    public static AppState Create()
    {
        return new AppState
        {
            Impromptu =
            [
                new ImpromptuItem
                {
                    Id = Q3SlideId,
                    Source = "Slack DM",
                    AskedBy = "Morgan Hale (PM)",
                    Title = "Sanity-check the Q3 review slide before standup",
                    Context = "Sent in a direct message. The deck is on the shared drive under Q3/review. No ticket was opened.",
                    FollowUpNote = "Reply before 10:00.",
                    CreatedAt = new DateTimeOffset(2026, 10, 28, 8, 40, 0, TimeSpan.Zero)
                },
                new ImpromptuItem
                {
                    Id = VendorPoId,
                    Source = "Email",
                    AskedBy = "Accounts payable",
                    Title = "Vendor needs a new PO number",
                    Context = "Thread from Northwind Supplies about invoice 1842. Procurement does not have a ticket for it.",
                    FollowUpNote = "Send the PO number, or name the person who owns it.",
                    CreatedAt = new DateTimeOffset(2026, 10, 27, 16, 5, 0, TimeSpan.Zero)
                },
                new ImpromptuItem
                {
                    Id = PrinterId,
                    Source = "Walk-in",
                    AskedBy = "Jordan Lee (facilities)",
                    Title = "4th floor printer is jammed again",
                    Context = "Asked at the desk between meetings. It is not in the facilities queue.",
                    FollowUpNote = "Ping facilities if it is still down tomorrow.",
                    CreatedAt = new DateTimeOffset(2026, 10, 27, 11, 20, 0, TimeSpan.Zero)
                },
                new ImpromptuItem
                {
                    Id = OutageNoteId,
                    Source = "Verbal",
                    AskedBy = "Skip-level, in the hallway",
                    Title = "Add the outage timeline to Friday's notes",
                    Context = "Said after the incident review. The shared notes doc has no action item for it.",
                    FollowUpNote = "Add a short paragraph to Friday's notes.",
                    CreatedAt = new DateTimeOffset(2026, 10, 26, 18, 10, 0, TimeSpan.Zero)
                }
            ],
            Cards =
            [
                new KanbanCard
                {
                    Id = Guid.Parse("6a2d8b20-1c4e-4f60-8d22-1b2c3d4e5f01"),
                    Title = "Draft the Friday outage timeline note",
                    Detail = "From a hallway ask. Keep a local copy of the paragraph.",
                    Column = BoardColumn.Backlog,
                    ImpromptuItemId = OutageNoteId
                },
                new KanbanCard
                {
                    Id = Guid.Parse("6a2d8b20-1c4e-4f60-8d22-1b2c3d4e5f02"),
                    Title = "Collect printer screenshots if facilities asks",
                    Detail = "Walk-in from Jordan. Nothing to do unless they reply.",
                    Column = BoardColumn.Backlog,
                    ImpromptuItemId = PrinterId
                },
                new KanbanCard
                {
                    Id = Guid.Parse("6a2d8b20-1c4e-4f60-8d22-1b2c3d4e5f03"),
                    Title = "Reply on the vendor PO email",
                    Detail = "From the impromptu inbox. Invoice 1842.",
                    Column = BoardColumn.Todo,
                    ImpromptuItemId = VendorPoId
                },
                new KanbanCard
                {
                    Id = Guid.Parse("6a2d8b20-1c4e-4f60-8d22-1b2c3d4e5f04"),
                    Title = "Sanity-check the Q3 review slide",
                    Detail = "Slack DM from Morgan. Due before standup.",
                    Column = BoardColumn.Doing,
                    ImpromptuItemId = Q3SlideId
                },
                new KanbanCard
                {
                    Id = Guid.Parse("6a2d8b20-1c4e-4f60-8d22-1b2c3d4e5f05"),
                    Title = "Reinstall the VPN profile",
                    Detail = "Finished yesterday. Kept here as a record of the work.",
                    Column = BoardColumn.Done
                }
            ],
            Routines =
            [
                new Routine
                {
                    Id = Guid.Parse("8c3e9c30-2d5f-4071-9e33-2c3d4e5f6a01"),
                    Name = "Start of day",
                    Description = "A few minutes before the first meeting.",
                    Steps =
                    [
                        Step("8c3e9c30-2d5f-4071-9e33-2c3d4e5f6a11", "Open the impromptu inbox and read anything new."),
                        Step("8c3e9c30-2d5f-4071-9e33-2c3d4e5f6a12", "Glance at today's calendar for likely walk-ins."),
                        Step("8c3e9c30-2d5f-4071-9e33-2c3d4e5f6a13", "Move one TODO card to DOING.")
                    ]
                },
                new Routine
                {
                    Id = Guid.Parse("8c3e9c30-2d5f-4071-9e33-2c3d4e5f6a02"),
                    Name = "End of day",
                    Description = "Leave a short record of what actually moved.",
                    Steps =
                    [
                        Step("8c3e9c30-2d5f-4071-9e33-2c3d4e5f6a21", "Move finished cards to DONE.", isDone: true),
                        Step("8c3e9c30-2d5f-4071-9e33-2c3d4e5f6a22", "Write one line on anything still waiting on someone else."),
                        Step("8c3e9c30-2d5f-4071-9e33-2c3d4e5f6a23", "Clear notifications you already captured.")
                    ]
                },
                new Routine
                {
                    Id = Guid.Parse("8c3e9c30-2d5f-4071-9e33-2c3d4e5f6a03"),
                    Name = "Release prep",
                    Description = "A lightweight checklist for the handoff. The real pipeline still lives elsewhere.",
                    Steps =
                    [
                        Step("8c3e9c30-2d5f-4071-9e33-2c3d4e5f6a31", "Confirm the build you are handing off."),
                        Step("8c3e9c30-2d5f-4071-9e33-2c3d4e5f6a32", "Note who asked for last-minute exceptions."),
                        Step("8c3e9c30-2d5f-4071-9e33-2c3d4e5f6a33", "File unticketed exceptions in the impromptu inbox.")
                    ]
                }
            ]
        };
    }

    private static RoutineStep Step(string id, string text, bool isDone = false)
    {
        return new RoutineStep
        {
            Id = Guid.Parse(id),
            Text = text,
            IsDone = isDone
        };
    }
}
