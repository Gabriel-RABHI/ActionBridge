# ActionBridge

## Overview

The most reliable way to build software using AI coding agents is to request work in small, incremental steps executed within a highly scoped context. Prompts must be highly descriptive and precise, yet completely devoid of irrelevant noise. ActionBridge introduces a structured, file-system-based workflow built on the principle of **Hierarchical Context Cascading**. This principle aggressively optimizes AI agent execution by maximizing the signal-to-noise ratio, delivering superior results within complex codebases.
### The Basics

Any large codebase is typically divided into multiple projects, each featuring a deep directory hierarchy housing the source code. The core concept of ActionBridge is to store agent definitions, contexts, and rules in specific folders within this hierarchy, directly alongside the source code they pertain to.

An agent working in the data access layer does not need the same rules or documentation as an agent working on the user interface. However, all agents working on unit testing might share a common baseline of rules while inheriting specific guidelines depending on their local directory. Even the toolsets, Model Context Protocols (MCPs), and Skills will vary by location.

It is well established that the more precise, localized, and contextually dense an AI's prompt is, the better the generated code will be. When using ActionBridge, any task is defined from specific starting points within the source code tree. These starting points dynamically build the optimal context, guidelines, and documentation strictly for that specific branch.
### Spec-Driven Development

ActionBridge is designed to build a complete, high-quality, localized specification system through an iterative, emergent process. Developers can utilize it in two primary ways:

- **Coding Assistant:** Processing transient work items using explicit programming delegation, similar to traditional AI coding tools.
    
- **Specification-Driven Development:** The developer focuses entirely on editing specifications. While treating source code as a purely disposable artifact is a radical, long-term directional goal, practically, this approach allows the specifications to act as the ultimate source of architectural and business truth. The LLM acts as the execution engine that handles syntactic implementation, making the source code a highly malleable output that effortlessly adapts as the specifications evolve.
    
### The Three Spaces

ActionBridge minimizes bureaucratic overhead by strictly separating persistent architectural truth from disposable execution steps. The default configuration establishes three distinct spaces:

- **The Specification Space (Persistent):** This is the authoritative core of the system. It contains the overarching **Intents** (large-scale descriptions of what must be built) and the **Requirements**. A Requirement is highly flexible; it can dictate extremely precise technical details or outline broad business rules. Ultimately, a Requirement is simply a _“thing that must be true.”_ This space acts as the project's recipe, holding the ingredients (guidelines, rules, knowledge) and the permanent goals.
    
- **The Workflow Space (Transient):** This space handles execution without cluttering the project's long-term memory. It contains ephemeral work items: **Tickets** and **Tasks**. Tickets reference specific Requirements and are broken down into granular Tasks (the actual programming instructions). Because they are purely transient, once a Task is completed, these items can and should be deleted, ensuring developer velocity remains high.
    
- **The Deliverable Space (Output):** Contains the tangible results that must exist independently of the workflow, such as the actual source code, testing suites, or public documentation.
    
### The Generation Chain

This system naturally forms a clear, cascading generation chain: `Intents` -> `Requirements` -> `Tickets` -> `Tasks`.

Crucially, ActionBridge allows the developer to enter this chain at any level of abstraction, delegating the remaining downward steps to the AI agents. You can scale the AI's autonomy based on your immediate needs:

- **Manual Execution:** The developer creates **Tasks** directly to execute specific programming instructions, maintaining granular control over the immediate code generation.
    
- **Delegated Planning:** The developer creates **Tickets**, allowing an Agent to plan the work and automatically break it down into actionable Tasks.
    
- **Automated Workflow:** The developer defines the **Requirements**. Agents then read these rules to create the necessary Tickets, which subsequently generate the execution Tasks.
    
- **Full Orchestration:** The developer focuses solely on high-level **Intents**. Agents take over entirely to align and generate the Requirements, cascade them into Tickets, and finally execute the Tasks.
    
Regardless of where the developer chooses to intervene, generating the final deliverable relies on the same core principle: observing what currently exists in the Deliverable Space, checking it against the Specification Space, and spinning up a transient workflow to make the code perfectly reflect the things that must be true.
### Logical Ordering

You can instruct ActionBridge to process a single item, a series of items, or all items. `Requirements` are processed in a specific order. Each one is assigned a unique number at the project level. Just like `Requirements`, `Tickets` and `Tasks` are also assigned unique numbers upon creation.

`Requirements` form a persistent, logical series that can be executed one by one to generate `Tickets`, and subsequently `Tasks`, to perform the actual programming work. Developers can insert new `Requirements` anywhere in the chain. However, the complete `Requirement` chain must remain logically coherent. For instance, a `Requirement` instructing the system to build a user management module must be positioned before a `Requirement` that adds a specific rule for user accounts.

### A "Distance Reduction" Engine

When processing an `Intent`, the LLM is instructed to verify whether the existing `Requirements` align with the `Intent`'s content. If a `Requirement` is not cohesive with the `Intent`, it is corrected. Furthermore, a new `Requirement` can be generated if a specific aspect of an `Intent` is not yet represented.

When processing a `Requirement`, the LLM reads the source code to evaluate whether the requirement is currently met. If the source code does not align with the `Requirement`, `Tickets` are generated to describe the necessary changes.

A `Ticket` acts as a dynamic state evaluation request. During the processing of a `Ticket`, the LLM compares the `Ticket`'s content with what actually exists in the files (the source code reality). The goal of a `Ticket` is simply to emit a sequence of `Tasks` required to reduce the distance between the ticket's intent and the current reality to zero. The LLM can achieve this in multiple steps—it does not have to emit the complete, exact list of `Tasks` all at once. For example, it can emit 3 `Tasks`, re-evaluate the state, emit 2 more `Tasks`, evaluate again, emit 1 final `Task`, and then validate the `Ticket` as complete.

- **Example:** You have a `Ticket` titled "Add PayPal support to Checkout." If you run this on a fresh branch, the AI sees a large gap between intent and reality, emitting 4 tasks to create files, write logic, and map models. If you change the `Requirements` a month later after the API structure has changed, the AI evaluates the _current_ reality, sees a smaller gap, and emits a new `Ticket`, followed by 2 tasks to update the specific API mappings.
    
- **Benefits:**
    
    - **Idempotent Execution:** You can safely rerun `Requirements` or `Tickets`. If the code already matches the intents, the AI will evaluate the state and do nothing.
        
    - **Automated Refactoring:** Reactivating old `Requirements` against a newly updated foundational architecture forces the AI to bridge the gap and automatically update the local code to match the new global standards.
        
### Workflow Summary

A summary of the complete workflow:

- **Vibe Coding**: Create work items as `Intents` in the Specification Space. ActionBridge will then generate, modify, or expand the `Requirements`. New `Tickets` and `Tasks` may be generated and processed.
    
- **Requirement-Driven Coding**: Create a logical, ordered chain of `Requirements`. ActionBridge will verify that the chain is coherent. If valid, it generates the necessary `Tickets`.
    
- **Ticket-Driven Coding**: Create a `Ticket`. The resulting `Tasks` serve as complete prompts, containing exactly what the LLM needs to reduce the distance between the code reality and the intent.
    
- **Execute Tasks**: Create a `Task` to be executed immediately.
    
As a developer, you can mix and match all four of these approaches within this ActionBridge configuration.
### Bidirectional State Reconciliation

ActionBridge operates as a bidirectional state reconciliation engine. While the standard flow cascades down from Specifications to Deliverables, the system can also run in reverse to verify that actual code behaviors and implemented rules remain perfectly reflected in the `Requirements`.

If a developer introduces a manual change directly into the business logic, this triggers a back-propagation of the source code reality into the Specification Space. When executed, this back-propagation ensures the `Requirements` remain the genuine, ultimate source of truth by forcing them to adapt to the actual, compiling source code.

This reverse-sync unlocks powerful workflows. Developers can manually write or insert large portions of legacy code and instruct ActionBridge to automatically generate the underlying `Requirements`. Furthermore, this enables a continuous "round-trip" delta reduction loop: a developer can quickly draft poorly optimized code, use back-propagation to extract its logical intent into a `Requirement`, and then trigger a forward pass, instructing the AI to perfectly refactor and rewrite the code based on that newly minted specification.
### Hierarchical Context Cascading

In the directory hierarchy, the configuration define two directories :
- `.specs` : this directories contains the specifications, including `Intents` and `Requirements`.
- `.works` : this directories contains `Tickets` and `Tasks`.

---
---
---


> [!warning] WORK IN PROGRESS






To build a prompt, ActionBrige find all `.specs` directories, and aggregate the request matching files. For a given Agent, it allows the LLM to discover only the most relevant, "need-to-know" local instructions—ensuring system prompt, tools (MCP, Skills), rules, guidelines, knowledge and documentation are injected exactly where they belong in the directory tree in a cumulative way.
### Stateless, Iterative Planning (The OODA Loop)

The default workflow is based on Tickets execution :
- As **Coding Assistant**, the `Tickets` are directly generated by the user.
- As **SDD** (Specification Driven Development), the `Tickets` are generated by an LLM by comparing Specifications and Deliverable spaces.

Then, **Tickets** are used to generate **Tasks**. When a **Ticket** is created, the `Planner` Agent wakes up, reads the **Ticket** intent, evaluates the _current_ state of the code and the local history, plans only the immediate 1 to 3 sequential tasks, and terminates.

The generated **Tasks** are "all-in-one" items executed by short-lived, highly specialized micro-agents dynamically configured by the `Planner`. These `Worker` agents wake up with a precise persona (e.g., "Blazor UI Expert"), execute the code, document their exact changes, and die. They never have to waste tokens reflecting on their broader purpose.

When the **Ticket** is completed - it mean that there is no remaining **Tasks** to be done for this Ticket and declared completed - a final **Summary** is produced. There is a summary **Index** that reference each **Tickets** summary. **Tickets** summary explain (1) the task intent, (2) the starting point, (3) problems and (4) changes. Upcoming **Ticket** processing **Agents** can read the **Index** and read the various tickets summaries as knowledge base.

### Iterative Context Enhancement

Traditionally, documentation rots because it is secondary to the code. In the ActionBridge workflow, the local Specification Space _drive_ the code generation. Therefore, refining the context is the primary development activity. Developers iterate on the local AI context as intensively as they used to iterate on source code. By adding branch-specific Specification Space artefacts (Tool-set, System Prompts, Agent Configuration, Guildelines, Rules, etc), and cherry pick historical ticket summaries, developers step-by-step build a localized knowledge base that acts as a perfect mirror of the desired code state.

- **Example:** A developer notices the AI generated a component that doesn't handle edge-case error states correctly. Instead of manually editing the code to fix the bug, the developer adds a rule to the local `ui-exception-rules.md` file: _"All UI components in this directory must explicitly handle and render `TimeoutException` states."_ The Ticket is then rerun, and the AI generates the correct code.
- **Benefits:**
    - **Living Documentation:** The documentation is inherently kept up to date because if it isn't, the AI cannot generate the correct code.
    - **Compounding Value:** Every rule added to the context makes all future AI operations in that directory smarter, safer, and more predictable.
### The Legacy free "Delete and Rebuild" Paradigm

When local, branch-specific contextual guidance, business rules, and documentation achieve a high enough descriptive power, the source code itself loses its sacred status.

- **The Sub-Concept:** Source code becomes a completely disposable, temporary artifact. The true asset of your software is the recursively aggregated definitions and intents.
- **Example:** Imagine realizing a foundational design flaw in a massive legacy module. In a traditional codebase, this means months of risky, manual refactoring. In ActionBridge, if your local context and `Intents` are highly descriptive, you can literally highlight the entire directory, press `Delete`, and simply re-activate all the Intents. The AI agents will read the precise rules, intents, and constraints, and perfectly reconstruct the entire module from scratch, conforming to the new architectural rules.
- **Benefits:**
    - **Zero Technical Debt:** Code never has time to "rot." If it gets messy, you don't refactor it; you refine the rules and regenerate it perfectly clean.
    - **Absolute Developer Confidence:** The fear of breaking legacy code vanishes when you know the code is just a transient output that can be reliably rebuilt on demand.
## Workflow

### Components

The ActionBridge tooling is composed of few components :
- **The Marker Tool** : it is a Visual Studio or VS Code plug-in to create a context file from which starting a new operation.
- **The Dashboard:** On top is a UI tool that allows users to configure, visualize, and manage the entire workflow.
- **The Bridge:** In between is the `bridge.exe` CLI application, which manages the workflow files and orchestrates the state machine.

A project root directory must be recorded in the `Dashboard` to be managed, and an instance of `bridge.exe --listen` must be started with the project root directory path as parameter.
### Usage

In the IDE, in the source editor for a specific file, right-click permit to call the `Bridge` plug-ins command "Create a marker". It create a **Marker** JSON file containing (1) the file path, (2) the line number of the caret, and (3) the selected text in the editor.

In the **Dashboard** application, this **Marker** files are stacked as inputs data. This inputs can be selected for two purpose :
- Build a **Ticket**, a prompt to send to the LLM tool to plan a given programming work as Tasks.
- Navigate the Spaces: the **Dashboard** show the tree composed of the .specs and .works directories, in a hierarchical way.
## Create a Tickets

To directly create a **Ticket**, there is a list of the various **Agents** seen from the main selected path. You can select an **Agent**, then write a prompt. The Agent `pre-prompt` and `post-prompt` defined in the various parent directories are shown stacked. During edition of the prompt, you can see instructions that tell the LLM not to execute the prompt directly, but to compare if the source code is already implementing the intent and if not, generate few Tasks file to reduce the distance between the code and the intent. When you have written the prompt, you can click "Run". If there no existing **Ticket** pending or in process, the **Ticket** is converted in **Tasks** by a fresh new LLM session that immediately quit. Then each **Tasks** are executed in a fresh new session, one by one. When all **Tasks** are done, the **Ticket** is again processed by a fresh new session: the LLM evaluate the source code to create the next few **Tasks** needed to execute the intent of the **Ticket**. This process continue in this loop.

In a particular case, the Ticket processing can be paused to get details or decisions from the human. The Dashboard show the question, the user modify the Ticket to avoid the question.

When the **Ticket** is completed - it mean that there is no remaining **Tasks** to be done for this Ticket and declared completed - a final **Summary** is produced. There is a summary **Index** that reference each **Tickets** summary. **Tickets** summary explain (1) the task intent, (2) the starting point, (3) problems and (4) changes. Upcoming **Ticket** processing **Agents** can read the **Index** and read the various tickets summaries as knowledge base.

Tickets are stored as an archived large intent chain.
## Crate an Intent

To create an Intent, you can use the Marker as anchor point, and create an `Intent` file in the nearest `.specs/Intents/` directory. An intent is a high level planning step. 
# Technical Overview
## Conventions

ActionBridge is done to be extremely configurable. The fixed conventions are tiny. The workflow orchestration use a single directory :
- `.bridge`: one at root directory of the project. It contains global configurations and global states.

Any agent wake-up is managed by the `Bridge.exe` application, based on file changes observation. A `Dashboard` application with a rich UI Web user interface is used to view and drive the complete ActionBridge process.
## Workflow Definition

### Directory Configuration
The default configuration is in `.bridge/_map.json`. This file contains directories definitions :

| Space | Directory | EntitiesFileConfig             | Bridge    |
| ----- | --------- | ------------------------------ | --------- |
| Specs | .specs    | `_specification.entities.json` | FIXED     |
| Works | .works    | `_workflow.entities.json`      | TRANSIENT |
This configuration define two directories. We can have one instance of each at at root directory and many in various branches :
- `.specs`: each local .specs directory act as a contextual information containner that contribute to any `Intents` or `Ticket` in a particular point in project's directory hierarchy.
- `.works`: a `Task` defined in a `.works` directory can be executed at the same time of another one in an another `.work` directory.

| Field              | Decription                                                                                                                                                                                                                                                                                                                                         |
| ------------------ | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Space              | Space name.                                                                                                                                                                                                                                                                                                                                        |
| Directory          | Name of the directory.                                                                                                                                                                                                                                                                                                                             |
| EntitiesFileConfig | The name of the `Entity` configuration file.                                                                                                                                                                                                                                                                                                       |
| Bridge             | The behavior of the directory from `bridge.exe` and the `Dashboard` application. `FIXED` mean this directory contains inputs, it must be persisted in the Git Repository.  In this context, it contains specification files. `TRANSIENT` means this directory contains removable files. In this context, is contains work-items, short life files. |
The workflow definition is based on a list of `Entity` definitions for each directory. An Entity is a type of `.md` files that contribute to the global system. The `_specification.entities.json` file define a set of entities in the Specification Space in `.specs` directory. The `_workflow.entities.json` file define a set of entities in the Workflow Space in `.works` directory.
### Default Specification Entities
This configuration define one, single active file type.

| Name   | Directory | Format     | State   | Agent            | Bridge  | Icon        | Color     | Tag   |
| ------ | --------- | ---------- | ------- | ---------------- | ------- | ----------- | --------- | ----- |
| Intent | Intents   | `*_{0000}` |         |                  | INTENT  |             |           |       |
|        |           |            | pending | Agents/Architect | PENDING |             | blue      | point |
|        |           |            | wip     |                  | WIP     |             | magenta   | point |
|        |           | `_begin.*` |         |                  |         | right-arrow | dark-blue |       |
|        |           | `_end.*`   |         |                  |         | left-arrow  | dark-blue |       |
With this configuration, every `.specs` directory can contains an "Intents" directory. Any file in this directory is an intent.
#### Format
The `_{0000}` naming component mean that each Intent has a number that define processing order of each intent file. The same `.specs` directory can contains an `Agents` directory, with a directory for each agent. Any `.md` file in an agent directory will be added in his prompt.
#### State
An intent `.md` file name can be written in mutiple formats. Example :
- `payment-sys_0005.md` : this is a neutral, descriptive step in the product building.
- `payment-sys_0005.pending.md` : this intent must be processed by an agent named `Architect`. The .pending.md termination act as a trigger for the Agent wake-up.
- `payment-sys_0005.wip.md` : this intent is currently processed by an agent.
#### Agent
Some states are invoking an agent. Here, any file named `*.peinding.md` will trigger an Agent named `Architecte` to process the content as a prompt. Any agent wake-up is done by the `.bridge.exe` orchestration application. The Bridge rôle field permit to the `Dashboard` to understand the purpose or status of each file.

Al `Intent` files are processed in a serialized workflow.
#### Bridge
The `INTENT` bridge keyword means that such files are seen as an `Intent` in the UI. The `WIP` key-word mean that an agent is processing the element. And intent can be `INTENT`, `INTENT-PENDING` or `INTENT-WIP` from the dashboard point of view.

Format

| Field      | Decription                                                                                                                                                                                                                |
| ---------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Name       | Name of the Entity type. Used in the Dashboard.                                                                                                                                                                           |
| Directory  | Name of the directory, in the .specs or .works, or any defined in hierarchy directory.                                                                                                                                    |
| Format     | A name formatting pattern. Support wildcard (`*`) and numbering `{0000}` and reference (`$$`). If numbering is used, a globally unique incremented identifier is managed at project root level.                           |
| State      | The various states the Entity can have. Exemple : `Ticket_1234.closed.md`.                                                                                                                                                |
| Transition | The mode of the state : `RENAME` mean the transition of a file to this mode is only a matter of renaming the file name, `NEW` mean the Agent may create a new file with this state, containing reason for this new state. |
| Agent      | The agent to start if a such Entity with this state is appearing.                                                                                                                                                         |
| Bridge     | The                                                                                                                                                                                                                       |
|            |                                                                                                                                                                                                                           |

### Default Workflow Entities

| Name   | Directory     | Format                      | State      | Agent           | Bridge  | Icon   | Color   | Tag      |
| ------ | ------------- | --------------------------- | ---------- | --------------- | ------- | ------ | ------- | -------- |
| Ticket | Board/Tickets | `Ticket_{0000}_{00}`        |            |                 | INPUT   | ticket |         |          |
|        |               |                             | pending    | Agents/Planner  | PENDING |        |         |          |
|        |               |                             | processing |                 | WIP     |        |         |          |
|        |               |                             | rejected   |                 | ERROR   |        | orange  | stop     |
|        |               |                             | request    |                 | REQUEST |        | yellow  | question |
|        |               |                             | closed     | Agents/Scrib    | DONE    |        |         | check    |
| Task   | Board/Tasks   | `$Ticket$_Task_{0000}_{00}` |            |                 |         | task   |         |          |
|        |               |                             | pending    | Agents/Worker   | PENDING |        |         |          |
|        |               |                             | processing |                 | WIP     |        | magenta |          |
|        |               |                             | review     | Agents/Reviewer | PENDING |        |         |          |
|        |               |                             | reviewing  |                 |         |        |         |          |
|        |               |                             | rework     | Agents/Worker   | WIP     |        |         |          |
|        |               |                             | failled    |                 | ERROR   |        | red     | warning  |
|        |               |                             | done       |                 | DONE    |        |         | check    |

### Agents/Planner

`_begin.agent.md`
```
# Agent Definition
You are the Ticket Planner.

# Goal
- Analyze the Ticket intent.
- Check if the source code is doing the Ticket intent.
	  - If the Ticket intent is not fullfill in the 
```

`_end.agent.md`
```
# Next Operations
...
```


Key-Words

`$AGENT <name>`



| Name   | Directory     | Format                      | State      | Agent           | Bridge  | Icon   | Color   | Tag      |
| ------ | ------------- | --------------------------- | ---------- | --------------- | ------- | ------ | ------- | -------- |
| Ticket | Board/Tickets | `Ticket_{0000}_{00}`        |            |                 | INPUT   | ticket |         |          |
|        |               |                             | pending    | *               | PENDING |        |         |          |
|        |               |                             | processing |                 | WIP     |        |         |          |
|        |               |                             | rejected   |                 | ERROR   |        | orange  | stop     |
|        |               |                             | request    |                 | REQUEST |        | yellow  | question |
|        |               |                             | closed     | Agents/Scrib    | DONE    |        |         | check    |
| Task   | Board/Tasks   | `$Ticket$_Task_{0000}_{00}` |            |                 |         | task   |         |          |
|        |               |                             | pending    | Agents/Worker   | PENDING |        |         |          |
|        |               |                             | processing |                 | WIP     |        | magenta |          |
|        |               |                             | review     | Agents/Reviewer | PENDING |        |         |          |
|        |               |                             | reviewing  |                 |         |        |         |          |
|        |               |                             | rework     | Agents/Worker   | WIP     |        |         |          |
|        |               |                             | failled    |                 | ERROR   |        | red     | warning  |
|        |               |                             | done       |                 | DONE    |        |         | check    |

### Agents/Planner