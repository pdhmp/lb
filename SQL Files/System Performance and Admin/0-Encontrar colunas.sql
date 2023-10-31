SELECT a.name,b.*
FROM sys.tables A
inner join
sys.columns B
on a.object_id=b.object_id
where a.name LIKE '%'
and b.name LIKE '%Id_Ticker%'