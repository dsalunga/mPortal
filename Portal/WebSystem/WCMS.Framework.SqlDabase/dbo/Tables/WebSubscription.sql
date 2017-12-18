CREATE TABLE [dbo].[WebSubscription] (
    [SubscriptionId] INT NOT NULL,
    [ObjectId]       INT NOT NULL,
    [RecordId]       INT NOT NULL,
    [PartId]         INT CONSTRAINT [DF_WebSubscription_PartId] DEFAULT ((-1)) NOT NULL,
    [PageId]         INT CONSTRAINT [DF_ArticleSubscription_PageId] DEFAULT ((-1)) NOT NULL,
    [Allow]          INT CONSTRAINT [DF_ArticleSubscription_Allow] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_WebSubscription] PRIMARY KEY CLUSTERED ([SubscriptionId] ASC)
);

