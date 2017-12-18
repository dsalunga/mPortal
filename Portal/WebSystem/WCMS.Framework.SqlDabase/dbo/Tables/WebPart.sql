CREATE TABLE [dbo].[WebPart] (
    [PartId]   INT            NOT NULL,
    [Name]     NVARCHAR (255) NOT NULL,
    [Identity] NVARCHAR (255) NOT NULL,
    [Active]   INT            NOT NULL,
    CONSTRAINT [PK_WebParts] PRIMARY KEY CLUSTERED ([PartId] ASC)
);

