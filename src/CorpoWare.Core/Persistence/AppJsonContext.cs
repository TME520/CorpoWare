using System.Text.Json.Serialization;
using CorpoWare.Models;

namespace CorpoWare.Persistence;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    UseStringEnumConverter = true)]
[JsonSerializable(typeof(AppState))]
internal partial class AppJsonContext : JsonSerializerContext
{
}
