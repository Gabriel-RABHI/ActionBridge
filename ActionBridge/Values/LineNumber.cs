using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActionBridge.Values
{
    [JsonConverter(typeof(LineNumberConverter))]
    public record LineNumber
    {
        public uint Value { get; init; }

        public static implicit operator uint(LineNumber lineNumber) => lineNumber.Value;

        [JsonConstructor]
        public LineNumber(uint value) => Value = value;

        public LineNumber() => Value = 0;
    }

    public class LineNumberConverter : JsonConverter<LineNumber>
    {
        public override LineNumber Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new LineNumber(reader.GetUInt32());

        public override void Write(Utf8JsonWriter writer, LineNumber value, JsonSerializerOptions options) => writer.WriteNumberValue(value.Value);
    }
}
