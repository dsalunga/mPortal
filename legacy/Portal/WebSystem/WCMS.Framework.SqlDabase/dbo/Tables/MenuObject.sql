CREATE TABLE [dbo].[MenuObject] (
    [Id]             INT           NOT NULL,
    [RecordId]       INT           NOT NULL,
    [ObjectId]       INT           NOT NULL,
    [Width]          NVARCHAR (63) NOT NULL,
    [Height]         NVARCHAR (63) NOT NULL,
    [Horizontal]     INT           NOT NULL,
    [MenuId]         INT           NOT NULL,
    [ParameterSetId] INT           CONSTRAINT [DF_MenuObject_ParameterSetId] DEFAULT ((-1)) NOT NULL,
    [RenderMode]     INT           CONSTRAINT [DF_MenuObject_RenderMode] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SiteMenu] PRIMARY KEY CLUSTERED ([Id] ASC)
);

