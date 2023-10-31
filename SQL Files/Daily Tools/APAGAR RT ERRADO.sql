
EXEC NESTRT.dbo.Delete_Incorrect_RT 4
EXEC NESTRT.dbo.Delete_Incorrect_RT 43
EXEC NESTRT.dbo.Delete_Incorrect_RT 10
EXEC NESTRT.dbo.Delete_Incorrect_RT 18

RETURN

--delete
--from NESTRT.dbo.Tb001_Intraday_Performance
--where (curPerformance)>0.05 and convert(varchar, Perf_DateTime, 112)=convert(varchar, getdate(), 112)
--order by  Perf_DateTime desc

--delete
--from NESTRT.dbo.Tb001_Intraday_Performance
--where (curPerformance)<-0.10 and convert(varchar, Perf_DateTime, 112)=convert(varchar, getdate(), 112)
--order by  Perf_DateTime desc