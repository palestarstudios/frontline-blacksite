# Contributing to Frontline: Blacksite

First off — thank you for taking the time to contribute. Whether you’re reporting bugs, testing builds, or submitting code, your help directly improves the quality and stability of the project.

This repository hosts **development-facing source and builds**. It is not a mirror of the live Steam release.

---

## Scope of This Repository

This repository exists to:

* Collect **reproducible bug reports** from development builds
* Enable **engine and gameplay iteration**
* Support **modding, experimentation, and learning**

> [!TIP]
> There is already plans to implement an extensive modding API into Frontline: Blacksite. "Support" in this context refers to allowing the mod developer to have in-depth access to the internals without needing to decompile the game and its supporting libraries.

It does **not** represent:

* The final feature set
* Live-service behavior
* Production security posture

Some systems are intentionally disabled or stubbed out.

---

## Ways to Contribute

### Bug Reports (Highly Welcome)

Bug reports are the most valuable contribution at this stage.

Please include:

* Exact build or commit hash
* Clear reproduction steps
* Expected vs actual behavior
* Frequency of occurrence

Bug reports without reproduction steps may be closed.

Use the provided issue templates whenever possible.

### Community Feature Requests

Feature requests are welcome, but expectations need to be set clearly. This repository is not a roadmap voting system. Feature requests are used to:
- Identify pain points
- Spot recurring themes
- Capture ideas that align with the project’s direction

They are not guarantees of implementation.

**Good feature requests:**
- Clearly describe the problem being solved
- Explain why the feature is useful, not just what it is
- Acknowledge potential tradeoffs or complexity

**Poor feature requests (likely to be closed):**
- Requests that redesign core gameplay philosophy
- Balance changes or class tuning without **extensive reasoning and evidence as to why this adjustment needs to occur**
- Requests framed as demands or comparisons to other games

"Wouldn’t it be cool if…" ideas without practical grounding

> [!NOTE]
> Silence on a feature request does not mean it was ignored. Many requests are tracked internally without public discussion.

If a feature aligns with the project’s goals and timing, it may be revisited later in development.

---

### 🔧 Code Contributions

Code contributions are welcome **within scope**.

Good candidates:

* Bug fixes
* Performance improvements
* Code clarity / refactors
* Tooling or developer UX improvements

Before submitting a PR:

* Keep changes focused and minimal
* Avoid unrelated formatting-only changes
* Prefer clarity over cleverness

Large changes should be discussed in an issue first. Changes such as refactoring entire subsystems, adding new systems or making potentially breaking changes to internal and external APIs.

---

### Testing & Feedback

Testing development builds is encouraged.

Useful feedback includes:

* Crashes
* Deterministic bugs
* Performance regressions

Out-of-scope feedback:

* Balance discussions
* Feature requests
* Economy or progression systems
* Comparisons to the live Steam build

These will likely be closed without comment.

---

## Modding & Scripting

Modding hooks and scripting APIs are provided for experimentation.

Rules:

* Mods must not attempt to bypass disabled systems
* Mods should not rely on private or undocumented APIs
* Stability is not guaranteed across commits

---

## Style & Architecture Guidelines

General principles:

* Avoid allocations in hot paths
* Prefer data-oriented designs
* Be explicit about ownership and lifetimes
* Keep gameplay logic deterministic where possible

If in doubt, match existing patterns.

---

## Licensing Reminder

By contributing, you agree that your contributions are licensed under the same license as this repository.

Contributions must be original work.

---

## Final Notes

This project prioritizes:

* Technical clarity
* Player safety
* Community trust

We appreciate contributors who respect those goals.

Thanks for helping build something solid.
