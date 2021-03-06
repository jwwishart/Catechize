/*
   Monday, 28 March 20118:25:59 AM
   User: 
   Server: WISHART-LAPTOP\JUSTIN
   Database: Catechize
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
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
CREATE TABLE dbo.Part
	(
	PartID int NOT NULL,
	CourseID int NOT NULL,
	OrdinalNo int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Part ADD CONSTRAINT
	PK_Part PRIMARY KEY CLUSTERED 
	(
	PartID,
	CourseID,
	OrdinalNo
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Part SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Part', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Part', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Part', 'Object', 'CONTROL') as Contr_Per 