IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicket_GroupId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] DROP CONSTRAINT [DF_IncidentTicket_GroupId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicket_AssignedUserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] DROP CONSTRAINT [DF_IncidentTicket_AssignedUserId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicket_TicketGuid]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] DROP CONSTRAINT [DF_IncidentTicket_TicketGuid]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicket_SubmitterId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] DROP CONSTRAINT [DF_IncidentTicket_SubmitterId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicket_ETA]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] DROP CONSTRAINT [DF_IncidentTicket_ETA]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicket_TypeId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] DROP CONSTRAINT [DF_IncidentTicket_TypeId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicket_NotifyAlso]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] DROP CONSTRAINT [DF_IncidentTicket_NotifyAlso]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentT__Insta__28A8896F]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicket] DROP CONSTRAINT [DF__IncidentT__Insta__28A8896F]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IncidentTicket]') AND type in (N'U'))
DROP TABLE [dbo].[IncidentTicket]
GO
