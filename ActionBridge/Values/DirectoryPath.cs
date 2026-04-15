using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActionBridge.Values
{
    [JsonConverter(typeof(DirectoryPathConverter))]
    public record DirectoryPath
    {
        public string Value { get; init; }

        [JsonIgnore]
        public DirectoryPath Parent => new DirectoryPath(Path.GetDirectoryName(Value) ?? string.Empty);

        [JsonIgnore]
        public string Name => Path.GetFileName(Value);

        public static implicit operator string(DirectoryPath directoryPath) => directoryPath.Value;

        [JsonConstructor]
        public DirectoryPath(string value)
        {
            Value = value?.Replace('\\', '/') ?? string.Empty;
        }

        public DirectoryPath() => Value = string.Empty;

        public FilePath Combine(FileName fileName) => new FilePath(Path.Combine(Value, fileName.Value));
    }

    public class DirectoryPathConverter : JsonConverter<DirectoryPath>
    {
        public override DirectoryPath Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new DirectoryPath(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, DirectoryPath value, JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
    }
}
