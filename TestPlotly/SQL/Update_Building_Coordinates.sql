
-- DECLARE @building uniqueidentifier 
-- DECLARE @lat decimal(23,20) 
-- DECLARE @lng decimal(23,20)  

-- SET @building = '921A3415-51A1-431B-B12B-00AE5E867ABE' 
-- SET @lat = 46.235301 
-- SET @lng = 6.1255158 


UPDATE T_AP_Gebaeude 
SET  GB_GM_Lat = @lat 
	,GB_GM_Lng = @lng 
WHERE T_AP_Gebaeude.GB_UID = @building 
;
