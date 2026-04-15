using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActionBridge.Values
{
    [JsonConverter(typeof(UtcTimeStampConverter))]
    public record UtcTimeStamp
    {
        public DateTime Value { get; init; }

        public static implicit operator DateTime(UtcTimeStamp utcTimeStamp) => utcTimeStamp.Value;

        [JsonConstructor]
        public UtcTimeStamp(DateTime value)
        {
            Value = value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();
        }

        public UtcTimeStamp() => Value = DateTime.UtcNow;
    }

    public class UtcTimeStampConverter : JsonConverter<UtcTimeStamp>
    {
        public override UtcTimeStamp Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new UtcTimeStamp(reader.GetDateTime());

        public override void Write(Utf8JsonWriter writer, UtcTimeStamp value, JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
    }
}
