CREATE TABLE [dbo].[WebShare] (
    [Id]            INT NOT NULL,
    [ObjectId]      INT CONSTRAINT [DF__WebShare__Object] DEFAULT ((-1)) NOT NULL,
    [RecordId]      INT CONSTRAINT [DF__WebShare__Record] DEFAULT ((-1)) NOT NULL,
    [ShareObjectId] INT CONSTRAINT [DF__WebShare__ShareO] DEFAULT ((-1)) NOT NULL,
    [ShareRecordId] INT CONSTRAINT [DF__WebShare__ShareR] DEFAULT ((-1)) NOT NULL,
    [Allow]         INT CONSTRAINT [DF__WebShare__Allow] DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

