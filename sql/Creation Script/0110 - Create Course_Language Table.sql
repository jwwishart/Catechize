

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Course_Language
	(
	CourseID int NOT NULL,
	Title nvarchar(200) NOT NULL,
	Description nvarchar(1000) NOT NULL,
	CultureName nvarchar(84) NOT NULL,
	IsPublished bit NOT NULL,
	IsEnabled bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Course_Language SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Course_Language', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Course_Language', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Course_Language', 'Object', 'CONTROL') as Contr_Per 

GO

