USE master

--select hostname, count(hostname) from sysprocesses group by hostname order by count(hostname) desc

--drop table tempTableLuis

select spid into tempTableLuis from sysprocesses where hostname='NEST-85' 

DECLARE @tString VARCHAR(15)
DECLARE @spid int
--While exists(select top 1 spid from tempTableLuis)
	BEGIN
		Select top 1 @spid = spid from tempTableLuis 
		SET @tString = 'KILL ' + CAST(@spid AS VARCHAR(5))
		EXEC(@tString)
	END

drop table tempTableLuis
