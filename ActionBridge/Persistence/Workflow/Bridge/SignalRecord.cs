using ActionBridge.Structures.Workflow;

namespace ActionBridge.Persistence.Workflow.Bridge
{
    public class SignalRecord
    {
        public MarkerRecord Marker { get; set; }
    }

    public class MarkerRecord
    {
        public UtcTimeStamp CreationUtcTimeStamp { get; set; }

        public FilePath FilePath { get; set; }

        public LineNumber LineNumber { get; set; }

        public TextExtract TextExtract { get; set; }
    }
}
