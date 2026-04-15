using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActionBridge.Values
{
    [JsonConverter(typeof(FilePathConverter))]
    public record FilePath
    {
        public string Value { get; init; }

        [JsonIgnore]
        public DirectoryPath Directory => new DirectoryPath(Path.GetDirectoryName(Value) ?? string.Empty);

        [JsonIgnore]
        public FileName Name => new FileName(Path.GetFileName(Value));

        [JsonIgnore]
        public string Extension => Path.GetExtension(Value);

        public static implicit operator string(FilePath filePath) => filePath.Value;

        [JsonConstructor]
        public FilePath(string value) => Value = value?.Replace('\\', '/') ?? string.Empty;

        public FilePath() => Value = string.Empty;
    }

    public class FilePathConverter : JsonConverter<FilePath>
    {
        public override FilePath Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new FilePath(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, FilePath value, JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
    }
}
