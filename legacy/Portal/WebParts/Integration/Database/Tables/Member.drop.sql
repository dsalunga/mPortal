IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Member_Flag]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Member] DROP CONSTRAINT [DF_Member_Flag]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Member_MembershipDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Member] DROP CONSTRAINT [DF_Member_MembershipDate]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Member]') AND type in (N'U'))
DROP TABLE [dbo].[Member]
GO
