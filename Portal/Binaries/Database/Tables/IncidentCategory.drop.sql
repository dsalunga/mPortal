IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IncidentCategory]') AND type in (N'U'))
DROP TABLE [dbo].[IncidentCategory]
GO
