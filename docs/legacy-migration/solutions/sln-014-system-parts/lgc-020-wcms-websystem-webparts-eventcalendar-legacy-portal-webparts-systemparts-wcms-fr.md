# LGC-020 - WCMS.WebSystem.WebParts.EventCalendar

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-020 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/WCMS.WebSystem.Apps.EventCalendar.csproj` |
| Modern Project File / Evidence | `Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/WCMS.WebSystem.Apps.EventCalendar.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:24, Not Applicable:1, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 22 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Migration to .NET 10 complete. All source files compile with 0 errors. |
| WebForms Surface Present | No | N/A |
| Endpoint Surface Present | No | N/A |
| Class/Component Porting | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarCategory | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./CalendarCategory.cs` | `./CalendarCategory.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarEvent | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./CalendarEvent.cs` | `./CalendarEvent.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarEventField | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./CalendarEventField.cs` | `./CalendarEventField.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./CalendarHelper.cs` | `./CalendarHelper.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarItem | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./CalendarItem.cs` | `./CalendarItem.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarLocation | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./CalendarLocation.cs` | `./CalendarLocation.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarRecurrence | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./CalendarRecurrence.cs` | `./CalendarRecurrence.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarTemplate | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./CalendarTemplate.cs` | `./CalendarTemplate.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarTemplateField | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./CalendarTemplateField.cs` | `./CalendarTemplateField.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: Constants | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Constants.cs` | `./Constants.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarCategoryProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/CalendarCategoryProvider.cs` | `./Providers/CalendarCategoryProvider.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarEventFieldProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/CalendarEventFieldProvider.cs` | `./Providers/CalendarEventFieldProvider.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarEventProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/CalendarEventProvider.cs` | `./Providers/CalendarEventProvider.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarLocationProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/CalendarLocationProvider.cs` | `./Providers/CalendarLocationProvider.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarRecurrenceProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/CalendarRecurrenceProvider.cs` | `./Providers/CalendarRecurrenceProvider.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarTemplateFieldProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/CalendarTemplateFieldProvider.cs` | `./Providers/CalendarTemplateFieldProvider.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarTemplateProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/CalendarTemplateProvider.cs` | `./Providers/CalendarTemplateProvider.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: EventCalendarProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/EventCalendarProvider.cs` | `./Providers/EventCalendarProvider.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: IEventCalendarProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Providers/IEventCalendarProvider.cs` | `./Providers/IEventCalendarProvider.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder :: EmailReminder | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Reminder/EmailReminder.cs` | `./Reminder/EmailReminder.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder :: EventReminderSenderTask | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Reminder/EventReminderSenderTask.cs` | `./Reminder/EventReminderSenderTask.cs` |
| LGC-020 | Completed | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder :: IReminder | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Reminder/IReminder.cs` | `./Reminder/IReminder.cs` |
