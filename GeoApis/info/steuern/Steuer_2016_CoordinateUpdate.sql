
-- ALTER TABLE [dbo].[__Steuern_2016] add latitude [decimal](9, 6) NULL, longitude [decimal](9, 6) NULL

GO

;WITH CTE AS 
(
    SELECT 
        __Steuern_2016.[kanton]
        ,[__Steuern_2016].[gemeindenummer]
        ,[__Steuern_2016].[gemeinde]
        ,__Steuern_2016.latitude
        ,__Steuern_2016.longitude
        ,__Steuern_2014.latitude AS oldLat 
        ,__Steuern_2014.longitude AS oldLong 
    FROM [TestDb].[dbo].[__Steuern_2016]
    LEFT JOIN TestDb.dbo.__Steuern_2014 ON __Steuern_2016.gemeindenummer = __Steuern_2014.gemeindenummer
    WHERE __Steuern_2014.gemeindenummer IS NULL 
) 
SELECT * FROM CTE 
-- UPDATE CTE SET latitude = oldLat, longitude = oldLong 


/*
SELECT __Steuern_2016.* 
FROM TestDb.dbo.__Steuern_2016 
LEFT JOIN TestDb.dbo.__Steuern_2014 ON __Steuern_2016.gemeindenummer = __Steuern_2014.gemeindenummer 
WHERE __Steuern_2014.gemeindenummer IS NULL 
*/
