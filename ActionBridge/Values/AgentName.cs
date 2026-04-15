using ActionBridge.Constants;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActionBridge.Values
{
    [JsonConverter(typeof(AgentNameConverter))]
    public record AgentName
    {
        public string Name { get; init; }

        public static implicit operator string(AgentName agentName) => agentName.Name;

        [JsonConstructor]
        public AgentName(string value) => Name = ActionBridgeConstants.ComplientAsName(value, nameof(AgentName));

        public AgentName() => Name = ActionBridgeConstants.DefaultAgentName;
    }

    public class AgentNameConverter : JsonConverter<AgentName>
    {
        public override AgentName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new AgentName(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, AgentName value, JsonSerializerOptions options) => writer.WriteStringValue(value.Name);
    }
}
