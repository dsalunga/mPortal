IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__Incid__2A5D5E65]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] DROP CONSTRAINT [DF__IncidentI__Incid__2A5D5E65]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__SLAHi__2B51829E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] DROP CONSTRAINT [DF__IncidentI__SLAHi__2B51829E]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__SLALo__2C45A6D7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] DROP CONSTRAINT [DF__IncidentI__SLALo__2C45A6D7]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__SLANo__2D39CB10]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] DROP CONSTRAINT [DF__IncidentI__SLANo__2D39CB10]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__SLAWa__2E2DEF49]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] DROP CONSTRAINT [DF__IncidentI__SLAWa__2E2DEF49]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IncidentInstance]') AND type in (N'U'))
DROP TABLE [dbo].[IncidentInstance]
GO
