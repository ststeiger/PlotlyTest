
-- DECLARE @BE_ID varchar(50) 
-- SET @BE_ID = (SELECT TOP 1 CAST(BE_ID  AS varchar(50)) FROM T_Benutzer WHERE BE_User = 'administrator') 
-- SET @BE_ID = (SELECT TOP 1 BE_Hash FROM T_Benutzer WHERE BE_User = 'administrator') 


DECLARE @__user_id int 
SET @__user_id = (SELECT TOP 1 BE_ID FROM T_Benutzer WHERE BE_User = 'administrator') 

DECLARE @__stichtag datetime 
SET @__stichtag = CAST(FLOOR(CAST(CURRENT_TIMESTAMP AS float)) AS datetime) 


SELECT 
	 T_AP_Gebaeude.GB_UID AS OBJ_UID 
	,T_OV_Ref_ObjektTyp.OBJT_Code AS OBJT_Code
	--,T_AP_Gebaeude.GB_Nr AS OBJ_Nr 

	--,T_AP_Gebaeude.GB_Bezeichnung AS OBJ_Bez
	--,T_AP_Gebaeude._GB_Label AS OBJ_Label 

	,  
	   ISNULL(NULLIF(T_AP_Gebaeude.GB_Nr, '') + ' ', '')
	 + ISNULL(NULLIF(T_AP_Gebaeude.GB_Bezeichnung, '') 
	 + CHAR(13) + CHAR(10), '') 
	 + ISNULL(NULLIF(ISNULL(NULLIF(T_AP_Gebaeude.GB_Strasse, '') + ' ', '') + ISNULL(NULLIF(T_AP_Gebaeude.GB_StrasseNr, ''), ''), '') 
	 + CHAR(13) + CHAR(10), '') 
	 + ISNULL(NULLIF(T_AP_Gebaeude.GB_PLZ, '') + ' ', '') + ISNULL(NULLIF(T_AP_Gebaeude.GB_Ort, ''), '') 
	 AS OBJ_Label 

	
	,NULLIF(T_AP_Gebaeude.GB_GM_Lat, 0.0) AS OBJ_Lat
	,NULLIF(T_AP_Gebaeude.GB_GM_Lng, 0.0) AS OBJ_Long 
	--,T_AP_Gebaeude.GB_GM_SVLat
	--,T_AP_Gebaeude.GB_GM_SVLng
	--,T_AP_Gebaeude._GB_Sort AS OBJ_Sort 


	--,T_AP_Gebaeude.GB_Nr
	--,T_AP_Gebaeude.GB_Strasse
	--,T_AP_Gebaeude.GB_StrasseNr
	--,T_AP_Gebaeude.GB_PLZ
	--,T_AP_Gebaeude.GB_Ort
	--,T_AP_Gebaeude.GB_Bezeichnung
	--,T_AP_Gebaeude.GB_Bemerkung 

	--,T_AP_Standort.SO_UID 
	--,T_AP_Standort.SO_Bezeichnung 
	--,T_AP_Standort.SO_Nr 
FROM T_AP_Gebaeude 

INNER JOIN T_Benutzer 
	ON T_Benutzer.BE_ID = @__user_id 
	AND T_Benutzer.BE_Status = 1 

INNER JOIN T_OV_Ref_ObjektTyp -- GRU, SO, GB 
	ON T_OV_Ref_ObjektTyp.OBJT_Code = 'GB' 
	AND T_OV_Ref_ObjektTyp.OBJT_Status = 1 

INNER JOIN T_COR_Objekte 
	ON T_COR_Objekte.OBJ_UID = T_AP_Gebaeude.GB_UID 
	AND T_COR_Objekte.OBJ_OBJT_Code = T_OV_Ref_ObjektTyp.OBJT_Code 
    AND @__stichtag BETWEEN T_COR_Objekte.OBJ_DatumVon AND T_COR_Objekte.OBJ_DatumBis 
    AND 
	(
        (T_COR_Objekte.OBJ_usePRT = 0) 
		OR
        (
            T_COR_Objekte.OBJ_usePRT = 1 
			AND 
            EXISTS
			(
                SELECT T_COR_ZO_ObjektRechte_Lesen.ZO_OBJR_OBJ_UID
				FROM T_Benutzer_Benutzergruppen
				INNER JOIN T_COR_ZO_ObjektRechte_Lesen 
					ON T_COR_ZO_ObjektRechte_Lesen.ZO_OBJR_ID = T_Benutzer_Benutzergruppen.BEBG_BG
					AND (T_Benutzer_Benutzergruppen.BEBG_BE = T_Benutzer.BE_ID)
					AND (T_COR_ZO_ObjektRechte_Lesen.ZO_OBJR_OBJ_OBJT_Code = T_COR_Objekte.OBJ_OBJT_Code) 
				WHERE T_COR_ZO_ObjektRechte_Lesen.ZO_OBJR_OBJ_UID = T_COR_Objekte.OBJ_UID 
            )
        )
    )

INNER JOIN T_AP_Standort 
	ON T_AP_Standort.SO_UID = T_AP_Gebaeude.GB_SO_UID 
	AND T_AP_Standort.SO_Status = 1 
	AND @__stichtag BETWEEN T_AP_Standort.SO_DatumVon AND T_AP_Standort.SO_DatumBis 
	
WHERE T_AP_Gebaeude.GB_Status = 1 
AND @__stichtag BETWEEN T_AP_Gebaeude.GB_DatumVon AND T_AP_Gebaeude.GB_DatumBis 

ORDER BY 
	 --OBJ_Sort 
	--,OBJ_Nr 
	 T_AP_Gebaeude._GB_Sort 
	,OBJ_Label 
	 