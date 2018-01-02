IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__Incid__1B4E8E51]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] DROP CONSTRAINT [DF__IncidentI__Incid__1B4E8E51]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__SLAHi__1C42B28A]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] DROP CONSTRAINT [DF__IncidentI__SLAHi__1C42B28A]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__SLALo__1D36D6C3]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] DROP CONSTRAINT [DF__IncidentI__SLALo__1D36D6C3]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__SLANo__1E2AFAFC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] DROP CONSTRAINT [DF__IncidentI__SLANo__1E2AFAFC]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__IncidentI__SLAWa__1F1F1F35]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[IncidentInstance] DROP CONSTRAINT [DF__IncidentI__SLAWa__1F1F1F35]
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IncidentInstance]') AND type in (N'U'))
DROP TABLE [dbo].[IncidentInstance]
GO
