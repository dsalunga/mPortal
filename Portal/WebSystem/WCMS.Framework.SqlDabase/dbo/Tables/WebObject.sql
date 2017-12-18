CREATE TABLE [dbo].[WebObject] (
    [Id]               INT            NOT NULL,
    [Name]             NVARCHAR (50)  NOT NULL,
    [IdentityColumn]   NVARCHAR (50)  NOT NULL,
    [ObjectType]       VARCHAR (50)   CONSTRAINT [DF_WebObjects_ObjectType] DEFAULT ('T') NOT NULL,
    [Owner]            NVARCHAR (250) NOT NULL,
    [Prefix]           NVARCHAR (50)  CONSTRAINT [DF_WebObject_Prefix] DEFAULT ('') NOT NULL,
    [LastRecordId]     INT            CONSTRAINT [DF_WebObjects_LastRecordId] DEFAULT ((0)) NOT NULL,
    [MaxCacheCount]    INT            CONSTRAINT [DF_WebObjects_MaxCacheSize] DEFAULT ((-1)) NOT NULL,
    [AccessTypeId]     INT            CONSTRAINT [DF_WebObjects_AccessTypeId] DEFAULT ((-1)) NOT NULL,
    [CacheTypeId]      INT            CONSTRAINT [DF_WebObjects_CacheTypeId] DEFAULT ((-1)) NOT NULL,
    [MaxHistoryCount]  INT            CONSTRAINT [DF_WebObjects_MaxHistorySize] DEFAULT ((-1)) NOT NULL,
    [DataProviderName] NVARCHAR (250) CONSTRAINT [DF_WebObject_DataProviderName] DEFAULT ('') NOT NULL,
    [TypeName]         NVARCHAR (250) CONSTRAINT [DF_WebObject_TypeName] DEFAULT ('') NOT NULL,
    [CacheInterval]    INT            CONSTRAINT [DF_WebObject_CacheInterval] DEFAULT ((-1)) NOT NULL,
    [DateModified]     DATETIME       CONSTRAINT [DF_WebObject_DateModified] DEFAULT (getdate()) NOT NULL,
    [ManagerName]      NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebObject_ManagerName] DEFAULT ('') NOT NULL,
    [NameColumn]       NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebObject_NameColumn] DEFAULT ('') NOT NULL,
    [FriendlyName]     NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebObject_FriendlyName] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_WebObjects] PRIMARY KEY CLUSTERED ([Id] ASC)
);

