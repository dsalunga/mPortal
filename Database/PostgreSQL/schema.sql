-- mPortal CMS — PostgreSQL Schema DDL
-- Auto-generated from SQL Server table definitions
-- Tables: 121
--
-- Data type mapping:
--   NVARCHAR(n)      → VARCHAR(n)
--   NVARCHAR(MAX)    → TEXT
--   NTEXT            → TEXT
--   DATETIME         → TIMESTAMP
--   BIT              → BOOLEAN
--   FLOAT(53)        → DOUBLE PRECISION
--   UNIQUEIDENTIFIER → UUID
--   IDENTITY(1,1)    → SERIAL / BIGSERIAL
--   getdate()        → NOW()
--   newid()          → gen_random_uuid()
--
-- Usage:
--   psql -h localhost -U postgres -d mPortal -f schema.sql
--

-- Table: ArticleColumn
CREATE TABLE IF NOT EXISTS "ArticleColumn" (
    "ColumnId" INTEGER NOT NULL,
    "Name" VARCHAR(500) NOT NULL,
    "TemplateId" INTEGER NOT NULL,
    "Id" INTEGER NOT NULL,
    "IsSingle" INTEGER NOT NULL,
    PRIMARY KEY ("ColumnId")
);

-- Table: ArticleLink
CREATE TABLE IF NOT EXISTS "ArticleLink" (
    "Id" INTEGER NOT NULL,
    "Rank" INTEGER NOT NULL,
    "Style" VARCHAR(2500) NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "ArticleId" INTEGER NOT NULL,
    "SiteId" INTEGER NOT NULL,
    "CommentOn" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: ArticleList
CREATE TABLE IF NOT EXISTS "ArticleList" (
    "ListId" INTEGER NOT NULL,
    "PageSize" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "TemplateId" INTEGER NOT NULL,
    "SiteId" INTEGER NOT NULL,
    "FolderId" INTEGER DEFAULT -1 NOT NULL,
    "CommentOn" INTEGER NOT NULL,
    PRIMARY KEY ("ListId")
);

-- Table: ArticleTemplate
CREATE TABLE IF NOT EXISTS "ArticleTemplate" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "Date" TIMESTAMP NOT NULL,
    "File" VARCHAR(250) NOT NULL,
    "ImageUrl" VARCHAR(250) NOT NULL,
    "ListItemTemplate" VARCHAR(2500) DEFAULT '' NOT NULL,
    "ListTemplate" VARCHAR(2500) DEFAULT '' NOT NULL,
    "DetailsTemplate" VARCHAR(2500) DEFAULT '' NOT NULL,
    "DateFormat" VARCHAR(500) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: Articles
CREATE TABLE IF NOT EXISTS "Articles" (
    "Id" INTEGER NOT NULL,
    "Title" VARCHAR(255) NOT NULL,
    "Image" VARCHAR(255) NOT NULL,
    "Description" VARCHAR(1024) NOT NULL,
    "Date" TIMESTAMP NOT NULL,
    "Content" TEXT NOT NULL,
    "Author" VARCHAR(255) NOT NULL,
    "SiteId" INTEGER NOT NULL,
    "Active" INTEGER NOT NULL,
    "DateModified" TIMESTAMP NOT NULL,
    "UserId" INTEGER NOT NULL,
    "ModifiedUserId" INTEGER NOT NULL,
    "DirectoryId" INTEGER DEFAULT -1 NOT NULL,
    "Tags" VARCHAR(250) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BasicList
CREATE TABLE IF NOT EXISTS "BasicList" (
    "BasicListID" SERIAL NOT NULL,
    "DateCreated" TIMESTAMP,
    "PageType" INTEGER,
    "SitePageItemID" INTEGER,
    "RepeatColumns" INTEGER,
    "ShowField2" BOOLEAN,
    "ShowField3" BOOLEAN,
    "CellPadding" INTEGER,
    "ItemTemplate" VARCHAR(256),
    "PageSize" INTEGER,
    "GridLines" INTEGER,
    "AlternatingColor" VARCHAR(64),
    "TextColor" VARCHAR(64),
    "UserId" UUID,
    PRIMARY KEY ("BasicListID")
);

-- Table: BasicListItem
CREATE TABLE IF NOT EXISTS "BasicListItem" (
    "ListItemID" SERIAL NOT NULL,
    "PageType" INTEGER,
    "SitePageItemID" INTEGER,
    "Field1" VARCHAR(255),
    "Field2" VARCHAR(255),
    "Field3" VARCHAR(255),
    "Rank" INTEGER,
    PRIMARY KEY ("ListItemID")
);

-- Table: CalendarEventField
CREATE TABLE IF NOT EXISTS "CalendarEventField" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "FieldString" VARCHAR(250) NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: CalendarTemplateField
CREATE TABLE IF NOT EXISTS "CalendarTemplateField" (
    "Id" INTEGER NOT NULL,
    "FieldId" INTEGER NOT NULL,
    "TemplateId" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: Contact
CREATE TABLE IF NOT EXISTS "Contact" (
    "ContactId" INTEGER NOT NULL,
    "Name" VARCHAR(256),
    "Email" VARCHAR(256),
    "Details" VARCHAR(1500),
    "Rank" INTEGER,
    "IsActive" INTEGER,
    "Subject" VARCHAR(256),
    PRIMARY KEY ("ContactId")
);

-- Table: ContactInquiry
CREATE TABLE IF NOT EXISTS "ContactInquiry" (
    "InquiryId" INTEGER NOT NULL,
    "SenderName" VARCHAR(256) NOT NULL,
    "Subject" VARCHAR(256) NOT NULL,
    "Message" TEXT NOT NULL,
    "Email" VARCHAR(256) NOT NULL,
    "Address1" VARCHAR(256) NOT NULL,
    "Address2" VARCHAR(256),
    "City" VARCHAR(256),
    "CountryCode" INTEGER,
    "StateCode" INTEGER,
    "ZipCode" VARCHAR(63),
    "Phone" VARCHAR(256),
    "Fax" VARCHAR(256),
    "SendTo" VARCHAR(256),
    "InqDateTime" TIMESTAMP,
    "IsActive" INTEGER,
    "SendToEmail" VARCHAR(256),
    "InquiryType" VARCHAR(256),
    "RecordId" INTEGER,
    "ObjectId" INTEGER,
    "UserId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("InquiryId")
);

-- Table: ContactLink
CREATE TABLE IF NOT EXISTS "ContactLink" (
    "Id" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "ContactId" INTEGER NOT NULL,
    "Mode" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: Country
CREATE TABLE IF NOT EXISTS "Country" (
    "CountryCode" INTEGER NOT NULL,
    "CountryName" VARCHAR(256) DEFAULT '' NOT NULL,
    "RegionCode" INTEGER DEFAULT -1 NOT NULL,
    "Description" VARCHAR(500) DEFAULT '' NOT NULL,
    "ISOCode" VARCHAR(50) DEFAULT '' NOT NULL,
    "DialingCode" INTEGER DEFAULT -1 NOT NULL,
    "MaxPhoneDigit" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("CountryCode")
);

-- Table: CountryState
CREATE TABLE IF NOT EXISTS "CountryState" (
    "StateCode" INTEGER NOT NULL,
    "StateName" VARCHAR(256) DEFAULT '' NOT NULL,
    "ZipCode" VARCHAR(64) DEFAULT '' NOT NULL,
    "CountryCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("StateCode")
);

-- Table: Download
CREATE TABLE IF NOT EXISTS "Download" (
    "DownloadID" SERIAL NOT NULL,
    "Name" VARCHAR(255),
    "Description" TEXT,
    "FileDate" TIMESTAMP,
    "Filename" VARCHAR(255),
    "DateModified" TIMESTAMP,
    "Rank" INTEGER,
    "UserId" UUID,
    PRIMARY KEY ("DownloadID")
);

-- Table: DownloadLocation
CREATE TABLE IF NOT EXISTS "DownloadLocation" (
    "DownloadLocationID" SERIAL NOT NULL,
    "SiteID" INTEGER,
    "PageType" INTEGER,
    "SitePageItemID" INTEGER,
    "DownloadID" INTEGER,
    PRIMARY KEY ("DownloadLocationID")
);

-- Table: DownloadProperty
CREATE TABLE IF NOT EXISTS "DownloadProperty" (
    "DownloadPropertyID" SERIAL NOT NULL,
    "PageType" INTEGER,
    "SitePageItemID" INTEGER,
    "InitialControl" VARCHAR(255),
    "Columns" INTEGER,
    "Rows" INTEGER,
    "MaxRecords" INTEGER,
    "ForceDownload" BOOLEAN,
    PRIMARY KEY ("DownloadPropertyID")
);

-- Table: EventCalendar
CREATE TABLE IF NOT EXISTS "EventCalendar" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "SiteId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: EventCalendarCategories
CREATE TABLE IF NOT EXISTS "EventCalendarCategories" (
    "CategoryId" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "TemplateId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("CategoryId")
);

-- Table: EventCalendarEvents
CREATE TABLE IF NOT EXISTS "EventCalendarEvents" (
    "EventId" INTEGER NOT NULL,
    "Subject" VARCHAR(500) NOT NULL,
    "Message" VARCHAR(2500) NOT NULL,
    "Location" VARCHAR(250) NOT NULL,
    "StartDate" TIMESTAMP NOT NULL,
    "EndDate" TIMESTAMP NOT NULL,
    "CategoryId" INTEGER DEFAULT -1 NOT NULL,
    "RepeatUntil" TIMESTAMP NOT NULL,
    "ReminderTo" VARCHAR(4000) NOT NULL,
    "ReminderBefore" INTEGER DEFAULT -1 NOT NULL,
    "RecurrenceId" INTEGER NOT NULL,
    "LocationId" INTEGER DEFAULT -1 NOT NULL,
    "Weekdays" INTEGER DEFAULT -1 NOT NULL,
    "LastReminderSent" TIMESTAMP NOT NULL,
    "BookLocation" INTEGER DEFAULT 0 NOT NULL,
    "CalendarId" INTEGER DEFAULT -1 NOT NULL,
    "TemplateId" INTEGER DEFAULT -1 NOT NULL,
    "SendReminderVia" INTEGER DEFAULT 2 NOT NULL,
    PRIMARY KEY ("EventId")
);

-- Table: EventCalendarLocations
CREATE TABLE IF NOT EXISTS "EventCalendarLocations" (
    "LocationId" INTEGER NOT NULL,
    "Name" VARCHAR(500) NOT NULL,
    "Bookable" INTEGER DEFAULT 1 NOT NULL,
    PRIMARY KEY ("LocationId")
);

-- Table: EventCalendarRecurrences
CREATE TABLE IF NOT EXISTS "EventCalendarRecurrences" (
    "RecurrenceId" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "Rank" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("RecurrenceId")
);

-- Table: EventCalendarTemplates
CREATE TABLE IF NOT EXISTS "EventCalendarTemplates" (
    "TemplateId" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "ReminderHtml" TEXT NOT NULL,
    "ForeColor" VARCHAR(10) NOT NULL,
    "BackColor" VARCHAR(10) NOT NULL,
    "SmsContent" VARCHAR(1000) DEFAULT '' NOT NULL,
    PRIMARY KEY ("TemplateId")
);

-- Table: EventLog
CREATE TABLE IF NOT EXISTS "EventLog" (
    "Id" INTEGER NOT NULL,
    "EventDate" TIMESTAMP DEFAULT NOW() NOT NULL,
    "Content" TEXT DEFAULT '' NOT NULL,
    "UserId" INTEGER DEFAULT -1 NOT NULL,
    "EventName" VARCHAR(250) DEFAULT '' NOT NULL,
    "IPAddress" VARCHAR(50) NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: FileIdentity
CREATE TABLE IF NOT EXISTS "FileIdentity" (
    "Id" INTEGER NOT NULL,
    "ObjectId" INTEGER DEFAULT -1 NOT NULL,
    "RecordId" INTEGER DEFAULT -1 NOT NULL,
    "LibraryId" INTEGER DEFAULT -1 NOT NULL,
    "FilePath" VARCHAR(4000) DEFAULT '' NOT NULL,
    "Name" VARCHAR(500) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: FileVersion
CREATE TABLE IF NOT EXISTS "FileVersion" (
    "Id" INTEGER NOT NULL,
    "FileId" INTEGER NOT NULL,
    "VersionDate" TIMESTAMP DEFAULT NOW() NOT NULL,
    "Activity" INTEGER DEFAULT 0 NOT NULL,
    "UserId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: Gallery
CREATE TABLE IF NOT EXISTS "Gallery" (
    "GalleryID" SERIAL NOT NULL,
    "Caption" VARCHAR(256),
    "Thumbnail" VARCHAR(256),
    "ImageURL" VARCHAR(256),
    "DateCreated" TIMESTAMP,
    "SiteID" INTEGER,
    "CategoryID" INTEGER,
    "IsActive" BOOLEAN,
    PRIMARY KEY ("GalleryID")
);

-- Table: GalleryCategory
CREATE TABLE IF NOT EXISTS "GalleryCategory" (
    "CategoryID" SERIAL NOT NULL,
    "Title" VARCHAR(256),
    "ImageURL" VARCHAR(256),
    "Width" INTEGER DEFAULT -1 NOT NULL,
    "PhotoHeight" INTEGER DEFAULT 75 NOT NULL,
    "FolderName" VARCHAR(250) DEFAULT '' NOT NULL,
    "PhotoWidth" INTEGER DEFAULT 112 NOT NULL,
    "DateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    PRIMARY KEY ("CategoryID")
);

-- Table: GalleryCategoryLink
CREATE TABLE IF NOT EXISTS "GalleryCategoryLink" (
    "Id" SERIAL NOT NULL,
    "SiteId" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "CategoryId" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: GalleryLink
CREATE TABLE IF NOT EXISTS "GalleryLink" (
    "Id" SERIAL NOT NULL,
    "SiteId" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "GalleryId" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: GalleryObject
CREATE TABLE IF NOT EXISTS "GalleryObject" (
    "Id" SERIAL NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "InitialControl" VARCHAR(256) NOT NULL,
    "ThumbColumns" INTEGER NOT NULL,
    "ThumbRows" INTEGER NOT NULL,
    "AlbumColumns" INTEGER NOT NULL,
    "AlbumCellPadding" INTEGER NOT NULL,
    "MaxPhotoWidth" INTEGER DEFAULT 700 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: GenericList
CREATE TABLE IF NOT EXISTS "GenericList" (
    "Id" SERIAL NOT NULL,
    "Title" VARCHAR(255) NOT NULL,
    "Description" TEXT,
    "IsActive" INTEGER NOT NULL,
    "EndingText" VARCHAR(2000),
    "ShowPageCaption" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: GenericListColumn
CREATE TABLE IF NOT EXISTS "GenericListColumn" (
    "Id" SERIAL NOT NULL,
    "ListId" INTEGER NOT NULL,
    "PartitionId" INTEGER NOT NULL,
    "Rank" INTEGER NOT NULL,
    "Label" VARCHAR(2500) NOT NULL,
    "IsHorizontal" INTEGER NOT NULL,
    "IsRequired" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: GenericListColumnOption
CREATE TABLE IF NOT EXISTS "GenericListColumnOption" (
    "Id" SERIAL NOT NULL,
    "ColumnId" INTEGER NOT NULL,
    "OptionTypeId" INTEGER NOT NULL,
    "Rank" INTEGER NOT NULL,
    "Caption" VARCHAR(2000),
    "DefaultValue" INTEGER,
    PRIMARY KEY ("Id")
);

-- Table: GenericListColumnOptionType
CREATE TABLE IF NOT EXISTS "GenericListColumnOptionType" (
    "Id" INTEGER NOT NULL,
    "Label" VARCHAR(255) NOT NULL,
    "Template" TEXT,
    PRIMARY KEY ("Id")
);

-- Table: GenericListField
CREATE TABLE IF NOT EXISTS "GenericListField" (
    "Id" SERIAL NOT NULL,
    "RowId" INTEGER NOT NULL,
    "ColumnId" INTEGER NOT NULL,
    "Answer" TEXT,
    PRIMARY KEY ("Id")
);

-- Table: GenericListLink
CREATE TABLE IF NOT EXISTS "GenericListLink" (
    "Id" SERIAL NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "ListId" INTEGER NOT NULL,
    "SiteId" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: GenericListPartition
CREATE TABLE IF NOT EXISTS "GenericListPartition" (
    "Id" SERIAL NOT NULL,
    "ListId" INTEGER NOT NULL,
    "Rank" INTEGER NOT NULL,
    "Title" VARCHAR(256),
    "Description" VARCHAR(2000),
    "ActionOptionId" INTEGER,
    "ActionPartitionId" INTEGER,
    "ActionOptionValue" VARCHAR(50),
    PRIMARY KEY ("Id")
);

-- Table: GenericListRow
CREATE TABLE IF NOT EXISTS "GenericListRow" (
    "Id" SERIAL NOT NULL,
    "ListId" INTEGER NOT NULL,
    "IsCompleted" INTEGER NOT NULL,
    "DateTimeTaken" TIMESTAMP NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: IncidentCategory
CREATE TABLE IF NOT EXISTS "IncidentCategory" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(500) NOT NULL,
    "GroupId" INTEGER NOT NULL,
    "Description" VARCHAR(4000) NOT NULL,
    "Rank" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: IncidentTicket
CREATE TABLE IF NOT EXISTS "IncidentTicket" (
    "Id" INTEGER NOT NULL,
    "UserId" INTEGER NOT NULL,
    "DateCreated" TIMESTAMP NOT NULL,
    "CategoryId" INTEGER NOT NULL,
    "Description" TEXT NOT NULL,
    "Urgency" INTEGER NOT NULL,
    "Status" INTEGER NOT NULL,
    "AssignedGroupId" INTEGER DEFAULT -1 NOT NULL,
    "AssignedUserId" INTEGER DEFAULT -1 NOT NULL,
    "TicketGuid" VARCHAR(50) DEFAULT '' NOT NULL,
    "DateClosed" TIMESTAMP NOT NULL,
    "SubmitterId" INTEGER DEFAULT -1 NOT NULL,
    "ETA" TIMESTAMP DEFAULT '1800-01-01' NOT NULL,
    "TypeId" INTEGER DEFAULT -1 NOT NULL,
    "NotifyAlso" TEXT DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: IncidentTicketHistory
CREATE TABLE IF NOT EXISTS "IncidentTicketHistory" (
    "Id" INTEGER NOT NULL,
    "TicketId" INTEGER NOT NULL,
    "UserId" INTEGER NOT NULL,
    "Content" VARCHAR(4000) NOT NULL,
    "DateCreated" TIMESTAMP NOT NULL,
    "Type" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: IncidentType
CREATE TABLE IF NOT EXISTS "IncidentType" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(500) NOT NULL,
    "FollowStdSLA" INTEGER DEFAULT 1 NOT NULL,
    "Rank" INTEGER DEFAULT 1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: Job
CREATE TABLE IF NOT EXISTS "Job" (
    "Id" INTEGER NOT NULL,
    "Title" VARCHAR(2000) NOT NULL,
    "Description" TEXT NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: MChapter
CREATE TABLE IF NOT EXISTS "MChapter" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(500) DEFAULT '' NOT NULL,
    "ParentId" INTEGER DEFAULT -1 NOT NULL,
    "Address" VARCHAR(1500) DEFAULT '' NOT NULL,
    "ChapterType" INTEGER DEFAULT 0 NOT NULL,
    "Latitude" DOUBLE PRECISION DEFAULT 0 NOT NULL,
    "Longitude" DOUBLE PRECISION DEFAULT 0 NOT NULL,
    "AccessType" INTEGER DEFAULT 0 NOT NULL,
    "CountryCode" INTEGER DEFAULT 0 NOT NULL,
    "Email" VARCHAR(500) DEFAULT '' NOT NULL,
    "Mobile" VARCHAR(500) DEFAULT '' NOT NULL,
    "Telephone" VARCHAR(500) DEFAULT '' NOT NULL,
    "ServiceSchedule" VARCHAR(1500) DEFAULT '' NOT NULL,
    "MoreInfo" TEXT DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: Menu
CREATE TABLE IF NOT EXISTS "Menu" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(256) NOT NULL,
    "IsActive" INTEGER NOT NULL,
    "DateCreated" TIMESTAMP NOT NULL,
    "SiteId" INTEGER NOT NULL,
    "UserId" INTEGER NOT NULL,
    "PageId" INTEGER DEFAULT -2 NOT NULL,
    "IncludeChildren" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: MenuItem
CREATE TABLE IF NOT EXISTS "MenuItem" (
    "Id" INTEGER NOT NULL,
    "NavigateUrl" VARCHAR(256) NOT NULL,
    "Text" VARCHAR(256) NOT NULL,
    "Target" VARCHAR(256) NOT NULL,
    "ParentId" INTEGER DEFAULT -1 NOT NULL,
    "MenuId" INTEGER NOT NULL,
    "IsActive" INTEGER NOT NULL,
    "Rank" INTEGER NOT NULL,
    "PageId" INTEGER DEFAULT -1 NOT NULL,
    "Type" INTEGER DEFAULT 1 NOT NULL,
    "CheckPermission" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: MenuObject
CREATE TABLE IF NOT EXISTS "MenuObject" (
    "Id" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "Width" VARCHAR(63) NOT NULL,
    "Height" VARCHAR(63) NOT NULL,
    "Horizontal" INTEGER NOT NULL,
    "MenuId" INTEGER NOT NULL,
    "ParameterSetId" INTEGER DEFAULT -1 NOT NULL,
    "RenderMode" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: Music
CREATE TABLE IF NOT EXISTS "Music" (
    "Id" SERIAL NOT NULL,
    "Title" VARCHAR(500) DEFAULT '' NOT NULL,
    "LanguageCode" VARCHAR(50) DEFAULT '' NOT NULL,
    "CountryCode" VARCHAR(50) DEFAULT '' NOT NULL,
    "FolderName" VARCHAR(500) DEFAULT '' NOT NULL,
    "Tags" TEXT DEFAULT '' NOT NULL,
    "IsOriginal" INTEGER DEFAULT 1 NOT NULL,
    "ParentId" INTEGER DEFAULT -1 NOT NULL,
    "CategoryId" INTEGER DEFAULT -1 NOT NULL,
    "DateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    "Composer" VARCHAR(500) DEFAULT '' NOT NULL,
    "DateComposed" TIMESTAMP DEFAULT NOW() NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: MusicEntry
CREATE TABLE IF NOT EXISTS "MusicEntry" (
    "Id" SERIAL NOT NULL,
    "MusicId" INTEGER DEFAULT -1 NOT NULL,
    "EntryTypeId" INTEGER DEFAULT -1 NOT NULL,
    "FileName" VARCHAR(500) DEFAULT '' NOT NULL,
    "Tags" TEXT DEFAULT '' NOT NULL,
    "DateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    "FileSize" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: Newsletter
CREATE TABLE IF NOT EXISTS "Newsletter" (
    "Id" SERIAL NOT NULL,
    "Name" VARCHAR(500) NOT NULL,
    "Email" VARCHAR(50) NOT NULL,
    "IPAddress" VARCHAR(50) NOT NULL,
    "SubscribeDate" TIMESTAMP NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "SiteId" INTEGER NOT NULL,
    "Gender" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: ReminderTemplateResources
CREATE TABLE IF NOT EXISTS "ReminderTemplateResources" (
    "TemplateResourceId" INTEGER NOT NULL,
    "TemplateId" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "FileName" VARCHAR(250) NOT NULL,
    "Identity" VARCHAR(250) NOT NULL,
    PRIMARY KEY ("TemplateResourceId")
);

-- Table: ReminderTemplates
CREATE TABLE IF NOT EXISTS "ReminderTemplates" (
    "TemplateId" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "TemplateFile" VARCHAR(250) NOT NULL,
    PRIMARY KEY ("TemplateId")
);

-- Table: Reminders
CREATE TABLE IF NOT EXISTS "Reminders" (
    "ReminderId" INTEGER NOT NULL,
    "MemberId" INTEGER NOT NULL,
    "TemplateId" INTEGER NOT NULL,
    "PhotoFile" VARCHAR(250) NOT NULL,
    PRIMARY KEY ("ReminderId")
);

-- Table: RemoteDataMap
CREATE TABLE IF NOT EXISTS "RemoteDataMap" (
    "Id" INTEGER NOT NULL,
    "InstanceId" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "LocalId" INTEGER NOT NULL,
    "RemoteId" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: RemoteInstance
CREATE TABLE IF NOT EXISTS "RemoteInstance" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(500) NOT NULL,
    "BaseUrl" VARCHAR(500) NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: RemoteItem
CREATE TABLE IF NOT EXISTS "RemoteItem" (
    "Id" INTEGER NOT NULL,
    "LibraryId" INTEGER DEFAULT -1 NOT NULL,
    "Name" VARCHAR(300) DEFAULT '' NOT NULL,
    "RelativePath" VARCHAR(500) DEFAULT '' NOT NULL,
    "TypeId" INTEGER DEFAULT 0 NOT NULL,
    "DateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    "Size" INTEGER DEFAULT 0 NOT NULL,
    "Content" TEXT DEFAULT '' NOT NULL,
    "ParentId" INTEGER DEFAULT -1 NOT NULL,
    "DownloadCount" INTEGER DEFAULT 0 NOT NULL,
    "DisplayName" VARCHAR(500) DEFAULT '' NOT NULL,
    "IndexDateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    "FileCacheEnabled" INTEGER DEFAULT -1 NOT NULL,
    "Cached" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: RemoteLibrary
CREATE TABLE IF NOT EXISTS "RemoteLibrary" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(300) DEFAULT '' NOT NULL,
    "SourceTypeId" INTEGER DEFAULT 0 NOT NULL,
    "BaseAddress" VARCHAR(500) DEFAULT '' NOT NULL,
    "UserName" VARCHAR(50) DEFAULT '' NOT NULL,
    "Password" VARCHAR(50) DEFAULT '' NOT NULL,
    "LastIndexDate" TIMESTAMP DEFAULT NOW() NOT NULL,
    "Active" INTEGER DEFAULT 1 NOT NULL,
    "DisplayBaseAddress" VARCHAR(500) DEFAULT '' NOT NULL,
    "DownloadCountSince" TIMESTAMP DEFAULT NOW() NOT NULL,
    "FileCacheEnabled" INTEGER DEFAULT 0 NOT NULL,
    "FileCacheFolder" VARCHAR(500) DEFAULT '' NOT NULL,
    "FileCacheMinDownloadCount" INTEGER DEFAULT -1 NOT NULL,
    "FileCacheCeilingSize" INTEGER DEFAULT -1 NOT NULL,
    "FileCacheMaxSize" INTEGER DEFAULT -1 NOT NULL,
    "FileCacheMinDiskFreeMB" INTEGER DEFAULT -1 NOT NULL,
    "Size" BIGINT DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: SiteListProperty
CREATE TABLE IF NOT EXISTS "SiteListProperty" (
    "ListingPagePropertyID" SERIAL NOT NULL,
    "PageType" INTEGER,
    "SitePageItemID" INTEGER,
    "ParentID" INTEGER,
    "RepeatColumns" INTEGER,
    "HeaderText" VARCHAR(256),
    "CellPadding" INTEGER,
    "SortByName" BOOLEAN,
    PRIMARY KEY ("ListingPagePropertyID")
);

-- Table: Sportsfest
CREATE TABLE IF NOT EXISTS "Sportsfest" (
    "Id" INTEGER NOT NULL,
    "MemberId" INTEGER DEFAULT -1 NOT NULL,
    "Name" VARCHAR(100) NOT NULL,
    "GroupColor" VARCHAR(50) NOT NULL,
    "Age" INTEGER NOT NULL,
    "Mobile" VARCHAR(50) NOT NULL,
    "EntryDate" TIMESTAMP DEFAULT NOW() NOT NULL,
    "Sports" VARCHAR(50) NOT NULL,
    "Locale" VARCHAR(500) DEFAULT '' NOT NULL,
    "Suggestion" VARCHAR(2500) DEFAULT '' NOT NULL,
    "CountryCode" INTEGER DEFAULT -1 NOT NULL,
    "ShirtSize" VARCHAR(50) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: StdMenu
CREATE TABLE IF NOT EXISTS "StdMenu" (
    "StdMenuID" SERIAL NOT NULL,
    "SitePageItemID" INTEGER,
    "PageType" INTEGER,
    "Width" VARCHAR(64),
    "Height" VARCHAR(64),
    "Horizontal" BOOLEAN,
    "SiteID" INTEGER,
    "ShowHome" BOOLEAN,
    "SiteSectionID" INTEGER,
    "HomeText" VARCHAR(64),
    PRIMARY KEY ("StdMenuID")
);

-- Table: StdMenuProperties
CREATE TABLE IF NOT EXISTS "StdMenuProperties" (
    "ListingPagePropertyID" SERIAL NOT NULL,
    "PageType" INTEGER,
    "SitePageItemID" INTEGER,
    "RepeatColumns" INTEGER,
    "HeaderText" VARCHAR(256),
    "ParentID" INTEGER,
    "ListingType" VARCHAR(64),
    "CellPadding" INTEGER,
    PRIMARY KEY ("ListingPagePropertyID")
);

-- Table: UserProvider
CREATE TABLE IF NOT EXISTS "UserProvider" (
    "Id" INTEGER NOT NULL,
    "ProviderName" VARCHAR(500) NOT NULL,
    "Name" VARCHAR(500) NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WallPlugin
CREATE TABLE IF NOT EXISTS "WallPlugin" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(2000) NOT NULL,
    "EventTypeId" INTEGER NOT NULL,
    "FileName" VARCHAR(2000) NOT NULL,
    "TypeName" VARCHAR(2000) NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WallUpdate
CREATE TABLE IF NOT EXISTS "WallUpdate" (
    "Id" INTEGER NOT NULL,
    "UpdateRecordId" INTEGER NOT NULL,
    "UpdateObjectId" INTEGER NOT NULL,
    "ByRecordId" INTEGER NOT NULL,
    "ByObjectId" INTEGER NOT NULL,
    "Content" TEXT DEFAULT '' NOT NULL,
    "UpdateDate" TIMESTAMP DEFAULT NOW() NOT NULL,
    "EventTypeId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebAccessType
CREATE TABLE IF NOT EXISTS "WebAccessType" (
    "AccessTypeId" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    PRIMARY KEY ("AccessTypeId")
);

-- Table: WebAddress
CREATE TABLE IF NOT EXISTS "WebAddress" (
    "Id" INTEGER NOT NULL,
    "AddressLine1" VARCHAR(250) DEFAULT '' NOT NULL,
    "AddressLine2" VARCHAR(250) DEFAULT '' NOT NULL,
    "CityTown" VARCHAR(50) DEFAULT '' NOT NULL,
    "StateProvince" VARCHAR(50) DEFAULT '' NOT NULL,
    "StateProvinceCode" INTEGER DEFAULT -1 NOT NULL,
    "CountryCode" INTEGER DEFAULT -1 NOT NULL,
    "ZipCode" VARCHAR(50) DEFAULT '' NOT NULL,
    "PhoneNumber" VARCHAR(50) DEFAULT '' NOT NULL,
    "ObjectId" INTEGER DEFAULT -1 NOT NULL,
    "RecordId" INTEGER DEFAULT -1 NOT NULL,
    "Tag" VARCHAR(50) NOT NULL,
    "LastUpdated" TIMESTAMP DEFAULT NOW() NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebApplication
CREATE TABLE IF NOT EXISTS "WebApplication" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(500) NOT NULL,
    "AppKey" VARCHAR(500) DEFAULT '' NOT NULL,
    "IpAddresses" VARCHAR(50) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebAttachment
CREATE TABLE IF NOT EXISTS "WebAttachment" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(500) NOT NULL,
    "FilePath" VARCHAR(500) NOT NULL,
    "Size" BIGINT NOT NULL,
    "DateUploaded" TIMESTAMP NOT NULL,
    "UserId" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "BatchGuid" VARCHAR(50) NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebCategory
CREATE TABLE IF NOT EXISTS "WebCategory" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(500) DEFAULT -1 NOT NULL,
    "ObjectId" INTEGER DEFAULT -1 NOT NULL,
    "Rank" INTEGER DEFAULT 0 NOT NULL,
    "ParentId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebComment
CREATE TABLE IF NOT EXISTS "WebComment" (
    "Id" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "UserId" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "DateCreated" TIMESTAMP NOT NULL,
    "ParentId" INTEGER DEFAULT -1 NOT NULL,
    "UserName" VARCHAR(500) DEFAULT '' NOT NULL,
    "UserEmail" VARCHAR(500) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebConstant
CREATE TABLE IF NOT EXISTS "WebConstant" (
    "ConstantId" INTEGER NOT NULL,
    "Value" VARCHAR(256) NOT NULL,
    "Rank" INTEGER NOT NULL,
    "Category" VARCHAR(50) DEFAULT '' NOT NULL,
    "Text" VARCHAR(256) NOT NULL,
    "ObjectId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("ConstantId")
);

-- Table: WebContent
CREATE TABLE IF NOT EXISTS "WebContent" (
    "ContentId" INTEGER NOT NULL,
    "Title" VARCHAR(256) NOT NULL,
    "Content" TEXT NOT NULL,
    "VersionOf" INTEGER DEFAULT -1 NOT NULL,
    "VersionNo" INTEGER DEFAULT -1 NOT NULL,
    "DirectoryId" INTEGER DEFAULT -1 NOT NULL,
    "DateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    "ContentTypeId" INTEGER DEFAULT 4 NOT NULL,
    "Active" INTEGER DEFAULT 1 NOT NULL,
    "SiteId" INTEGER DEFAULT -1 NOT NULL,
    "EditorSensitive" INTEGER DEFAULT 0 NOT NULL,
    "ActiveContent" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("ContentId")
);

-- Table: WebFile
CREATE TABLE IF NOT EXISTS "WebFile" (
    "FileId" INTEGER NOT NULL,
    "FolderId" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    PRIMARY KEY ("FileId")
);

-- Table: WebFolder
CREATE TABLE IF NOT EXISTS "WebFolder" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "ParentId" INTEGER NOT NULL,
    "ShareName" VARCHAR(250) DEFAULT '' NOT NULL,
    "ObjectId" INTEGER DEFAULT -1 NOT NULL,
    "SiteId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebGlobalPolicy
CREATE TABLE IF NOT EXISTS "WebGlobalPolicy" (
    "GlobalPolicyId" INTEGER DEFAULT -1 NOT NULL,
    "Name" VARCHAR(250) DEFAULT '' NOT NULL,
    PRIMARY KEY ("GlobalPolicyId")
);

-- Table: WebGroup
CREATE TABLE IF NOT EXISTS "WebGroup" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "ParentId" INTEGER DEFAULT -1 NOT NULL,
    "IsSystem" INTEGER DEFAULT 0 NOT NULL,
    "DateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    "OwnerId" INTEGER DEFAULT -1 NOT NULL,
    "JoinApproval" INTEGER DEFAULT 0 NOT NULL,
    "JoinAlert" INTEGER DEFAULT 0 NOT NULL,
    "PageUrl" VARCHAR(250) DEFAULT '' NOT NULL,
    "PageId" INTEGER DEFAULT -1 NOT NULL,
    "Description" TEXT DEFAULT '' NOT NULL,
    "Managers" TEXT DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebJob
CREATE TABLE IF NOT EXISTS "WebJob" (
    "Id" INTEGER DEFAULT -1 NOT NULL,
    "Name" VARCHAR(250) DEFAULT '' NOT NULL,
    "RecurrenceId" INTEGER DEFAULT -1 NOT NULL,
    "Weekdays" INTEGER DEFAULT 0 NOT NULL,
    "OccursEvery" INTEGER DEFAULT 1 NOT NULL,
    "ExecutionStartDate" TIMESTAMP DEFAULT NOW() NOT NULL,
    "ExecutionEndDate" TIMESTAMP DEFAULT NOW() NOT NULL,
    "ExecutionStatus" INTEGER DEFAULT 0 NOT NULL,
    "ExecutionMessage" TEXT DEFAULT '' NOT NULL,
    "Enabled" INTEGER DEFAULT 1 NOT NULL,
    "TypeName" VARCHAR(250) DEFAULT '' NOT NULL,
    "StartDate" TIMESTAMP DEFAULT NOW() NOT NULL,
    "Description" VARCHAR(4000) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebMasterPage
CREATE TABLE IF NOT EXISTS "WebMasterPage" (
    "MasterPageId" INTEGER NOT NULL,
    "SiteId" INTEGER NOT NULL,
    "TemplateId" INTEGER NOT NULL,
    "Name" VARCHAR(256) NOT NULL,
    "OwnerPageId" INTEGER DEFAULT -1 NOT NULL,
    "PublicAccess" INTEGER DEFAULT 1 NOT NULL,
    "DateCreated" TIMESTAMP DEFAULT NOW() NOT NULL,
    "DateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    "ManagementAccess" INTEGER DEFAULT 0 NOT NULL,
    "ThemeId" INTEGER DEFAULT -1 NOT NULL,
    "SkinId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("MasterPageId")
);

-- Table: WebMessageQueue
CREATE TABLE IF NOT EXISTS "WebMessageQueue" (
    "Id" INTEGER NOT NULL,
    "FromObjectId" INTEGER DEFAULT -1 NOT NULL,
    "FromRecordId" INTEGER DEFAULT -1 NOT NULL,
    "EmailSubject" VARCHAR(4000) NOT NULL,
    "EmailMessage" TEXT NOT NULL,
    "SmsMessage" VARCHAR(4000) NOT NULL,
    "To" VARCHAR(4000) NOT NULL,
    "ToFailed" VARCHAR(4000) NOT NULL,
    "ToExcluded" VARCHAR(4000) NOT NULL,
    "ToOrBcc" INTEGER DEFAULT 0 NOT NULL,
    "DateCreated" TIMESTAMP DEFAULT NOW() NOT NULL,
    "DateSent" TIMESTAMP DEFAULT NOW() NOT NULL,
    "Status" INTEGER DEFAULT 0 NOT NULL,
    "SendVia" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebObject
CREATE TABLE IF NOT EXISTS "WebObject" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(50) NOT NULL,
    "IdentityColumn" VARCHAR(50) NOT NULL,
    "ObjectType" VARCHAR(50) DEFAULT 'T' NOT NULL,
    "Owner" VARCHAR(250) NOT NULL,
    "Prefix" VARCHAR(50) DEFAULT '' NOT NULL,
    "LastRecordId" INTEGER DEFAULT 0 NOT NULL,
    "MaxCacheCount" INTEGER DEFAULT -1 NOT NULL,
    "AccessTypeId" INTEGER DEFAULT -1 NOT NULL,
    "CacheTypeId" INTEGER DEFAULT -1 NOT NULL,
    "MaxHistoryCount" INTEGER DEFAULT -1 NOT NULL,
    "DataProviderName" VARCHAR(250) DEFAULT '' NOT NULL,
    "TypeName" VARCHAR(250) DEFAULT '' NOT NULL,
    "CacheInterval" INTEGER DEFAULT -1 NOT NULL,
    "DateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    "ManagerName" VARCHAR(250) DEFAULT '' NOT NULL,
    "NameColumn" VARCHAR(250) DEFAULT '' NOT NULL,
    "FriendlyName" VARCHAR(250) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebObjectColumn
CREATE TABLE IF NOT EXISTS "WebObjectColumn" (
    "Id" INTEGER DEFAULT -1 NOT NULL,
    "ObjectId" INTEGER DEFAULT -1 NOT NULL,
    "Name" VARCHAR(250) DEFAULT '' NOT NULL
);

-- Table: WebObjectContent
CREATE TABLE IF NOT EXISTS "WebObjectContent" (
    "ObjectContentId" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "ContentId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    PRIMARY KEY ("ObjectContentId")
);

-- Table: WebObjectHeader
CREATE TABLE IF NOT EXISTS "WebObjectHeader" (
    "ObjectHeaderId" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "TextResourceId" INTEGER NOT NULL,
    PRIMARY KEY ("ObjectHeaderId")
);

-- Table: WebObjectIPAddress
CREATE TABLE IF NOT EXISTS "WebObjectIPAddress" (
    "Id" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "IPAddress" VARCHAR(50) NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebObjectLink
CREATE TABLE IF NOT EXISTS "WebObjectLink" (
    "Id" INTEGER NOT NULL,
    "LeftRecordId" INTEGER NOT NULL,
    "LeftObjectId" INTEGER NOT NULL,
    "RightRecordId" INTEGER NOT NULL,
    "RightObjectId" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebObjectSecurity
CREATE TABLE IF NOT EXISTS "WebObjectSecurity" (
    "Id" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "SecurityObjectId" INTEGER NOT NULL,
    "SecurityRecordId" INTEGER NOT NULL,
    "Public" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebObjectSecurityPermission
CREATE TABLE IF NOT EXISTS "WebObjectSecurityPermission" (
    "Id" INTEGER NOT NULL,
    "ObjectSecurityId" INTEGER NOT NULL,
    "PermissionId" INTEGER NOT NULL,
    "Allow" INTEGER DEFAULT 1 NOT NULL,
    "Deny" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebObjectType
CREATE TABLE IF NOT EXISTS "WebObjectType" (
    "ObjectTypeId" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "SourceTypeId" INTEGER NOT NULL,
    "SourceLocationId" INTEGER NOT NULL,
    "IdField" VARCHAR(250) NOT NULL,
    "NameField" VARCHAR(250) NOT NULL,
    PRIMARY KEY ("ObjectTypeId")
);

-- Table: WebOffice
CREATE TABLE IF NOT EXISTS "WebOffice" (
    "OfficeId" INTEGER NOT NULL,
    "Name" VARCHAR(250) DEFAULT '' NOT NULL,
    "ParentId" INTEGER DEFAULT -1 NOT NULL,
    "AddressLine1" VARCHAR(250) DEFAULT '' NOT NULL,
    "MobileNumber" VARCHAR(50) DEFAULT '' NOT NULL,
    "PhoneNumber" VARCHAR(50) DEFAULT '' NOT NULL,
    "EmailAddress" VARCHAR(50) DEFAULT '' NOT NULL,
    "ContactPerson" VARCHAR(250) DEFAULT '' NOT NULL,
    PRIMARY KEY ("OfficeId")
);

-- Table: WebPage
CREATE TABLE IF NOT EXISTS "WebPage" (
    "PageId" INTEGER NOT NULL,
    "Name" VARCHAR(255) NOT NULL,
    "SiteId" INTEGER NOT NULL,
    "Rank" INTEGER NOT NULL,
    "Active" INTEGER NOT NULL,
    "Identity" VARCHAR(255) NOT NULL,
    "ParentId" INTEGER NOT NULL,
    "Title" VARCHAR(255) NOT NULL,
    "MasterPageId" INTEGER NOT NULL,
    "PartControlTemplateId" INTEGER DEFAULT -1 NOT NULL,
    "Published" INTEGER DEFAULT -1 NOT NULL,
    "VersionOfId" INTEGER DEFAULT -1 NOT NULL,
    "PublicAccess" INTEGER DEFAULT 128 NOT NULL,
    "DateCreated" TIMESTAMP DEFAULT NOW() NOT NULL,
    "DateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    "PageType" INTEGER DEFAULT 0 NOT NULL,
    "UsePartTemplatePath" INTEGER DEFAULT 1 NOT NULL,
    "ManagementAccess" INTEGER DEFAULT 0 NOT NULL,
    "ThemeId" INTEGER DEFAULT -1 NOT NULL,
    "LocaleId" INTEGER DEFAULT -1 NOT NULL,
    "SkinId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("PageId")
);

-- Table: WebPageElement
CREATE TABLE IF NOT EXISTS "WebPageElement" (
    "PageElementId" INTEGER NOT NULL,
    "ObjectId" INTEGER DEFAULT 2 NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "TemplatePanelId" INTEGER NOT NULL,
    "Rank" INTEGER NOT NULL,
    "PartControlTemplateId" INTEGER NOT NULL,
    "Active" INTEGER NOT NULL,
    "UsePartTemplatePath" INTEGER DEFAULT 1 NOT NULL,
    "PublicAccess" INTEGER DEFAULT 1 NOT NULL,
    "DateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    "ManagementAccess" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("PageElementId")
);

-- Table: WebPagePanel
CREATE TABLE IF NOT EXISTS "WebPagePanel" (
    "PagePanelId" INTEGER NOT NULL,
    "TemplatePanelId" INTEGER NOT NULL,
    "PageId" INTEGER DEFAULT -1 NOT NULL,
    "UsageTypeId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("PagePanelId")
);

-- Table: WebParameter
CREATE TABLE IF NOT EXISTS "WebParameter" (
    "Id" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "Value" TEXT NOT NULL,
    "IsRequired" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebParameterSet
CREATE TABLE IF NOT EXISTS "WebParameterSet" (
    "Id" INTEGER DEFAULT -1 NOT NULL,
    "Name" VARCHAR(250) DEFAULT '' NOT NULL,
    "SchemaParameterName" VARCHAR(250) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebPart
CREATE TABLE IF NOT EXISTS "WebPart" (
    "PartId" INTEGER NOT NULL,
    "Name" VARCHAR(255) NOT NULL,
    "Identity" VARCHAR(255) NOT NULL,
    "Active" INTEGER NOT NULL,
    PRIMARY KEY ("PartId")
);

-- Table: WebPartAdmin
CREATE TABLE IF NOT EXISTS "WebPartAdmin" (
    "PartAdminId" INTEGER NOT NULL,
    "PartId" INTEGER NOT NULL,
    "Name" VARCHAR(256) NOT NULL,
    "FileName" VARCHAR(256) NOT NULL,
    "ParentId" INTEGER NOT NULL,
    "Active" INTEGER DEFAULT 1 NOT NULL,
    "Visible" INTEGER DEFAULT 1 NOT NULL,
    "InSiteContext" INTEGER DEFAULT 0 NOT NULL,
    "TemplateEngineId" INTEGER DEFAULT 1 NOT NULL,
    PRIMARY KEY ("PartAdminId")
);

-- Table: WebPartConfig
CREATE TABLE IF NOT EXISTS "WebPartConfig" (
    "PartConfigId" INTEGER NOT NULL,
    "PartId" INTEGER NOT NULL,
    "Name" VARCHAR(256) NOT NULL,
    "FileName" VARCHAR(256) NOT NULL,
    PRIMARY KEY ("PartConfigId")
);

-- Table: WebPartControl
CREATE TABLE IF NOT EXISTS "WebPartControl" (
    "PartControlId" INTEGER NOT NULL,
    "PartId" INTEGER NOT NULL,
    "Name" VARCHAR(256) NOT NULL,
    "Identity" VARCHAR(256) NOT NULL,
    "ConfigFileName" VARCHAR(256) NOT NULL,
    "PartAdminId" INTEGER DEFAULT -1 NOT NULL,
    "EntryPoint" INTEGER DEFAULT 1 NOT NULL,
    "ParentId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("PartControlId")
);

-- Table: WebPartControlTemplate
CREATE TABLE IF NOT EXISTS "WebPartControlTemplate" (
    "PartControlTemplateId" INTEGER NOT NULL,
    "PartControlId" INTEGER NOT NULL,
    "Name" VARCHAR(256) NOT NULL,
    "FileName" VARCHAR(256) NOT NULL,
    "Identity" VARCHAR(256) NOT NULL,
    "Path" VARCHAR(250) DEFAULT '' NOT NULL,
    "Standalone" INTEGER DEFAULT 0 NOT NULL,
    "TemplateEngineId" INTEGER DEFAULT 1 NOT NULL,
    PRIMARY KEY ("PartControlTemplateId")
);

-- Table: WebPermission
CREATE TABLE IF NOT EXISTS "WebPermission" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "IsSystem" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebPermissionSet
CREATE TABLE IF NOT EXISTS "WebPermissionSet" (
    "Id" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER DEFAULT -1 NOT NULL,
    "PermissionId" INTEGER NOT NULL,
    "Public" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebRegistry
CREATE TABLE IF NOT EXISTS "WebRegistry" (
    "RegistryId" INTEGER NOT NULL,
    "Key" VARCHAR(255) NOT NULL,
    "Value" TEXT,
    "ParentId" INTEGER NOT NULL,
    "StageId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("RegistryId")
);

-- Table: WebRole
CREATE TABLE IF NOT EXISTS "WebRole" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "IsSystem" INTEGER DEFAULT 0 NOT NULL,
    "ParentId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebSecurityEntity
CREATE TABLE IF NOT EXISTS "WebSecurityEntity" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebShare
CREATE TABLE IF NOT EXISTS "WebShare" (
    "Id" INTEGER NOT NULL,
    "ObjectId" INTEGER DEFAULT -1 NOT NULL,
    "RecordId" INTEGER DEFAULT -1 NOT NULL,
    "ShareObjectId" INTEGER DEFAULT -1 NOT NULL,
    "ShareRecordId" INTEGER DEFAULT -1 NOT NULL,
    "Allow" INTEGER DEFAULT 1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebShortUrl
CREATE TABLE IF NOT EXISTS "WebShortUrl" (
    "Id" SERIAL NOT NULL,
    "Name" VARCHAR(500) DEFAULT '' NOT NULL,
    "PageId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebSite
CREATE TABLE IF NOT EXISTS "WebSite" (
    "SiteId" INTEGER NOT NULL,
    "Name" VARCHAR(256) NOT NULL,
    "Rank" INTEGER NOT NULL,
    "Active" INTEGER NOT NULL,
    "Identity" VARCHAR(64) NOT NULL,
    "Title" VARCHAR(256) NOT NULL,
    "ParentId" INTEGER NOT NULL,
    "HomePageId" INTEGER NOT NULL,
    "DefaultMasterPageId" INTEGER NOT NULL,
    "HostName" VARCHAR(256) NOT NULL,
    "Published" INTEGER DEFAULT -1 NOT NULL,
    "VersionOf" INTEGER DEFAULT -1 NOT NULL,
    "PublicAccess" INTEGER DEFAULT 128 NOT NULL,
    "DateCreated" TIMESTAMP DEFAULT NOW() NOT NULL,
    "DateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    "LoginPage" VARCHAR(250) DEFAULT '' NOT NULL,
    "AccessDeniedPage" VARCHAR(250) DEFAULT '' NOT NULL,
    "PageTitleFormat" VARCHAR(250) DEFAULT '' NOT NULL,
    "ManagementAccess" INTEGER DEFAULT 0 NOT NULL,
    "BaseAddress" VARCHAR(500) DEFAULT '' NOT NULL,
    "ThemeId" INTEGER DEFAULT -1 NOT NULL,
    "LocaleId" INTEGER DEFAULT -1 NOT NULL,
    "SkinId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("SiteId")
);

-- Table: WebSiteIdentity
CREATE TABLE IF NOT EXISTS "WebSiteIdentity" (
    "Id" INTEGER NOT NULL,
    "SiteId" INTEGER DEFAULT -1 NOT NULL,
    "HostName" VARCHAR(256) DEFAULT '' NOT NULL,
    "UrlPath" VARCHAR(256) DEFAULT '' NOT NULL,
    "Port" INTEGER DEFAULT 80 NOT NULL,
    "IPAddress" VARCHAR(50) DEFAULT '' NOT NULL,
    "RedirectUrl" VARCHAR(500) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebSkin
CREATE TABLE IF NOT EXISTS "WebSkin" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(500) DEFAULT '' NOT NULL,
    "Rank" INTEGER DEFAULT 0 NOT NULL,
    "ObjectId" INTEGER DEFAULT -1 NOT NULL,
    "RecordId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebSubscription
CREATE TABLE IF NOT EXISTS "WebSubscription" (
    "SubscriptionId" INTEGER NOT NULL,
    "ObjectId" INTEGER NOT NULL,
    "RecordId" INTEGER NOT NULL,
    "PartId" INTEGER DEFAULT -1 NOT NULL,
    "PageId" INTEGER DEFAULT -1 NOT NULL,
    "Allow" INTEGER DEFAULT 1 NOT NULL,
    PRIMARY KEY ("SubscriptionId")
);

-- Table: WebTab
CREATE TABLE IF NOT EXISTS "WebTab" (
    "Id" INTEGER NOT NULL,
    "Text" VARCHAR(250) DEFAULT '' NOT NULL,
    "Target" VARCHAR(250) DEFAULT '' NOT NULL,
    "Rank" INTEGER DEFAULT 0 NOT NULL,
    "TabSetId" INTEGER DEFAULT -1 NOT NULL,
    "Name" VARCHAR(250) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebTemplate
CREATE TABLE IF NOT EXISTS "WebTemplate" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(256) NOT NULL,
    "FileName" VARCHAR(256) NOT NULL,
    "Identity" VARCHAR(256) NOT NULL,
    "PrimaryPanelId" INTEGER DEFAULT -1 NOT NULL,
    "Version" INTEGER DEFAULT 1 NOT NULL,
    "VersionOf" INTEGER DEFAULT 1 NOT NULL,
    "Content" TEXT DEFAULT '' NOT NULL,
    "DateModified" TIMESTAMP NOT NULL,
    "ThemeId" INTEGER DEFAULT -1 NOT NULL,
    "Standalone" INTEGER DEFAULT 0 NOT NULL,
    "ParentId" INTEGER DEFAULT -1 NOT NULL,
    "SkinId" INTEGER DEFAULT -1 NOT NULL,
    "TemplateEngineId" INTEGER DEFAULT 1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebTemplatePanel
CREATE TABLE IF NOT EXISTS "WebTemplatePanel" (
    "TemplatePanelId" INTEGER NOT NULL,
    "Name" VARCHAR(256) NOT NULL,
    "TemplateId" INTEGER DEFAULT -1 NOT NULL,
    "PanelName" VARCHAR(256) NOT NULL,
    "Rank" INTEGER DEFAULT 0 NOT NULL,
    "ObjectId" INTEGER DEFAULT -1 NOT NULL,
    "RecordId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("TemplatePanelId")
);

-- Table: WebTextResource
CREATE TABLE IF NOT EXISTS "WebTextResource" (
    "TextResourceId" INTEGER NOT NULL,
    "ContentTypeId" INTEGER NOT NULL,
    "Title" VARCHAR(250) NOT NULL,
    "Content" TEXT NOT NULL,
    "DirectoryId" INTEGER DEFAULT -1 NOT NULL,
    "Rank" INTEGER DEFAULT 0 NOT NULL,
    "DateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    "OwnerObjectId" INTEGER DEFAULT -1 NOT NULL,
    "OwnerRecordId" INTEGER DEFAULT -1 NOT NULL,
    "DatePersisted" TIMESTAMP DEFAULT NOW() NOT NULL,
    "PhysicalPath" VARCHAR(500) DEFAULT '' NOT NULL,
    PRIMARY KEY ("TextResourceId")
);

-- Table: WebTheme
CREATE TABLE IF NOT EXISTS "WebTheme" (
    "Id" INTEGER NOT NULL,
    "TemplateId" INTEGER DEFAULT -1 NOT NULL,
    "Name" VARCHAR(500) DEFAULT '' NOT NULL,
    "ParentId" INTEGER DEFAULT -1 NOT NULL,
    "Identity" VARCHAR(500) DEFAULT '' NOT NULL,
    "SkinId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebThemePartTemplate
CREATE TABLE IF NOT EXISTS "WebThemePartTemplate" (
    "Id" INTEGER NOT NULL,
    "ThemeId" INTEGER NOT NULL,
    "PartTemplateId" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WebUser
CREATE TABLE IF NOT EXISTS "WebUser" (
    "UserId" INTEGER NOT NULL,
    "UserName" VARCHAR(50) NOT NULL,
    "Password" VARCHAR(1000) NOT NULL,
    "FirstName" VARCHAR(256) NOT NULL,
    "MiddleName" VARCHAR(250) DEFAULT '' NOT NULL,
    "LastName" VARCHAR(256) NOT NULL,
    "Email" VARCHAR(250) NOT NULL,
    "LastUpdate" TIMESTAMP DEFAULT NOW() NOT NULL,
    "Active" INTEGER DEFAULT 0 NOT NULL,
    "ActivationKey" VARCHAR(250) DEFAULT '' NOT NULL,
    "DateCreated" TIMESTAMP DEFAULT NOW() NOT NULL,
    "NewEmail" VARCHAR(250) DEFAULT '' NOT NULL,
    "Email2" VARCHAR(250) DEFAULT '' NOT NULL,
    "Gender" CHAR(1) DEFAULT 'U' NOT NULL,
    "NameSuffix" VARCHAR(50) DEFAULT '' NOT NULL,
    "MobileNumber" VARCHAR(50) DEFAULT '' NOT NULL,
    "TelephoneNumber" VARCHAR(50) DEFAULT '' NOT NULL,
    "LastLogin" TIMESTAMP DEFAULT NOW() NOT NULL,
    "StatusText" VARCHAR(1500) DEFAULT '' NOT NULL,
    "PasswordExpiryDate" TIMESTAMP DEFAULT '1800-01-01' NOT NULL,
    "PhotoPath" VARCHAR(500) DEFAULT '' NOT NULL,
    "ProviderId" INTEGER DEFAULT -1 NOT NULL,
    "Status" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("UserId")
);

-- Table: WebUserGroup
CREATE TABLE IF NOT EXISTS "WebUserGroup" (
    "Id" INTEGER NOT NULL,
    "UserId" INTEGER NOT NULL,
    "GroupId" INTEGER NOT NULL,
    "Active" INTEGER DEFAULT 1 NOT NULL,
    "DateJoined" TIMESTAMP DEFAULT NOW() NOT NULL,
    "ObjectId" INTEGER DEFAULT 21 NOT NULL,
    "RecordId" INTEGER DEFAULT -1 NOT NULL,
    "Remarks" TEXT DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WeeklySchedulerItem
CREATE TABLE IF NOT EXISTS "WeeklySchedulerItem" (
    "Id" INTEGER NOT NULL,
    "TaskId" INTEGER NOT NULL,
    "StartDateTime" TIMESTAMP NOT NULL,
    "UserId" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: WeeklySchedulerTask
CREATE TABLE IF NOT EXISTS "WeeklySchedulerTask" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    PRIMARY KEY ("Id")
);

