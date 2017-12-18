SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebGlobalPolicy]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebGlobalPolicy](
	[GlobalPolicyId] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
 CONSTRAINT [PK_WebGlobalPolicy] PRIMARY KEY CLUSTERED 
(
	[GlobalPolicyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGlobalPolicy_GlobalPolicyId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGlobalPolicy] ADD  CONSTRAINT [DF_WebGlobalPolicy_GlobalPolicyId]  DEFAULT ((-1)) FOR [GlobalPolicyId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebGlobalPolicy_Name]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebGlobalPolicy] ADD  CONSTRAINT [DF_WebGlobalPolicy_Name]  DEFAULT ('') FOR [Name]
END

GO
