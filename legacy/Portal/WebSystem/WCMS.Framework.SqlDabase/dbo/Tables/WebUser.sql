CREATE TABLE [dbo].[WebUser] (
    [UserId]             INT             NOT NULL,
    [UserName]           NVARCHAR (50)   NOT NULL,
    [Password]           NVARCHAR (1000) NOT NULL,
    [FirstName]          NVARCHAR (256)  NOT NULL,
    [MiddleName]         NVARCHAR (250)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebUser_MiddleName] DEFAULT ('') NOT NULL,
    [LastName]           NVARCHAR (256)  NOT NULL,
    [Email]              NVARCHAR (250)  NOT NULL,
    [LastUpdate]         DATETIME        CONSTRAINT [DF_WebUser_LastUpdate] DEFAULT (getdate()) NOT NULL,
    [Active]             INT             CONSTRAINT [DF_WebUser_Active] DEFAULT ((0)) NOT NULL,
    [ActivationKey]      NVARCHAR (250)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebUser_RegCode] DEFAULT ('') NOT NULL,
    [DateCreated]        DATETIME        CONSTRAINT [DF_WebUser_DateCreated] DEFAULT (getdate()) NOT NULL,
    [NewEmail]           NVARCHAR (250)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebUser_NewEmail] DEFAULT ('') NOT NULL,
    [Email2]             NVARCHAR (250)  CONSTRAINT [DF_WebUser_Email2] DEFAULT ('') NOT NULL,
    [Gender]             NCHAR (1)       CONSTRAINT [DF_WebUser_Gender] DEFAULT ('U') NOT NULL,
    [NameSuffix]         NVARCHAR (50)   CONSTRAINT [DF_WebUser_NameSuffix] DEFAULT ('') NOT NULL,
    [MobileNumber]       NVARCHAR (50)   CONSTRAINT [DF_WebUser_MobileNumber] DEFAULT ('') NOT NULL,
    [TelephoneNumber]    NVARCHAR (50)   CONSTRAINT [DF_WebUser_TelephoneNumber] DEFAULT ('') NOT NULL,
    [LastLogin]          DATETIME        CONSTRAINT [DF_WebUser_LastLoginDate] DEFAULT (getdate()) NOT NULL,
    [StatusText]         NVARCHAR (1500) CONSTRAINT [DF_WebUser_StatusText] DEFAULT ('') NOT NULL,
    [PasswordExpiryDate] DATETIME        CONSTRAINT [DF_WebUser_PasswordExpiryDate] DEFAULT ('1800-01-01') NOT NULL,
    [PhotoPath]          NVARCHAR (500)  CONSTRAINT [DF_WebUser_PhotoPath] DEFAULT ('') NOT NULL,
    [ProviderId]         INT             CONSTRAINT [DF_WebUser_ProviderId] DEFAULT ((-1)) NOT NULL,
    [Status]             INT             CONSTRAINT [DF__WebUser__Status] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WebUsers] PRIMARY KEY CLUSTERED ([UserId] ASC)
);



