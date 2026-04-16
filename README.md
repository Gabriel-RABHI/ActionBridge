# ActionBridge

## Overview

ActionBridge introduces a structured, simple, file-system-based workflow for **AI-assisted, large-scale software development**. Its goal is to optimize AI agents' context and workflow to deliver the most reliable results in complex codebases. It is designed to power a new class of architecture dedicated to AI-assisted LOB (Line of Business) software development.

## Why?

The foundational premise of ActionBridge is that applying LLMs to traditional software architectures—systems inherently designed _by_ humans, _for_ humans—is fundamentally flawed. When unleashed on medium-to-large legacy codebases, AI inevitably triggers context hallucinations, code duplication, silent bugs, and eventual code collapse.

Currently, the only viable workaround is a rigidly step-by-step, highly localized, and deeply human-reviewed programming workflow. However, as codebase size and complexity grow, developer confidence plummets. This creates a critical paradox: the lightning-fast speed of AI code generation induces an overwhelming, counterproductive bottleneck in human review. In the past, developers knew what was in the code, how it worked, and the root of the design choices. Now, AI decides and writes the code, while developers simply push it. What is seen as a productivity gain becomes a knowledge debt.

We are reaching the limits of what generic tooling and raw model scaling can achieve. The future of AI-assisted software engineering is not about asking, _"How do we endlessly enhance LLM capabilities to understand human chaos?"_ Instead, it is about asking, _"How do we enhance software architecture to perfectly align with what LLMs are actually capable of producing?"_

The next generation of software architecture must be explicitly designed for **AI-assisted development**. Boilerplate and glue code must be abstracted away from the AI's scope, allowing the models to focus entirely on specific business domains and distinct functional aspects.

In this new paradigm, large projects can be built using **vertical modularization** (e.g., Sales, Users, Orders, Shipping) intersecting with **horizontal layers** (UI Components, UI State, Domain Logic, Read Models, and Asynchronous Services).

The intersection of a vertical slice and a horizontal layer forms a **Cell**. Because Cells can encapsulate other Cells, the architecture is inherently fractal. Source code is distributed across separate projects, and within each project, every aspect is organized into deep directory trees.

Crucially, each Cell completely hides its internal data structures, processes, and logic, exposing only a strict, reduced set of abstractions:

- **Commands** (what the Cell can do) and **Read Models** (what data it exposes).
    
- **States** (what the UI must render) and **Actions** (what the State can process).
    
- **Jobs** (what background operations a service can perform), **JobStatus**, and **JobResult**.
    
In such an architecture, at any given level, only the abstractions of the underlying components are visible, and external client components are unknown. The context is a space limited by its dependencies, public capabilities, and published artifacts. Each encapsulating component acts purely as a transformer (aggregating, rebuilding, composing) or a filter (passing data upward). This limited context provides the exact space where an AI Coding Agent can specialize in generating code using a significantly smaller context window and reduced reasoning effort. We believe this strict encapsulation is the necessary foundation for next-decade, large-scale LOB systems.

This kind of architecture needs new approaches to software design. It requires new technical foundations: persistence, database concepts, communication, and distribution across servers must evolve to materialize such an architectural shift. A groundbreaking, dedicated distributed .NET persistent memory model technology implementing an Entity Component System (ECS) tailored for LOB software is currently in development to natively support this model at scale.

While ActionBridge is perfectly cohesive with this new fractal architectural model, it is entirely agnostic. It can be implemented in **any** legacy or modern project now to enhance the predictability, quality, and robustness of AI-generated code.
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
- **Coding Tools:** On the opposite side is the LLM coding tool, such as **Goose**, **Claude Code**, or any CLI-based LLM runner.
- **The Bridge:** In between is the `bridge.exe` CLI application, which manages the workflow files and orchestrates the state machine.
- **The Dashboard:** On top is a UI tool that allows users to configure, visualize, and manage the entire workflow.
## Workflow

In the IDE, in the source editor for a specific file, right-click permit to call the Bridge plug-ins command "Create a signal". It create a **Signal** JSON file containing (1) the file path, (2) the line number of the caret, and (3) the selected text in the editor.

In the **Dashboard** application UI, this **Signal** files are stacked as inputs data. This inputs can be selected to build a **Ticket**, a prompt to send to the LLM tool to plan a given programming work. To do it, there is a list of the various **Agents** seen from the main selected path. Under each agent, the defined **Actions** are listed. You can select an **Action** or write a prompt yourself. The prompt is previewed immediately with the **Agent** specific context. It can be manually edited too. Then you can click "Run". The pending **Ticket** is sent to the LLM coding tool if there no existing **Ticket** pending or in process. The **Ticket** is converted in **Tasks** by the LLM Coding Tool, then this **Tasks** are send to the LLM Coding Tool for execution.

When the **Ticket** is completed - it mean that there is no remaining **Tasks** to be done for this Ticket and declared completed - a final **Summary** is produced. There is a summary **Index** that reference each **Tickets** summary. **Tickets** summary explain (1) the task intent, (2) the starting point, (3) problems and (4) changes. Upcoming **Ticket** processing **Agents** can read the **Index** and read the various tickets summaries as knowledge base.
Here is a deep, structured expansion of the "What does it change?" section. It breaks down your core ideas into distinct sub-concepts, provides concrete examples, and highlights the specific benefits of this architectural shift to make your vision clear and compelling.
## What Does This Change?

ActionBridge fundamentally shifts the developer's role from writing and maintaining source code to defining and refining intent, rules, and context. By treating code as a temporary byproduct of highly localized knowledge, it introduces a completely new paradigm for software engineering.

Here is a deep dive into the core concepts driving this change.
### 1. Fractal Prompt Building: Contextual Locality

In traditional AI coding workflows, the agent is often fed an overwhelming amount of global context, most of which is irrelevant to the specific task. Fractal Prompt Building mimics the mental model of an expert human developer by strictly isolating context based on location in the architecture.

- **The Sub-Concept:** Context is aggregated hierarchically from the root of the project down to the specific file. An agent working deep within a specific folder only sees the global rules that cascade down, combined with the hyper-specific rules of its current directory.
    
- **Example:** If an AI agent is tasked with modifying a front-end `SubmitButton` component, it inherits global UI design tokens and the local state-management rules of that specific form. It is _completely blind_ to the database schema, the background message queue policies, or the Order module's business logic.
    
- **Benefits:**
    
    - **Zero Context Bloat:** By starving the AI of irrelevant information, you preserve its reasoning capabilities and maximize its effective context window.
        
    - **Eradication of Hallucinations:** The agent cannot accidentally invent dependencies or call unauthorized backend services if it doesn't even know they exist.
        
### 2. Iterative Context Enhancement: The Knowledge Base as the Source of Truth

Traditionally, documentation rots because it is secondary to the code. In the ActionBridge workflow, the local context, business rules, constraints, and documentation _drive_ the code generation. Therefore, refining the context is the primary development activity.

- **The Sub-Concept:** Developers iterate on the local AI context as intensively as they used to iterate on source code. By adding branch-specific specifications, constraints, and historical ticket summaries, developers step-by-step build a localized knowledge base that acts as a perfect mirror of the desired code state.
    
- **Example:** A developer notices the AI generated a component that doesn't handle edge-case error states correctly. Instead of manually editing the code to fix the bug, the developer adds a rule to the local `context.md` file: _"All UI components in this directory must explicitly handle and render `TimeoutException` states."_ The Ticket is then rerun, and the AI generates the correct code.
    
- **Benefits:**
    - **Living Documentation:** The documentation is inherently kept up to date because if it isn't, the AI cannot generate the correct code.
    - **Compounding Value:** Every rule added to the context makes all future AI operations in that directory smarter, safer, and more predictable.
        
### 3. The Ticket as a "Distance Reduction" Engine

ActionBridge treats Tickets not as static prompts, but as dynamic state evaluators. A Ticket continuously compares what the user wants (Intent) with what actually exists in the files (Code Reality).

- **The Sub-Concept:** The goal of a Ticket is simply to emit the exact sequence of Tasks required to reduce the distance between the intent and the current reality to zero.
    
- **Example:** You have a Ticket titled "Add PayPal support to Checkout." If you run this on a fresh branch, the AI sees a large gap between intent and reality, emitting 10 tasks to create files, write logic, and map models. If you reactivate the exact same Ticket a month later after the API structure has changed, the AI evaluates the _current_ reality, sees a smaller gap, and emits only 2 tasks to update the specific API mappings.
    
- **Benefits:**
    - **Idempotent Execution:** You can safely rerun Tickets. If the code already matches the intent, the AI will evaluate the state and do nothing.
    - **Automated Refactoring:** Reactivating old Tickets against a newly updated foundational architecture forces the AI to bridge the gap and automatically update the local code to match new global standards.
        
### 4. The Ultimate Goal: The "Delete and Rebuild" Paradigm

When local, branch-specific contextual guidance, business rules, and documentation achieve a high enough descriptive power, the source code itself loses its sacred status.

- **The Sub-Concept:** Source code becomes a completely disposable, temporary artifact. The true asset of your software is the recursively aggregated definitions and intents.
    
- **Example:** Imagine realizing a foundational design flaw in a massive legacy module. In a traditional codebase, this means months of risky, manual refactoring. In ActionBridge, if your local context and Tickets are highly descriptive, you can literally highlight the entire directory, press `Delete`, and simply re-activate all the Tickets. The AI agents will read the precise rules, intents, and constraints, and perfectly reconstruct the entire module from scratch, conforming to the new architectural rules.
    
- **Benefits:**
    - **Zero Technical Debt:** Code never has time to "rot." If it gets messy, you don't refactor it; you refine the rules and regenerate it perfectly clean.
    - **Absolute Developer Confidence:** The fear of breaking legacy code vanishes when you know the code is just a transient output that can be reliably rebuilt on demand.
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
```

## 🤝 Contributing

Contributions are welcome! Whether you are building new Execution Adapters, improving the C# Context Resolver, or creating new default Agent Profiles, please submit a Pull Request.

1. Fork the Project
    
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
    
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
    
4. Push to the Branch (`git push origin feature/AmazingFeature`)
    
5. Open a Pull Request
    
## 📄 License

Distributed under the MIT License. See `LICENSE` for more information.