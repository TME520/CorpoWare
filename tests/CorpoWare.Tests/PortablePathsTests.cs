using CorpoWare.Persistence;
using Xunit;

namespace CorpoWare.Tests;

public class PortablePathsTests
{
    [Fact]
    public void PublishedExeKeepsDataBesideTheExecutable()
    {
        var exePath = Path.Combine("usb", "CorpoWare", "CorpoWare.exe");
        var extractPath = Path.Combine("tmp", "net", "CorpoWare", "extract");

        var directory = PortablePaths.ResolveExecutableDirectory(exePath, extractPath);

        Assert.Equal(Path.Combine("usb", "CorpoWare"), directory);
    }

    [Theory]
    [InlineData("dotnet")]
    [InlineData("dotnet.exe")]
    [InlineData("DOTNET.EXE")]
    public void DotnetMuxerFallsBackToTheAppBase(string muxer)
    {
        var directory = PortablePaths.ResolveExecutableDirectory(
            Path.Combine("/usr/share/dotnet", muxer),
            "/workspace/src/CorpoWare/bin/Debug/net8.0-windows/");

        Assert.Equal("/workspace/src/CorpoWare/bin/Debug/net8.0-windows", directory);
    }

    [Fact]
    public void MissingProcessPathFallsBackToTheAppBase()
    {
        var directory = PortablePaths.ResolveExecutableDirectory(null, "/opt/corpware/");

        Assert.Equal("/opt/corpware", directory);
    }
}
