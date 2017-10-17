
SELECT 
	 SO_UID AS OBJ_UID 
	,SO_Nr + ' ' + SO_Bezeichnung AS Location 
	,SO_GM_Lat 
	,SO_GM_Lng 
	,SO_ORT_UID 
FROM T_AP_Standort 
WHERE (1=1) 
AND 
(
	(
		SO_GM_Lat IS NULL 
		OR 
		SO_GM_Lng IS NULL 
	)
	OR 
	(
		SO_GM_Lat = 0.0
		AND 
		SO_GM_Lng = 0.0 
	)
)
