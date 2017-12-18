CREATE TABLE [dbo].[Articles] (
    [Id]             INT             NOT NULL,
    [Title]          NVARCHAR (255)  NOT NULL,
    [Image]          NVARCHAR (255)  NOT NULL,
    [Description]    NVARCHAR (1024) NOT NULL,
    [Date]           DATETIME        NOT NULL,
    [Content]        NVARCHAR (MAX)  NOT NULL,
    [Author]         NVARCHAR (255)  NOT NULL,
    [SiteId]         INT             NOT NULL,
    [Active]         INT             NOT NULL,
    [DateModified]   DATETIME        NOT NULL,
    [UserId]         INT             NOT NULL,
    [ModifiedUserId] INT             NOT NULL,
    [DirectoryId]    INT             CONSTRAINT [DF_Articles_DirectoryId] DEFAULT ((-1)) NOT NULL,
    [Tags]           NVARCHAR (250)  COLLATE Latin1_General_CI_AI CONSTRAINT [DF_Articles_Tags] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED ([Id] ASC)
);

