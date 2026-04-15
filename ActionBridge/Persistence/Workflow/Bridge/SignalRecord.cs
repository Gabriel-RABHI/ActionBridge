using ActionBridge.Values;

namespace ActionBridge.Persistence.Workflow.Bridge
{
    public class SignalRecord
    {
        public UtcTimeStamp CreationUtcTimeStamp { get; set; }

        public FilePath FilePath { get; set; }

        public LineNumber LineNumber { get; set; }

        public TextExtract TextExtract { get; set; }
    }
}
