USE NESTDB

SELECT ROUTINE_NAME,Data_Type, OBJECT_DEFINITION (OBJECT_ID(ROUTINE_NAME))
FROM NESTDB.INFORMATION_SCHEMA.ROUTINES T
WHERE OBJECT_DEFINITION (OBJECT_ID(ROUTINE_NAME)) like '%Tb901_Event_Log%'
ORDER BY T.ROUTINE_NAME


--RETORNA TDO
SELECT A.name, B.text,xtype
FROM sys.sysobjects A INNER JOIN syscomments B
ON A.id = B.id
WHERE B.text LIKE '%Adjusted%' 
--AND sys.sysobjects.type = 'P'
ORDER BY A.NAME

SELECT B.*, A.* FROM msdb.dbo.sysjobsteps A INNER JOIN msdb.dbo.sysjobs B ON A.job_id=B.job_id  
WHERE command LIKE '%Proc_Check_Process_Table_Transaction_Fowards%'
