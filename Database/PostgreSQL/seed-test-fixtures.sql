-- =============================================================================
-- mPortal CMS - PostgreSQL Integration Test Fixtures
--
-- Purpose:
-- - deterministic login fixture user for integration tests
-- - explicit fixture marker for backup/promotion safety checks
--
-- Default fixture credentials:
--   username: itest.user
--   password: itest-pass
-- =============================================================================

BEGIN;

-- Remove stale duplicates by username to keep fixtures deterministic.
DELETE FROM "WebUser"
WHERE "UserName" = 'itest.user'
  AND "UserId" <> 900001;

INSERT INTO "WebUser" (
    "UserId", "UserName", "Password", "FirstName", "MiddleName", "LastName", "Email",
    "LastUpdate", "Active", "ActivationKey", "DateCreated", "NewEmail", "Email2", "Gender",
    "NameSuffix", "MobileNumber", "TelephoneNumber", "LastLogin", "StatusText", "PasswordExpiryDate",
    "PhotoPath", "ProviderId", "Status"
)
VALUES (
    900001, 'itest.user', 'itest-pass', 'Integration', '', 'Tester', 'itest.user@localhost',
    NOW(), 1, '', NOW(), '', '', 'U',
    '', '', '', NOW(), 'integration-fixture', '2099-12-31 00:00:00',
    '', -1, 1
)
ON CONFLICT ("UserId") DO UPDATE SET
    "UserName" = EXCLUDED."UserName",
    "Password" = EXCLUDED."Password",
    "FirstName" = EXCLUDED."FirstName",
    "MiddleName" = EXCLUDED."MiddleName",
    "LastName" = EXCLUDED."LastName",
    "Email" = EXCLUDED."Email",
    "LastUpdate" = EXCLUDED."LastUpdate",
    "Active" = EXCLUDED."Active",
    "ActivationKey" = EXCLUDED."ActivationKey",
    "DateCreated" = EXCLUDED."DateCreated",
    "NewEmail" = EXCLUDED."NewEmail",
    "Email2" = EXCLUDED."Email2",
    "Gender" = EXCLUDED."Gender",
    "NameSuffix" = EXCLUDED."NameSuffix",
    "MobileNumber" = EXCLUDED."MobileNumber",
    "TelephoneNumber" = EXCLUDED."TelephoneNumber",
    "LastLogin" = EXCLUDED."LastLogin",
    "StatusText" = EXCLUDED."StatusText",
    "PasswordExpiryDate" = EXCLUDED."PasswordExpiryDate",
    "PhotoPath" = EXCLUDED."PhotoPath",
    "ProviderId" = EXCLUDED."ProviderId",
    "Status" = EXCLUDED."Status";

INSERT INTO "WebConstant" ("ConstantId", "Value", "Rank", "Category", "Text", "ObjectId")
VALUES (900001, 'integration-fixtures-enabled', 900001, 'Integration', 'FixtureMarker', -1)
ON CONFLICT ("ConstantId") DO UPDATE SET
    "Value" = EXCLUDED."Value",
    "Rank" = EXCLUDED."Rank",
    "Category" = EXCLUDED."Category",
    "Text" = EXCLUDED."Text",
    "ObjectId" = EXCLUDED."ObjectId";

COMMIT;
