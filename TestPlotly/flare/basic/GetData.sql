
DECLARE @__counts  TABLE
(
    table_schema varchar(255),
	table_name varchar(255),
    row_count int
)


INSERT INTO @__counts 
(
	 table_schema
	,table_name
	,row_count
)
SELECT
	  SCHEMA_NAME(A.schema_id) 
	 ,A.Name
	,SUM(B.rows) AS 'RowCount'
FROM sys.objects A
INNER JOIN sys.partitions B ON A.object_id = B.object_id
WHERE A.type = 'U'
GROUP BY A.schema_id, A.Name



DECLARE @__foreign_keys TABLE
(
	 FK_CONSTRAINT_NAME nvarchar(128) NOT NULL 
	,FK_TABLE_NAME nvarchar(128) NOT NULL 
	,FK_COLUMN_NAME nvarchar(128) NULL 
	,FK_ORDINAL_POSITION int NOT NULL 
	,REFERENCED_CONSTRAINT_NAME nvarchar(128) NOT NULL 
	,REFERENCED_TABLE_NAME nvarchar(128) NOT NULL 
	,REFERENCED_COLUMN_NAME nvarchar(128) NULL 
	,REFERENCED_ORDINAL_POSITION int NOT NULL 
)

INSERT INTO @__foreign_keys 
(
	 [FK_CONSTRAINT_NAME]
	,[FK_TABLE_NAME]
	,[FK_COLUMN_NAME]
	,[FK_ORDINAL_POSITION]
	,[REFERENCED_CONSTRAINT_NAME]
	,[REFERENCED_TABLE_NAME]
	,[REFERENCED_COLUMN_NAME]
	,[REFERENCED_ORDINAL_POSITION]
)

SELECT  
     KCU1.CONSTRAINT_NAME AS FK_CONSTRAINT_NAME 
	,KCU1.TABLE_NAME AS FK_TABLE_NAME 
	,KCU1.COLUMN_NAME AS FK_COLUMN_NAME 
	,KCU1.ORDINAL_POSITION AS FK_ORDINAL_POSITION 
	,KCU2.CONSTRAINT_NAME AS REFERENCED_CONSTRAINT_NAME 
	,KCU2.TABLE_NAME AS REFERENCED_TABLE_NAME 
	,KCU2.COLUMN_NAME AS REFERENCED_COLUMN_NAME 
	,KCU2.ORDINAL_POSITION AS REFERENCED_ORDINAL_POSITION 
FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC 

INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU1 
	ON KCU1.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG  
	AND KCU1.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA 
	AND KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME 
	
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU2 
	ON KCU2.CONSTRAINT_CATALOG = RC.UNIQUE_CONSTRAINT_CATALOG  
	AND KCU2.CONSTRAINT_SCHEMA = RC.UNIQUE_CONSTRAINT_SCHEMA 
	AND KCU2.CONSTRAINT_NAME = RC.UNIQUE_CONSTRAINT_NAME 
	AND KCU2.ORDINAL_POSITION = KCU1.ORDINAL_POSITION 
	



SELECT 
	 ist.TABLE_CATALOG
	,ist.TABLE_SCHEMA
	,ist.TABLE_NAME	
	,ist.TABLE_TYPE

	,',{ "name": "' + REPLACE(ist.TABLE_NAME, '"', '\"')	 + '", "size": ' 
	+ CAST(COALESCE(NULLIF(tCounts.row_count, 0), 1) AS varchar(30))   
	+ 
	COALESCE 
	(
		', "imports": [' 
		+ 
		STUFF
		(
			(
				SELECT 
					', "' + REPLACE(REFERENCED_TABLE_NAME, '"', '\"') + '"' 
				FROM @__foreign_keys 
				WHERE FK_TABLE_NAME = ist.TABLE_NAME 
				FOR XML PATH(''), TYPE 
			).value('.', 'nvarchar(MAX)') 
			,1
			, 2
			, ''
		) 
		+ N']' 
		,''
	) 
	+ ' }' 
	AS Exports 

FROM INFORMATION_SCHEMA.TABLES AS ist 
LEFT JOIN @__counts AS tCounts  
	ON tCounts.table_name = ist.TABLE_NAME 

WHERE ist.TABLE_TYPE = 'BASE TABLE' 
AND ist.TABLE_NAME LIKE 'T[_]%' 


-- https://bl.ocks.org/mbostock/7607999


/*

text
{
    font: 300 10px "Helvetica Neue";
}


path {
    fill: none;
    stroke: #6DA4CB;
}



var saveData = (function () {
    var a = document.createElement("a");
    document.body.appendChild(a);
    a.style = "display: none";
    return function (data, fileName) {
            blob = new Blob([data], {type: "octet/stream"}),
            url = window.URL.createObjectURL(blob);
        a.href = url;
        a.download = fileName;
        a.click();
        window.URL.revokeObjectURL(url);
    };
}());

var s = new XMLSerializer();
var data = s.serializeToString(document.getElementsByTagName("svg")[0]);
var fileName = "myfile.svg";

saveData(data, fileName);

/*
function download(filename, text) {
    var pom = document.createElement('a');
    pom.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));
    pom.setAttribute('download', filename);

    if (document.createEvent) {
        var event = document.createEvent('MouseEvents');
        event.initEvent('click', true, true);
        pom.dispatchEvent(event);
    }
    else {
        pom.click();
    }
}
*/


*/
