CREATE TABLE [dbo].[WebPartAdmin] (
    [PartAdminId]      INT            NOT NULL,
    [PartId]           INT            NOT NULL,
    [Name]             NVARCHAR (256) NOT NULL,
    [FileName]         NVARCHAR (256) NOT NULL,
    [ParentId]         INT            NOT NULL,
    [Active]           INT            CONSTRAINT [DF_WebPartAdmin_Active] DEFAULT ((1)) NOT NULL,
    [Visible]          INT            CONSTRAINT [DF_WebPartAdmin_Visible] DEFAULT ((1)) NOT NULL,
    [InSiteContext]    INT            CONSTRAINT [DF_WebPartAdmin_InSite] DEFAULT ((0)) NOT NULL,
    [TemplateEngineId] INT            CONSTRAINT [DF_WebPartAdmin_EngineId] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_WebPartAdmin] PRIMARY KEY CLUSTERED ([PartAdminId] ASC)
);



