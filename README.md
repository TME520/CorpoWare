# CorpoWare

Self-contained Windows 11 desktop app for staying on top of corporate work that slips through the cracks — especially the stuff that never makes it into Jira.

**License:** GPLv3 (see [LICENSE](LICENSE))

## Vision

In most companies, a large share of real work arrives as Slack DMs, emails, hallway / walk-in questions, and other ad-hoc asks. Nobody wants to open a ticket for every one of them — and then people get blamed for missing a follow-up.

CorpoWare exists so you can capture those **impromptu** items in seconds, move them through a simple Kanban, and run repeatable **routines** (checklists) without leaving a heavy, always-on footprint on the machine.

Design goals:

- **Portable single EXE** for Windows 11 — no installer, no admin rights, runnable from a USB stick or any user-writable folder.
- **Innocuous by default** — behave like a normal user-mode productivity tool (no hooks, no drivers, no stealth). The aim is to avoid false positives from antivirus, not to evade security policy.
- **Local-first & side-by-side data** — backlog and notes live next to the app (USB-friendly); no cloud unless you add it later.
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

| Item         | Target |
|--------------|--------|
| OS           | Windows 11 |
| Distribution | Portable — run from a folder or USB stick, **no installer** |
| Privileges   | Standard user only (**no admin**) |
| Data         | Stored next to the app (e.g. a `data/` folder), not in Program Files or machine-wide registry |
| Packaging    | Self-contained single-file (or small folder) `.exe`; intended stack: **.NET 8 + WPF** (trimmed publish) |

The app must be usable when the working copy lives on removable media: no dependency on a prior install, no write to protected system locations, and settings/DB follow the executable.

## Build, run, and publish

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Windows 11 to run the WPF UI

`EnableWindowsTargeting` is set, so `dotnet build` also works on Linux and macOS. Running the window still requires Windows.

### Build

From the repository root:

```bash
dotnet build
```

### Run from source (Windows)

```bash
dotnet run --project src/CorpoWare/CorpoWare.csproj
```

The first launch creates `data/state.json` next to that build's `CorpoWare.exe` (under `src/CorpoWare/bin/`). Sample inbox, board, and routine rows are written only when the file is missing. The app manifest requests `asInvoker`, so it stays a standard-user process.

### Publish a portable executable

From the repository root:

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true
```

The project file bundles native libraries into the executable and compresses the single-file package, so that command is the full publish. It also opts in to partial trimming (`TrimMode=partial`). The .NET SDK otherwise blocks `PublishTrimmed` for WPF. Partial trimming removes unused assemblies; member-level trimming is left off so the WPF shell can still load. Output:

```text
src/CorpoWare/bin/Release/net8.0-windows/win-x64/publish/CorpoWare.exe
```

Copy `CorpoWare.exe` to a folder or a USB stick and start it there. Nothing is installed, and the process does not request administrator rights. On first launch a `data` folder appears beside the executable and holds `state.json`. Remove that folder to load the sample seed again.

The data directory follows the real executable path, including when the app is published as a single file (the single-file host extracts assemblies to a temp folder; app data is not stored there).

### Repository layout

```text
CorpoWare.sln
src/CorpoWare.Core/     models and the JSON store (data/state.json beside the exe)
src/CorpoWare/          WPF shell
tests/CorpoWare.Tests/  store and portable-path checks (no UI)
```

### Tests

From the repository root:

```bash
dotnet test
```

The tests cover the JSON file beside a chosen path and the rule that data follows the executable rather than the single-file extraction folder. They do not open the WPF window.

## Status

The portable shell is in place. Impromptu, Kanban, and Routines open from the sidebar and read `data/state.json` beside the executable. Lists are seeded with sample rows. Checking a routine step saves that file. Adding items, editing text, and moving Kanban cards are not built yet.

## Contributing

This is a personal / team tooling project under GPLv3. Open issues or PRs against the working branch; do not push straight to `main` without discussion.

## Disclaimer

CorpoWare is a productivity aid. Use it in line with your employer’s IT and security policies. “Innocuous for antivirus” means shipping ordinary, signed-when-possible desktop software — not bypassing corporate security controls.
