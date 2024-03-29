
IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[V_DEV_ForeignKeys]'))
DROP VIEW [dbo].[V_DEV_ForeignKeys]
GO




CREATE VIEW dbo.V_DEV_ForeignKeys 
AS 
SELECT  
     KCU1.CONSTRAINT_NAME AS FK_CONSTRAINT_NAME 
	,KCU1.TABLE_NAME AS FK_TABLE_NAME 
	,KCU1.COLUMN_NAME AS FK_COLUMN_NAME 
	,KCU1.ORDINAL_POSITION AS FK_ORDINAL_POSITION 
	,KCU2.CONSTRAINT_NAME AS REFERENCED_CONSTRAINT_NAME 
	,KCU2.TABLE_NAME AS REFERENCED_TABLE_NAME 
	,KCU2.COLUMN_NAME AS REFERENCED_COLUMN_NAME 
	,KCU2.ORDINAL_POSITION AS REFERENCED_ORDINAL_POSITION 
FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC 

INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU1 
	ON KCU1.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG  
	AND KCU1.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA 
	AND KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME 
	
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU2 
	ON KCU2.CONSTRAINT_CATALOG = RC.UNIQUE_CONSTRAINT_CATALOG  
	AND KCU2.CONSTRAINT_SCHEMA = RC.UNIQUE_CONSTRAINT_SCHEMA 
	AND KCU2.CONSTRAINT_NAME = RC.UNIQUE_CONSTRAINT_NAME 
	AND KCU2.ORDINAL_POSITION = KCU1.ORDINAL_POSITION 
	
