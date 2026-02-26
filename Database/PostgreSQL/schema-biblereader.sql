-- mPortal CMS — BibleReader Database PostgreSQL Schema DDL
-- Auto-generated from SQL Server table definitions
-- Tables: 60
--
-- Usage:
--   psql -h localhost -U postgres -d mPortal -f schema-biblereader.sql
--

-- Table: BIBLE_ABBTAG
CREATE TABLE IF NOT EXISTS "BIBLE_ABBTAG" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_ABTAG
CREATE TABLE IF NOT EXISTS "BIBLE_ABTAG" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_ADBBCL
CREATE TABLE IF NOT EXISTS "BIBLE_ADBBCL" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_ADBCEB
CREATE TABLE IF NOT EXISTS "BIBLE_ADBCEB" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_ADBILN
CREATE TABLE IF NOT EXISTS "BIBLE_ADBILN" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_ADBPAM
CREATE TABLE IF NOT EXISTS "BIBLE_ADBPAM" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_ADBSAM
CREATE TABLE IF NOT EXISTS "BIBLE_ADBSAM" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_ADBTAG
CREATE TABLE IF NOT EXISTS "BIBLE_ADBTAG" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_ADHIL
CREATE TABLE IF NOT EXISTS "BIBLE_ADHIL" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_ALT
CREATE TABLE IF NOT EXISTS "BIBLE_ALT" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_AOV
CREATE TABLE IF NOT EXISTS "BIBLE_AOV" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_ASV
CREATE TABLE IF NOT EXISTS "BIBLE_ASV" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_BBE
CREATE TABLE IF NOT EXISTS "BIBLE_BBE" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_BIBELN
CREATE TABLE IF NOT EXISTS "BIBLE_BIBELN" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_CEV
CREATE TABLE IF NOT EXISTS "BIBLE_CEV" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_DARBY
CREATE TABLE IF NOT EXISTS "BIBLE_DARBY" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_DRB
CREATE TABLE IF NOT EXISTS "BIBLE_DRB" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_DSV
CREATE TABLE IF NOT EXISTS "BIBLE_DSV" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_EMTV
CREATE TABLE IF NOT EXISTS "BIBLE_EMTV" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_ESV
CREATE TABLE IF NOT EXISTS "BIBLE_ESV" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_FLS
CREATE TABLE IF NOT EXISTS "BIBLE_FLS" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_GB
CREATE TABLE IF NOT EXISTS "BIBLE_GB" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_GLB
CREATE TABLE IF NOT EXISTS "BIBLE_GLB" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_GNB
CREATE TABLE IF NOT EXISTS "BIBLE_GNB" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_GW
CREATE TABLE IF NOT EXISTS "BIBLE_GW" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_HIL
CREATE TABLE IF NOT EXISTS "BIBLE_HIL" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_ISV
CREATE TABLE IF NOT EXISTS "BIBLE_ISV" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_ITB
CREATE TABLE IF NOT EXISTS "BIBLE_ITB" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_KJV
CREATE TABLE IF NOT EXISTS "BIBLE_KJV" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_LBLA
CREATE TABLE IF NOT EXISTS "BIBLE_LBLA" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_LITV
CREATE TABLE IF NOT EXISTS "BIBLE_LITV" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_MBBTAG
CREATE TABLE IF NOT EXISTS "BIBLE_MBBTAG" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_MKJV
CREATE TABLE IF NOT EXISTS "BIBLE_MKJV" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_NIV
CREATE TABLE IF NOT EXISTS "BIBLE_NIV" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_NORSK
CREATE TABLE IF NOT EXISTS "BIBLE_NORSK" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_PJFA
CREATE TABLE IF NOT EXISTS "BIBLE_PJFA" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_RSV
CREATE TABLE IF NOT EXISTS "BIBLE_RSV" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_SF
CREATE TABLE IF NOT EXISTS "BIBLE_SF" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_SRV
CREATE TABLE IF NOT EXISTS "BIBLE_SRV" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_SSE
CREATE TABLE IF NOT EXISTS "BIBLE_SSE" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_SWB
CREATE TABLE IF NOT EXISTS "BIBLE_SWB" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_VULGATE
CREATE TABLE IF NOT EXISTS "BIBLE_VULGATE" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_WEB
CREATE TABLE IF NOT EXISTS "BIBLE_WEB" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_WEBSTER
CREATE TABLE IF NOT EXISTS "BIBLE_WEBSTER" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_WYC
CREATE TABLE IF NOT EXISTS "BIBLE_WYC" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BIBLE_YLT
CREATE TABLE IF NOT EXISTS "BIBLE_YLT" (
    "CompositeCode" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "Id" INTEGER NOT NULL,
    "BookCode" INTEGER DEFAULT -1 NOT NULL,
    "ChapterCode" INTEGER DEFAULT -1 NOT NULL,
    "VerseCode" INTEGER DEFAULT -1 NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BibleArticle
CREATE TABLE IF NOT EXISTS "BibleArticle" (
    "date_post" TIMESTAMP,
    "title" VARCHAR(500),
    "content" TEXT,
    "imgfname" VARCHAR(500),
    "username" VARCHAR(500),
    "id" INTEGER
);

-- Table: BibleBookName
CREATE TABLE IF NOT EXISTS "BibleBookName" (
    "BookNameCode" INTEGER NOT NULL,
    "BookCode" INTEGER NOT NULL,
    "Name" VARCHAR(500) NOT NULL,
    "MaxChapter" INTEGER NOT NULL,
    "Id" INTEGER NOT NULL,
    "ShortName" VARCHAR(50) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BibleFact
CREATE TABLE IF NOT EXISTS "BibleFact" (
    "category" INTEGER,
    "subcategory" INTEGER,
    "date_post" TIMESTAMP,
    "title" VARCHAR(500),
    "content" TEXT,
    "imgfname" VARCHAR(500),
    "username" VARCHAR(500),
    "id" INTEGER
);

-- Table: BibleFactCategory
CREATE TABLE IF NOT EXISTS "BibleFactCategory" (
    "category" VARCHAR(500),
    "id" INTEGER
);

-- Table: BibleFactSubcategory
CREATE TABLE IF NOT EXISTS "BibleFactSubcategory" (
    "subcategory" VARCHAR(500),
    "category" INTEGER,
    "id" INTEGER
);

-- Table: BibleTranslation
CREATE TABLE IF NOT EXISTS "BibleTranslation" (
    "Name" VARCHAR(500) NOT NULL,
    "Id" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BibleTranslation_old
CREATE TABLE IF NOT EXISTS "BibleTranslation_old" (
    "TranslationCode" INTEGER NOT NULL,
    "Name" VARCHAR(250) NOT NULL,
    "LanguageCode" VARCHAR(50) NOT NULL,
    "CountryCode" VARCHAR(50) NOT NULL,
    PRIMARY KEY ("TranslationCode")
);

-- Table: BibleVersion
CREATE TABLE IF NOT EXISTS "BibleVersion" (
    "BibleTableName" VARCHAR(500) NOT NULL,
    "Name" VARCHAR(500) NOT NULL,
    "BookNameCode" INTEGER NOT NULL,
    "OldAndNew" INTEGER NOT NULL,
    "LanguageType" INTEGER NOT NULL,
    "TranslationType" INTEGER NOT NULL,
    "Copyright" INTEGER NOT NULL,
    "Id" INTEGER NOT NULL,
    "Active" INTEGER NOT NULL,
    "ShortName" VARCHAR(50) DEFAULT '' NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: BibleVersionLanguage
CREATE TABLE IF NOT EXISTS "BibleVersionLanguage" (
    "Name" VARCHAR(500) NOT NULL,
    "Id" INTEGER NOT NULL,
    PRIMARY KEY ("Id")
);

-- Table: tbl_cubt
CREATE TABLE IF NOT EXISTS "tbl_cubt" (
    "category" INTEGER,
    "date_post" TIMESTAMP,
    "title" VARCHAR(500),
    "content" TEXT,
    "imgfname" VARCHAR(500),
    "username" VARCHAR(500),
    "id" INTEGER
);

-- Table: tbl_cubtcategory
CREATE TABLE IF NOT EXISTS "tbl_cubtcategory" (
    "category" VARCHAR(500),
    "id" INTEGER
);

-- Table: tbl_randverse
CREATE TABLE IF NOT EXISTS "tbl_randverse" (
    "book" INTEGER,
    "chapter" INTEGER,
    "verse" INTEGER,
    "id" INTEGER
);

-- Table: tbl_tools
CREATE TABLE IF NOT EXISTS "tbl_tools" (
    "date_post" TIMESTAMP,
    "title" VARCHAR(500),
    "content" TEXT,
    "imgfname" VARCHAR(500),
    "username" VARCHAR(500),
    "id" INTEGER
);

-- Table: tbl_users
CREATE TABLE IF NOT EXISTS "tbl_users" (
    "userid" INTEGER,
    "name" VARCHAR(500),
    "email" VARCHAR(500),
    "username" VARCHAR(500),
    "password" VARCHAR(500),
    "info" VARCHAR(500),
    "user_level" VARCHAR(500),
    "signup_date" TIMESTAMP
);

