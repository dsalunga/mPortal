IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGlobalPolicy_GlobalPolicyId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGlobalPolicy] DROP CONSTRAINT [DF_WebGlobalPolicy_GlobalPolicyId]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGlobalPolicy_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGlobalPolicy] DROP CONSTRAINT [DF_WebGlobalPolicy_Name]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebGlobalPolicy]') AND type in (N'U'))
DROP TABLE [dbo].[WebGlobalPolicy]
GO
