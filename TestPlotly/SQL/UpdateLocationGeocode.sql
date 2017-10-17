
-- DECLARE @obj_uid uniqueidentifier 
-- DECLARE @lat decimal(22,20) 
-- DECLARE @lng decimal(23,20)
-- DECLARE @latMin decimal(23,20) 
-- DECLARE @lngMin decimal(23,20) 
-- DECLARE @latMax decimal(23,20) 
-- DECLARE @lngMax decimal(23,20) 

-- SET @obj_uid = '00000000-0000-0000-0000-000000000000' 
-- SET @lat = 0.0 
-- SET @lng = 0.0 
-- SET @latMin = 0.0 
-- SET @lngMin = 0.0 
-- SET @latMax = 0.0 
-- SET @lngMax = 0.0 


UPDATE T_AP_Ref_Ort 
	SET  ORT_GM_Lat = @lat 
		,ORT_GM_Lng = @lng 
		,ORT_Min_Lat = @latMin 
		,ORT_Min_Lng = @lngMin 
		,ORT_Max_Lat = @latMax 
		,ORT_Max_Lng = @lngMax 
WHERE ORT_UID = @obj_uid 
;
