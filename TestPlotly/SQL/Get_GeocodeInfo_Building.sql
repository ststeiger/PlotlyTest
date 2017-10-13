
-- DECLARE @building uniqueidentifier 
-- SET @building = '921A3415-51A1-431B-B12B-00AE5E867ABE' 

/*
SELECT
	 GB_UID
	,GB_Nr 
	,GB_Bezeichnung 
	,GB_Strasse 
	,GB_StrasseNr
	,GB_PLZ
	,GB_Ort

	,GB_Strasse + ', ' + GB_PLZ + ' ' + GB_Ort AS RPT_PlaceToGeocode 
	,GB_GM_Lat
	,GB_GM_Lng
FROM T_AP_Gebaeude 
WHERE GB_UID = @building 
; 
*/


-- DECLARE @building uniqueidentifier 
-- SET @building = '921A3415-51A1-431B-B12B-00AE5E867ABE' 


SELECT TOP 1 GB_Strasse + ', ' + GB_PLZ + ' ' + GB_Ort AS RPT_PlaceToGeocode 
FROM T_AP_Gebaeude 
WHERE GB_UID = @building 
