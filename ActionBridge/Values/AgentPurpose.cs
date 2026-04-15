using ActionBridge.Constants;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActionBridge.Values
{

    [JsonConverter(typeof(AgentPurposeConverter))]
    public record AgentPurpose
    {
        public string Description { get; init; }

        public static implicit operator string(AgentPurpose description) => description.Description;

        [JsonConstructor]
        public AgentPurpose(string value) => Description = ActionBridgeConstants.ComplientAsDescription(value);
    }

    public class AgentPurposeConverter : JsonConverter<AgentPurpose>
    {
        public override AgentPurpose Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new AgentPurpose(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, AgentPurpose value, JsonSerializerOptions options) => writer.WriteStringValue(value.Description);
    }
}
