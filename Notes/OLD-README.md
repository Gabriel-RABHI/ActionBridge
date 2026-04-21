# ActionBridge

## Overview

ActionBridge introduces a structured, file-system-based workflow for large-scale, AI-assisted software development. By utilizing Hierarchical, Fractal Prompt Building, it aggressively optimizes AI agent context and execution, delivering better results within complex codebases. It permit to build a persistent multi-aspect specification space in parallel to the transient, deliverable source code space. The main objective is to treats code as a temporary, replaceable artifact to avoid the legacy source code. 

## Why ?

ActionBridge was born as the logical continuation of a deeper architectural technology: **Ghost Body Object (GBO)**. While GBO is a groundbreaking .NET, high performance, transactional, persistent and distributed memory model designed primarily to radically simplify software design for _human_ developers, ActionBridge is the Agentic Framework built to leverage GBO's benefits. What work for *humans*, work for *AI*.

However, ActionBridge is entirely agnostic. It is independent from the GBO technology, don't need it and do not use it. Its workflow can be seamlessly applied to **any** existing, structured codebase to enhance AI reliability and output quality.

> NOTE : The **Ghost Body Object** (GBO) technology isn't just a prototype - it's the result of 15 years of relentless trial, testing, and refinement. It powers a mission-critical healthcare system across 450 production instances since 2022. It is used for a complex event-sourced LOB application since 2023. The upcoming published release is the fifth major iteration, extracting battle-tested principles into a generalized, high-performance infrastructure for the .NET ecosystem.

Before exposing how ActionBridge can be used for any codebase, we have to overview the foundational premise of Ghost Body Object and ActionBridge combination.
## The Ideal Architecture

Applying LLMs to traditional software architectures—systems inherently designed _by_ humans, _for_ humans—is fundamentally flawed. When unleashed on medium-to-large legacy codebases, AI inevitably triggers code duplication, silent bugs, technical dept and code collapse.

Currently, the only viable workaround is a rigidly step-by-step, localized, and human-reviewed programming workflow. However, as codebase size and complexity grow, developer confidence plummets—even with the most rigorous Unit and Integration test strategies. This creates a critical paradox: the lightning-fast speed of AI code generation can induce an overwhelming, counterproductive bottleneck in human review. Pull Requests (PRs) are often validated with minimal scrutiny. In the past, developers knew exactly what was in the code, how it worked, how it was tested, and the root of its design choices. Now, AI decides and writes the code, while developers often simply push it because the tests are green.

What appears to be a productivity gain actually creates massive knowledge debt, and inevitably, technical debt.

We are reaching the limits of what generic tooling and raw model scaling can achieve. The future of AI-assisted software engineering is not about asking, _"How do we endlessly enhance LLM capabilities to understand the chaos of large codebases?"_ Instead, it is about asking, _"How do we restructure software architecture to perfectly align with what LLMs are actually capable of producing?"_
## The Rererence Architecture

The next generation of software architecture must be explicitly designed for AI-assisted development. Boilerplate and glue code must be abstracted away, allowing both humans and AI to focus entirely on specific business domains and distinct functional aspects, using a drastically simplified and shorter source code.

To achieve this, we are developing **Ghost Body Object (GBO)**—a high-performance, distributed, and transactional .NET persistent memory technology. Implementing an Entity Component System (ECS) backed by Event Sourcing (or Mutation Sourcing) specifically tailored for LOB software, GBO entirely obsoletes traditional DBMS, ORMs, Message Brokers, and brittle integration tests.

GBO was built for human developers first, reducing system complexity and unintended side effects by an order of magnitude. The result is a codebase that is at least three times smaller for an average LOB solution. System complexity and unintended side effects are reduced by an order of magnitude—a reduction tangibly measured by the lower LLM token consumption required to implement any given feature.
### The "Cell"

GBO relies on a specific reference architecture that makes it uniquely perfect for ActionBridge AI agents. Large projects are built using vertical modularization (e.g., Sales, Users, Orders, Shipping) intersecting with horizontal layers (UI Components, UI State, Domain Logic, Read Models, and Asynchronous Services).

The intersection of a vertical slice and a horizontal layer forms a **Cell**. Because Cells can encapsulate other Cells, the architecture is inherently fractal. Source code is distributed across separate projects, and within each project, every aspect is organized into deep directory trees.

> **NOTE:** Vertical alignment is not an absolute architectural expectation. Overlapping and intentional misalignment are sometimes necessary mitigations. The "Cell" is more of a guiding conceptual model than a rigid technical constraint.

Crucially, each Cell completely hides its internal data structures, processes, and logic, exposing only a strict, reduced set of abstractions:

- **Commands** (what the Cell can do) and **Read Models** (what data it exposes).
- **States** (what the UI must render) and **Actions** (what the State can process).
- **Jobs** (what background operations a service can perform), **JobStatus**, and **JobResult**.
### The Perfect AI Environment

In such an architecture, at any given level, only the abstractions of the underlying components are visible, and external client components remain unknown. The context is a space limited entirely by its dependencies, public capabilities, and published artifacts. This aspects embrasse the SOLID principles and DDD philosophy.

This limited context provides the exact boundary where an ActionBridge AI Agent can specialize in generating code using a significantly smaller context window and reduced reasoning effort. The extreme simplification achieved by GBO is tangibly measured by the vastly lower LLM token consumption ActionBridge requires to implement any given feature.

The main principle is to makes the specicification space localized to each Cell : each Cell have his own speicification and building process store within the deliverable, the source code. This permit to specialize each Cell's Coding Agent in the most optimized way.

While ActionBridge is perfectly cohesive with this new Cell oriented architectural model, it is entirely agnostic. The local specification can be implemented in **any** legacy or modern project now to enhance the predictability and robustness of AI-generated code.
## The Enhanced Legacy, Now

If you try to build a single AI Agent to handle all the specifications, rules, guidelines and technologies documentation—while expecting it to understand both the macro-architecture and the micro-details of each component—its context window becomes overwhelmingly large. This context bloat induces "hallucinations" and paralyzes the model's reasoning capabilities.

Furthermore, if you attempt a "one-shot" implementation using a traditional persistent Orchestrator-to-Worker swarm, the conversational memory for both agents grows indefinitely. As the chat history fills with dead ends and debugging loops, the AI's attention mechanism decays. This inevitably leads to massive, silent code collapse and insurmountable technical debt.

ActionBridge abandons the persistent "chat" paradigm. Instead, it introduces a deterministic, file-system-driven state machine aligned with any structured architecture:

- **Hierarchical, Fractal Prompt Building:** To eliminate context bloat, ActionBridge contextualizes each task using recursive prompt aggregation. It reads from the agent's current file location up to the root of the project. This allows the LLM to discover only the most relevant, "need-to-know" local instructions—ensuring architectures, technologies, and business rules are injected exactly where they belong in the directory tree. Where a classical agent framework will have a single "agent" directory for the while project, ActionBridge support many `.bridge` directory at any level of the project's directory hierarchy. The prompt for an Agent that work in given source code file is the aggregation of the parent hierarchy prompt definitions.
    
- **Stateless, Iterative Planning (The OODA Loop):** ActionBridge does not use persistent planning agents. Instead, Tickets are processed statelessly. The Planner wakes up, reads the Ticket intent, evaluates the _current_ state of the code and the local history, plans only the immediate 1 to 3 sequential tasks, and terminates. This prevents "hallucination drift" and ensures the AI is always operating on the true reality of the codebase.
    
- **Ephemeral, Zero-Reflection Execution:** The generated Tasks are executed by short-lived, highly specialized micro-agents dynamically configured by the Planner. These Worker agents wake up with a precise persona (e.g., "Blazor UI Expert"), execute the code, document their exact changes, and die. They never have to waste tokens reflecting on their broader purpose.
    
- **Reduce and Refine Historical Information:** Rather than feeding raw chat logs back into the AI, ActionBridge enforces a strict documentation mandate. It periodically refines an indexed synthesis of previous work localized to a specific branch. Each step explains (1) the intent, (2) the starting point, (3) encountered and fixed problems, and (4) the resulting changes, creating a clean knowledge tree for future agents.
    
- **Serialize the Workflow by Branch:** Blindly fusing AI-generated code across a massive project is an intensive, token-burning process that guarantees merge conflicts and compiler crashes. ActionBridge supports global parallelization, but uses a simple, intuitive compartment system to serialize execution _within_ a branch. By utilizing the directory tree to establish hierarchical locking points, it entirely avoids code-fusion conflicts.
## How it Works

**ActionBridge** leverages the local file system as a database, message broker, and state machine, allowing multiple AI agents to be invoked to perform iterative programming work. Built around a **CLI-first architecture** (`bridge.exe`), ActionBridge acts as the glue between user intent and LLM coding tools:
- **Signals:** On one side are Signals. A Signal is a JSON file containing a file path, a line number, and a text extraction, which serves as the starting point for an operation. Signals can be generated directly from the code editor using custom IDE extensions for VS Code or Visual Studio.
- **The Bridge:** In between is the `bridge.exe` CLI application, which manages the workflow files and orchestrates the state machine.
- **The Dashboard:** On top is a UI tool that allows users to configure, visualize, and manage the entire workflow.
- **Coding Tools:** On the opposite side is the LLM coding tool, **Goose** as default. **Claude Code**, or any CLI-based LLM runner can be used.
## Workflow

In the IDE, in the source editor for a specific file, right-click permit to call the Bridge plug-ins command "Create a signal". It create a **Signal** JSON file containing (1) the file path, (2) the line number of the caret, and (3) the selected text in the editor.

In the **Dashboard** application UI, this **Signal** files are stacked as inputs data. This inputs can be selected to build a **Ticket**, a prompt to send to the LLM tool to plan a given programming work. To do it, there is a list of the various **Agents** seen from the main selected path. Under each agent, the defined **Actions** are listed. You can select an **Action** or write a prompt yourself (or select an Action, then edit the prompt). The prompt is previewed immediately with the **Agent** specific context. It can be manually edited. Then you can click "Run". The pending **Ticket** is sent to the LLM coding tool if there no existing **Ticket** pending or in process. The **Ticket** is converted in **Tasks** by a fresh new LLM Coding Tool session that immediately quit. Then each **Tasks** are executed in a fresh new LLM Coding Tool session, one by one. When all Tasks are done, the Ticket is again processed by a fresh new session: the LLM evaluate the source code to create the next few Tasks needed to execute the intent of the Ticket. This process continue in this loop.

In a particular case, the Ticket processing can be paused to get details or decisions from the human. The Dashboard show the question, the user give the response.

When the **Ticket** is completed - it mean that there is no remaining **Tasks** to be done for this Ticket and declared completed - a final **Summary** is produced. There is a summary **Index** that reference each **Tickets** summary. **Tickets** summary explain (1) the task intent, (2) the starting point, (3) problems and (4) changes. Upcoming **Ticket** processing **Agents** can read the **Index** and read the various tickets summaries as knowledge base.

Tickets are stored as an archived large intent chain.
## The Recursive Exploration

In the Dashboard, the visible Agents is the one found in a recursive scan from the selected file point to the root directory of the project. The same principle apply to Actions and prompt component file. This materialize the foundational, per domain and aspect adaptations and optimizations of the context. It can be used for many adaptations, including :

- **Specialized Local Agents** : Agents can be defined at any point in the project tree. Specific agents can be defined locally to a project, or to a branch in a project, to perform really specific tasks. In any case, they will inherit of prompt defined directly in the `Agents` directories of parent `.bridge` directories.

- **Agent Local Customization** : For any existing Agent, local directive can be added for better local results. It permit to build a rich but selective context. A `SeniorDev` Agent defined at the root of the project can integrate a new local `.bridge/Agents/SeniorDev/ui_components.md` file in his prompt. This new, local directives are not the same in the Web UI directories and in the Data Access directories.

- **Specific Actions** : Many actions can be defined locally. They can be generic or completely dedicated to a specific feature set or to the purpose of the branch source code. A logical suite of Action can become a "replayable" construction history steps. "Add Paypal as payment gate" can be a business oriented action.

- **Per Task Agent Configuration** : When generating the Tasks, the tool-set necessary to perform the task are attached to the Task. In the same way, the exact LLM to use is specified : for large analysis and hard coding works, a large model must be used, but for simple tasks, a faster and less expenssive model can be use.

- **Globalization** : general purpose directives and generic actions are defined in the highest `.bridge` directories. The prompt aggregation logic permit to link operations or specify general purpose indications.



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

## Iterative Specs Building

There is two spaces :
- The Specification Space : what is in the .bridge directories.
- The Deliverable Space : what is not in the .bridge directories - the Source Code, User Documentation, Web-Site, etc.

Tasks generation from Tickets is a "Distance Reduction" process between Specifications and Deliverable.

First the LLM check that the reality of the Deliverable is aligned with the Specification. Tickets are in the Specification Space. This check goes far beyond Tickets intent : aggregated Rules, Business Logic and Design Guidances must be checked too. If any of this elements changed, new Tasks can be emitted to reduce the space between the Specification Space and the Deliverable Space.

The distance measurement is an explicit instruction. This Instructions can be enforced by definition of what a distance is, by aspects definition, and by adding more frequently measurements.

Unit tests are an in between Deliverable and Specification : the Unit Tests are not delivered to final end users, while they are deeply express the Specification space.

LLM can edit existing local specifications, or create new ones. 



So, the process can be :
- Write a Ticket with intent, like "Add PayPal support to Checkout.", then reduce the distance with Source Code with Rules, Documentation and Guidance as constraints.
- Change the Rules, Documentation or Guidance, and then reduce the distance with Source Code.
- Write or modify Unit Test code, then makes the Souce Code working with Unit Test code.

### Distance Reduction Step Size

It is possible to define the optimal step size that ganrantee the most fiable result, to adapt to LLM capability, or to adapt with the local complexity.

### Systematic Check
- "Verify that source code is complient with Rules"

### Iterative Workflow
- Keep what work.
- Change what fails.
- Delete the code, replay the Tickets.

Loop.

### What is the Spec ?

Rules, Documentation, Guidance, Tickets and kept Unit Test are the specifications. Code can be deleted and rebuilt.



## Ticket External Source

Ticket generation can be a part of a wider development process. In teams or companies that have a solid feature triage, backlog and user story management, or simply a massage based customer interface or hotline based on messaging system, all this request, problem and fix tracking systems are a Goldmine for a wider, contextualised, priorized automatic Ticket generation process. 
## Why Goose?

ActionBridge’s initial implementation utilizes **Goose** (an open-source AI agent) as its primary LLM Coding Tool. This is a highly intentional choice driven by the need for absolute control over the AI's cognitive load.

The overarching goal of ActionBridge is to drastically reduce context size and enforce hyper-specialization at the Ticket or Task level. Goose is uniquely suited for this because it allows for granular, dynamic control over the **system prompt** by letting us configure exactly which tools are exposed to the agent for any given task. The tool set can be customized for a specific language too - the Roslyn based .Net MCP tool for code exploration and refactoring is a perfect example.

By integrating ActionBridge's architecture with Goose, we achieve several critical breakthroughs:

- **Granular Tool Control (System Prompt Reduction):** A generic AI agent usually carries a massive system prompt loaded with dozens of tools (file search, bash execution, browser access, etc.) just in case it needs them. ActionBridge configures Goose to strip away the excess. If a Task only requires updating a specific markdown file, the agent is spawned _only_ with the tool needed to write to that file. This drastically shrinks the system prompt.
    
- **Hyper-Specialization at the Task Level:** When you combine ActionBridge’s _Fractal Prompt Building_ (which aggressively minimizes the user context) with Goose’s _Tool Pruning_ (which minimizes the system prompt), the AI is placed in a highly constrained, hyper-focused environment. It doesn't have to waste reasoning power deciding _which_ tool to use or _what_ part of the codebase to look at.
    
- **Benchmarking and "Right-Sizing" the LLM:** Because tasks are highly isolated and the prompts are minimized, ActionBridge allows developers to empirically measure exactly how much "LLM power" is actually required for specific operations. You no longer have to throw a massive, expensive frontier model at a simple data-mapping task just to guarantee success.
    
- **The Endgame: Unlocking Local, Small-Parameter LLMs:** This is the most profound benefit. In traditional, high-context AI workflows, large, cloud-based frontier models are the only option capable of handling the noise. However, by strictly minimizing both the system prompt and the local context, ActionBridge radically reduces the reasoning effort required. **This is the only viable pathway to utilizing fast, local, small-parameter LLMs (e.g., 8B or 14B models) for real, large-scale enterprise development.** Ultimately, this strategy removes the dependency on expensive, latency-heavy cloud models. It allows line-of-business software to be generated locally, ensuring absolute data privacy, near-instant execution speeds, and zero token costs, all while maintaining enterprise-grade reliability.
# File System

**ActionBridge** use only files and directory to store data, configurations and manage workflows. No database, no external state management. All configurations, Tickets and Tasks are stored in `.bridge` directories. There is not one, single `.bridge` directory per project, but many. They are disseminated in the project directory tree to personalize rules, instructions, agents, actions.

Each `.bridge` directory can be compliant to few conventions :
- `.bridge/Agents` directory where each **Agent** definition have a directory.
- `.bridge/Agents/.../Actions` directory : this are predefined task prompts with templating capability.
- `.bridge/Board` directory, with `/Tickets/` and `/Tasks/` sub-directories, to manage workflows. It acts as a locking point too.
- `.bridge/History` directory, where refined work's summaries are accumulated.
- `.bridge/.archive` directory, where all, older Tasks and Tickets are saved.
- `.bridge/Signals` is only located in top `.bridge` directory. This is where signal files are stored.
## Agent Definitions

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

Note that the `_end.md` files permit to chain operations by starting new Tickets or new Tasks.
## Action Definitions

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
## Workflow System

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
	- `Ticket_XXXXX.request.md` : blocked. Contains a questions for humans or orchestrators.
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
## Default Agents

ActionBridge need few default Agents to makes work done :
- `Dev` : for every coding Ticket.
- `Reviewer` : Tickets to critics the written code and generate new fixing tasks.
- `Scrib` : Tickets to write documentation, summaries and maintains history.
# Work In Progress
### Conception
- [ ] Conceptual definition
	- [x] Overview
	- [ ] GBO definition
	- [x] Workflow
	- [x] Components
- [ ] Technical Specification
	- [ ] File system
	- [ ] 
### Implementation
- [ ] Basic Implementation
- [ ] Proof Of Concept
- [ ] Complete File System Implementation
- [ ] Dashboard Core

## 🤝 Contributing

Contributions are welcome! Whether you are building new Execution Adapters, improving the C# Context Resolver, or creating new default Agent Profiles, please submit a Pull Request.
1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request
    
## 📄 License

Distributed under the MIT License. See `LICENSE` for more information.