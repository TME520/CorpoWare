# CorpoWare

Self-contained Windows 11 desktop app for staying on top of corporate work that slips through the cracks — especially the stuff that never makes it into Jira.

**License:** GPLv3 (see [LICENSE](LICENSE))

## Vision

In most companies, a large share of real work arrives as Slack DMs, emails, hallway / walk-in questions, and other ad-hoc asks. Nobody wants to open a ticket for every one of them — and then people get blamed for missing a follow-up.

CorpoWare exists so you can capture those **impromptu** items in seconds, move them through a simple Kanban, and run repeatable **routines** (checklists) without leaving a heavy, always-on footprint on the machine.

Design goals:

- **Single EXE** for Windows 11 — no installer maze, no service farm, easy to carry or drop on a workstation.
- **Innocuous by default** — behave like a normal user-mode productivity tool (no hooks, no drivers, no stealth). The aim is to avoid false positives from antivirus, not to evade security policy.
- **Local-first** — your backlog and notes stay on your machine unless you explicitly add sync later.
- **Fast capture** — getting an impromptu ask out of your head and into BACKLOG should take seconds.

## Core features

### 1. Impromptu inbox

Track anything that does not belong in Jira (or that nobody will create a ticket for):

- Slack DMs and threads
- Emails
- Walk-in / desk questions
- Quick verbal asks from meetings

Each item can hold source, who asked, context, and a follow-up reminder so you are not left holding the bag for something that was never formally assigned.

### 2. Kanban board

A four-column board for the work you own:

| Column   | Meaning                                      |
|----------|----------------------------------------------|
| BACKLOG  | Captured, not started                        |
| TODO     | Chosen for the near term                     |
| DOING    | Actively in progress                         |
| DONE     | Finished (kept for history / proof of work)  |

Impromptu items and other tasks move through the same board.

### 3. Routines

Reusable checklists you can run when a process repeats (start of day, release prep, handoff, weekly report, etc.). Check off steps as you go; leave a lightweight record that the routine was completed.

## Non-goals (for now)

- Replacing Jira / Slack / Outlook
- Mandatory cloud sync or admin telemetry
- Kernel drivers, accessibility hooks for keylogging, or anything that looks like malware to AV vendors
- Auto-scraping corporate accounts without explicit user action and credentials they control

## Platform

| Item        | Target                          |
|-------------|---------------------------------|
| OS          | Windows 11                      |
| Distribution| Self-contained `.exe`           |
| Privileges  | Standard user (no admin required for normal use) |

Stack and packaging details will land here once chosen (e.g. .NET / Tauri / similar), as long as the result remains a single portable EXE with a clean AV profile.

## Status

Early scaffolding. Vision and product shape are defined; implementation is starting.

## Contributing

This is a personal / team tooling project under GPLv3. Open issues or PRs against the working branch; do not push straight to `main` without discussion.

## Disclaimer

CorpoWare is a productivity aid. Use it in line with your employer’s IT and security policies. “Innocuous for antivirus” means shipping ordinary, signed-when-possible desktop software — not bypassing corporate security controls.
