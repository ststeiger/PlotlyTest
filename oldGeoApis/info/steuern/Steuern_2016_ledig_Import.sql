
-- First, remove emply lines consisting of:
-- ,,,,,,,,,,,,,,,,,,,,,,,,,,\n
-- in the CSV-file; CSV with OpenOffice  



-- Windows:
BULK INSERT __Steuern_2016
FROM 'D:\temp\steuern_ledig.csv'
WITH
(
    CODEPAGE = '65001',
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '\n',   --Use to shift the control to next row
    TABLOCK
)




-- Linux:
BULK INSERT TestDb.dbo.__Steuern_2016
    FROM '/web/steuern_ledig.csv'
WITH
(
	-- CODEPAGE = '65001', -- Codepage not supported on Linux...
    FORMAT = 'CSV', 
    FIELDQUOTE = '"',
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a', --'\n',   --Use to shift the control to next row
    -- ERRORFILE = 'C:\web\SchoolsErrorRows.csv',
    TABLOCK
)

-- SELECT * FROM TestDb.dbo.__Steuern_2014

-- TRUNCATE TABLE TestDb.dbo.__Steuern_2016

-- https://docs.microsoft.com/en-us/sql/t-sql/statements/bulk-insert-transact-sql
-- CODEPAGE 65001 (UTF-8 encoding).

SELECT * FROM TestDb.dbo.__Steuern_2016
LEFT JOIN TestDb.dbo.__Steuern_2014 ON __Steuern_2016.gemeindenummer = __Steuern_2014.gemeindenummer
WHERE __Steuern_2014.gemeindenummer IS NOT NULL  
