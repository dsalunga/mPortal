# LGC-020 - WCMS.WebSystem.WebParts.EventCalendar

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-020 |
| Project Type | Component |
| Project File | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/WCMS.WebSystem.Apps.EventCalendar.csproj` |
| Project Directory | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Not Stated |
| Status Basis | Legacy target framework only (v4.8). |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 22 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Discovery / Planning | Assess framework/API compatibility and plan library porting. |
| WebForms Surface Present | No | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarCategory | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarCategory.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarEvent | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarEvent.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarEventField | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarEventField.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarHelper.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarItem | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarItem.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarLocation | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarLocation.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarRecurrence | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarRecurrence.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarTemplate | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarTemplate.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarTemplateField | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarTemplateField.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: Constants | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Constants.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarCategoryProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/CalendarCategoryProvider.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarEventFieldProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/CalendarEventFieldProvider.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarEventProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/CalendarEventProvider.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarLocationProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/CalendarLocationProvider.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarRecurrenceProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/CalendarRecurrenceProvider.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarTemplateFieldProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/CalendarTemplateFieldProvider.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarTemplateProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/CalendarTemplateProvider.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: EventCalendarProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/EventCalendarProvider.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: IEventCalendarProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/IEventCalendarProvider.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder :: EmailReminder | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder/EmailReminder.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder :: EventReminderSenderTask | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder/EventReminderSenderTask.cs` |
| LGC-020 | Not Stated | Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder :: IReminder | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder/IReminder.cs` |
