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
        public TicketId TopTicketId { get; set; } = new TicketId();

        public SignalId TopSignalId { get; set; } = new TicketId();
    }
}
