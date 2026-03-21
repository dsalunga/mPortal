CREATE TABLE [dbo].[WebCategory] (
    [Id]       INT            NOT NULL,
    [Name]     NVARCHAR (500) CONSTRAINT [DF__WebCategor__Name] DEFAULT ((-1)) NOT NULL,
    [ObjectId] INT            CONSTRAINT [DF__WebCatego__ObjectId] DEFAULT ((-1)) NOT NULL,
    [Rank]     INT            CONSTRAINT [DF__WebCategor__Rank] DEFAULT ((0)) NOT NULL,
    [ParentId] INT            CONSTRAINT [DF__WebCatego__ParentId] DEFAULT ((-1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

