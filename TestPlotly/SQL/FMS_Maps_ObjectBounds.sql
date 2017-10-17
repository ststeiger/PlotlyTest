
-- DECLARE @obj_uid uniqueidentifier 
-- SET @obj_uid = 'E54C4E15-55CC-4B9A-9EE7-01002E721504' -- Land 
-- SET @obj_uid = '3E7C328D-9807-48A7-8DCC-014E8573CA59' -- Ort 
-- SET @obj_uid = '5F8170F6-A92C-4633-AED5-0D7ADA97C6DB' -- SO 
-- SET @obj_uid = '921A3415-51A1-431B-B12B-00AE5E867ABE' -- GB 


SELECT 
	 'LD' AS OBJT_UID 
	,T_AP_Ref_Land.LD_UID AS OBJ_UID 
	 
	,CAST(NULL AS decimal(23, 20)) AS OBJ_Lat 
	,CAST(NULL AS decimal(23, 20)) AS OBJ_Lng 
	 
	,T_AP_Ref_Land.LD_Min_Lat AS OBJ_Min_Lat 
	,T_AP_Ref_Land.LD_Min_Long AS OBJ_Min_Long 
	 
	,T_AP_Ref_Land.LD_MAX_Lat AS OBJ_Max_Lat 
	,T_AP_Ref_Land.LD_MAX_Lng AS OBJ_Max_Lng 
FROM T_AP_Ref_Land  
WHERE T_AP_Ref_Land.LD_UID = @obj_uid 


UNION ALL 


SELECT 
	'ORT' AS OBJT_UID 
	,T_AP_Ref_Ort.ORT_UID AS OBJ_UID 
	 
	,T_AP_Ref_Ort.ORT_GM_Lat AS OBJ_Lat 
	,T_AP_Ref_Ort.ORT_GM_Lng AS OBJ_Lng 
	 
	,CAST(NULL AS decimal(23, 20)) AS OBJ_Min_Lat 
	,CAST(NULL AS decimal(23, 20)) AS OBJ_Min_Long 
	 
	,CAST(NULL AS decimal(23, 20)) AS OBJ_Max_Lat 
	,CAST(NULL AS decimal(23, 20)) AS OBJ_Max_Lng 
FROM T_AP_Ref_Ort 
WHERE T_AP_Ref_Ort.ORT_UID = @obj_uid 


UNION ALL 


SELECT 
	 'SO' AS OBJT_UID 
	,T_AP_Standort.SO_UID AS OBJ_UID 
	 
	,T_AP_Standort.SO_GM_Lat AS OBJ_Lat 
	,T_AP_Standort.SO_GM_Lng AS OBJ_Lng 
	 
	,MIN(T_AP_Gebaeude.GB_GM_Lat) AS OBJ_Min_Lat 
	,MIN(T_AP_Gebaeude.GB_GM_Lng) AS OBJ_Min_Long 
	 
	,MAX(T_AP_Gebaeude.GB_GM_Lat) AS OBJ_Max_Lat 
	,MAX(T_AP_Gebaeude.GB_GM_Lng) AS OBJ_Max_Lng 
FROM T_AP_Standort 

LEFT JOIN T_AP_Gebaeude 
	ON T_AP_Gebaeude.GB_SO_UID = T_AP_Standort.SO_UID 
	AND T_AP_Gebaeude.GB_Status <> 99 

WHERE (1=1) 
AND T_AP_Standort.SO_UID = @obj_uid 

GROUP BY 
	 T_AP_Standort.SO_UID 
	,T_AP_Standort.SO_GM_Lat 
	,T_AP_Standort.SO_GM_Lng 
	 


UNION ALL 



SELECT 
	'GB' AS OBJT_UID 
	,T_AP_Gebaeude.GB_UID AS OBJ_UID 
	 
	,T_AP_Gebaeude.GB_GM_Lat AS OBJ_Lat 
	,T_AP_Gebaeude.GB_GM_Lng AS OBJ_Lng 
	 
	,CAST(NULL AS decimal(23, 20)) AS OBJ_Min_Lat 
	,CAST(NULL AS decimal(23, 20)) AS OBJ_Min_Long 
	 
	,CAST(NULL AS decimal(23, 20)) AS OBJ_Max_Lat 
	,CAST(NULL AS decimal(23, 20)) AS OBJ_Max_Lng 
FROM T_AP_Gebaeude  
WHERE (1=1) 
AND T_AP_Gebaeude.GB_UID = @obj_uid 
