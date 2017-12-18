CREATE TABLE [dbo].[WebSkin] (
    [Id]       INT            NOT NULL,
    [Name]     NVARCHAR (500) CONSTRAINT [DF__WebSkin__Name] DEFAULT ('') NOT NULL,
    [Rank]     INT            CONSTRAINT [DF__WebSkin__Rank] DEFAULT ((0)) NOT NULL,
    [ObjectId] INT            CONSTRAINT [DF__WebSkin__ObjectId] DEFAULT ((-1)) NOT NULL,
    [RecordId] INT            CONSTRAINT [DF__WebSkin__RecordId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WebSkin] PRIMARY KEY CLUSTERED ([Id] ASC)
);



