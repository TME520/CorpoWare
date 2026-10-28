using System.IO;
using System.Text.Json;
using CorpoWare.Models;

namespace CorpoWare.Persistence;

public sealed class AppStore
{
    public AppStore(string dataFilePath, AppState state)
    {
        DataFilePath = dataFilePath;
        State = state;
    }

    public string DataFilePath { get; }

    public AppState State { get; }

    public static AppStore Open()
    {
        return Open(PortablePaths.StateFilePath());
    }

    public static AppStore Open(string path)
    {
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (!File.Exists(path))
        {
            var seeded = SeedData.Create();
            var created = new AppStore(path, seeded);
            created.Save();
            return created;
        }

        try
        {
            var json = File.ReadAllText(path);
            var state = JsonSerializer.Deserialize(json, AppJsonContext.Default.AppState)
                ?? throw new InvalidDataException("The state file is empty.");
            Normalize(state);
            return new AppStore(path, state);
        }
        catch (Exception ex) when (ex is JsonException or InvalidDataException)
        {
            throw new InvalidDataException(
                $"Could not read {path}. The file was left unchanged. Rename or remove it to start from the sample data.",
                ex);
        }
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(State, AppJsonContext.Default.AppState);
        var temporary = DataFilePath + ".tmp";
        File.WriteAllText(temporary, json);
        File.Move(temporary, DataFilePath, overwrite: true);
    }

    private static void Normalize(AppState state)
    {
        state.Impromptu ??= new List<ImpromptuItem>();
        state.Cards ??= new List<KanbanCard>();
        state.Routines ??= new List<Routine>();
        foreach (var routine in state.Routines)
        {
            routine.Steps ??= new List<RoutineStep>();
        }
    }
}
