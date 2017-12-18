SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendarLocations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventCalendarLocations](
	[LocationId] [int] NOT NULL,
	[Name] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Bookable] [int] NOT NULL,
 CONSTRAINT [PK_EventCalendarLocations] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarLocations_Bookable]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarLocations] ADD  CONSTRAINT [DF_EventCalendarLocations_Bookable]  DEFAULT ((1)) FOR [Bookable]
END

GO
