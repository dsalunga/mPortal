IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentTicketHistory_IsNote]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentTicketHistory] DROP CONSTRAINT [DF_IncidentTicketHistory_IsNote]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IncidentTicketHistory]') AND type in (N'U'))
DROP TABLE [dbo].[IncidentTicketHistory]
GO
