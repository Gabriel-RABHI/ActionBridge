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

```mermaid
flowchart LR
Intents --> Requirements --> Tickets --> Tasks
```

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

![[Pasted image 20260424000022.png]]
### Bidirectional State Reconciliation

ActionBridge operates as a bidirectional state reconciliation engine. While the standard flow cascades down from Specifications to Deliverables, the system can also run in reverse to verify that actual code behaviors and implemented rules remain perfectly reflected in the `Requirements`.

If a developer introduces a manual change directly into the business logic, this triggers a back-propagation of the source code reality into the Specification Space. When executed, this back-propagation ensures the `Requirements` remain the genuine, ultimate source of truth by forcing them to adapt to the actual, compiling source code.

This reverse-sync unlocks powerful workflows. Developers can manually write or insert large portions of legacy code and instruct ActionBridge to automatically generate the underlying `Requirements`. Furthermore, this enables a continuous "round-trip" delta reduction loop: a developer can quickly draft poorly optimized code, use back-propagation to extract its logical intent into a `Requirement`, and then trigger a forward pass, instructing the AI to perfectly refactor and rewrite the code based on that newly minted specification.
### Hierarchical Context Cascading

In the directory hierarchy, the configuration define two directories :
- `.specs` : this directories contains the specifications, including `Intents` and `Requirements`.
- `.works` : this directories contains `Tickets` and `Tasks`.


> [!CAUTION]
> WORK IN PROGRESS

