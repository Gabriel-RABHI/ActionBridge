using System;
using System.Collections.Generic;
using System.Text;

namespace ActionBridge.Persistence.Workflow.PlugIns
{
    public class PendingAgentPromptRecord
    {
        /// <summary>
        /// The .md file containning the prompt
        /// </summary>
        public string MarkdownFilePath { get; set; }

        /// <summary>
        /// The name of the Agent
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// Is set to true by the Agent manager when the Agent's Chat is running
        /// </summary>
        public bool Running { get; set; }

        /// <summary>
        /// Is periodically checked by the Agent manager to stop and close the Agent's Chat
        /// </summary>
        public bool ToStop { get; set; }

        /// <summary>
        /// Is set to true by the Agent manager when the Agent's Chat closed
        /// </summary>
        public bool Closed { get; set; }
    }
}
