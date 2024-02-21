using System.Text.Json.Serialization;

namespace Gray.Server.Models;

internal record StartupOptions(
    [property: JsonPropertyName("port")] int Port
);
