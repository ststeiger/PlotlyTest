
INSERT INTO T_SYS_MAP_CountryPolygon2
(
	 CTRP_UID
	,CTRP_CountryName
	,CTRP_CountryISO
	,CTRP_LD_UID
	,CTRP_Min_Lat
	,CTRP_Min_Long
	,CTRP_MAX_Lat
	,CTRP_MAX_Lng
	,CTRP_DatumVon
	,CTRP_DatumBis
	,CTRP_Status
)
 VALUES
(
	 newid()
	,@long 
	,@short 
	,NULL -- LD_UID, uniqueidentifier
	,@CTRP_Min_Lat
	,@CTRP_Min_Lng
	,@CTRP_MAX_Lat
	,@CTRP_MAX_Lng
	,'17530101'
	,'29991231'
	,1
)
;
