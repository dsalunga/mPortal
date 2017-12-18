CREATE TABLE [dbo].[WebPartControl] (
    [PartControlId]  INT            NOT NULL,
    [PartId]         INT            NOT NULL,
    [Name]           NVARCHAR (256) NOT NULL,
    [Identity]       NVARCHAR (256) NOT NULL,
    [ConfigFileName] NVARCHAR (256) NOT NULL,
    [PartAdminId]    INT            CONSTRAINT [DF_WebPartControl_AdminPartId] DEFAULT ((-1)) NOT NULL,
    [EntryPoint]     INT            CONSTRAINT [DF__WebPartCo__Entry] DEFAULT ((1)) NOT NULL,
    [ParentId]       INT            CONSTRAINT [DF_WebPartControl_ParentId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_WebPartControls] PRIMARY KEY CLUSTERED ([PartControlId] ASC)
);



