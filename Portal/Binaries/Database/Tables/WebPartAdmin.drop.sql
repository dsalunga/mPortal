IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartAdmin_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartAdmin] DROP CONSTRAINT [DF_WebPartAdmin_Active]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartAdmin_Visible]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartAdmin] DROP CONSTRAINT [DF_WebPartAdmin_Visible]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartAdmin_InSite]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartAdmin] DROP CONSTRAINT [DF_WebPartAdmin_InSite]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebPartAdmin_EngineId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartAdmin] DROP CONSTRAINT [DF_WebPartAdmin_EngineId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebPartAd__AutoTitle]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebPartAdmin] DROP CONSTRAINT [DF__WebPartAd__AutoTitle]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebPartAdmin]') AND type in (N'U'))
DROP TABLE [dbo].[WebPartAdmin]
GO
