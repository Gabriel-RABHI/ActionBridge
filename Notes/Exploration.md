I want to build a plug-in for Antigravity - a Agent Runner - that start agent with a file content as prompt. Here a technical description.

This Agent Runner is listening for a specific directory in local temp directory, "%TEMP%/ActionBridge/Prompts/" (windows) to detect when a new file is written or existing files are changing.

If a new file is written or changed, Agent Runner look at the content. It is a `.json` file that contains this C# structure, serialized :

```
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
```

Few cases :
- If `MarkdownFilePath` is a path outside the root directory of the current Antigravity root path, the file is ignored, added to an ignore list.
- If no agent is present with `AgentName` as name, and the `Running`, `ToStop` and `Closed` are all false, the content of the file is loaded and pushed as a prompt in a new Agent named `AgentName`. Then, the file is modified to set `Running` to true.
- If an agent named `AgentName` is present, if `Closed` is false and `ToStop` is true, he stop and close the Agent's Chat, and write `Closed` with the value "true" in the file.

This Agent Runner permit to pilot a set of Agent in Antigravity : start it, stop it.

How to detect that a Chat have finished his task ?