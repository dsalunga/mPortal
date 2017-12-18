SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebObjectSecurity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebObjectSecurity](
	[Id] [int] NOT NULL,
	[ObjectId] [int] NOT NULL,
	[RecordId] [int] NOT NULL,
	[SecurityObjectId] [int] NOT NULL,
	[SecurityRecordId] [int] NOT NULL,
	[Public] [int] NOT NULL,
 CONSTRAINT [PK_WebObjectAdmin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebObjectSecurity_Public]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebObjectSecurity] ADD  CONSTRAINT [DF_WebObjectSecurity_Public]  DEFAULT ((0)) FOR [Public]
END

GO
