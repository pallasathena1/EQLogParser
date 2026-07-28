\# EQLogParser EQL Roadmap
# EQLogParser EQL Roadmap

**Mission:** Build the best progression, camp analysis, and leveling analytics tool for EverQuest Legends while preserving the strengths of the original EQLogParser.


\## Vision



Transform EQLogParser from a combat parser into a character progression and camp analytics tool for EverQuest Legends while preserving compatibility with the upstream parser where practical.



\---



\# Beta 0.1 (Current)



Completed



\- \[x] Experience percentage parsing

\- \[x] Camp detection

\- \[x] Camp List window

\- \[x] XP totals

\- \[x] XP/hour

\- \[x] Average HP

\- \[x] Solo / Group / Mixed camp detection

\- \[x] Minimum camp duration filter

\- \[x] Zone tracking

\- \[x] Level tracking

\- \[x] Instance and difficulty tracking

\- \[x] /who observation timeline

\- \[x] /who-informed player level resolution



\---



\# Beta 0.2



Improve camp usability.



Goals



\- \[ ] ###COMMENT### timeline

\- \[ ] Camp comments column

\- \[ ] Camp drill-down to individual fights

\- \[ ] Search and filter improvements

\- \[ ] Export Camp List to CSV



\---



\# Beta 0.3



Improve player progression analysis.



Goals



\- \[ ] XP by zone

\- \[ ] XP by level

\- \[ ] XP by instance

\- \[ ] XP history graphs

\- \[ ] Character progression report



\---



\# Beta 0.4



Improve EverQuest Legends support.



Goals



\- \[ ] Build history from /who

\- \[ ] Parse additional EQL-specific log messages

\- \[ ] Equipment / augmentation tracking

\- \[ ] Better instance tracking

\- \[ ] Camp recommendations



\---



\# Version 1.0



Stable release.



Goals



\- \[ ] Windows installer

\- \[ ] GitHub Releases

\- \[ ] Updated supported .NET runtime

\- \[ ] Documentation

\- \[ ] Architecture guide

\- \[ ] User guide

\- \[ ] Stable upgrade path



\---



\## Design Principles



\- Prefer game-observed data over manually entered data.

\- Preserve timestamped observations rather than overwriting state.

\- Resolve context from timelines instead of storing mutable global state.

\- Keep parser logic and analytics separate.

\- Prefer many small commits over large feature branches.

\- Keep existing parser functionality working while extending EQL support.

