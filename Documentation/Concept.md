# What is ActionBridge?
## Overview
ActionBridge introduces a structured, simple file-system-based workflow for **large-scale software development**. Its goal is to optimize AI agents and workflow to deliver the most reliable results in complex codebases. It implements an AI programming workflow designed to power a new class of architecture dedicated to AI-assisted LOB (Line of Business) software development.
## Why?
The foundational premise of ActionBridge is that applying LLMs to traditional architectures—systems designed _by_ humans, _for_ humans—is fundamentally flawed. Managing medium-to-large codebases with AI in these legacy environments inevitably leads to context hallucinations, code duplication, mounting technical debt, silent bugs, and eventual code collapse.

The next generation of software architecture must be explicitly designed for **AI-assisted development**. Boilerplate and glue code must be abstracted away from the AI's scope, allowing the models to focus entirely on specific business domains and distinct functional aspects.

In this new paradigm, large projects are built using **vertical modularization** (e.g., Sales, Users, Orders, Shipping) intersecting with **horizontal layers** (UI Components, UI State, Domain Logic, Read Models, and Asynchronous Services).

The intersection of a vertical slice and a horizontal layer forms a **Cell**. Because Cells can encapsulate other Cells, the architecture is inherently fractal. Source code is distributed across separate projects, and within each project, every aspect is organized into deep directory trees.

Crucially, each Cell completely hides its internal data structures, processes, and logic, exposing only a strict, reduced set of abstractions:
- **Commands** (what the Cell can do) and **Read Models** (what data it exposes).
- **States** (what the UI must render) and **Actions** (what the State can process).
- **Jobs** (what background operations a service can perform), **JobStatus**, and **JobResult**.
    
In such an architecture, at any given level, only the abstractions of the underlying components are visible to the outside world. Each encapsulating component acts purely as a transformer (aggregating, rebuilding, composing) or a filter (passing data upward). We believe this strict encapsulation is the necessary foundation for next-decade, large-scale LOB (Line of Business) systems.

_(Note: A groundbreaking, dedicated .NET persistence technology implementing an Entity Component System (ECS) tailored for LOB software is currently in development to natively support this model)._

While ActionBridge is perfectly cohesive with this new fractal architectural model, it is entirely agnostic. It can be implemented in **any** legacy or modern project to drastically enhance the predictability, quality, and robustness of AI-generated code.
## The Agentic Solution
If you try to build a single AI Agent to handle all the specifications, rules, and technologies—while expecting it to understand both the macro-architecture and the micro-details of each **Cell**—its context window becomes overwhelmingly large. This context bloat induces "hallucinations" and paralyzes the model's reasoning capabilities.

Furthermore, if you attempt a "one-shot" implementation using a traditional persistent Orchestrator-to-Worker swarm, the conversational memory for both agents grows indefinitely. As the chat history fills with dead ends and debugging loops, the AI's attention mechanism decays. This inevitably leads to massive, silent code collapse and insurmountable technical debt.

ActionBridge abandons the persistent "chat" paradigm. Instead, it introduces a deterministic, file-system-driven state machine aligned with this fractal architecture:

- **Hierarchical, Fractal Prompt Building:** To eliminate context bloat, ActionBridge contextualizes each task using recursive prompt aggregation. It reads from the agent's current file location up to the root of the project. This allows the LLM to discover only the most relevant, "need-to-know" local instructions—ensuring architectures, technologies, and business rules are injected exactly where they belong in the directory tree.
    
- **Stateless, Iterative Planning (The OODA Loop):** ActionBridge does not use persistent planning agents. Instead, Tickets are processed statelessly. The Planner wakes up, reads the Ticket intent, evaluates the _current_ state of the code and the local history, plans only the immediate 1 to 3 sequential tasks, and terminates. This prevents "hallucination drift" and ensures the AI is always operating on the true reality of the codebase.
    
- **Ephemeral, Zero-Reflection Execution:** The generated Tasks are executed by short-lived, highly specialized micro-agents dynamically configured by the Planner. These Worker agents wake up with a precise persona (e.g., "Blazor UI Expert"), execute the code, document their exact changes, and die. They never have to waste tokens reflecting on their broader purpose.
    
- **Reduce and Refine Historical Information:** Rather than feeding raw chat logs back into the AI, ActionBridge enforces a strict documentation mandate. It periodically refines an indexed synthesis of previous work localized to a specific branch. Each step explains (1) the intent, (2) the starting point, (3) encountered and fixed problems, and (4) the resulting changes, creating a clean knowledge tree for future agents.
    
- **Serialize the Workflow by Branch:** Blindly fusing AI-generated code across a massive project is an intensive, token-burning process that guarantees merge conflicts and compiler crashes. ActionBridge supports global parallelization, but uses a simple, intuitive compartment system to serialize execution _within_ a branch. By utilizing the directory tree to establish hierarchical locking points, it entirely avoids code-fusion conflicts.
## How it Works
**ActionBridge** leverages the local file system as a database, message broker, and state machine, allowing multiple AI agents to be invoked to perform iterative programming work. Built around a **CLI-first architecture** (`bridge.exe`), ActionBridge acts as the glue between user intent and LLM coding tools:
- **Signals:** On one side are Signals. A Signal is a JSON file containing a file path, a line number, and a text extraction, which serves as the starting point for an operation. Signals can be generated directly from the code editor using custom IDE extensions for VS Code or Visual Studio.
- **Coding Tools:** On the opposite side is the LLM coding tool, such as **Antigravity**, **Claude Code**, or any CLI-based LLM runner.
- **The Bridge:** In between is the `bridge.exe` CLI application, which manages the workflow files and orchestrates the state machine.
- **The Dashboard:** On top is a UI tool that allows users to configure, visualize, and manage the entire workflow.
## Workflow
In the IDE, in the source editor for a specific file, right-click permit to call the Bridge plug-ins command "Create a signal". It create a **Signal** JSON file containing (1) the file path, (2) the line number of the caret, and (3) the selected text in the editor.

In the **Dashboard** application UI, this **Signal** files are stacked as inputs data. This inputs can be selected to build a **Ticket**, a prompt to send to the LLM tool to plan a given programming work. To do it, there is a list of the various **Agents** seen from the main selected path. Under each agent, the defined **Actions** are listed. You can select an **Action** or write a prompt yourself. The prompt is previewed immediately with the **Agent** specific context. It can be manually edited too. Then you can click "Run". The pending **Ticket** is sent to the LLM coding tool if there no existing **Ticket** pending or in process. The **Ticket** is converted in **Tasks** by the LLM Coding Tool, then this **Tasks** are send to the LLM Coding Tool for execution.

When the **Ticket** is completed - it mean that there is no remaining **Tasks** to be done for this Ticket and declared completed - a final **Summary** is produced. There is a summary **Index** that reference each **Tickets** summary. **Tickets** summary explain (1) the task intent, (2) the starting point, (3) problems and (4) changes. Upcoming **Ticket** processing **Agents** can read the **Index** and read the various tickets summaries as knowledge base.
# Components
## Four components goals
- **The Bridge Shell** : it is a library of methods for file management and workflow control. This shell is encapsulated in the `bridge.exe`, a CLI tool that can be called by any system - external tools or AI Agents. The Bridge can call the LLM Coding Tool to execute both Tickets and Tasks.
- **The Signalling tools** : it is a peace of software used to create Signals. A Signals file record a file path, a line number and a text.  Signals are written in a dedicated directory, and can be created by a IDE plug-in (Visual Studio, VS Code).
- **The Dashboard** : it is background application that implement a Web UI to manage the whole system : configure projects, create agents, define actions, list Signals, create Tickets and manage tasks completion.
## File System
**ActionBridge** use only files and directory to store data, configurations and manage workflows. No database, no external state management. All configurations, Tickets and Tasks are stored in `.bridge` directories. There is not one, single `.bridge` directory per project, but many. They are disseminated in the project directory tree to personalize rules, instructions, agents, actions.

Each `.bridge` directory can be compliant to few conventions :
- `.bridge/Agents` directory where each **Agent** definition have a directory.
- `.bridge/Agents/.../Actions` directory : this are predefined task prompts with templating capability.
- `.bridge/Board` directory, with `/Tickets/` and `/Tasks/` sub-directories, to manage workflows. It acts as a locking point too.
- `.bridge/History` directory, where refined work's summaries are accumulated.
- `.bridge/.archive` directory, where all, older Tasks and Tickets are saved.
- `.bridge/Signals` is only located in top `.bridge` directory. This is where signal files are stored.
### Directories
#### Agents
`.bridge/Agents/`
`.bridge/Agents/[agent name]/`
`.bridge/Agents/[agent name]/Actions/`
#### Board
`.bridge/Board/Tickets/`
`.bridge/Board/Tasks/`
#### History
`.bridge/History/`
`.bridge/.archive/`
#### Signals
`.bridge/Signals/`
# Agent Definitions
## Prompt Aggregation Convention
When building a Ticket using the Dashboard, the bridge shell will perform a prompt aggregation. This is based on the exploration of local `.bridge` directory to concatenate the various part of the Ticket prompt. There is few naming conventions :
- `_start.md` : all time concatenated first.
- `*.md`
- `_end.md` : all time last concatenated.
First, the scan goes to the Top `.bridge` directory, goes down to the lowest, and come back to the top.
### Sample
Directories and content :
- `Project/.bridge/Agents/_start.md` : S0
- `Project/.bridge/Agents/prompt.md` : P0
- `Project/.bridge/Agents/_end.md` : E0
- `Project/.bridge/Agents/Dev/_start.md` : S1
- `Project/.bridge/Agents/Dev/rule.md` : R1
- `Project/.bridge/Agents/Dev/_end.md` : E1
- `Project/SubProject/.bridge/Agents/Dev/_start.md` : S2
- `Project/SubProject/.bridge/Agents/Dev/rule.md` : R2
- `Project/SubProject/.bridge/Agents/Dev/_end.md` : E2

**Result prompt** from `"SubProject"` : (S0 + S1 + S2) + P0 + R1 + R2 + (E2 + E1 + E0)
**Caution** :  note that **ends** are reversed (E2 + E1 + E0), like brackets in code.
## The Resulting Rules
What is important to understand is that the "one-fit-all" is seen as a deceptive way to define the development process. Specialized Agent with a "know-all" strategy is not efficient in large code base. This recursive approach permit foundational, per domain and aspect adaptations and optimizations.
### Specialized Agents
Agents can be defined at any point in the project tree : specific agents can be defined locally to a project, or to a branch in a project, to perform really specific tasks. In any case, they will inherit of prompt defined directly in the `Agents` directories of parent `.bridge` directories.
### Agent Local Enrichment
For any Agent, local directive can be added for better local results. It permit to build a rich but selective context. Exemple : a `SeniorDev` Agent is defined at the root of the project. But a new `.bridge/Agents/SeniorDev/_start.md` file can be added in various place to makes this same Agent to have more directives. This directives are not the same in the Web UI parts and in the Data Access layers.
### Globalization
General purpose directives are defined in the highest .bridge directories. `_end.md` permit to link operations or specify general purpose indications. It can contains an instruction to start a new Tickets.
# Action Definitions
### Templating
Actions are predefined prompts that describe a task to be done. They support templating by inserting datas that comes from the **Signal** files records :
- `$FILE` : the complete file path, relative to root `.bridge` directory path.
- `$LINE` : the line number.
- `$TEXT` : the selected text grabbed in the file.

**Sample :** 
```
Review the file $FILE.
```
If an Action need multiple files, multiple signals can be selected. References can use a number for Signal references :
- `$FILE2` : the complete file path for the second Signal.
- `$LINE2` : the line number for the second Signal.
- `$TEXT2` : the selected text grabbed in the file for the second Signal.

Il multiple Signals are selected, the templating can loop over the various files, lines or texts :
- `$FILE#` : the complete file path.
- `$LINE#` : the line number.
- `$TEXT#` : the selected text grabbed in the file.

```
Check that $TEXT# in file $FILE# is correctly tested in unit tests.
```
### Conditional Instructions
The `Text` field can be empty. Templating support simple instructions. A line can begin with with :
- `$if TEXT` : execute if text is not empty
- `$else` : if statement alternative.
- `$endif` : end of if statement.

**Sample :** 
```
$if TEXT
Review the code $TEXT in the file $FILE".
$else
Review the code in file $FILE".
$endif
```
# Workflow System
## File Name Conventions
The cinematic of the workflow is this one :
- A Ticket is created using the Dashboard (or any other tool, or by an IA Coding Tool) in the `/Tickets/` directory. The ticket file name is made to avoid conflit and respect oredring sequance : "Ticket_00001.md"
- An AI Coding Tool is invoked with the Ticket content and follow instructions. His goal is to create few Tasks that contribute to reach the defined intent :
	- If it possible to create `Tasks` from the prompt, the Coding Tool create the first Tasks files in `/Tasks/` directory ("Ticket_00001_Task_0001.md", "Ticket_00001_Task_0002.md" ...). 
	- If the ticket goals are no possible, the Coding Tool create a "Ticket_00001.rejected.md" containing the reason the ticket is rejected. 
	- When a task file is found in `/Tasks/` directory, without any `wip` or `done` file for this task, the Coding Tool is invoked with the content of the Task file.
		- When starting the task, the Coding Tool create a `wip` file with a description of the work to be done.
		- When the task is done, the Coding Tool create a `done` file that describe the changes, encountered problems and fixes.
		- If the task fail, the Coding Tool create a `failled` file that describe the reason of the fail.
### Workflow Files
- `Tickets` :  a work item to be converted in Tasks.
	- `Ticket_XXXXX.md` : is currently written by a process, to be processed.
	- `Ticket_XXXXX.rejected.md` : rejected by something. Contains the reason for rejection.
	- `Ticket_XXXXX.closed.md` : the Coding Tool consider the goals reached or complete failure.

- `Tasks` : a task to be done.
	- `Ticket_XXXXX_Task_XXXX.md` : is waiting for execution.
	- `Ticket_XXXXX_Task_XXXX.wip.md` : signal that the task is in execution. Contains a summary of planned operations, errors, problems and fixs.
	- `Ticket_XXXXX_Task_XXXX.failled.md` : la tache a échouée.
	- `Ticket_XXXXX_Task_XXXX.done.md` : la tache est terminée.

- `Summary` : a refined summary.
	- `Ticket_XXXXX_Summary.parse.md`
	- `Ticket_XXXXX_Summary.md`
	
- `Index` : the file that contains a list of all Summary.
	- `Summary_Index.md`
# Default Agents
## Minimal Workflow
ActionBridge need few default Agents to makes work done :
- `Dev` : for every coding Ticket.
- `Reviewer` : Tickets to critics the written code and generate new fixing tasks.
- `Scrib` : Tickets to write documentation, summaries and maintains history.
## Supervisor Instructions

# The Bridge
## The Bridge Shell Features
### Find The Root
```c#
string GetRootBridge(string fromPath);
string GetNearestBridge(string fromPath);
IEnumerable<string> GetFromNearestToRootBridges(string fromPath);
int CreateSignal(string fromFile, int line, string text);
IEnumerable<int> ListSignalsIds(string fromPath);
int GetNextSignalId(string fromPath);
int GetNextTicketId(string fromPath);

int CreateTickedFromPrompt(string fromPath, string prompt);
int CreateAgentTickedFromPrompt(string fromPath, string agent, string prompt);
int CreateAgentTickedFromOneAction(string fromPath, string agent, string action, param int[] signals);

string ValidateTicket(int ticketId);

string CreateTask(int ticketId, string prompt);
string CreateAgentTaskFromPrompt(int ticketId, string agent, string prompt);
string CreateAgentTaskFromOneAction(int ticketId, string agent, string action, param int[] signals);

```