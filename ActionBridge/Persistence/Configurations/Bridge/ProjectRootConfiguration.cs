using ActionBridge.Constants;
using ActionBridge.Values;

namespace ActionBridge.Persistence.Configurations.Bridge
{
    public class ProjectRootConfiguration
    {
        public ProjectName Name { get; set; } = ActionBridgeConstants.DefaultProjectName;

        public string Description { get; set; } = string.Empty;
    }

    public class ProjectRootStates
    {
        public int TopTicketId { get; set; }
    }
}
