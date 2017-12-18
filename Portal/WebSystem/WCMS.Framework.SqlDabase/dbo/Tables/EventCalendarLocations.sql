CREATE TABLE [dbo].[EventCalendarLocations] (
    [LocationId] INT            NOT NULL,
    [Name]       NVARCHAR (500) NOT NULL,
    [Bookable]   INT            CONSTRAINT [DF_EventCalendarLocations_Bookable] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_EventCalendarLocations] PRIMARY KEY CLUSTERED ([LocationId] ASC)
);

