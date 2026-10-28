using System.IO;

namespace CorpoWare.Persistence;

/// <summary>
/// Resolves the folder that should hold portable app data: the directory of the real executable.
/// Single-file publishes extract managed assemblies to a temp folder, so <see cref="AppContext.BaseDirectory"/>
/// is not the folder the user launched. <see cref="Environment.ProcessPath"/> still points at that executable.
/// </summary>
public static class PortablePaths
{
    public const string DataFolderName = "data";
    public const string StateFileName = "state.json";

    public static string ExecutableDirectory()
    {
        return ResolveExecutableDirectory(Environment.ProcessPath, AppContext.BaseDirectory);
    }

    /// <summary>
    /// Picks the directory that should sit next to the launched exe.
    /// <paramref name="processPath"/> is <see cref="Environment.ProcessPath"/>.
    /// <paramref name="baseDirectory"/> is the fallback (dev host, or the single-file extraction folder, which must not win when the exe path is known).
    /// </summary>
    public static string ResolveExecutableDirectory(string? processPath, string baseDirectory)
    {
        if (!string.IsNullOrEmpty(processPath))
        {
            var fileName = Path.GetFileName(processPath);
            var hostedByMuxer = fileName.Equals("dotnet", StringComparison.OrdinalIgnoreCase)
                || fileName.Equals("dotnet.exe", StringComparison.OrdinalIgnoreCase);
            if (!hostedByMuxer)
            {
                var directory = Path.GetDirectoryName(processPath);
                if (!string.IsNullOrEmpty(directory))
                {
                    return directory;
                }
            }
        }

        return baseDirectory.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
    }

    public static string DataDirectory()
    {
        return Path.Combine(ExecutableDirectory(), DataFolderName);
    }

    public static string StateFilePath()
    {
        return Path.Combine(DataDirectory(), StateFileName);
    }
}
