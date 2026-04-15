using ActionBridge.Constants;
using ActionBridge.Values;

namespace ActionBridge.Persistence.Configurations.Bridge
{
    public class AgentConfiguration
    {
        public AgentName AgentName { get; set; } = ActionBridgeConstants.DefaultAgentName;

        public AgentPurpose Purpose { get; set; } = new AgentPurpose(string.Empty);
    }
}
