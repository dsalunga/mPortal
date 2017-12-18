CREATE TABLE [dbo].[BasicList] (
    [BasicListID]      INT              IDENTITY (1, 1) NOT NULL,
    [DateCreated]      DATETIME         NULL,
    [PageType]         INT              NULL,
    [SitePageItemID]   INT              NULL,
    [RepeatColumns]    INT              NULL,
    [ShowField2]       BIT              NULL,
    [ShowField3]       BIT              NULL,
    [CellPadding]      INT              NULL,
    [ItemTemplate]     NVARCHAR (256)   COLLATE Latin1_General_CI_AI NULL,
    [PageSize]         INT              NULL,
    [GridLines]        INT              NULL,
    [AlternatingColor] NVARCHAR (64)    COLLATE Latin1_General_CI_AI NULL,
    [TextColor]        NVARCHAR (64)    COLLATE Latin1_General_CI_AI NULL,
    [UserId]           UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_BasicLists] PRIMARY KEY CLUSTERED ([BasicListID] ASC)
);

