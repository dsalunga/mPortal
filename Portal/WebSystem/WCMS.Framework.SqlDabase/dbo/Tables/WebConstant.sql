CREATE TABLE [dbo].[WebConstant] (
    [ConstantId] INT            NOT NULL,
    [Value]      NVARCHAR (256) NOT NULL,
    [Rank]       INT            NOT NULL,
    [Category]   NVARCHAR (50)  CONSTRAINT [DF__WebConsta__Category] DEFAULT ('') NOT NULL,
    [Text]       NVARCHAR (256) NOT NULL,
    [ObjectId]   INT            CONSTRAINT [DF__WebConsta__ObjectId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WebConstants] PRIMARY KEY CLUSTERED ([ConstantId] ASC)
);



