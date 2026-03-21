CREATE TABLE [dbo].[WebParameter] (
    [Id]         INT            NOT NULL,
    [ObjectId]   INT            NOT NULL,
    [RecordId]   INT            NOT NULL,
    [Name]       NVARCHAR (250) NOT NULL,
    [Value]      NVARCHAR (MAX) NOT NULL,
    [IsRequired] INT            CONSTRAINT [DF_WebParameter_IsRequired] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_WebParameter] PRIMARY KEY CLUSTERED ([Id] ASC)
);

