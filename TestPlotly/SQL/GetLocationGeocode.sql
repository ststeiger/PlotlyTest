
SELECT 
	 ORT_UID AS OBJ_UID 
	,ORT_Lang_DE + ', ' + T_AP_Ref_Land.LD_Lang_DE AS Location 
	,ORT_GM_Lat
	,ORT_GM_Lng
FROM T_AP_Ref_Ort
LEFT JOIN T_AP_Ref_Region ON RG_UID = ORT_RG_UID 
LEFT JOIN T_AP_Ref_Land ON LD_UID = RG_LD_UID 
WHERE (1=1) 
/*
AND 
(
	(
		ORT_GM_Lat IS NULL 
		OR 
		ORT_GM_Lng IS NULL 
	)
	OR 
	(
		ORT_GM_Lat = 0.0
		AND 
		ORT_GM_Lng = 0.0 
	)
)
*/