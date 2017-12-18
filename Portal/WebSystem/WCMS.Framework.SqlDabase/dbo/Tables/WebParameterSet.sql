CREATE TABLE [dbo].[WebParameterSet] (
    [Id]                  INT            CONSTRAINT [DF_WebParameterSet_Id] DEFAULT ((-1)) NOT NULL,
    [Name]                NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebParameterSet_Name] DEFAULT ('') NOT NULL,
    [SchemaParameterName] NVARCHAR (250) CONSTRAINT [DF_WebParameterSet_SchemaParameterName] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_WebParameterSet] PRIMARY KEY CLUSTERED ([Id] ASC)
);

