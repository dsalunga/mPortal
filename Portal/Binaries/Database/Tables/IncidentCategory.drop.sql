IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentCategory_Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentCategory] DROP CONSTRAINT [DF_IncidentCategory_Rank]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentC__Insta__187221A6]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentCategory] DROP CONSTRAINT [DF__IncidentC__Insta__187221A6]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IncidentCategory]') AND type in (N'U'))
DROP TABLE [dbo].[IncidentCategory]
GO
