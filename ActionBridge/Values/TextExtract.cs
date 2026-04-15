using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActionBridge.Values
{
    [JsonConverter(typeof(TextExtractConverter))]
    public record TextExtract
    {
        public string Value { get; init; }

        public static implicit operator string(TextExtract textExtract) => textExtract.Value;

        [JsonConstructor]
        public TextExtract(string value) => Value = value ?? string.Empty;

        public TextExtract() => Value = string.Empty;
    }

    public class TextExtractConverter : JsonConverter<TextExtract>
    {
        public override TextExtract Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new TextExtract(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, TextExtract value, JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
    }
}
