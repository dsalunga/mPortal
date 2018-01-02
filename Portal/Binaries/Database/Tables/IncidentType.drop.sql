IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentType_FollowStdSla]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentType] DROP CONSTRAINT [DF_IncidentType_FollowStdSla]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_IncidentType_Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentType] DROP CONSTRAINT [DF_IncidentType_Rank]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentT__Insta__3049AB37]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentType] DROP CONSTRAINT [DF__IncidentT__Insta__3049AB37]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IncidentType]') AND type in (N'U'))
DROP TABLE [dbo].[IncidentType]
GO
