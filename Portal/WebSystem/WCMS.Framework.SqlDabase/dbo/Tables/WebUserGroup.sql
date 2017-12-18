CREATE TABLE [dbo].[WebUserGroup] (
    [Id]         INT            NOT NULL,
    [UserId]     INT            NOT NULL,
    [GroupId]    INT            NOT NULL,
    [Active]     INT            CONSTRAINT [DF_WebUserGroup_Active] DEFAULT ((1)) NOT NULL,
    [DateJoined] DATETIME       CONSTRAINT [DF_WebUserGroup_DateJoined] DEFAULT (getdate()) NOT NULL,
    [ObjectId]   INT            CONSTRAINT [DF_WebUserGroup_ObjectId] DEFAULT ((21)) NOT NULL,
    [RecordId]   INT            CONSTRAINT [DF_WebUserGroup_RecordId] DEFAULT ((-1)) NOT NULL,
    [Remarks]    NVARCHAR (MAX) CONSTRAINT [DF_WebUserGroup_Remarks] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_WebUserRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);



