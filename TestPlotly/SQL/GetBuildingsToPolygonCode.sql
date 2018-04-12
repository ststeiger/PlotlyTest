
SELECT 
	 GB_UID 
	,GB_Nr 
	, GB_Bezeichnung 
	-- ,_GB_Label
	, GB_Strasse, GB_StrasseNr, GB_PLZ, GB_Ort
	--,T_AP_Ref_Ort.ORT_Lang_DE


	,GB_GM_Lat
	,GB_GM_Lng
	,GB_GM_SVLat
	,GB_GM_SVLng

	--,T_AP_Standort.*
FROM T_AP_Gebaeude 
LEFT JOIN T_AP_Standort ON SO_UID = GB_SO_UID 
LEFT JOIN T_AP_Ref_Ort ON ORT_UID = SO_ORT_UID 
WHERE GB_Status = 1 
AND CURRENT_TIMESTAMP BETWEEN GB_DatumVon AND GB_DatumBis 
AND SO_Bezeichnung = 'Altstetten' 


--SELECT * FROM T_ZO_Objekt_Wgs84Polygon 
