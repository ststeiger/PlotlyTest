
-- DECLARE @ZO_OBJ_WGS84_GB_UID uniqueidentifier
-- DECLARE @ZO_OBJ_WGS84_SO_UID uniqueidentifier
-- DECLARE @ZO_OBJ_WGS84_Sort  int
-- DECLARE @ZO_OBJ_WGS84_GM_Lat decimal(23, 20)
-- DECLARE @ZO_OBJ_WGS84_GM_Lng decimal(23, 20)


-- SET @ZO_OBJ_WGS84_GB_UID = NULL 
-- SET @ZO_OBJ_WGS84_SO_UID = NULL 
-- SET @ZO_OBJ_WGS84_Sort  = 1 
-- SET @ZO_OBJ_WGS84_GM_Lat = 12.3
-- SET @ZO_OBJ_WGS84_GM_Lng  = 42


-- TRUNCATE TABLE T_ZO_Objekt_Wgs84Polygon 
INSERT INTO T_ZO_Objekt_Wgs84Polygon
(
	 ZO_OBJ_WGS84_UID
	,ZO_OBJ_WGS84_GB_UID
	,ZO_OBJ_WGS84_SO_UID
	,ZO_OBJ_WGS84_Sort
	,ZO_OBJ_WGS84_GM_Lat
	,ZO_OBJ_WGS84_GM_Lng
)
VALUES
(
	 NEWID() -- ZO_OBJ_WGS84_UID -- uniqueidentifier
	,@ZO_OBJ_WGS84_GB_UID -- ZO_OBJ_WGS84_GB_UID -- uniqueidentifier
	,@ZO_OBJ_WGS84_SO_UID -- ZO_OBJ_WGS84_SO_UID -- uniqueidentifier
	,@ZO_OBJ_WGS84_Sort   -- ZO_OBJ_WGS84_Sort -- int
	,@ZO_OBJ_WGS84_GM_Lat -- ZO_OBJ_WGS84_GM_Lat -- decimal(23,20)
	,@ZO_OBJ_WGS84_GM_Lng -- ZO_OBJ_WGS84_GM_Lng -- decimal(23,20)
)
; 
