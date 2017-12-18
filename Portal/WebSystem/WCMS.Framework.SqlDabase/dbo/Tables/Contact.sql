CREATE TABLE [dbo].[Contact] (
    [ContactId] INT             NOT NULL,
    [Name]      NVARCHAR (256)  NULL,
    [Email]     NVARCHAR (256)  NULL,
    [Details]   NVARCHAR (1500) NULL,
    [Rank]      INT             NULL,
    [IsActive]  INT             NULL,
    [Subject]   NVARCHAR (256)  NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([ContactId] ASC)
);

