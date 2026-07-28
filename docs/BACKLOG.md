\# EQLogParser EQL Backlog



Ideas worth remembering.

Not prioritized.

Not promises.

Ideas worth remembering.

The backlog is intentionally not prioritized. Inclusion here is not a commitment to implement a feature.


\---



\## Camp Analytics



\- Merge camps separated by short AFKs or bio breaks.

\- Split camps on configurable inactivity timeout.

\- Camp drill-down to individual fights.

\- Camp heat map.

\- Median HP.

\- Median fight length.

\- Kill rate (kills/minute).

\- Downtime percentage.

\- Pull interval statistics.

\- Best camps by level.

\- Best camps by zone.

\- Best camps by build.



\---



\## Experience



\- XP per kill.

\- XP by NPC.

\- XP by named.

\- XP by zone.

\- XP by instance.

\- XP by difficulty.

\- XP history graph.

\- XP by play session.

\- Automatic level history reconstruction.



\---



\## Player Context



\- ###COMMENT### timeline.

\- Build history from /who.

\- Zone history from /who.

\- Instance history from /who.

\- Guild roster history.

\- Player history timeline.

\- Equipment/loadout snapshots (future).

\- Manual experiment notes.



\---



\## EverQuest Legends



\- Parse multiclass builds.

\- Parse /who zone instances.

\- Parse EQL-specific system messages.

\- Track augmentation level.

\- Track equipment upgrade level.

\- Named spawn statistics.

\- Best camps for each build.

\- Build comparison reports.



\---



\## User Interface



\- Camp export (CSV).

\- Camp export (Excel).

\- Search camps.

\- Filter by zone.

\- Filter by level.

\- Filter by build.

\- Filter by comments.

\- Sort by XP/hour.

\- Favorite camps.

\- Camp notes.

\- Dark-mode polish.



\---



\## Reports



\- Character progression report.

\- Daily XP summary.

\- Weekly XP summary.

\- Session summaries.

\- Named kill history.

\- Zone completion summaries.



\---



\## Parser



\- Multi-log support.

\- Guild log merge.

\- Timeline repair.

\- Better startup context recovery.

\- Automatic stale-context detection.

\- Additional parser diagnostics.



\---



\## Nice Ideas



\- Timeline viewer.

\- Interactive camp map.

\- Automatic leveling recommendations.

\- Compare two camps.

\- Compare two builds.

\- Share camp reports.

\- Plugin architecture.

## Build, Packaging, and Security

- Restore or document the WiX Toolset installer build process.
- Add the missing WiX Toolset dependency to developer setup documentation.
- Verify the minimum supported WiX version and installer project compatibility.
- Build and test the Windows installer from a clean machine.
- Confirm installer upgrade, uninstall, and settings-preservation behavior.
- Decide whether releases should use an installer, portable ZIP, or both.
- Automate creation of GitHub release artifacts.
- Investigate GitHub release asset-size limitations and artifact hosting.
- Add application version information to the window title and binaries.
- Add checksums for published release archives.
- Consider code signing to reduce Windows SmartScreen warnings.

- Migrate from the unsupported .NET 6 target framework to a supported .NET version.
- Test WPF, Syncfusion themes, spell loading, and installer behavior after framework migration.
- Review and update `Microsoft.Windows.Compatibility`.
- Review and update `log4net`.
- Audit NuGet dependencies for known security advisories.
- Add dependency-vulnerability checks to the release process.
- Resolve or document the `AllRules.ruleset` build warning.
- Review the self-assignment warning in `MainWindow.xaml.cs`.
- Review obsolete or incomplete THJ-specific data structures and warnings.