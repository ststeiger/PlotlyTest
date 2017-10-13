
DECLARE @BE_ID varchar(50) 
-- SET @BE_ID = (SELECT TOP 1 CAST(BE_ID  AS varchar(50)) FROM T_Benutzer WHERE BE_User = 'administrator') 
SET @BE_ID = (SELECT TOP 1 BE_Hash FROM T_Benutzer WHERE BE_User = 'administrator') 


;WITH CTE AS 
(
	SELECT 
		 GB_UID AS OBJ_UID 
		,GB_Strasse + ', ' + GB_PLZ + ' ' + GB_Ort AS OBJ_StringToGeoCode 
		,T_AP_Gebaeude.GB_GM_Lat AS OBJ_Lat 
		,T_AP_Gebaeude.GB_GM_Lng AS OBJ_Lng 

		,STUFF
		(
			(
				SELECT 
					',' 
					+ CAST(T_ZO_Objekt_Wgs84Polygon.ZO_OBJ_WGS84_GM_Lat AS varchar(25)) 
					+ ' ' 
					+ CAST(T_ZO_Objekt_Wgs84Polygon.ZO_OBJ_WGS84_GM_Lng AS varchar(25)) 
					AS [text()] 
				FROM T_ZO_Objekt_Wgs84Polygon
				WHERE T_ZO_Objekt_Wgs84Polygon.ZO_OBJ_WGS84_GB_UID = T_AP_Gebaeude.GB_UID
				-- WHERE T_ZO_Objekt_Wgs84Polygon.ZO_OBJ_WGS84_SO_UID = T_AP_Standort.SO_UID 
				ORDER BY T_ZO_Objekt_Wgs84Polygon.ZO_OBJ_WGS84_Sort ASC 
				FOR XML PATH(''), TYPE
			).value('.', 'varchar(MAX)') 
			,1
			,1
			, ''
		) 
		AS OBJ_Polygon 

	FROM T_AP_Gebaeude 
	
	WHERE (1=1) 

	/*
	-- Alle, die noch keine Koordinaten haben
	AND 
	(
		(
			GB_GM_Lat IS NULL 
			OR 
			GB_GM_Lng IS NULL 
		)
		OR 
		(
			GB_GM_Lat = 0.0 
			OR 
			GB_GM_Lng = 0.0 
		)
	)
	*/

	-- Alle, die Koordinaten haben 
	AND 
	(
		(
			GB_GM_Lat IS NOT NULL 
			AND 
			GB_GM_Lng IS NOT NULL 
		)
		AND NOT 
		(
			GB_GM_Lat = 0.0 
			AND 
			GB_GM_Lng = 0.0 
		)
	)

)
SELECT 
	 OBJ_UID 
	,OBJ_StringToGeoCode 
	,OBJ_Lat 
	,OBJ_Lng 
	,OBJ_Polygon 
FROM CTE 
WHERE (1=1) 
AND OBJ_Polygon IS NULL 

ORDER BY 
	 OBJ_UID  
	 