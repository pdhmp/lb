SELECT * FROM sys.dm_db_missing_index_groups where index_group_handle=3987



SELECT * FROM sys.dm_db_missing_index_group_stats
order by avg_total_user_cost desc

SELECT * FROM sys.dm_db_missing_index_details order by index_handle

SELECT name,user_seeks,user_scans,user_lookups,b.*, a.*
FROM sys.dm_db_index_usage_stats A
LEFT JOIN sys.indexes B
ON A.object_id=b.object_id
and a.index_id=b.index_id
WHERE database_id = DB_ID( 'NESTRT') AND OBJECT_NAME(a.OBJECT_ID) ='Tb000_Posicao_Atual'