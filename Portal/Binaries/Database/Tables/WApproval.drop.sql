IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WGroupApproval_DateApproved]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WApproval] DROP CONSTRAINT [DF_WGroupApproval_DateApproved]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WGroupApproval_Comments]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WApproval] DROP CONSTRAINT [DF_WGroupApproval_Comments]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WApproval]') AND type in (N'U'))
DROP TABLE [dbo].[WApproval]
GO
