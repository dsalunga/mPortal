CREATE TABLE [dbo].[WeeklySchedulerItem] (
    [Id]            INT      NOT NULL,
    [TaskId]        INT      NOT NULL,
    [StartDateTime] DATETIME NOT NULL,
    [UserId]        INT      NOT NULL,
    CONSTRAINT [PK_WeeklySchedulerItem] PRIMARY KEY CLUSTERED ([Id] ASC)
);

