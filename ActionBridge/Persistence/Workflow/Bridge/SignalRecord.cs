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

        public MarkerFilePath FilePath { get; set; }

        public MarkerLineNumber LineNumber { get; set; }

        public MarkerTextExtract TextExtract { get; set; }
    }
}
