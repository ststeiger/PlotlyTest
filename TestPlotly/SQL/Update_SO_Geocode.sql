
-- DECLARE @obj uniqueidentifier 
-- DECLARE @lat decimal(22,20) 
-- DECLARE @lng decimal(23,20)


-- SET @obj = '00000000-0000-0000-0000-000000000000'  
-- SET @lat = 0.0 
-- SET @lng = 0.0 


UPDATE T_AP_Standort 
	SET  SO_GM_Lat = @lat 
		,SO_GM_Lng =  @lng 
WHERE SO_UID = @obj 
;
