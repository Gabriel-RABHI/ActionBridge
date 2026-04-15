using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActionBridge.Values
{
    [JsonConverter(typeof(SignalIdConverter))]
    public record SignalId
    {
        public uint Value { get; init; }

        public static implicit operator uint(SignalId signalId) => signalId.Value;

        [JsonConstructor]
        public SignalId(uint value) => Value = value;

        public SignalId() => Value = 0;
    }

    public class SignalIdConverter : JsonConverter<SignalId>
    {
        public override SignalId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new SignalId(reader.GetUInt32());

        public override void Write(Utf8JsonWriter writer, SignalId value, JsonSerializerOptions options) => writer.WriteNumberValue(value.Value);
    }
}
