CREATE TABLE [dbo].[WebObjectColumn] (
    [Id]       INT            CONSTRAINT [DF_WebObjectColumn_Id] DEFAULT ((-1)) NOT NULL,
    [ObjectId] INT            CONSTRAINT [DF_WebObjectColumn_ObjectId] DEFAULT ((-1)) NOT NULL,
    [Name]     NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebObjectColumn_Name] DEFAULT ('') NOT NULL
);

