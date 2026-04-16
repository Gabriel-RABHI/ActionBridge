using ActionBridge.Values;

namespace ActionBridge.Persistence.Configurations.Bridge
{
    public class ProjectRootStates
    {
        public TicketId TopTicketId { get; set; } = new TicketId();

        public SignalId TopSignalId { get; set; } = new SignalId();
    }
}
