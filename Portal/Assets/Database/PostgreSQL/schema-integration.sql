-- mPortal CMS — Integration Database PostgreSQL Schema DDL
-- Auto-generated from SQL Server table definitions
-- Tables: 16
--
-- Usage:
--   psql -h localhost -U postgres -d mPortal -f schema-integration.sql
--

-- Table: BibleReaderAccess
CREATE TABLE IF NOT EXISTS "BibleReaderAccess" (
    "Id" INTEGER NOT NULL,
    "UserId" INTEGER NOT NULL,
    "AppAccessCount" INTEGER DEFAULT -1 NOT NULL,
    "LastAccessed" TIMESTAMP DEFAULT NOW() NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BibleReaderVersionAccess
CREATE TABLE IF NOT EXISTS "BibleReaderVersionAccess" (
    "Id" INTEGER NOT NULL,
    "BibleAccessId" INTEGER NOT NULL,
    "BibleVersionId" INTEGER DEFAULT -1 NOT NULL,
    "BibleVersionName" VARCHAR(250) DEFAULT '' NOT NULL,
    "LastAccessed" TIMESTAMP DEFAULT NOW() NOT NULL,
    "VersionAccessCount" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: MCCandidate
CREATE TABLE IF NOT EXISTS "MCCandidate" (
    "Id" SERIAL NOT NULL,
    "Name" VARCHAR(250) DEFAULT '' NOT NULL,
    "Entry" VARCHAR(2000) DEFAULT '' NOT NULL,
    "Lyrics" TEXT DEFAULT '' NOT NULL,
    "SourceUrl" VARCHAR(500) DEFAULT '' NOT NULL,
    "SourceUrl2" VARCHAR(500) DEFAULT '' NOT NULL,
    "Lyricist" VARCHAR(250) DEFAULT '' NOT NULL,
    "Interpreter" VARCHAR(250) DEFAULT '' NOT NULL,
    "PhotoFile" VARCHAR(500) DEFAULT '' NOT NULL,
    "CompetitionId" INTEGER DEFAULT -1 NOT NULL,
    "Rank" INTEGER DEFAULT 0 NOT NULL,
    "WinnerRank" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: MusicCompetition
CREATE TABLE IF NOT EXISTS "MusicCompetition" (
    "Id" SERIAL NOT NULL,
    "Name" VARCHAR(500) NOT NULL,
    "Judges" VARCHAR(1000) DEFAULT '' NOT NULL,
    "ScoreLocked" INTEGER DEFAULT 0 NOT NULL,
    "CompetitionDate" TIMESTAMP DEFAULT NOW() NOT NULL,
    "VoteLocked" INTEGER DEFAULT 0 NOT NULL,
    "VoteMasked" INTEGER DEFAULT 0 NOT NULL,
    "PeoplesChoiceId" INTEGER DEFAULT -1 NOT NULL,
    "BestInterpreterId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: MCComposer
CREATE TABLE IF NOT EXISTS "MCComposer" (
    "Id" INTEGER NOT NULL,
    "Name" VARCHAR(500) DEFAULT '' NOT NULL,
    "Entry" TEXT DEFAULT '' NOT NULL,
    "Locale" VARCHAR(500) DEFAULT '' NOT NULL,
    "Work" VARCHAR(500) DEFAULT '' NOT NULL,
    "Description" TEXT DEFAULT '' NOT NULL,
    "PhotoFile" VARCHAR(500) DEFAULT '' NOT NULL,
    "NickName" VARCHAR(500) DEFAULT '' NOT NULL,
    "Active" INTEGER DEFAULT 1 NOT NULL,
    "CompetitionId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: MCInterpreterScore
CREATE TABLE IF NOT EXISTS "MCInterpreterScore" (
    "Id" INTEGER NOT NULL,
    "JudgeId" INTEGER NOT NULL,
    "VoiceQuality" INTEGER NOT NULL,
    "Interpretation" INTEGER NOT NULL,
    "StagePresence" INTEGER NOT NULL,
    "OverallImpact" INTEGER NOT NULL,
    "DateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    "CandidateId" INTEGER DEFAULT -1 NOT NULL,
    "CompetitionId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: MCSongScore
CREATE TABLE IF NOT EXISTS "MCSongScore" (
    "Id" INTEGER NOT NULL,
    "JudgeId" INTEGER NOT NULL,
    "Musicality" INTEGER NOT NULL,
    "LyricsMessage" INTEGER NOT NULL,
    "OverallImpact" INTEGER NOT NULL,
    "DateModified" TIMESTAMP DEFAULT NOW() NOT NULL,
    "CandidateId" INTEGER DEFAULT -1 NOT NULL,
    "CompetitionId" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: MCVote
CREATE TABLE IF NOT EXISTS "MCVote" (
    "Id" SERIAL NOT NULL,
    "Code" VARCHAR(250) DEFAULT '' NOT NULL,
    "FirstName" VARCHAR(250) DEFAULT '' NOT NULL,
    "LastName" VARCHAR(250) DEFAULT '' NOT NULL,
    "MobileNumber" VARCHAR(250) DEFAULT '' NOT NULL,
    "Email" VARCHAR(250) DEFAULT '' NOT NULL,
    "CandidateId" INTEGER DEFAULT -1 NOT NULL,
    "DateVoted" TIMESTAMP NOT NULL,
    "UserName" VARCHAR(250) DEFAULT '' NOT NULL,
    "Status" INTEGER DEFAULT 0 NOT NULL,
    "CompetitionId" INTEGER DEFAULT -1 NOT NULL,
    "IPAddress" VARCHAR(50) DEFAULT '' NOT NULL,
    "Spam" INTEGER DEFAULT 0 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: LessonReviewerVideo
CREATE TABLE IF NOT EXISTS "LessonReviewerVideo" (
    "ServiceStartDate" TIMESTAMP NOT NULL,
    "ServiceScheduleId" INTEGER DEFAULT -1 NOT NULL,
    "Duration" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("ServiceScheduleId")
);

-- Table: Member
CREATE TABLE IF NOT EXISTS "Member" (
    "MemberID" BIGINT NOT NULL,
    "ExternalIDNo" VARCHAR(20),
    "TemporaryIDNo" VARCHAR(20),
    "FirstName" VARCHAR(30) NOT NULL,
    "MiddleName" VARCHAR(30) NOT NULL,
    "LastName" VARCHAR(30) NOT NULL,
    "BirthDate" TIMESTAMP,
    "BirthPlace" VARCHAR(200),
    "Gender" CHAR(1),
    "BloodType" VARCHAR(3),
    "CivilStatusID" INTEGER,
    "CitizenshipID" INTEGER,
    "RaceID" INTEGER,
    "Phone" VARCHAR(20),
    "Mobile" VARCHAR(20),
    "Email" VARCHAR(100),
    "IsActive" INTEGER,
    "Flag" CHAR(1) DEFAULT 'M' NOT NULL,
    "NickName" VARCHAR(250),
    "DateCreated" TIMESTAMP,
    "DateUpdated" TIMESTAMP,
    "MembershipDate" TIMESTAMP DEFAULT NOW() NOT NULL,
    PRIMARY KEY ("MemberID")
);

-- Table: MemberLink
CREATE TABLE IF NOT EXISTS "MemberLink" (
    "MemberLinkId" INTEGER NOT NULL,
    "UserId" INTEGER NOT NULL,
    "MemberId" INTEGER DEFAULT -1 NOT NULL,
    "ExternalIdNo" VARCHAR(50) DEFAULT '' NOT NULL,
    "HomeAddressLine1" VARCHAR(250) DEFAULT '' NOT NULL,
    "HomeAddressStateCode" INTEGER DEFAULT -1 NOT NULL,
    "HomeAddressCountryCode" INTEGER DEFAULT -1 NOT NULL,
    "HomeAddressZipCode" VARCHAR(50) DEFAULT '' NOT NULL,
    "MobileNumber" VARCHAR(50) DEFAULT '' NOT NULL,
    "HomePhone" VARCHAR(50) DEFAULT '' NOT NULL,
    "WorkAddressLine1" VARCHAR(250) DEFAULT '' NOT NULL,
    "WorkAddressStateCode" INTEGER DEFAULT -1 NOT NULL,
    "WorkAddressCountryCode" INTEGER DEFAULT -1 NOT NULL,
    "WorkAddressZipCode" VARCHAR(50) DEFAULT '' NOT NULL,
    "WorkDesignation" VARCHAR(250) DEFAULT '' NOT NULL,
    "WorkPhone" VARCHAR(50) DEFAULT '' NOT NULL,
    "Nickname" VARCHAR(50) DEFAULT '' NOT NULL,
    "LastUpdate" TIMESTAMP DEFAULT NOW() NOT NULL,
    "PhotoPath" VARCHAR(500) DEFAULT '' NOT NULL,
    "MembershipDate" TIMESTAMP DEFAULT NOW() NOT NULL,
    "Approved" INTEGER DEFAULT 0 NOT NULL,
    "Locale" VARCHAR(2000) DEFAULT '' NOT NULL,
    "HomeAddressLine2" VARCHAR(250) DEFAULT '' NOT NULL,
    "WorkAddressLine2" VARCHAR(250) DEFAULT '' NOT NULL,
    "Private" INTEGER DEFAULT 0 NOT NULL,
    "AdditionalInfo" VARCHAR(4000) DEFAULT '' NOT NULL,
    PRIMARY KEY ("MemberLinkId")
);

-- Table: MemberStatuses
CREATE TABLE IF NOT EXISTS "MemberStatuses" (
    "MemberStatusID" BIGSERIAL NOT NULL,
    "MemberID" BIGINT,
    "MemberTypeID" SMALLINT,
    "MembershipStatusID" SMALLINT,
    "LocaleStatusID" SMALLINT,
    "LocaleID" INTEGER,
    "GroupID" SMALLINT,
    "CommitteeID" SMALLINT,
    "MembershipDate" TIMESTAMP,
    "MembershipPlace" VARCHAR(200),
    "OrientedByID" BIGINT,
    "OnboardedByID" BIGINT,
    "PreviousOrganizationID" SMALLINT,
    "WithID" BOOLEAN,
    PRIMARY KEY ("MemberStatusID")
);

-- Table: Membership
CREATE TABLE IF NOT EXISTS "Membership" (
    "MembershipID" BIGSERIAL NOT NULL,
    "MemberID" BIGINT,
    "MemberTypeID" SMALLINT,
    "MembershipStatusID" SMALLINT,
    "LocaleStatusID" SMALLINT,
    "LocaleID" INTEGER,
    "GroupID" SMALLINT,
    "CommitteeID" SMALLINT,
    "MembershipDate" TIMESTAMP,
    "MembershipPlace" VARCHAR(200),
    "OrientedBy" VARCHAR(100),
    "OnboardedBy" VARCHAR(100),
    "PreviousOrganization" VARCHAR(200),
    PRIMARY KEY ("MembershipID")
);

-- Table: ODKVisit
CREATE TABLE IF NOT EXISTS "ODKVisit" (
    "Id" INTEGER DEFAULT -1 NOT NULL,
    "CreatedUserId" INTEGER DEFAULT -1 NOT NULL,
    "DateCreated" TIMESTAMP DEFAULT NOW() NOT NULL,
    "ActualReport" TEXT DEFAULT '' NOT NULL,
    "Status" TEXT DEFAULT '' NOT NULL,
    "GroupId" INTEGER DEFAULT -1 NOT NULL,
    "Name" VARCHAR(250) DEFAULT '' NOT NULL,
    "VisitedUserId" INTEGER DEFAULT -1 NOT NULL,
    "DateVisited" TIMESTAMP DEFAULT NOW() NOT NULL,
    "ActionTaken" TEXT DEFAULT '' NOT NULL,
    "ContactNo" VARCHAR(50) DEFAULT '' NOT NULL,
    "TimesVisited" INTEGER DEFAULT 0 NOT NULL,
    "Address" VARCHAR(250) DEFAULT '' NOT NULL,
    "MembershipDate" TIMESTAMP DEFAULT '1900-01-01' NOT NULL,
    "Tags" VARCHAR(1000) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: Registration
CREATE TABLE IF NOT EXISTS "Registration" (
    "Id" INTEGER NOT NULL,
    "EntryDate" TIMESTAMP DEFAULT NOW() NOT NULL,
    "Country" VARCHAR(250) DEFAULT '' NOT NULL,
    "Locale" VARCHAR(500) DEFAULT '' NOT NULL,
    "ExternalId" VARCHAR(50) DEFAULT '' NOT NULL,
    "Name" VARCHAR(100) DEFAULT '' NOT NULL,
    "Designation" VARCHAR(250) DEFAULT '' NOT NULL,
    "ArrivalDate" TIMESTAMP NOT NULL,
    "Airline" VARCHAR(250) DEFAULT '' NOT NULL,
    "FlightNo" VARCHAR(250) DEFAULT '' NOT NULL,
    "DepartureDate" TIMESTAMP NOT NULL,
    "Address" VARCHAR(2500) DEFAULT '' NOT NULL,
    "Age" INTEGER DEFAULT -1 NOT NULL,
    "Gender" VARCHAR(50) DEFAULT '' NOT NULL,
    "PlaceType" VARCHAR(250) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: LessonReviewerSession
CREATE TABLE IF NOT EXISTS "LessonReviewerSession" (
    "Id" INTEGER NOT NULL,
    "ServiceScheduleID" INTEGER DEFAULT -1 NOT NULL,
    "ServiceStartDate" TIMESTAMP DEFAULT NOW() NOT NULL,
    "ServiceName" VARCHAR(150) DEFAULT '' NOT NULL,
    "DateStarted" TIMESTAMP DEFAULT NOW() NOT NULL,
    "DateCompleted" TIMESTAMP DEFAULT NOW() NOT NULL,
    "MemberId" INTEGER DEFAULT -1 NOT NULL,
    "AbsentReason" VARCHAR(4000) DEFAULT '' NOT NULL,
    "CouncillorNotes" VARCHAR(4000) DEFAULT '' NOT NULL,
    "CouncillorUserId" INTEGER DEFAULT -1 NOT NULL,
    "Status" INTEGER DEFAULT 0 NOT NULL,
    "DateApproved" TIMESTAMP DEFAULT NOW() NOT NULL,
    "AdditionalNotes" VARCHAR(4000) DEFAULT '' NOT NULL,
    "AttendanceType" INTEGER DEFAULT 1 NOT NULL,
    "PageId" INTEGER DEFAULT -1 NOT NULL,
    "Extra" VARCHAR(4000) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Compatibility fixups for legacy schema variants
DO $$
BEGIN
    IF EXISTS (
        SELECT 1
        FROM information_schema.columns
        WHERE table_schema = 'public'
          AND table_name = 'LessonReviewerSession'
          AND column_name = 'WorkerNotes'
    ) AND NOT EXISTS (
        SELECT 1
        FROM information_schema.columns
        WHERE table_schema = 'public'
          AND table_name = 'LessonReviewerSession'
          AND column_name = 'CouncillorNotes'
    ) THEN
        ALTER TABLE "LessonReviewerSession" RENAME COLUMN "WorkerNotes" TO "CouncillorNotes";
    END IF;

    IF EXISTS (
        SELECT 1
        FROM information_schema.columns
        WHERE table_schema = 'public'
          AND table_name = 'LessonReviewerSession'
          AND column_name = 'WorkerUserId'
    ) AND NOT EXISTS (
        SELECT 1
        FROM information_schema.columns
        WHERE table_schema = 'public'
          AND table_name = 'LessonReviewerSession'
          AND column_name = 'CouncillorUserId'
    ) THEN
        ALTER TABLE "LessonReviewerSession" RENAME COLUMN "WorkerUserId" TO "CouncillorUserId";
    END IF;
END
$$;

ALTER TABLE "LessonReviewerSession" ADD COLUMN IF NOT EXISTS "AttendanceType" INTEGER DEFAULT 1 NOT NULL;
ALTER TABLE "LessonReviewerSession" ADD COLUMN IF NOT EXISTS "PageId" INTEGER DEFAULT -1 NOT NULL;
ALTER TABLE "LessonReviewerSession" ADD COLUMN IF NOT EXISTS "Extra" VARCHAR(4000) DEFAULT '' NOT NULL;
