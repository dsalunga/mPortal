CREATE TABLE [dbo].[BibleFact] (
    [category]    INT            NULL,
    [subcategory] INT            NULL,
    [date_post]   DATETIME       NULL,
    [title]       NVARCHAR (500) NULL,
    [content]     NTEXT          NULL,
    [imgfname]    NVARCHAR (500) NULL,
    [username]    NVARCHAR (500) NULL,
    [id]          INT            NULL
);

