namespace ActionBridge.Persistence.Workflow.Bridge
{
    public class TicketRecord
    {
        public TicketId Id { get; set; }

        public MarkerFilePath StartLocation { get; set; }

        public List<MarkerRecord> Markers { get; set; }

        public MarkdownText InitialRequest { get; set; }

        public List<TicketPrecisionRequestRecord> Refinements { get; set; }

        public bool Accepted { get; set; }
    }


    public class TicketPrecisionRequestRecord
    {
        public UtcTimeStamp CreationUtcTimeStamp { get; set; }

        public MarkdownText Query { get; set; }

        public MarkdownText Response { get; set; }
    }
}
