IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_Id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_ODKVisit_Id]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_UserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_Table1_UserId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_ODKVisit_DateCreated]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_VisitReport]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_Table1_VisitReport]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_Table1_Status]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_GroupId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_ODKVisit_GroupId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_MemberName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_Table1_MemberName]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Table1_MemberUserId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_Table1_MemberUserId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_DateVisited]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_ODKVisit_DateVisited]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_ActionTaken]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_ODKVisit_ActionTaken]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_ContactNo]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_ODKVisit_ContactNo]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_TimesVisited]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_ODKVisit_TimesVisited]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_Address]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_ODKVisit_Address]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_MembershipDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_ODKVisit_MembershipDate]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ODKVisit_Tags]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ODKVisit] DROP CONSTRAINT [DF_ODKVisit_Tags]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ODKVisit]') AND type in (N'U'))
DROP TABLE [dbo].[ODKVisit]
GO
