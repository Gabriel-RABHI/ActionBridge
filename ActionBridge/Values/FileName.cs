using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActionBridge.Values
{
    [JsonConverter(typeof(FileNameConverter))]
    public record FileName
    {
        public string Value { get; init; }

        [JsonIgnore]
        public string Extension => Path.GetExtension(Value);

        [JsonIgnore]
        public string NameWithoutExtension => Path.GetFileNameWithoutExtension(Value);

        public static implicit operator string(FileName fileName) => fileName.Value;

        [JsonConstructor]
        public FileName(string value) => Value = value ?? string.Empty;

        public FileName() => Value = string.Empty;
    }

    public class FileNameConverter : JsonConverter<FileName>
    {
        public override FileName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new FileName(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, FileName value, JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
    }
}
