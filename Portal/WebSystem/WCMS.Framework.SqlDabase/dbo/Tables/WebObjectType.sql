CREATE TABLE [dbo].[WebObjectType] (
    [ObjectTypeId]     INT            NOT NULL,
    [Name]             NVARCHAR (250) NOT NULL,
    [SourceTypeId]     INT            NOT NULL,
    [SourceLocationId] INT            NOT NULL,
    [IdField]          NVARCHAR (250) NOT NULL,
    [NameField]        NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_WebObjectTypes] PRIMARY KEY CLUSTERED ([ObjectTypeId] ASC)
);

