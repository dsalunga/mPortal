SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountryState]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CountryState](
	[StateCode] [int] NOT NULL,
	[StateName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ZipCode] [nvarchar](64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CountryCode] [int] NOT NULL,
 CONSTRAINT [PK_MISC_CountryState] PRIMARY KEY CLUSTERED 
(
	[StateCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_USState_StateName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CountryState] ADD  CONSTRAINT [DF_USState_StateName]  DEFAULT ('') FOR [StateName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_USState_ZipCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CountryState] ADD  CONSTRAINT [DF_USState_ZipCode]  DEFAULT ('') FOR [ZipCode]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_USState_CountryCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CountryState] ADD  CONSTRAINT [DF_USState_CountryCode]  DEFAULT ((-1)) FOR [CountryCode]
END

GO
