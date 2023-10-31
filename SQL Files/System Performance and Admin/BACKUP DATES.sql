-- Backup of data
SELECT Database_Name, Type,
    CONVERT( SmallDateTime , MAX(Backup_Finish_Date)) as Last_Backup, 
    DATEDIFF(d, MAX(Backup_Finish_Date), Getdate()) as Days_Since_Last
FROM MSDB.dbo.BackupSet
WHERE Type = 'd' AND database_name='NESTDB'
GROUP BY Database_Name, Type
UNION ALL
-- Find the backup of Transaction Log files
SELECT Database_Name, Type,
    CONVERT( SmallDateTime , MAX(Backup_Finish_Date)) as Last_Backup, 
    DATEDIFF(d, MAX(Backup_Finish_Date), Getdate()) as Days_Since_Last
FROM MSDB.dbo.BackupSet
WHERE Type = 'l' AND database_name='NESTDB'
GROUP BY Database_Name, Type

SELECT database_name, server_name, Backup_Start_Date, Backup_Finish_Date, [type], [name], [backup_size], DATEDIFF(mi,Backup_Start_Date,Backup_Finish_Date) AS Time_Taken_mins
FROM MSDB.dbo.BackupSet 
WHERE database_name='NESTDB'
ORDER BY Backup_Start_Date DESC
