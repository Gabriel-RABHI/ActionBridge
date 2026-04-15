using ActionBridge.Constants;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActionBridge.Values
{
    [JsonConverter(typeof(AgentNameConverter))]
    public record ProjectName
    {
        public string Name { get; init; }

        public static implicit operator string(ProjectName projectName) => projectName.Name;

        [JsonConstructor]
        public ProjectName(string value) => Name = ActionBridgeConstants.ComplientAsName(value, nameof(AgentName));
    }

    public class ProjectNameConverter : JsonConverter<ProjectName>
    {
        public override ProjectName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new ProjectName(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, ProjectName value, JsonSerializerOptions options) => writer.WriteStringValue(value.Name);
    }
}
