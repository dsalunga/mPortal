CREATE TABLE [dbo].[WebPartConfig] (
    [PartConfigId] INT           NOT NULL,
    [PartId]       INT           NOT NULL,
    [Name]         VARCHAR (256) NOT NULL,
    [FileName]     VARCHAR (256) NOT NULL,
    CONSTRAINT [PK_WebPartConfig] PRIMARY KEY CLUSTERED ([PartConfigId] ASC)
);

