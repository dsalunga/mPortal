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

| Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarCategory | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarCategory.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarEvent | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarEvent.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarEventField | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarEventField.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarHelper | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarItem | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarItem.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarLocation | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarLocation.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarRecurrence | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarRecurrence.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarTemplate | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarTemplate.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: CalendarTemplateField | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/CalendarTemplateField.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar :: Constants | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Constants.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarCategoryProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/CalendarCategoryProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarEventFieldProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/CalendarEventFieldProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarEventProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/CalendarEventProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarLocationProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/CalendarLocationProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarRecurrenceProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/CalendarRecurrenceProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarTemplateFieldProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/CalendarTemplateFieldProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: CalendarTemplateProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/CalendarTemplateProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: EventCalendarProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/EventCalendarProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers :: IEventCalendarProvider | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Providers/IEventCalendarProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder :: EmailReminder | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder/EmailReminder.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder :: EventReminderSenderTask | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder/EventReminderSenderTask.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder :: IReminder | `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/Reminder/IReminder.cs` | Library/business component; assess API compatibility and dependencies. |
