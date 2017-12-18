SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebApplication]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebApplication](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AppKey] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IpAddresses] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebApplication_AppKey]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebApplication] ADD  CONSTRAINT [DF_WebApplication_AppKey]  DEFAULT ('') FOR [AppKey]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebApplication_IpAddresses]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebApplication] ADD  CONSTRAINT [DF_WebApplication_IpAddresses]  DEFAULT ('') FOR [IpAddresses]
END

GO
