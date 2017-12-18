CREATE TABLE [dbo].[WallUpdate] (
    [Id]             INT      NOT NULL,
    [UpdateRecordId] INT      NOT NULL,
    [UpdateObjectId] INT      NOT NULL,
    [ByRecordId]     INT      NOT NULL,
    [ByObjectId]     INT      NOT NULL,
    [Content]        NTEXT    CONSTRAINT [DF_Wall_Content] DEFAULT ('') NOT NULL,
    [UpdateDate]     DATETIME CONSTRAINT [DF_Wall_UpdateDate] DEFAULT (getdate()) NOT NULL,
    [EventTypeId]    INT      CONSTRAINT [DF_Wall_EventTypeId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WallUpdate] PRIMARY KEY CLUSTERED ([Id] ASC)
);

