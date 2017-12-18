SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventCalendarRecurrences]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventCalendarRecurrences](
	[RecurrenceId] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Rank] [int] NOT NULL,
 CONSTRAINT [PK_EventCalendarRecurrences] PRIMARY KEY CLUSTERED 
(
	[RecurrenceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_EventCalendarRecurrences_Rank]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EventCalendarRecurrences] ADD  CONSTRAINT [DF_EventCalendarRecurrences_Rank]  DEFAULT ((-1)) FOR [Rank]
END

GO
