using CorpoWare.Models;
using CorpoWare.Persistence;
using Xunit;

namespace CorpoWare.Tests;

public class AppStoreTests : IDisposable
{
    private readonly string _directory;

    public AppStoreTests()
    {
        _directory = Path.Combine(Path.GetTempPath(), "corpware-tests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_directory);
    }

    [Fact]
    public void MissingFileIsSeededNextToTheGivenPath()
    {
        var path = Path.Combine(_directory, "data", "state.json");

        var store = AppStore.Open(path);

        Assert.True(File.Exists(path));
        Assert.Contains(store.State.Impromptu, item => item.Source == "Slack DM");
        Assert.Contains(store.State.Cards, card => card.Column == BoardColumn.Doing);
        Assert.Contains(store.State.Routines, routine => routine.Name == "Start of day");
        Assert.All(
            store.State.Cards.Where(card => card.ImpromptuItemId is not null),
            card => Assert.Contains(store.State.Impromptu, item => item.Id == card.ImpromptuItemId));

        var json = File.ReadAllText(path);
        Assert.Contains("\"column\": \"Backlog\"", json);
        Assert.DoesNotContain("\"column\": 0", json);
    }

    [Fact]
    public void SavingARoutineStepRoundTrips()
    {
        var path = Path.Combine(_directory, "state.json");
        var store = AppStore.Open(path);
        var step = store.State.Routines.Single(routine => routine.Name == "Start of day").Steps[0];
        step.IsDone = true;
        store.Save();

        var reloaded = AppStore.Open(path);
        var reloadedStep = reloaded.State.Routines.Single(routine => routine.Name == "Start of day").Steps[0];
        Assert.True(reloadedStep.IsDone);
        Assert.Equal(step.Text, reloadedStep.Text);
    }

    [Fact]
    public void CorruptFileIsLeftUntouched()
    {
        var path = Path.Combine(_directory, "state.json");
        File.WriteAllText(path, "{ this is not json");

        var error = Assert.Throws<InvalidDataException>(() => AppStore.Open(path));

        Assert.Contains(path, error.Message);
        Assert.Equal("{ this is not json", File.ReadAllText(path));
    }

    [Fact]
    public void NullListsInTheFileBecomeEmpty()
    {
        var path = Path.Combine(_directory, "state.json");
        File.WriteAllText(path, """{"impromptu":null,"cards":null,"routines":[{"name":"Handoff","steps":null}]}""");

        var store = AppStore.Open(path);

        Assert.Empty(store.State.Impromptu);
        Assert.Empty(store.State.Cards);
        var routine = Assert.Single(store.State.Routines);
        Assert.Equal("Handoff", routine.Name);
        Assert.NotNull(routine.Steps);
        Assert.Empty(routine.Steps);
    }

    public void Dispose()
    {
        if (Directory.Exists(_directory))
        {
            Directory.Delete(_directory, recursive: true);
        }
    }
}
