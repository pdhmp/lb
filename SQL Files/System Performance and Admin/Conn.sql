execute sp_who


SELECT * FROM sys.dm_exec_sessions 
where host_name='NEST-40'
order by Program_name,host_name

SELECT login_name, COUNT(*) FROM sys.dm_exec_sessions 
WHERE is_user_process = 1 AND client_interface_name = 'ODBC' 
    AND program_name = '' 
GROUP BY login_name
