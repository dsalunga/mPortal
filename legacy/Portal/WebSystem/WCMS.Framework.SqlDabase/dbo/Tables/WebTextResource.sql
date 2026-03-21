CREATE TABLE [dbo].[WebTextResource] (
    [TextResourceId] INT            NOT NULL,
    [ContentTypeId]  INT            NOT NULL,
    [Title]          NVARCHAR (250) NOT NULL,
    [Content]        NVARCHAR (MAX) NOT NULL,
    [DirectoryId]    INT            CONSTRAINT [DF_WebTextResources_DirectoryId] DEFAULT ((-1)) NOT NULL,
    [Rank]           INT            CONSTRAINT [DF_WebTextResources_Rank] DEFAULT ((0)) NOT NULL,
    [DateModified]   DATETIME       CONSTRAINT [DF_WebTextResource_DateModified] DEFAULT (getdate()) NOT NULL,
    [OwnerObjectId]  INT            CONSTRAINT [DF_WebTextResource_OwnerObjectId] DEFAULT ((-1)) NOT NULL,
    [OwnerRecordId]  INT            CONSTRAINT [DF_WebTextResource_OwnerRecordId] DEFAULT ((-1)) NOT NULL,
    [DatePersisted]  DATETIME       CONSTRAINT [DF_WebTextResource_DatePersisted] DEFAULT (getdate()) NOT NULL,
    [PhysicalPath]   NVARCHAR (500) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebTextResource_PhysicalPath] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_WebTextResources] PRIMARY KEY CLUSTERED ([TextResourceId] ASC)
);

