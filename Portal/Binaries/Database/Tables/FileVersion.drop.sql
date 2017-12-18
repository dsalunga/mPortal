IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileVersion_VersionDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileVersion] DROP CONSTRAINT [DF_FileVersion_VersionDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileVersion_Activity]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileVersion] DROP CONSTRAINT [DF_FileVersion_Activity]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_FileVersion_UserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileVersion] DROP CONSTRAINT [DF_FileVersion_UserId]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FileVersion]') AND type in (N'U'))
DROP TABLE [dbo].[FileVersion]
GO
