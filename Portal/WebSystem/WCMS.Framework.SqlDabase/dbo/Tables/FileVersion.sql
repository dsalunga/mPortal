CREATE TABLE [dbo].[FileVersion] (
    [Id]          INT      NOT NULL,
    [FileId]      INT      NOT NULL,
    [VersionDate] DATETIME CONSTRAINT [DF_FileVersion_VersionDate] DEFAULT (getdate()) NOT NULL,
    [Activity]    INT      CONSTRAINT [DF_FileVersion_Activity] DEFAULT ((0)) NOT NULL,
    [UserId]      INT      CONSTRAINT [DF_FileVersion_UserId] DEFAULT ((-1)) NOT NULL,
    CONSTRAINT [PK_FileVersion] PRIMARY KEY CLUSTERED ([Id] ASC)
);

