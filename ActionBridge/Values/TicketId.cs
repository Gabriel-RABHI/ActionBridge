using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActionBridge.Values
{
    [JsonConverter(typeof(TicketIdConverter))]
    public record TicketId
    {
        public uint Value { get; init; }

        public static implicit operator uint(TicketId ticketId) => ticketId.Value;

        [JsonConstructor]
        public TicketId(uint value) => Value = value;

        public TicketId() => Value = 0;
    }

    public class TicketIdConverter : JsonConverter<TicketId>
    {
        public override TicketId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new TicketId(reader.GetUInt32());

        public override void Write(Utf8JsonWriter writer, TicketId value, JsonSerializerOptions options) => writer.WriteNumberValue(value.Value);
    }
}
