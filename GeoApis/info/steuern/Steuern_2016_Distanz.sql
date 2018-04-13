
DECLARE @source geography
-- SET @source = 'POINT(47.551845 9.226530)' -- Erlen 
SET @source = geography::Point(47.551845, 9.226530, 4326) -- Erlen 
-- SET @source = geography::Point(47.377463, 9.546913, 4326) -- Altsätten SG 



-- select * from __Steuern_2016 WHERE gemeinde LIKE '%altstätten%'



;WITH CTE AS 
(
    SELECT 
        __Steuern_2016.kanton
        ,__Steuern_2016.gemeindenummer
        ,__Steuern_2016.gemeinde
        ,__Steuern_2016.from_80000
        ,__Steuern_2016.from_90000
        ,__Steuern_2016.from_100000
        ,__Steuern_2016.from_125000
        ,__Steuern_2016.latitude
        ,__Steuern_2016.longitude
        --,geography::Point(latitude, longitude, 4326)
        ,@source.STDistance(geography::Point(latitude, longitude, 4326)) AS delta 
    FROM __Steuern_2016
    -- WHERE gemeinde LIKE '%erlen%'
    -- WHERE gemeindenummer = 4476 
) 
SELECT * FROM CTE 
WHERE delta < 30975 
ORDER BY from_80000, delta 
