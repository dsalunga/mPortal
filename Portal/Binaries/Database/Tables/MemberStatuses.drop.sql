IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MemberStatuses]') AND type in (N'U'))
DROP TABLE [dbo].[MemberStatuses]
GO
