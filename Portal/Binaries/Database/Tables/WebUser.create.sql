SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebUser](
	[UserId] [int] NOT NULL,
	[UserName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Password] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FirstName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MiddleName] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[LastName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
	[Active] [int] NOT NULL,
	[ActivationKey] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[NewEmail] [nvarchar](250) COLLATE Latin1_General_CI_AI NOT NULL,
	[Email2] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Gender] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NameSuffix] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MobileNumber] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TelephoneNumber] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastLogin] [datetime] NOT NULL,
	[StatusText] [nvarchar](1500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PasswordExpiryDate] [datetime] NOT NULL,
	[PhotoPath] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProviderId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[MaritalStatusId] [int] NOT NULL,
	[LastLoginFailureDate] [datetime] NOT NULL,
	[LoginFailureCount] [int] NOT NULL,
 CONSTRAINT [PK_WebUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_MiddleName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_MiddleName]  DEFAULT ('') FOR [MiddleName]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_LastUpdate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_Active]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_Active]  DEFAULT ((0)) FOR [Active]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_RegCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_RegCode]  DEFAULT ('') FOR [ActivationKey]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_NewEmail]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_NewEmail]  DEFAULT ('') FOR [NewEmail]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_Email2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_Email2]  DEFAULT ('') FOR [Email2]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_Gender]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_Gender]  DEFAULT ('U') FOR [Gender]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_NameSuffix]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_NameSuffix]  DEFAULT ('') FOR [NameSuffix]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_MobileNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_MobileNumber]  DEFAULT ('') FOR [MobileNumber]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_TelephoneNumber]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_TelephoneNumber]  DEFAULT ('') FOR [TelephoneNumber]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_LastLoginDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_LastLoginDate]  DEFAULT (getdate()) FOR [LastLogin]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_StatusText]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_StatusText]  DEFAULT ('') FOR [StatusText]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_PasswordExpiryDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_PasswordExpiryDate]  DEFAULT ('1800-01-01') FOR [PasswordExpiryDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_PhotoPath]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_PhotoPath]  DEFAULT ('') FOR [PhotoPath]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_ProviderId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_ProviderId]  DEFAULT ((-1)) FOR [ProviderId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_Status]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_Status]  DEFAULT ((-1)) FOR [Status]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_MaritalStatus]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_MaritalStatus]  DEFAULT ((-1)) FOR [MaritalStatusId]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_WebUser_LastLoginFailureDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF_WebUser_LastLoginFailureDate]  DEFAULT ('1800-01-01') FOR [LastLoginFailureDate]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__WebUser__LoginFailureCount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[WebUser] ADD  CONSTRAINT [DF__WebUser__LoginFailureCount]  DEFAULT ((0)) FOR [LoginFailureCount]
END

GO
