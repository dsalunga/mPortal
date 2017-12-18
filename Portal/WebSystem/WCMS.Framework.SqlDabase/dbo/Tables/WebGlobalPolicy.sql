CREATE TABLE [dbo].[WebGlobalPolicy] (
    [GlobalPolicyId] INT            CONSTRAINT [DF_WebGlobalPolicy_GlobalPolicyId] DEFAULT ((-1)) NOT NULL,
    [Name]           NVARCHAR (250) COLLATE Latin1_General_CI_AI CONSTRAINT [DF_WebGlobalPolicy_Name] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_WebGlobalPolicy] PRIMARY KEY CLUSTERED ([GlobalPolicyId] ASC)
);

