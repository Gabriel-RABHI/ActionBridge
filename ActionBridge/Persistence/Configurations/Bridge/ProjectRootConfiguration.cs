using ActionBridge.Constants;
using ActionBridge.Values;

namespace ActionBridge.Persistence.Configurations.Bridge
{
    public class ProjectRootConfiguration
    {
        public ProjectName AgentName { get; set; } = ActionBridgeConstants.DefaultAgentName;

        public ProjectPurpose Purpose { get; set; } = new AgentPurpose(string.Empty);
    }
}
