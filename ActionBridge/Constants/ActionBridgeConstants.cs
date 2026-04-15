using ActionBridge.Structures.Workflow;
using ActionBridge.Values;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActionBridge.Constants
{
    public static class ActionBridgeConstants
    {
        public const int DescriptionMacLenght = 1024;

        /// <summary>
        /// The directory where are record local system wide informations (list of the managed projects for the Dashboard)
        /// </summary>
        public const string ActionBridgeConfigurationDirectory = "ActionBridgeRoot";

        public const string BridgeDirectory = ".bridge";

        public const string AgentsDirectory = "Agents";
        public const string ActionsDirectory = "Actions";
        public const string SignalDirectory = "Signals";

        public const string WorkflowDirectory = "Workflow";
        public const string TicketsDirectory = "Tickets";
        public const string TasksDirectory = "Tasks";
        public const string HistoryDirectory = "History";

        private static readonly string[] ReservedNames =
        {
            "CON", "PRN", "AUX", "NUL",
            "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
            "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9",
            BridgeDirectory, AgentsDirectory, ActionsDirectory, SignalDirectory
        };

        public static AgentName DefaultAgentName => new AgentName("Unnamed Agent");

        public static string ComplientAsName(string value, string context)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty or whitespace.", context);

            // 1. Length constraint (255 is the standard safe limit for NTFS, ext4, and APFS)
            if (value.Length > 255)
                throw new ArgumentException("Name cannot exceed 255 characters.", context);

            // 2. Invalid characters (<, >, :, ", /, \, |, ?, *, and ASCII 0-31)
            if (value.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new ArgumentException("Name contains invalid characters.", context);

            // 3. Windows-specific: Cannot end with a space or a period
            if (value.EndsWith('.') || value.EndsWith(' '))
                throw new ArgumentException("Name cannot end with a space or a period.", context);

            // 4. Windows-specific: Cannot match a reserved system name (case-insensitive)
            // We check the exact string, but also without the extension, as 'CON.txt' is also invalid.
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(value);
            if (ReservedNames.Contains(nameWithoutExtension, StringComparer.OrdinalIgnoreCase))
                throw new ArgumentException($"'{value}' is a reserved system name and cannot be used.", context);

            return value;
        }

        public static string ComplientAsDescription(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            if (value.Length > DescriptionMacLenght)
                throw new ArgumentException($"Name cannot exceed {DescriptionMacLenght} characters.", nameof(value));

            return value;
        }
    }
}
